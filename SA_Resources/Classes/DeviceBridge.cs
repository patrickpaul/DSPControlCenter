/*
 * File     : DeviceBridge.cs
 * Created  : 28 July 2013
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : A class that manages all communication between the Device and DSPCC
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace SA_Resources.USB
{
    public class DeviceBridge
    {

        #region Variables & Constructor

        private SAUSB _USBConn;

        private SerialPort serialPort;
        public bool isOpen = false; 

        Object DEVICE_LOCK = new Object();

        //private Dictionary<int, string> ERROR_LOOKUP;

        public DeviceBridge(SerialPort _serialPort = null)
        {
            serialPort = _serialPort;

            _USBConn = new SAUSB(serialPort);
        }


        #endregion

        #region Connection Tasks and Status

        public bool IsReady()
        {
            return _USBConn.IsOpen;
        }

        public bool Open(string portName)
        {
            try
            {
                return _USBConn.Open(portName);
            }
            catch (SA_USB_EXCEPTION sa_ex)
            {
               throw new DEVICE_BRIDGE_EXCEPTION("Exception trying to open " + portName,sa_ex);
            }

        }

        public void Close()
        {
            try
            {
                _USBConn.Close();
            }
            catch (SA_USB_EXCEPTION sa_ex)
            {
                throw new DEVICE_BRIDGE_EXCEPTION("Exception trying to close SA USB connection", sa_ex);
            }

        }

        public bool getRTS()
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x02, 0x01, 0x03 }, new byte[] { 0x06, 0x01, 0x03 });
            }
            catch (SA_USB_EXCEPTION sa_ex)
            {
                throw new DEVICE_BRIDGE_EXCEPTION("Unable to getRTS.", sa_ex);
            }
        }


        public void FlushBuffer()
        {
            _USBConn.FlushBuffer();
        }


        public void PrintError(byte dummyByte)
        {
            Console.WriteLine("Error");
        }

        #endregion

        #region Device Tasks and Program Information

        public bool SoftReboot()
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x02, 0x05, 0x03 }, new byte[] { 0x06, 0x05, 0x03 }, 1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        
        public int GetDeviceID()
        {
            try
            {
                Byte[] readBytes;
                int device_id = 0;

                _USBConn.WriteByteArray(new byte[] {0x02, 0x04, 0x03}, out readBytes, 5);

                if (readBytes[0] == 0x06 && readBytes[1] == 0x04)
                {
                    device_id = device_id | readBytes[2];
                    device_id <<= 8;
                    return (device_id | readBytes[3]);
                }

            } catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetDeviceID]: " + ex.Message);
            }

            return 0x00;
        }


        public int GetCurrentProgram()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x10, 0x08, 0x08, 0x03 }, out readBytes, 4);

                if (readBytes[0] == 0x06 && readBytes[1] == 0x08)
                {
                    return (int)readBytes[2];
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetCurrentProgram]: " + ex.Message);
            }

            return 0x00;
        }

        public double GetDeviceFirmwareVersion()
        {

            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x02, 0x07, 0x03 }, out readBytes, 5);

                if (readBytes[0] == 0x06 && readBytes[1] == 0x07)
                {
                    return double.Parse(readBytes[2] + "." + readBytes[3]);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetDeviceFirmwareVersion]: " + ex.Message);
            }

            return 0.00;
        }

        public bool DisableTimers()
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x2, 0x10, 0x03 }, new byte[] { 0x06, 0x10, 0x03 }, 30);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public bool SwitchActiveProgram(int new_index)
        {

            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x10, 0x01, (byte)new_index, 0x03 }, new byte[] { 0x06, 0x01, (byte)new_index, 0x03 }, 500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        #endregion

        #region Live DSP Reads/Changes

        public UInt32 Read_Live_DSP_Value(UInt32 DSP_address)
        {
            try
            {
                _USBConn.FlushBuffer();

                if (!_USBConn.IsOpen) { return 0xFFFFFFFF; }

                uint byte4 = DSP_address & 0xFF;
                DSP_address = DSP_address >> 8;

                uint byte3 = DSP_address & 0xFF;
                DSP_address = DSP_address >> 8;

                uint byte2 = DSP_address & 0xFF;

                uint byte1 = DSP_address >> 8;

                Byte[] readBytes;

                _USBConn.ReadLiveByteArray(new byte[] { 0x08, (byte)byte1, (byte)byte2, (byte)byte3, (byte)byte4, 0x03 }, out readBytes);


                if (readBytes.Count() < 6)
                {
                    return 0xFFFFFFFF;
                }

                if ((readBytes[0] == 0x06) && (readBytes[5] == 0x03))
                {
                    /* INTENTIONAL REVERSAL!! */
                    UInt32 test_value = 0x00000000 | (uint) readBytes[1];
                    test_value = test_value << 8;
                    test_value = test_value | (uint) readBytes[2];
                    test_value = test_value << 8;
                    test_value = test_value | (uint) readBytes[3];
                    test_value = test_value << 8;
                    test_value = test_value | (uint) readBytes[4];

                    FlushBuffer();

                    return test_value;
                }

                return 0xFFFFFFFF;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Read_Live_DSP_Value: " + ex.Message);
            }

            return 0xFFFFFFFF;
        }

       
        public bool SetLiveDSPValue(uint address_index, UInt32 value)
        {

            lock (DEVICE_LOCK)
            {
                FlushBuffer();

                uint address_index1, address_index2;

                if (address_index > 255)
                {
                    address_index2 = address_index & 0xFF;
                    address_index >>= 8;    
                    address_index1 = address_index;
                }
                else
                {
                    address_index1 = 0;
                    address_index2 = address_index;
                }

                uint byte4 = value & 0xFF;
                value = value >> 8;

                uint byte3 = value & 0xFF;
                value = value >> 8;

                uint byte2 = value & 0xFF;

                uint byte1 = value >> 8;

                /* INTENTIONAL REVERSAL!! */
                UInt32 test_value = 0x00000000 | byte1;
                test_value = test_value << 8;
                test_value = test_value | byte2;
                test_value = test_value << 8;
                test_value = test_value | byte3;
                test_value = test_value << 8;
                test_value = test_value | byte4;

                if (!serialPort.IsOpen) return false;

                byte[] buff = new byte[8];

                buff[0] = 0x04;
                buff[1] = (byte)address_index1;
                buff[2] = (byte)address_index2;
                buff[3] = (byte)byte1;
                buff[4] = (byte)byte2;
                buff[5] = (byte)byte3;
                buff[6] = (byte)byte4;
                buff[7] = 0x03;


                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 8);
                    Thread.Sleep(40);

                    if (serialPort.BytesToRead >= 4)
                    {

                        Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[0] == 0x06 && bytes[1] == (byte)address_index1 && bytes[2] == (byte)address_index2)
                        {
                            return true;
                        }

                        if (bytes[0] == 0x15)
                        {
                            PrintError(bytes[1]);
                        }

                    }
                    else
                    {
                        FlushBuffer();
                    }
                }

                return false;
            }

        }

        #endregion

        #region Phantom Power

        public bool UpdatePhantomPower()
        {
            try
            {
                lock (DEVICE_LOCK)
                {
                    FlushBuffer();

                    if (!serialPort.IsOpen) return false;

                    byte[] buff = new byte[10];

                    buff[0] = 0x02;
                    buff[1] = 0x09;
                    buff[2] = 0x03;


                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 3);
                        Thread.Sleep(30);

                        if (serialPort.BytesToRead >= 3)
                        {

                            Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if (bytes[0] == 0x06 && bytes[1] == 0x09)
                            {
                                return true;
                            }

                            if (bytes[0] == 0x15)
                            {
                                //print_error(bytes[1]);
                            }

                        }
                        else
                        {
                            FlushBuffer();
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.UpdatePhantomPower]: " + ex.Message);
            }

            return false;
        }


        #endregion

        #region Streaming

        public string StreamData = "";

        public void StreamReadPage(int prog_num, int page_num, ref List<UInt32> FLASH_READ_VALUES, int index_offset)
        {
            lock (DEVICE_LOCK)
            {
                FlushBuffer();

                byte[] buff = new byte[5];

                buff[0] = 0x10;
                buff[1] = 0x09;
                buff[2] = (byte)prog_num;
                buff[3] = (byte)page_num;
                buff[4] = 0x03;

                serialPort.Write(buff, 0, buff.Length);

                Thread.Sleep(5);

                while (serialPort.BytesToRead < 1)
                {
                    Thread.Sleep(1);
                }

                while (serialPort.BytesToRead < 261)
                {
                    Thread.Sleep(5);
                }

                if (serialPort.BytesToRead > 1)
                {

                    // we can assume we're getting a stream

                    while (serialPort.BytesToRead < 261)
                    {
                        Thread.Sleep(1);
                    }

                    if (serialPort.BytesToRead != 261)
                    {
                        Debug.WriteLine("serialPort.BytesToRead was not == 261, it was " + serialPort.BytesToRead);

                        FlushBuffer();
                        return;
                    }

                    Byte[] bytes = new Byte[serialPort.BytesToRead];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x09 && bytes[2] == prog_num && bytes[3] == page_num && bytes[260] == 0x03)
                    {
                        //Debug.WriteLine("Data looks good!");

                        UInt32 temp_value = 0;
                        byte byte_msb, byte_3, byte_2, byte_lsb;

                        for (int i = 4; i < 260; )
                        {
                            temp_value = 0;

                            byte_lsb = bytes[i++];
                            byte_3 = bytes[i++];
                            byte_2 = bytes[i++];
                            byte_msb = bytes[i++];

                            temp_value = temp_value | byte_msb;
                            temp_value <<= 8;
                            temp_value = temp_value | byte_2;
                            temp_value <<= 8;
                            temp_value = temp_value | byte_3;
                            temp_value <<= 8;
                            temp_value = temp_value | byte_lsb;

                            FLASH_READ_VALUES.Add(temp_value);

                        }
                        return;
                    }
                    else
                    {
                        Debug.WriteLine("[ERROR] First byte read was " + bytes[0]);
                        return;
                    }

                }
            }


        }

        public bool InitiateWriteStream(int prog_num, int page_num, int num_bytes = 256)
        {
            FlushBuffer();

            byte[] buff = new byte[7];

            int num_bytes_lsb = num_bytes & 0xFF;
            num_bytes >>= 8;
            int num_bytes_msb = num_bytes & 0xFF;

            buff[0] = 0x10;
            buff[1] = 0x11;
            buff[2] = (byte)prog_num;
            buff[3] = (byte)page_num;
            buff[4] = (byte)num_bytes_msb;
            buff[5] = (byte)num_bytes_lsb;
            buff[6] = 0x03;

            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 7);
                Thread.Sleep(30);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == prog_num && bytes[2] == page_num && bytes[3] == 0x03)
                    {
                        //Debug.WriteLine("got start stream ack. Prog = " + prog_num + ", Page = " + page_num + ", Bytes left - " + serialPort1.BytesToRead);
                        return true;
                    }

                    if (bytes[0] == 0x15)
                    {
                        //print_error(bytes[1]);
                    }

                }
                else
                {
                    FlushBuffer();
                }
            }

            return false;
        }

        public bool SendStreamNibble(UInt32[] input_values)
        {

            byte[] buff = new byte[256];

            int value_counter = 0;

            byte byte_msb, byte2, byte3, byte_lsb;

            for (int i = 0; i < 64; i++)
            {
                byte_lsb = (byte)(input_values[i] & 0xFF);
                input_values[i] >>= 8;

                byte3 = (byte)(input_values[i] & 0xFF);
                input_values[i] >>= 8;

                byte2 = (byte)(input_values[i] & 0xFF);
                input_values[i] >>= 8;

                byte_msb = (byte)(input_values[i] & 0xFF);

                buff[value_counter++] = byte_lsb;
                buff[value_counter++] = byte3;
                buff[value_counter++] = byte2;
                buff[value_counter++] = byte_msb;
            }


            byte[] nibble_buff = new byte[32];

            Byte[] rx_buffer = new Byte[256];

            int bytes_to_read = 0;

            int iterations = 0;

            bool error_found = false;
            for (int nibble_counter = 0; nibble_counter < 8; nibble_counter++)
            {
                serialPort.Write(buff, (nibble_counter * 32), 32);

                Array.Copy(buff, (nibble_counter * 32), nibble_buff, 0, 32);

                iterations = 0;
                while (serialPort.BytesToRead < 1 && iterations < 100)
                {
                    Thread.Sleep(1);
                    iterations++;
                }

                if (iterations == 100)
                {
                    throw new Exception("Timeout trying to send nibble");
                }
                if (serialPort.BytesToRead >= 1)
                {
                    bytes_to_read = serialPort.BytesToRead;
                    serialPort.Read(rx_buffer, 0, serialPort.BytesToRead);

                    Crc16Ccitt crc = new Crc16Ccitt(InitialCrcValue.NonZero1);
                    int checksum = (crc.ComputeChecksum(nibble_buff) & 0xFF);

                    if (rx_buffer[0] == checksum)
                    {
                        error_found = false;
                        //Debug.WriteLine("CRC verified! - CRC=" + rx_buffer[0] + ", BTR=" + bytes_to_read);
                    }
                    else
                    {
#if DEBUG
                        //MessageBox.Show("CRC INVALID! - CRC=" + rx_buffer[0] + ", Expecting - " + checksum + ", BTR=" + bytes_to_read);
#endif
                        error_found = true;
                    }
                }
                //}
            }



            while (serialPort.BytesToRead < 5)
            {

            }

            if (serialPort.BytesToRead >= 5)
            {

                Byte[] bytes = new Byte[serialPort.BytesToRead];

                serialPort.Read(bytes, 0, serialPort.BytesToRead);


                if (bytes[0] == 0x06 && bytes[1] == 0x01)
                {
                }
                else
                {
#if DEBUG
                    //MessageBox.Show("Invalid response from write to flash page");
#endif  
                    error_found = true;
                }
            }

            return !error_found;
        }

        #endregion

        #region Amplifier Bridge Mode

        public int ReadAmplifierMode()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x15, 0x01, 0x03 }, out readBytes, 4,100);

                if (readBytes[0] == 0x06 && readBytes[1] == 0x01)
                {
                    return (int)readBytes[2];
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetCurrentProgram]: " + ex.Message);
            }

            return 0; 
        }


        public bool SetAmplifierMode(int newMode)
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x14, 0x01, (byte)newMode, 0x03 }, new byte[] { 0x06, 0x01, (byte)newMode, 0x03 }, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false; 
        }

        #endregion

        #region RS232

        public bool ReadRS232Mute(int channel)
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x12, 0x01, (byte)channel, 0x03 }, out readBytes, 2, 100);

                return (readBytes[1] == 0x01);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.ReadRS232Mute]: " + ex.Message);
            }

            return false; 
        }

        public int ReadRS232Vol(int channel)
        {

            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x12, 0x02, (byte)channel, 0x03 }, out readBytes, 2, 100);

                if (readBytes[0] == 0x06 && readBytes[1] >= 0 && readBytes[1] <= 100)
                {
                    return readBytes[1];
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.ReadRS232Mute]: " + ex.Message);
            }

            return -1;
        }

        public bool SetRS232Mute(int channel, int new_mute)
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x13, (byte)new_mute, (byte)channel, 0x03 }, new byte[] { 0x06, (byte)new_mute, 0x03 }, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false; 
        }

        public bool ResetRS232Volume(int channel)
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x13, 0x03, (byte)channel, 0x03 }, new byte[] { 0x06, 0x03, 0x03 }, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false; 
           
        }

        #endregion

        #region Remote Volume Control

        public string ReadCurrentRVC()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x15, 0x14, 0x03 }, out readBytes, 5, 100);

                if (readBytes[0] == 0x16 & readBytes[1] == 0x14)
                {
                    return readBytes[2].ToString() + " - " + readBytes[3].ToString() + "%";
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetCurrentProgram]: " + ex.Message);
            }

            return "ERROR"; 
        }

        public int ReadRVCMax()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x15, 0x04, 0x03 }, out readBytes, 4, 100);

                if (readBytes[0] == 0x06 & readBytes[1] == 0x04)
                {
                    return readBytes[2];
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetCurrentProgram]: " + ex.Message);
            }

            return 0; 
          
        }

        public int ReadRVCMin()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x15, 0x03, 0x03 }, out readBytes, 4, 100);

                if (readBytes[0] == 0x06 & readBytes[1] == 0x03)
                {
                    return readBytes[2];
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetCurrentProgram]: " + ex.Message);
            }

            return 0; 
        }

        public int CalibrateUpperRVC()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x14, 0x04, 0x01, 0x03 }, out readBytes, 4, 200);

                if (readBytes[0] == 0x06 & readBytes[1] == 0x04)
                {
                    return readBytes[2];

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.CalibrateUpperRVC]: " + ex.Message);
            }

            return 0; 
        }

        public int CalibrateLowerRVC()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x14, 0x03, 0x01, 0x03 }, out readBytes, 4, 200);

                if (readBytes[0] == 0x06 & readBytes[1] == 0x03)
                {
                    return readBytes[2];

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.CalibrateUpperRVC]: " + ex.Message);
            }

            return 0; 
        }

        #endregion

        #region SleepMode

        public bool ReadSleepModeEnable()
        {
            try
            {
                Byte[] readBytes;

                _USBConn.WriteByteArray(new byte[] { 0x15, 0x10, 0x03 }, out readBytes, 4, 100);

                if (readBytes[0] == 0x06 & readBytes[1] == 0x10)
                {
                    return (readBytes[2] == 0x01);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceBridge.GetCurrentProgram]: " + ex.Message);
            }

            return false; 

        }

        public bool SetSleepModeEnable(bool newMode)
        {
            try
            {
                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x14, 0x10, newMode ? (byte)0x01 : (byte)0x00, 0x03 }, new byte[] { 0x06, 0x10, 0xFF, 0x03 }, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false; 
        }

        public Int16 ReadSleepModeSeconds()
        {
            try
            {
                string readLine = "";

                _USBConn.WriteByteArrayReadString(new byte[] { 0x15, 0x11, 0x03 }, out readLine, 4, 150);

                if (readLine.Length >= 2)
                {
                    return Int16.Parse(readLine.Substring(2));
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0; 
        }

        public bool SetSleepModeSeconds(int newSeconds)
        {
            try
            {
                int lsb_byte, msb_byte;

                lsb_byte = newSeconds & 0xFF;
                msb_byte = newSeconds >> 8;

                return _USBConn.WriteByteArrayVerifyReturn(new byte[] { 0x14, 0x11, (byte)lsb_byte, (byte)msb_byte, 0x03 }, new byte[] { 0x06, 0x11,0xFF, 0x03 }, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false; 

        }
            
        #endregion
    }

    #region DEVICE_BRIDGE_EXCEPTION 

    /// <summary>
    /// Exceptions thrown by errors within the DeviceBridge class.
    /// </summary>
    /// 
    [global::System.Serializable]
    public class DEVICE_BRIDGE_EXCEPTION : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DEVICE_BRIDGE_EXCEPTION" /> class.
        /// </summary>
        public DEVICE_BRIDGE_EXCEPTION()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DEVICE_BRIDGE_EXCEPTION" /> class.
        /// </summary>
        /// <param name="message">Exception Message</param>
        public DEVICE_BRIDGE_EXCEPTION(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DEVICE_BRIDGE_EXCEPTION" /> class.
        /// </summary>
        /// <param name="message">Exception Message.</param>
        /// <param name="inner">Exception leading to this exception.</param>
        public DEVICE_BRIDGE_EXCEPTION(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DEVICE_BRIDGE_EXCEPTION" /> class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected DEVICE_BRIDGE_EXCEPTION(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }

    #endregion
}