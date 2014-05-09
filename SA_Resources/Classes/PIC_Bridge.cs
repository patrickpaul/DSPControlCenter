using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;

namespace SA_Resources.USB
{
    public class PIC_Bridge
    {
        private SerialPort serialPort;
        public bool isOpen = false; 

        uint last_address1, last_address2;
        uint last_byte1, last_byte2, last_byte3, last_byte4;

        int delay_ms = 30;

        Object PIC_LOCK = new Object();

        private Dictionary<int, string> ERROR_LOOKUP;


        public PIC_Bridge(SerialPort _serialPort = null)
        {
            ERROR_LOOKUP = new Dictionary<int, string>();

            ERROR_LOOKUP.Add(0x02, "Invalid start command");
            ERROR_LOOKUP.Add(0x09, "Unknown error");
            ERROR_LOOKUP.Add(0x10, "Timeout reading command char");
            ERROR_LOOKUP.Add(0x11, "Timeout reading end char");
            ERROR_LOOKUP.Add(0x12, "Invalid end char");
            ERROR_LOOKUP.Add(0x13, "Invalid command char");
            ERROR_LOOKUP.Add(0x14, "Timeout reading address char");
            ERROR_LOOKUP.Add(0x16, "Invalid address char");
            ERROR_LOOKUP.Add(0x17, "Timeout reading data bytes");
            ERROR_LOOKUP.Add(0x18, "Timeout reading phantom bool");
            ERROR_LOOKUP.Add(0x19, "Invalid phantom bool");
            ERROR_LOOKUP.Add(0x20, "Timeout reading phantom power channel");
            ERROR_LOOKUP.Add(0x21, "Invalid phantom power channel");
            ERROR_LOOKUP.Add(0x22, "Invalid phantom bool");

            serialPort = _serialPort;

            isOpen = false;
        }

        public void AssignSerialPort(SerialPort _serialPort)
        {
            serialPort = _serialPort;
        }

        public bool Open(string portName)
        {

            lock (PIC_LOCK)
            {

                if (portName.Length == 0)
                {
                    return false;
                    //throw new Exception("No serial port chosen");
                }

                try
                {
                    serialPort.PortName = portName;
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in PIC_Bridge.Open: " + ex.Message);
                    return false;
                    // throw new Exception("Unable to open port " + portName + ". Error encountered: " + ex.Message);
                }

                if (!serialPort.IsOpen)
                {
                    return false;
                    //throw new Exception("Serial port is not open");
                }

                if (!getRTS())
                {
                    serialPort.Close();

                    return false;
                    //throw new Exception("Device did not respond after opening Serial Port");
                }

                isOpen = true;
                return true;
            }
        }

        public void Close()
        {
            serialPort.Close();
            isOpen = false;

        }

        public void FlushBuffer()
        {
            if (!serialPort.IsOpen) return;

            if (serialPort.BytesToRead == 0)
            {
                return;
            }
            Console.Write("Cleared from serial buffer: ");

            while (serialPort.BytesToRead > 0)
            {
                byte singleByte = (byte)serialPort.ReadByte();

                Console.Write(" 0x" + singleByte.ToString("X2"));

                if(singleByte >= 0x20 && singleByte <= 0x7F)
                {
                    Console.Write(" (" + (char)singleByte + ")");
                }
            }

            Console.WriteLine();


        }

        public void PrintError(int error_id)
        {
            Console.WriteLine("PIC_Bridge Error: " + ERROR_LOOKUP[error_id]);
        }

        public bool getRTS()
        {
            try
            {
                lock (PIC_LOCK)
                {
                    FlushBuffer();

                    if (!serialPort.IsOpen) return false;

                    byte[] buff = new byte[10];

                    buff[0] = 0x02;
                    buff[1] = 0x01;
                    buff[2] = 0x03;


                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 3);
                        Thread.Sleep(30);

                        if (serialPort.BytesToRead >= 3)
                        {

                            Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if (bytes[0] == 0x06 && bytes[1] == 0x01)
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
            } catch (Exception ex)
            {
                
            }

            return false;
        }

        public int GetDeviceID()
        {
            try
            {
                lock (PIC_LOCK)
                {
                    if (!getRTS())
                    {
                        throw new Exception("Device did not respond to RTS request");
                    }

                    byte[] buff = new byte[3];

                    buff[0] = 0x02;
                    buff[1] = 0x04;
                    buff[2] = 0x03;

                    int device_id = 0;

                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 3);
                        Thread.Sleep(30);

                        if (serialPort.BytesToRead >= 5)
                        {

                            Byte[] bytes = new Byte[serialPort.BytesToRead];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if (bytes[0] == 0x06 && bytes[1] == 0x04)
                            {
                                device_id = device_id | bytes[2];
                                device_id <<= 8;
                                return (device_id | bytes[3]);
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

                    return 0;
                }
            } catch (Exception ex)
            {
                
            }

            return 0x00;
        }


        public double GetDeviceFirmwareVersion()
        {
            try
            {
                lock (PIC_LOCK)
                {
                    if (!getRTS())
                    {
                        throw new Exception("Device did not respond to RTS request");
                    }

                    byte[] buff = new byte[3];

                    buff[0] = 0x02;
                    buff[1] = 0x07;
                    buff[2] = 0x03;

                    double version = 0;

                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 3);
                        Thread.Sleep(30);

                        if (serialPort.BytesToRead >= 5)
                        {

                            Byte[] bytes = new Byte[serialPort.BytesToRead];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if (bytes[0] == 0x06 && bytes[1] == 0x07)
                            {
                                version = double.Parse(bytes[2] + "." + bytes[3]);
                                return version;
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

                    return 0;
                }
            } catch (Exception ex)
            {
                
            }

            return 0.00;
        }

        public UInt32 Read_DSP_Value(uint address_index)
        {
            try
            {


                lock (PIC_LOCK)
                {
                    FlushBuffer();

                    uint address_index1, address_index2;

                    if (address_index > 255)
                    {
                        address_index1 = 255;
                        address_index2 = address_index - 255;
                    }
                    else
                    {
                        address_index1 = address_index;
                        address_index2 = 0;
                    }

                    if (!serialPort.IsOpen) return 0x00000000;

                    byte[] buff = new byte[4];

                    buff[0] = 0x08;
                    buff[1] = (byte) address_index1;
                    buff[2] = (byte) address_index2;
                    buff[3] = 0x03;

                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 4);
                        Thread.Sleep(delay_ms);

                        if (serialPort.BytesToRead > 2)
                        {
                            Byte[] bytes = new Byte[serialPort.BytesToRead];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if ((bytes[0] == 0x06) && (bytes[1] == (byte) address_index1) && (bytes[2] == (byte) address_index2))
                            {
                                /* INTENTIONAL REVERSAL!! */
                                UInt32 test_value = 0x00000000 | (uint) bytes[3];
                                test_value = test_value << 8;
                                test_value = test_value | (uint) bytes[4];
                                test_value = test_value << 8;
                                test_value = test_value | (uint) bytes[5];
                                test_value = test_value << 8;
                                test_value = test_value | (uint) bytes[6];

                                return test_value;
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

                    return 0xFFFFFFFF;
                }
            } catch (Exception ex)
            {
                
            }

            return 0xFFFFFFFF;
        }


        public UInt32 Read_Live_DSP_Value(UInt32 DSP_address)
        {

            try
            {


                lock (PIC_LOCK)
                {
                FlushBuffer();

                if (!serialPort.IsOpen) return 0x00000000;

                uint byte4 = DSP_address & 0xFF;
                DSP_address = DSP_address >> 8;

                uint byte3 = DSP_address & 0xFF;
                DSP_address = DSP_address >> 8;

                uint byte2 = DSP_address & 0xFF;

                uint byte1 = DSP_address >> 8;

                byte[] buff = new byte[6];

                buff[0] = 0x08;
                buff[1] = (byte)byte1;
                buff[2] = (byte)byte2;
                buff[3] = (byte)byte3;
                buff[4] = (byte)byte4;
                buff[5] = 0x03;


                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 6);
                    Thread.Sleep(60);

                    if (serialPort.BytesToRead > 5)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if ((bytes[0] == 0x06) && (bytes[5] == 0x03))
                        {
                            /* INTENTIONAL REVERSAL!! */
                            UInt32 test_value = 0x00000000 | (uint)bytes[1];
                            test_value = test_value << 8;
                            test_value = test_value | (uint)bytes[2];
                            test_value = test_value << 8;
                            test_value = test_value | (uint)bytes[3];
                            test_value = test_value << 8;
                            test_value = test_value | (uint)bytes[4];

                            return test_value;
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

                    return 0xFFFFFFFF;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION in Read_Live_DSP_Value]: " + ex.Message);
            }

            return 0xFFFFFFFF;
        }

        public bool Reboot()
        {

            lock (PIC_LOCK)
            {
                if (!getRTS())
                {
                    throw new Exception("Device did not respond to RTS request");
                }

                byte[] buff = new byte[3];

                buff[0] = 0x02;
                buff[1] = 0X07;
                buff[2] = 0x03;

                serialPort.Write(buff, 0, 3);

                int bytesToRead = 0;

                serialPort.Write(buff, 0, 3);

                for (int count = 0; count <= 5; count++)
                {
                    bytesToRead = serialPort.BytesToRead;

                    if (bytesToRead == 4)
                    {
                        break;
                    }

                    Thread.Sleep(100);

                    if (count == 5)
                    {
                        return false;
                    }

                }

                Byte[] bytes = new Byte[bytesToRead];

                serialPort.Read(bytes, 0, bytesToRead);

                if (bytes[0] == 0x06 && bytes[1] == 0x07)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool SetDSPValue(uint address_index, UInt32 value)
        {

            lock (PIC_LOCK)
            {
                FlushBuffer();

                uint address_index1, address_index2;

                if (address_index > 255)
                {
                    address_index1 = 255;
                    address_index2 = address_index - 255;
                }
                else
                {
                    address_index1 = address_index;
                    address_index2 = 0;
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

                buff[0] = 0x05;
                buff[1] = (byte)address_index1;
                buff[2] = (byte)address_index2;
                buff[3] = (byte)byte1;
                buff[4] = (byte)byte2;
                buff[5] = (byte)byte3;
                buff[6] = (byte)byte4;
                buff[7] = 0x03;

                last_address1 = address_index1;
                last_address2 = address_index2;
                last_byte1 = byte1;
                last_byte2 = byte2;
                last_byte3 = byte3;
                last_byte4 = byte4;


                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 8);
                    Thread.Sleep(delay_ms);

                    if (serialPort.BytesToRead > 3)
                    {

                        Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[0] == 0x06 && bytes[1] == 0x05 && bytes[2] == (byte)address_index1 && bytes[3] == (byte)address_index2)
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

        public bool SetLiveDSPValue(uint address_index, UInt32 value)
        {

            lock (PIC_LOCK)
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

                last_address1 = address_index1;
                last_address2 = address_index2;
                last_byte1 = byte1;
                last_byte2 = byte2;
                last_byte3 = byte3;
                last_byte4 = byte4;


                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 8);
                    Thread.Sleep(50);

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

        public bool UpdatePhantomPower()
        {
            try
            {
                lock (PIC_LOCK)
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

            }

            return false;
        }

        public bool sendAckdData(byte commandAddr, byte commandData, int delay_value = 50, byte extra_byte = 0xFF)
        {

            lock (PIC_LOCK)
            {
                byte[] buff;
                if (!serialPort.IsOpen) return false;

                FlushBuffer();
                int bytes_to_write = 0;

                if (extra_byte == 0xFF)
                {
                    buff = new byte[4];

                    buff[0] = 0x09;
                    buff[1] = commandAddr;
                    buff[2] = commandData;
                    buff[3] = 0x03;
                    bytes_to_write = 4;

                }
                else
                {
                    buff = new byte[5];

                    buff[0] = 0x09;
                    buff[1] = commandAddr;
                    buff[2] = commandData;
                    buff[3] = extra_byte;
                    buff[4] = 0x03;
                    bytes_to_write = 5;
                }



                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, bytes_to_write);
                    Thread.Sleep(delay_ms);

                    if (serialPort.BytesToRead > 2)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[0] == 0x06 && bytes[1] == (byte)commandAddr)
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

        public bool sendAckdString(byte commandAddr, string in_string, int delay_value = 50)
        {

            lock (PIC_LOCK)
            {
                if (!serialPort.IsOpen) return false;

                int bytes_to_read = 0;

                byte[] buff = new byte[2];

                buff[0] = 0x02;
                buff[1] = commandAddr;

                serialPort.Write(buff, 0, 2);

                serialPort.WriteLine(in_string);

                buff = new byte[1];

                buff[0] = 0x03;

                serialPort.Write(buff, 0, 1);


                for (int count = 0; count <= 5; count++)
                {
                    bytes_to_read = serialPort.BytesToRead;

                    if (bytes_to_read == 4)
                    {

                        break;
                    }

                    Thread.Sleep(delay_value);

                    if (count == 3)
                    {
                        FlushBuffer();
                    }

                    if (count == 5)
                    {
                        return false;
                    }

                }

                Byte[] bytes = new Byte[bytes_to_read];

                serialPort.Read(bytes, 0, bytes_to_read);

                if (bytes[0] == 0x06 && bytes[1] == commandAddr)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        
        public bool sendAckdCommand(byte commandValue, int delay_value = 50)
        {

            lock (PIC_LOCK)
            {
                if (!serialPort.IsOpen) return false;

                FlushBuffer();

                byte[] buff = new byte[3];

                buff[0] = 0x02;
                buff[1] = commandValue;
                buff[2] = 0x03;

                serialPort.Write(buff, 0, 3);


                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 3);
                    Thread.Sleep(delay_value);

                    while (serialPort.BytesToRead == 0)
                    {
                        Thread.Sleep(50);
                    }

                    if (serialPort.BytesToRead == 3)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[0] == 0x06 && bytes[1] == commandValue)
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
                        if (delay_value == 8000)
                        {
                            //Console.WriteLine("Trying again in saving EEPROM");
                        }
                        FlushBuffer();
                    }
                }

                return false; // failed after 3 retries
            }

        }

        public bool verifyLastCommand()
        {
            lock (PIC_LOCK)
            {
                byte[] buff = new byte[3];

                buff[0] = 0x02;
                buff[1] = 0x03;
                buff[2] = 0x03;

                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 3);
                    Thread.Sleep(delay_ms);

                    if (serialPort.BytesToRead > 7)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if ((bytes[0] == 0x06) && (bytes[1] == 0x05) && (bytes[2] == (byte)last_address1) && (bytes[3] == (byte)last_address2) && (bytes[4] == (byte)last_byte1) && (bytes[5] == (byte)last_byte2) && (bytes[6] == (byte)last_byte3) && (bytes[7] == (byte)last_byte4))
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

        public bool SendChannelName(int channel, string name, bool is_output = false)
        {
            lock (PIC_LOCK)
            {
                FlushBuffer();

                bool error = false;
                byte[] outbuff = new byte[20];
                byte[] buff = new byte[(name.Length) + 4];

                int byte_counter = 3;
                int timeout_counter = 0;
                int max_timeout = 500;
                bool timeout_error = false;

                //0x10 0x01 0x58 0x58 0x58 0x58 0x58 0x03
                buff[0] = 0x09;
                if (is_output)
                {
                    buff[1] = 0x13;
                }
                else
                {
                    buff[1] = 0x11;
                }

                buff[2] = (byte)channel;

                for (int i = 0; i < name.Length; i++)
                {
                    buff[byte_counter++] = (byte)name[i];
                }

                buff[byte_counter] = 0x03;


                for (int i = 0; i <= byte_counter; i++)
                {
                    serialPort.Write(buff, i, 1);

                    if(i < 3)
                    {
                        Thread.Sleep(20);
                    }
                    else if (i == byte_counter)
                    {
                    }
                    else
                    {
                        timeout_counter = 0;
                        timeout_error = false;

                        while (serialPort.BytesToRead < 1)
                        {
                            Thread.Sleep(10);
                            timeout_counter += 10;
                            if (timeout_counter >= max_timeout)
                            {
                                timeout_error = true;
                            }
                        }

                        if (timeout_error == true)
                        {
                            error = true;
                            FlushBuffer();
                            break;
                        }

                        if (serialPort.BytesToRead != 1)
                        {
                            error = true;
                            FlushBuffer();
                            break;
                        }
                        else
                        {
                            serialPort.Read(outbuff, 0, 1);
                            if (outbuff[0] != buff[i])
                            {
                                error = true;
                                FlushBuffer();
                                break;
                            }
                        }
                    }
                }


                timeout_counter = 0;
                timeout_error = false;

                while (serialPort.BytesToRead < 5)
                {
                    Thread.Sleep(10);
                    timeout_counter += 10;
                    if (timeout_counter >= max_timeout)
                    {
                        timeout_error = true;
                    }
                }

                if(timeout_error)
                {
                    error = true;
                } else
                {
                    serialPort.Read(outbuff, 0, serialPort.BytesToRead);

                    if(outbuff[1] == 0x06)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }

                //Console.WriteLine("Returning error = " + error.ToString());
                return !error;
            }
        }

        // NOT ZERO-BASED
        public string ReadChannelName(int channel, bool is_output = false)
        {

            lock (PIC_LOCK)
            {
                FlushBuffer();

                if (!serialPort.IsOpen) return "";

                byte[] buff = new byte[4];

                buff[0] = 0x09;
                if (is_output)
                {
                    buff[1] = 0x14;
                }
                else
                {
                    buff[1] = 0x12;
                }
                buff[2] = (byte)channel;
                buff[3] = 0x03;

                System.Text.StringBuilder return_string = new System.Text.StringBuilder();

                int bytes_read = 0;
                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 4);
                    Thread.Sleep(100);

                    if (serialPort.BytesToRead > 2)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        bytes_read = serialPort.BytesToRead;
                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        for (int i = 3; i < bytes_read - 1; i++)
                        {
                            if (((char)bytes[i] < 0x20) || ((char)bytes[i]) > 0x7F)
                            {
                                // Exit here because we got an invalid character.
                                return return_string.ToString().Replace("  "," ");
                            } else
                            {
                                return_string.Append((char)bytes[i]); 
                            }
                            
                        }

                        return return_string.ToString().Replace("  ", " ");
                    }
                    else
                    {
                        FlushBuffer();
                    }
                }

                return "Unknown";
            }
        }

        public bool ReadRS232Mute(int channel)
        {

            lock (PIC_LOCK)
            {
                FlushBuffer();

                if (!serialPort.IsOpen) return false;

                byte[] buff = new byte[4];

                buff[0] = 0x12;
                buff[1] = 0x01;
                buff[2] = (byte)channel;
                buff[3] = 0x03;


                int bytes_read = 0;
                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 4);
                    Thread.Sleep(100);

                    if (serialPort.BytesToRead > 2)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        bytes_read = serialPort.BytesToRead;
                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[1] == 0x01)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
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

        public int ReadRS232Vol(int channel)
        {

            lock (PIC_LOCK)
            {
                FlushBuffer();

                if (!serialPort.IsOpen) return -1;

                byte[] buff = new byte[4];

                buff[0] = 0x12;
                buff[1] = 0x02;
                buff[2] = (byte)channel;
                buff[3] = 0x03;


                int bytes_read = 0;
                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 4);
                    Thread.Sleep(100);

                    if (serialPort.BytesToRead > 2)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        bytes_read = serialPort.BytesToRead;
                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[0] == 0x06 && bytes[1] >=0 && bytes[1] <= 100)
                        {
                            return bytes[1];
                        }
                        else
                        {
                            return -1;
                        }

                    }
                    else
                    {
                        FlushBuffer();
                    }
                }

                return -1;
            }
        }

        public bool SetRS232Mute(int channel, int new_mute)
        {

            lock (PIC_LOCK)
            {
                FlushBuffer();

                if (!serialPort.IsOpen) return false;

                byte[] buff = new byte[4];

                buff[0] = 0x13;

                buff[1] = (byte)new_mute;
                buff[2] = (byte)channel;
                buff[3] = 0x03;


                int bytes_read = 0;
                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 4);
                    Thread.Sleep(100);

                    if (serialPort.BytesToRead > 2)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        bytes_read = serialPort.BytesToRead;
                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[1] == (byte)new_mute)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
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

        public bool ResetRS232Volume(int channel)
        {

            lock (PIC_LOCK)
            {
                FlushBuffer();

                if (!serialPort.IsOpen) return false;

                byte[] buff = new byte[4];

                buff[0] = 0x13;
                buff[1] = 0x03; 
                buff[2] = (byte)channel;
                buff[3] = 0x03;


                int bytes_read = 0;
                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 4);
                    Thread.Sleep(100);

                    if (serialPort.BytesToRead > 2)
                    {
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        bytes_read = serialPort.BytesToRead;
                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if (bytes[1] == 0x03)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
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

        public string StreamData = "";

        public void StreamReadPage(int prog_num, int page_num, ref List<UInt32> FLASH_READ_VALUES, int index_offset)
        {
            lock (PIC_LOCK)
            {
                FlushBuffer();

                byte[] buff = new byte[5];

                buff[0] = 0x10;
                buff[1] = 0x09;
                buff[2] = (byte) prog_num;
                buff[3] = (byte) page_num;
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
                        Console.WriteLine("serialPort.BytesToRead was not == 261, it was " + serialPort.BytesToRead);

                        FlushBuffer();
                        return;
                    }

                    Byte[] bytes = new Byte[serialPort.BytesToRead];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x09 && bytes[2] == prog_num && bytes[3] == page_num && bytes[260] == 0x03)
                    {
                        //Console.WriteLine("Data looks good!");

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
                        Console.WriteLine("[ERROR] First byte read was " + bytes[0]);
                        return;
                    }

                }
                else
                {
                    /*
             Byte[] errbytes = new Byte[serialPort.BytesToRead];

             serialPort.Read(errbytes, 0, serialPort.BytesToRead);

             Console.WriteLine("serialPort.BytesToRead was not > 1, it was " + serialPort.BytesToRead);
             return false;
                 */
                }
            }


        }

        public void RestoreFactorySettings(object sender, DoWorkEventArgs doWorkEventArgs)
        {

            BackgroundWorker backgroundWorker = sender as BackgroundWorker;

            lock (PIC_LOCK)
            {
                FlushBuffer();

                if (!serialPort.IsOpen) return;

                backgroundWorker.ReportProgress(0, "Started factory reset");

                int timeout_counter = 0;
                int timeout_length = 0;


                bool timeout_error = false;

                byte[] buff = new byte[3];
                Byte[] bytes = null;


                #region Start Transaction

                buff[0] = 0x02;
                buff[1] = 0x77;
                buff[2] = 0x03;

                serialPort.Write(buff, 0, 3);

                Thread.Sleep(500);

                if (serialPort.BytesToRead == 4)
                {
                    bytes = new Byte[serialPort.BytesToRead];
                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[2] != 0x01)
                    {
                        // error
                        return;
                    }

                } else
                {
                    return;
                }

                backgroundWorker.ReportProgress(4,"Erasing all settings");

                #endregion

                #region EEPROM Erase

                timeout_counter = 0;

                timeout_length = 50;

                while (serialPort.BytesToRead < 4)
                {
                    Thread.Sleep(1000);
                    timeout_counter++;

                    if (timeout_counter >= timeout_length)
                    {
                        timeout_error = true;
                        break;

                    }

                    backgroundWorker.ReportProgress((int)((double)((double)timeout_counter / (double)timeout_length) * (38 - 4)) + 4);

                }

                if (timeout_error == true)
                {
                    // error
                    return;
                }

                bytes = new Byte[serialPort.BytesToRead];
                serialPort.Read(bytes, 0, serialPort.BytesToRead);

                if (bytes[2] != 0x02)
                {
                    // error
                    return;
                }

                backgroundWorker.ReportProgress(38, "Writing Factory Program 1");

                #endregion

                #region PROGRAM 1

                timeout_counter = 0;

                timeout_length = 20;

                while (serialPort.BytesToRead < 4)
                {
                    Thread.Sleep(1000);
                    timeout_counter++;

                    if (timeout_counter >= timeout_length)
                    {
                        timeout_error = true;
                        break;

                    }

                    backgroundWorker.ReportProgress((int)((double)((double)timeout_counter / (double)timeout_length) * (53 - 38)) + 38);
                }

                if (timeout_error == true)
                {
                    // error
                    return;
                }

                bytes = new Byte[serialPort.BytesToRead];
                serialPort.Read(bytes, 0, serialPort.BytesToRead);

                if (bytes[2] != 0x03)
                {
                    // error
                    return;
                }

                backgroundWorker.ReportProgress(53, "Writing Factory Program 2");

                #endregion

                #region PROGRAM 2

                timeout_counter = 0;

                timeout_length = 20;

                while (serialPort.BytesToRead < 4)
                {
                    Thread.Sleep(1000);
                    timeout_counter++;

                    if (timeout_counter >= timeout_length)
                    {
                        timeout_error = true;
                        break;

                    }

                    backgroundWorker.ReportProgress((int)((double)((double)timeout_counter / (double)timeout_length) * (68 - 53)) + 53);
                }

                if (timeout_error == true)
                {
                    // error
                    return;
                }

                bytes = new Byte[serialPort.BytesToRead];
                serialPort.Read(bytes, 0, serialPort.BytesToRead);

                if (bytes[2] != 0x04)
                {
                    // error
                    return;
                }

                backgroundWorker.ReportProgress(68, "Writing Factory Program 3");

                #endregion

                #region PROGRAM 3

                timeout_counter = 0;

                timeout_length = 20;

                while (serialPort.BytesToRead < 4)
                {
                    Thread.Sleep(1000);
                    timeout_counter++;

                    if (timeout_counter >= timeout_length)
                    {
                        timeout_error = true;
                        break;

                    }

                    backgroundWorker.ReportProgress((int)((double)((double)timeout_counter / (double)timeout_length) * (83 - 68)) + 68);
                }

                if (timeout_error == true)
                {
                    // error
                    return;
                }

                bytes = new Byte[serialPort.BytesToRead];
                serialPort.Read(bytes, 0, serialPort.BytesToRead);

                if (bytes[2] != 0x05)
                {
                    // error
                    return;
                }

                backgroundWorker.ReportProgress(83, "Returning to Preset 1");

                #endregion

                #region SWITCH

                timeout_counter = 0;

                timeout_length = 5;

                while (serialPort.BytesToRead < 4)
                {
                    Thread.Sleep(1000);
                    timeout_counter++;

                    if (timeout_counter >= timeout_length)
                    {
                        timeout_error = true;
                        break;

                    }
                }

                if (timeout_error == true)
                {
                    // error
                    return;
                }

                bytes = new Byte[serialPort.BytesToRead];
                serialPort.Read(bytes, 0, serialPort.BytesToRead);

                if (bytes[2] != 0x06)
                {
                    // error
                    return;
                }

                backgroundWorker.ReportProgress(87, "Finalizing restore");

                #endregion

                #region ADDRESSES

                timeout_counter = 0;

                timeout_length = 15;

                while (serialPort.BytesToRead < 4)
                {
                    Thread.Sleep(1000);
                    timeout_counter++;

                    if (timeout_counter >= timeout_length)
                    {
                        timeout_error = true;
                        break;

                    }

                    backgroundWorker.ReportProgress((int)((double)((double)timeout_counter / (double)timeout_length) * (100 - 87)) + 87);
                }

                if (timeout_error == true)
                {
                    // error
                    return;
                }

                bytes = new Byte[serialPort.BytesToRead];
                serialPort.Read(bytes, 0, serialPort.BytesToRead);

                if (bytes[2] != 0x07)
                {
                    // error
                    return;
                }

                backgroundWorker.ReportProgress(100, "Restore Complete!");

                #endregion


            }


        }
    }
}
