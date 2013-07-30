using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace SA_Resources
{
    public class PIC_Bridge
    {
        private SerialPort serialPort;
        private int ReadDelayMS = 5;
        public bool isOpen = false; 

        uint last_address1, last_address2;
        uint last_byte1, last_byte2, last_byte3, last_byte4;

        int delay_ms = 30;

        private Dictionary<int, string> ERROR_LOOKUP;

        public PIC_Bridge(SerialPort _serialPort)
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

        public bool Open(string portName)
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

        public void Close()
        {
            serialPort.Close();
            isOpen = false;

        }

        public void FlushBuffer()
        {
            if (!serialPort.IsOpen) return;

            //TODO - GIVE THIS A TIMEOUT?
            while (serialPort.BytesToRead > 0)
            {

                Console.WriteLine("Cleared from serial buffer: " + serialPort.ReadExisting());
            }


        }

        public void print_error(int error_id)
        {
            Console.WriteLine("Error: " + ERROR_LOOKUP[error_id]);
        }

        public bool getRTS()
        {
            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[3];

            buff[0] = 0x02;
            buff[1] = 0x01;
            buff[2] = 0x03;


            FlushBuffer();


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(100);

                if (serialPort.BytesToRead > 2)
                {
                    Byte[] bytes = new Byte[serialPort.BytesToRead];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x01)
                    {
                        return true;
                    }

                    if(bytes[0] == 0x15)
                    {
                        print_error(bytes[1]);
                    }

                } else
                {
                    FlushBuffer();
                }
            }

            return false; // failed after 3 retries
        }

        public int GetDeviceID()
        {

            if (!getRTS())
            {
                throw new Exception("Device did not respond to RTS request");
            }

            byte[] buff = new byte[3];

            buff[0] = 0x02;
            buff[1] = 0X08;
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
                    return 0x00;
                }

            }

            Byte[] bytes = new Byte[bytesToRead];

            serialPort.Read(bytes, 0, bytesToRead);

            if (bytes[0] == 0x06 && bytes[1] == 0x08)
            {
                return bytes[2];
            }
            else
            {
                return 0x00;
            }
        }

        public UInt32 Read_DSP_Value(uint address_index)
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
            buff[1] = (byte)address_index1;
            buff[2] = (byte)address_index2;
            buff[3] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 4);
                Thread.Sleep(delay_ms);

                if (serialPort.BytesToRead > 2)
                {
                    Byte[] bytes = new Byte[serialPort.BytesToRead];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if ((bytes[0] == 0x06) && (bytes[1] == (byte)address_index1) && (bytes[2] == (byte)address_index2))
                    {
                        /* INTENTIONAL REVERSAL!! */
                        UInt32 test_value = 0x00000000 | (uint)bytes[3];
                        test_value = test_value << 8;
                        test_value = test_value | (uint)bytes[4];
                        test_value = test_value << 8;
                        test_value = test_value | (uint)bytes[5];
                        test_value = test_value << 8;
                        test_value = test_value | (uint)bytes[6];

                        return test_value;
                    }

                    if (bytes[0] == 0x15)
                    {
                        print_error(bytes[1]);
                    }

                }
                else
                {
                    FlushBuffer();
                }
            }

            return 0xFFFFFFFF;
        }

        public bool Reboot()
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

        public bool SetDSPValue(uint address_index, UInt32 value)
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

                    Byte[] bytes = new Byte[serialPort.BytesToRead+5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x05 && bytes[2] == (byte)address_index1 && bytes[3] == (byte)address_index2)
                    {
                        return true;
                    }

                    if (bytes[0] == 0x15)
                    {
                        print_error(bytes[1]);
                    }

                }
                else
                {
                    FlushBuffer();
                }
            }

            return false;

        }

        public bool sendAckdData(byte commandAddr, byte commandData, int delay_value = 50, byte extra_byte = 0xFF)
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

            } else
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
                        print_error(bytes[1]);
                    }

                }
                else
                {
                    FlushBuffer();
                }
            }

            return false;
        }

        public bool sendAckdString(byte commandAddr, string in_string, int delay_value = 50)
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

            serialPort.Write(buff,0,1);


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
        
        
        public bool sendAckdCommand(byte commandValue, int delay_value = 50)
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
                        print_error(bytes[1]);
                    }

                }
                else
                {
                    FlushBuffer();
                }
            }

            return false; // failed after 3 retries

        }

        public bool verifyLastCommand()
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
                        print_error(bytes[1]);
                    }

                }
                else
                {
                    FlushBuffer();
                }
            }

            return false; 
            
        }

        public bool SendChannelName(int channel, string name, bool is_output = false)
        {
            FlushBuffer();

            bool error = false;
            byte[] outbuff = new byte[10];
            byte[] buff = new byte[(name.Length)+4];

            int byte_counter = 3;
            //0x10 0x01 0x58 0x58 0x58 0x58 0x58 0x03
            buff[0] = 0x09;
            if (is_output)
            {
                buff[1] = 0x13;
            } else
            {
                buff[1] = 0x11;
            }

            buff[2] = (byte)channel;

            for (int i = 0; i < name.Length; i++)
            {
                buff[byte_counter++] = (byte) name[i];
            }

            buff[byte_counter] = 0x03;

            int buff_counter = 0;

            for (int i = 0; i <= byte_counter; i++)
            {
                serialPort.Write(buff, i, 1);

                Thread.Sleep(20);
                if(i < 3)
                {
                    continue;
                }

                if(i == byte_counter)
                {
                    break;
                }


                Thread.Sleep(30);
                
                if (serialPort.BytesToRead != 1)
                {
                    error = true;
                    serialPort.Read(outbuff, 0, serialPort.BytesToRead);
                }
                else
                {
                    serialPort.Read(outbuff, 0, 1);

                    if (outbuff[0] != buff[i])
                    {
                        error = true;
                        FlushBuffer();
                    }
                }
            }

            

            if (serialPort.BytesToRead > 0)
            {
                FlushBuffer();

            }

            return !error;
        }

        public string ReadChannelName(int channel, bool is_output = false)
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
                        return_string.Append((char)bytes[i]);
                    }

                    return return_string.ToString();

                }
                else
                {
                    FlushBuffer();
                }
            }

            return "Unknown";
        }

        public bool ReadPhantomPower(int channel)
        {

            FlushBuffer();

            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[4];

            buff[0] = 0x09;
            buff[1] = 0x10; 
            buff[2] = (byte)channel;
            buff[3] = 0x03;

            System.Text.StringBuilder return_string = new System.Text.StringBuilder();

            int bytes_read = 0;
            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 4);
                Thread.Sleep(100);

                if (serialPort.BytesToRead > 3)
                {
                    Byte[] bytes = new Byte[serialPort.BytesToRead];

                    bytes_read = serialPort.BytesToRead;
                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if(bytes[2] == 0x01)
                    {
                        return true;
                    } else
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
}
