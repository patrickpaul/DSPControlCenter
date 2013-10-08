using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Management;

namespace SA_Resources
{

    using System.Management;
    
    internal class ProcessConnection
    {

        
        
        public static ConnectionOptions ProcessConnectionOptions()
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }

        public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
        {
            ManagementScope connectScope = new ManagementScope();
            connectScope.Path = new ManagementPath(@"\\" + machineName + path);
            connectScope.Options = options;
            connectScope.Connect();
            return connectScope;
        }
    }

    public class COMPortInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public COMPortInfo() { }

        public static List<COMPortInfo> GetUSBCOMPorts()
        {
            List<COMPortInfo> comPortInfoList = new List<COMPortInfo>();

            ConnectionOptions options = ProcessConnection.ProcessConnectionOptions();
            ManagementScope connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");

            ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
            ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

            using (comPortSearcher)
            {
                string caption = null;
                string serialNumber = "";
                int last_index = 0;
                string obj_as_string = "";
                int obj_string_length = 0;
                foreach (ManagementObject obj in comPortSearcher.Get())
                {
                    if (obj != null)
                    {
                        
                        object captionObj = obj["Caption"];
                        if (captionObj != null)
                        {
                            //\\PATRICK-WS1\root\CIMV2:Win32_PnPEntity.DeviceID="FTDIBUS\\VID_0403+PID_6015+DA008AP4A\\0000"
                            
                            caption = captionObj.ToString();
                            if (caption.Contains("USB Serial Port (COM"))
                            {

                                    obj_as_string = obj.ToString();
                                    obj_string_length = obj_as_string.Length;
                                    last_index = obj_as_string.LastIndexOf("6015+");

                                    if ((last_index + 13) > obj_string_length)
                                    {
                                        serialNumber = "UNKNOWN";
                                    } else
                                    {
                                        serialNumber = obj_as_string.Substring(last_index + 5, 8);
                                    }
                                COMPortInfo comPortInfo = new COMPortInfo();
                                comPortInfo.Name = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", string.Empty).Replace(")",
                                                                     string.Empty);
                                comPortInfo.Description = caption;
                                comPortInfo.SerialNumber = serialNumber; 
                                comPortInfoList.Add(comPortInfo);
                            }
                        }
                    }
                }
            }
            return comPortInfoList;
        }
    }
}
