using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources.SADevices
{
    public class SADevicePlugin
    {
        public int ID;
        public string Name;
        public string Filepath;
        public string Assembly;
        public int DisplayOrder;
        public SADevicePlugin(int _deviceID, string _deviceName, string _filePath, string _assembly, int _displayOrder)
        {
            ID = _deviceID;
            Name = _deviceName;
            Filepath = _filePath;
            Assembly = _assembly;
            DisplayOrder = _displayOrder;
        }

        public bool MatchesDeviceID(int device_id)
        {
            return this.ID == device_id;
        }
    }
}
