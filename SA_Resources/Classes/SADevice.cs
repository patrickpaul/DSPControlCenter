using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
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

    public enum DeviceType
    {
        DSP4x4,
        FLX80,
        FLX160,
        FLX320,
        DSP1001,
        DSP1002,
        Unknown
    }
}
