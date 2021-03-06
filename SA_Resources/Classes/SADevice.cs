﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources.DeviceManagement
{
    public enum DeviceType
    {
        DSP4x4,
        FLX804,
        FLX804CV,
        FLX1602,
        FLX1602CV, 
        FLX3201,
        FLX804Net,
        FLX804CVNet,
        FLX1602Net,
        FLX1602CVNet,
        FLX3201CVNet, 
        DSP1001,
        DSP1002,
        DSP1001Net,
        DSP1002Net,
        DSP1002LZ,
        DSP1002LZNet,
        Unknown
    }

    public enum DeviceFamily
    {
        DSP4x4,
        FLX,
        DSP100,
        FLXNET,
        DSP100NET,
        Unknown
    }

    public class SADevice
    {
        public SADevice(string _name, int _port, DeviceType _type = DeviceType.Unknown, string _serial = "", int _id = 0, double _fw = 0.0)
        {
            Name = _name;
            Port = _port;
            Type = _type;
            SerialNumber = _serial;
            Id = _id;
            FW = _fw;
        }

        public string Name;
        public int Port;
        public DeviceType Type;
        public string SerialNumber;
        public int Id;
        public double FW;
    }
}