﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
                    throw new Exception("No serial port chosen");
                }

                try
                {
                    serialPort.PortName = portName;
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in PIC_Bridge.Open: " + ex.Message);
                    throw new Exception("Unable to open port " + portName + ". Error encountered: " + ex.Message);
                }

                if (!serialPort.IsOpen)
                {
                    throw new Exception("Serial port is not open");
                }

                if (!getRTS())
                {
                    serialPort.Close();
                    throw new Exception("Device did not respond after opening Serial Port");
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
                Console.WriteLine("[Exception in PIC_Bridge.getRTS]: " + ex.Message);
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
                Console.WriteLine("[Exception in PIC_Bridge.GetDeviceID]: " + ex.Message);
            }

            return 0x00;
        }


        public int GetCurrentProgram()
        {
            try
            {
                lock (PIC_LOCK)
                {
                    if (!getRTS())
                    {
                        throw new Exception("Device did not respond to RTS request");
                    }

                    byte[] buff = new byte[4];

                    buff[0] = 0x10;
                    buff[1] = 0x08;
                    buff[2] = 0x08; 
                    buff[3] = 0x03;


                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 4);
                        Thread.Sleep(30);

                        if (serialPort.BytesToRead >= 4)
                        {

                            Byte[] bytes = new Byte[serialPort.BytesToRead];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if (bytes[0] == 0x06 && bytes[1] == 0x08)
                            {
                                return (int) bytes[2];
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in PIC_Bridge.GetCurrentProgram]: " + ex.Message);
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
                Console.WriteLine("[Exception in PIC_Bridge.GetDeviceFirmwareVersion]: " + ex.Message);
            }

            return 0.00;
        }


        public bool SwitchActiveProgram(int new_index)
        {
            try
            {
                lock (PIC_LOCK)
                {
                    if (!getRTS())
                    {
                        throw new Exception("Device did not respond to RTS request");
                    }

                    byte[] buff = new byte[4];

                    buff[0] = 0x10;
                    buff[1] = 0x01;
                    buff[2] = (byte)new_index; 
                    buff[3] = 0x03;

                    for (int retry_count = 0; retry_count < 3; retry_count++)
                    {
                        serialPort.Write(buff, 0, 4);
                        Thread.Sleep(500);

                        if (serialPort.BytesToRead >= 4)
                        {

                            Byte[] bytes = new Byte[serialPort.BytesToRead];

                            serialPort.Read(bytes, 0, serialPort.BytesToRead);

                            if (bytes[0] == 0x06 && bytes[1] == 0x01 && bytes[2] == new_index)
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
                Console.WriteLine("[Exception in PIC_Bridge.SwitchActiveProgram]: " + ex.Message);
            }

            return false;
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
                Console.WriteLine("[Exception in PIC_Bridge.Read_DSP_Value]: " + ex.Message);
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
                //Thread.Sleep(5);

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

                    //Stopwatch sw = new Stopwatch();
                    //sw.Restart();
                    int ms_counter = 0;
                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, 6);

                    while (serialPort.BytesToRead < 6 && ms_counter < 100)
                    {
                        Thread.Sleep(1);
                        ms_counter += 1;
                    }

                    if (serialPort.BytesToRead == 6)
                    {
                        //sw.Stop();

                        //Console.WriteLine("Received reply from Read_Live_DSP_Value in " + sw.ElapsedMilliseconds + "ms");
                        Byte[] bytes = new Byte[serialPort.BytesToRead];

                        serialPort.Read(bytes, 0, serialPort.BytesToRead);

                        if ((bytes[0] == 0x06) && (bytes[5] == 0x03))
                        {
                            /* INTENTIONAL REVERSAL!! */
                            UInt32 test_value = 0x00000000 | (uint) bytes[1];
                            test_value = test_value << 8;
                            test_value = test_value | (uint) bytes[2];
                            test_value = test_value << 8;
                            test_value = test_value | (uint) bytes[3];
                            test_value = test_value << 8;
                            test_value = test_value | (uint) bytes[4];

                            FlushBuffer();

                            return test_value;
                        }
                        else
                        {
                            Console.WriteLine("Bad start stop");
                            Thread.Sleep(100);
                        }

                        if (bytes[0] == 0x15)
                        {
                            //print_error(bytes[1]);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Did not have 10 bytes to read, flushing");
                        FlushBuffer();
                    }
                    }

                    Console.WriteLine("Bad read");
                    return 0xFFFFFFFF;
                }
            }
            catch (ThreadAbortException taex)
            {
                Console.WriteLine("[ThreadAbortException in Read_Live_DSP_Value]: " + taex.Message);
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
                Console.WriteLine("[Exception in PIC_Bridge.UpdatePhantomPower]: " + ex.Message);
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

        public bool SoftReboot()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[10];

            buff[0] = 0x02;
            buff[1] = 0x05;
            buff[2] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(1000);

                if (serialPort.BytesToRead >= 3)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x05)
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
            //}

        }
        #region Streaming

        public string StreamData = "";

        public void StreamReadPage(int prog_num, int page_num, ref List<UInt32> FLASH_READ_VALUES, int index_offset)
        {
            lock (PIC_LOCK)
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
                        //Console.WriteLine("got start stream ack. Prog = " + prog_num + ", Page = " + page_num + ", Bytes left - " + serialPort1.BytesToRead);
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

        public void SendStreamNibble(UInt32[] input_values)
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
                    Console.WriteLine("Write timeout! ERROR!");
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
                        //Console.WriteLine("CRC verified! - CRC=" + rx_buffer[0] + ", BTR=" + bytes_to_read);
                    }
                    else
                    {
                        Console.WriteLine("CRC INVALID! - CRC=" + rx_buffer[0] + ", Expecting - " + checksum + ", BTR=" + bytes_to_read);
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
                    Console.WriteLine("Invalid response from write to flash page");
                }
            }
        }

        #endregion

        #region Amplifier Bridge Mode

        public int ReadAmplifierMode()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return 0;

            byte[] buff = new byte[10];

            buff[0] = 0x15;
            buff[1] = 0x01;
            buff[2] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x01)
                    {
                        return (int) bytes[2];
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

        public bool SetAmplifierMode(int newMode)
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[10];

            buff[0] = 0x14;
            buff[1] = 0x01;
            buff[2] = (byte) newMode;
            buff[3] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 4);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 && bytes[1] == 0x01 && bytes[2] == newMode)
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

        #endregion

        #region RS232

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


        #endregion

        #region Remote Volume Control

        public string ReadCurrentRVC()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return "ERROR";

            byte[] buff = new byte[10];

            buff[0] = 0x15;
            buff[1] = 0x14;
            buff[2] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 5)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x16 & bytes[1] == 0x14)
                    {
                        return bytes[2].ToString() + " - " + bytes[3].ToString() + "%";
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

            return "ERROR";
        }

        public int ReadRVCMax()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return 0;

            byte[] buff = new byte[10];

            buff[0] = 0x15;
            buff[1] = 0x04;
            buff[2] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x04)
                    {
                        return bytes[2];
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

        public int ReadRVCMin()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return 0;

            byte[] buff = new byte[10];

            buff[0] = 0x15;
            buff[1] = 0x03;
            buff[2] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x03)
                    {
                        return bytes[2];
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

        public int CalibrateUpperRVC()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return 0;

            byte[] buff = new byte[10];

            buff[0] = 0x14;
            buff[1] = 0x04;
            buff[2] = 0x01;
            buff[3] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 4);
                Thread.Sleep(200);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x04)
                    {
                        return bytes[2];

                    }
                    else
                    {
                        FlushBuffer();
                    }
                }
            }

            return 0;
        }

        public int CalibrateLowerRVC()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return 0;

            byte[] buff = new byte[10];

            buff[0] = 0x14;
            buff[1] = 0x03;
            buff[2] = 0x01; 
            buff[3] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 4);
                Thread.Sleep(200);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x03)
                    {
                        return bytes[2];

   

                    }
                    else
                    {
                        FlushBuffer();
                    }
                }

            }

            return 0;
        }


        #endregion

        #region SleepMode

        public bool ReadSleepModeEnable()
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[10];

            buff[0] = 0x15;
            buff[1] = 0x10;
            buff[2] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 3);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x10)
                    {
                        return (bytes[2] == 0x01);
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

        public bool SetSleepModeEnable(bool newMode)
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[10];

            buff[0] = 0x14;
            buff[1] = 0x10;
            buff[2] = newMode ? (byte)0x01 : (byte)0x00;
            buff[3] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 4);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x10)
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

        public Int16 ReadSleepModeSeconds()
        {
            try
            {
                byte[] buff;
                if (!serialPort.IsOpen) return 0;

                int bytes_to_write = 0;

                buff = new byte[4];

                buff[0] = 0x15;
                buff[1] = 0x11;
                buff[2] = 0x03;
                bytes_to_write = 3;


                for (int retry_count = 0; retry_count < 3; retry_count++)
                {
                    serialPort.Write(buff, 0, bytes_to_write);
                    Thread.Sleep(150);

                    if (serialPort.BytesToRead > 4)
                    {
                        string buffString = serialPort.ReadLine();

                        return Int16.Parse(buffString.Substring(2));
                    }
                    else
                    {
                        //FlushBuffer();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION in ReadData16]: " + ex.Message);
            }
            return 0;
        }

        public bool SetSleepModeSeconds(int newSeconds)
        {
            FlushBuffer();

            if (!serialPort.IsOpen) return false;

            byte[] buff = new byte[10];

            int lsb_byte, msb_byte;

            lsb_byte = newSeconds & 0xFF;
            msb_byte = newSeconds >> 8;

            buff[0] = 0x14;
            buff[1] = 0x11;
            buff[2] = (byte) lsb_byte;
            buff[3] = (byte) msb_byte;
            buff[4] = 0x03;


            for (int retry_count = 0; retry_count < 3; retry_count++)
            {
                serialPort.Write(buff, 0, 5);
                Thread.Sleep(100);

                if (serialPort.BytesToRead >= 4)
                {

                    Byte[] bytes = new Byte[serialPort.BytesToRead + 5];

                    serialPort.Read(bytes, 0, serialPort.BytesToRead);

                    if (bytes[0] == 0x06 & bytes[1] == 0x11)
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
            
        #endregion


    }
}
