using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SA_Resources;

namespace SA_Resources
{
    public partial class StartupForm : Form
    {

        private PIC_Bridge PIC_Conn;

        private readonly bool _vsDebug = System.Diagnostics.Debugger.IsAttached;

        private bool _isRunning;
        private bool loadingWithoutConnecting = false;

        private readonly Dictionary<int, string> _deviceIDs = new Dictionary<int, string>();

        private List<COMPortInfo> USBCOMPorts;
        private List<COMPortInfo> ProvenCOMPorts;
        public StartupForm()
        {
            InitializeComponent();

            _deviceIDs.Add(0x01, "FLX Tester");

            _deviceIDs.Add(0x02, "FLX80-4");
            _deviceIDs.Add(0x03, "FLX 480 70V");
            _deviceIDs.Add(0x04, "FLX 160-2 70V");
            _deviceIDs.Add(0x05, "FLX 320-1 70V");



            _deviceIDs.Add(0x10, "DSP100-1");
            _deviceIDs.Add(0x11, "DSP100-2");

            _deviceIDs.Add(0x20, "DSP 4x4");

            ProvenCOMPorts = new List<COMPortInfo>();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PIC_Conn = new PIC_Bridge(serialPort1);
            
            string[] device_files = Directory.GetFiles(@"Devices\", "*.dll"); // <-- Case-insensitive

            foreach (string name in device_files)
            {
                if(name.Contains("Resources"))
                {
                    continue;
                }

                DeviceListBox.Items.Add(Path.GetFileName(name).Replace(".dll", "").Replace("__", "-").Replace("_", " "));
            }
        }

        private void loadDevice(string deviceName)
        {


            //try
            //{

                String assembly_path = Directory.GetCurrentDirectory() + @"\Devices\" + deviceName.Replace(" ", "_").Replace("-", "_") + ".dll";
                String assembly_name = deviceName.Replace(" ", "_").Replace("-", "_");
                Assembly a = null;
                a = Assembly.LoadFrom(assembly_path);
                Type classType = a.GetType(assembly_name + ".MainForm");

                Form form;
                if (loadingWithoutConnecting)
                {
                    form = Activator.CreateInstance(classType, PIC_Conn, "") as Form;
                }
                else
                {
                    form = Activator.CreateInstance(classType, PIC_Conn, USBCOMPorts[listDevices.SelectedIndex].SerialNumber) as Form;
                }
                

                form.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Unable to load device plugin for " + deviceName + ". Error encountered: " + ex.Message, "Device Load Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            PIC_Conn.Close();
        }

        private void btnLoadDemo_Click(object sender, EventArgs e)
        {
            loadingWithoutConnecting = true;
            
            if (DeviceListBox.SelectedIndex == -1)
            {
                loadDevice(DeviceListBox.Items[0].ToString());
            }
            else
            {
                loadDevice(DeviceListBox.Items[DeviceListBox.SelectedIndex].ToString());
            }
            
        }

        private void DeviceListBox_DoubleClick(object sender, EventArgs e)
        {
            loadDevice(DeviceListBox.Items[DeviceListBox.SelectedIndex].ToString());
        }

        private void btnScanDevices_Click(object sender, EventArgs e)
        {
            //try
            //{
                int deviceCount = 0;

                lblScanStatus.Text = "Scanning for Devices";
                lblScanStatus.Invalidate();

                listDevices.Items.Clear();
                listDevices.Enabled = false;
                listDevices.Invalidate();
                USBCOMPorts = COMPortInfo.GetUSBCOMPorts();
                List<string> devices = new List<string>();
                //string[] ports = SerialPort.GetPortNames();

                if (USBCOMPorts.Count == 0)
                {
                        // Hide this message during development
                        lblScanStatus.Text = "None found";
                        lblScanStatus.Invalidate();
                        return;
                }
                else
                {

                    foreach (COMPortInfo port in USBCOMPorts)
                    {
                        if (PIC_Conn.Open(port.Name))
                        {
                            if (PIC_Conn.getRTS())
                            {

                                int device_type = PIC_Conn.GetDeviceID();

                                if (_deviceIDs.ContainsKey(device_type))
                                {
                                    listDevices.Enabled = true;
                                    devices.Add(_deviceIDs[device_type] + " (" + port.Name + ")");
                                    listDevices.Items.Add(_deviceIDs[device_type] + " (SN: " + port.SerialNumber + ")");
                                    ProvenCOMPorts.Add(port);
                                }
                            }

                            PIC_Conn.Close();

                        }
                    }

                    
                    lblScanStatus.Text = devices.Count + " devices found.";

                    if(devices.Count > 0)
                    {
                        listDevices.SelectedIndex = 0;
                        listDevices.Invalidate();
                        btnConnect.Enabled = true;
                    } else
                    {
                        btnConnect.Enabled = false;
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading application: \n\n" + ex.Message + "\n\nProgram will now exit.", "Exception During Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                

            //}
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                // This is a STOP command
                btnConnect.Text = "Connect";

                PIC_Conn.Close();
                _isRunning = false;
                return;
            }

            loadingWithoutConnecting = false;

            _isRunning = false;

            lblStatus.Text = "Connecting to Device...";

            try
            {
                string portName = ProvenCOMPorts[listDevices.SelectedIndex].Name;
                if (PIC_Conn.Open(portName))
                {
                    Thread.Sleep(100);
                    if (PIC_Conn.getRTS())
                    {
                        lblStatus.Text = "Connected\nDownloading...";

                        int device_type = PIC_Conn.GetDeviceID();

                        if (_deviceIDs.ContainsKey(device_type))
                        {
                            lblStatus.Text = "Connected to\n" + _deviceIDs[device_type];
                            loadDevice(_deviceIDs[device_type]);

                            btnConnect.Text = "Disconnect";
                            _isRunning = true;
                        }
                        else
                        {
                            lblStatus.Text = "Device ID not\nrecognized";
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Device not\nrecognized";
                    }
                }
                else
                {
                    lblStatus.Text = "Unable to Connect";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (!_isRunning)
            {
                PIC_Conn.Close();
            }
        }
    }
}
