using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace SA_Resources.USB
{
    public class SAUSB
    {

        private SerialPort serialPort;
        private bool _isOpen = false;

        Object DEVICE_LOCK = new Object();

        public SAUSB(SerialPort _serialPort = null)
        {
            serialPort = _serialPort;
            _isOpen = false;
        }

        public bool IsOpen
        {
            get { return _isOpen; }
            set { }
        }

        public int BytesAvailable
        {
            get { return serialPort.BytesToRead; }
            set { }
        }

        public bool Open(string portName)
        {
            lock (DEVICE_LOCK)
            {
                if (_isOpen)
                {
                    throw new SA_USB_EXCEPTION("Serial port is already open");
                }

                if (portName.Length == 0)
                {
                    throw new SA_USB_EXCEPTION("No serial port chosen");
                }

                try
                {
                    serialPort.PortName = portName;
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    throw new SA_USB_EXCEPTION("Unable to open port " + portName + ". Error encountered: " + ex.Message);
                }

                if (!serialPort.IsOpen)
                {
                    throw new SA_USB_EXCEPTION("Serial port is not open");
                }

                _isOpen = true;
                return true;
            }
        }

        public void Close()
        {
            try
            {
                serialPort.Close();
                _isOpen = false;
            }
            catch (IOException ioex)
            {
                throw new DEVICE_BRIDGE_EXCEPTION("Unable to close serial port.", ioex);
            }

        }
        
        
        public void FlushBuffer()
        {
            if (!serialPort.IsOpen)
            {
                throw new SA_USB_EXCEPTION("Attempted to communicate with device but the serial port was not open.");
            }

            try
            {
                if (serialPort.BytesToRead == 0)
                {
                    return;
                }

                while (serialPort.BytesToRead > 0)
                {
                    byte singleByte = (byte)serialPort.ReadByte();

                    Debug.Write(" 0x" + singleByte.ToString("X2"));

                    if (singleByte >= 0x20 && singleByte <= 0x7F)
                    {
                        Debug.Write(" (" + (char)singleByte + ")");
                    }

                    Debug.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                throw new SA_USB_EXCEPTION("Exception encountered attempting to flush device.", ex);
            }
        }

        public void WriteByteArray(byte[] bytestoWrite, out byte[] returnBytes, int expectedReturnBytes = 3, int delayTime = 30, int retryCount = 3)
        {
            try
            {
                lock (DEVICE_LOCK)
                {
                    FlushBuffer();

                    returnBytes = new byte[expectedReturnBytes];
                    if (!serialPort.IsOpen)
                    {
                        throw new SA_USB_EXCEPTION("Attempted to write to device but serial port was closed.");
                    }

                    for (int retries = 0; retries < retryCount; retries++)
                    {
                        serialPort.Write(bytestoWrite, 0, bytestoWrite.Count());

                        Thread.Sleep(delayTime);

                        if (serialPort.BytesToRead >= expectedReturnBytes)
                        {

                            returnBytes = new Byte[serialPort.BytesToRead + 5];

                            serialPort.Read(returnBytes, 0, serialPort.BytesToRead);
                        }
                        else
                        {
                            FlushBuffer();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SA_USB_EXCEPTION("Exception in WriteByteArray: " + ex.Message, ex);
            }
        }

        public bool WriteByteArrayVerifyReturn(byte[] bytestoWrite, byte[] expectedReturn, int delayTime = 30, int retryCount = 3)
        {
            try
            {
                byte[] actualReturn = new byte[expectedReturn.Count()];

                WriteByteArray(bytestoWrite, out actualReturn, expectedReturn.Count(), delayTime, retryCount);

                for (int i = 0; i < expectedReturn.Count(); i++)
                {
                    if ((actualReturn[i] != expectedReturn[i]) && (expectedReturn[i] != 0xFF))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new SA_USB_EXCEPTION("Exception while writing to device: " + ex.Message, ex);
            }
        }

        public void WriteByteArrayReadString(byte[] bytestoWrite, out string readString, int minStringChars, int delayTime = 30, int retryCount = 3)
        {
            try
            {
                lock (DEVICE_LOCK)
                {
                    FlushBuffer();

                    readString = "";

                    if (!serialPort.IsOpen)
                    {
                        throw new SA_USB_EXCEPTION("Attempted to write to device but serial port was closed.");
                    }

                    for (int retries = 0; retries < retryCount; retries++)
                    {
                        serialPort.Write(bytestoWrite, 0, bytestoWrite.Count());

                        Thread.Sleep(delayTime);

                        if (serialPort.BytesToRead >= minStringChars)
                        {

                            readString = serialPort.ReadLine();
                        }
                        else
                        {
                            FlushBuffer();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SA_USB_EXCEPTION("Exception in WriteByteArrayReturnString: " + ex.Message, ex);
            }
        }

        public void ReadLiveByteArray(byte[] bytestoWrite, out byte[] returnBytes, int expectedReturnBytes = 8)
        {
            try
            {
                lock (DEVICE_LOCK)
                {
                    FlushBuffer();

                    

                    if (!serialPort.IsOpen)
                    {
                        throw new SA_USB_EXCEPTION("Attempted to write to device but serial port was closed.");
                    }

                    int ms_counter = 0;

                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(bytestoWrite, 0, 6);

                        while (serialPort.BytesToRead < 6 && ms_counter < 100)
                        {
                            Thread.Sleep(1);
                            ms_counter += 1;
                        }

                        if (serialPort.BytesToRead == 6)
                        {

                            returnBytes = new Byte[serialPort.BytesToRead];

                            serialPort.Read(returnBytes, 0, serialPort.BytesToRead);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Did not have 6 bytes to read, flushing");
                            FlushBuffer();
                        }
                    }

                    Console.WriteLine("Bad read");

                    returnBytes = new byte[0];
                }
            }
            catch (Exception ex)
            {
                throw new SA_USB_EXCEPTION("Exception in ReadLiveByteArray: " + ex.Message, ex);
            }
        }

    }

    #region Custom Exception class - SA_USB_EXCEPTION

    /// <summary>
    /// Exceptions thrown by errors within the SAUSB class.
    /// </summary>
    /// 
    [global::System.Serializable]
    public class SA_USB_EXCEPTION : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SA_USB_EXCEPTION" /> class.
        /// </summary>
        public SA_USB_EXCEPTION()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SA_USB_EXCEPTION" /> class.
        /// </summary>
        /// <param name="message">Exception Message</param>
        public SA_USB_EXCEPTION(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SA_USB_EXCEPTION" /> class.
        /// </summary>
        /// <param name="message">Exception Message.</param>
        /// <param name="inner">Exception leading to this exception.</param>
        public SA_USB_EXCEPTION(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SA_USB_EXCEPTION" /> class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected SA_USB_EXCEPTION(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }

    #endregion

}
