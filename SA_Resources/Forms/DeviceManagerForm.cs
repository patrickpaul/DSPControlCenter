using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.DeviceManagement;
using SA_Resources.USB;

namespace SA_Resources.SAForms
{
    public partial class DeviceManagerForm : Form
    {
        private MainForm_Template PARENT_FORM;

        private List<SADevice> devicesFound;

        private bool timerClose = false;

        private UsbManager manager;

        private bool isConnected = false;

        public bool ONLYPROGRAM1 = false;

		private void USBDeviceStateChanged (UsbStateChangedEventArgs e)
		{
            timer1.Enabled = true;
		}


        public DeviceManagerForm(MainForm_Template _parent)
        {
            InitializeComponent();

            PARENT_FORM = _parent;

            devicesFound = new List<SADevice>();

            manager = new UsbManager();

            manager.StateChanged += new UsbStateChangedEventHandler(USBDeviceStateChanged);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                chkDebug.Visible = true;
            }

            if(!PARENT_FORM._vsDebug)
            {
                chkDebug.Visible = false;
                chkProgram1.Visible = false;
            }
        }


        private List<SADevice> GetDeviceList(int deviceTypeFilter = 0)
        {
            List<SADevice> returnList = new List<SADevice>();

            List<COMPortInfo> USBCOMPorts = new List<COMPortInfo>();

            try
            {
                USBCOMPorts = COMPortInfo.GetUSBCOMPorts();
                List<string> devices = new List<string>();

                if (USBCOMPorts.Count == 0)
                {
                    return returnList;
                }
                else
                {

                    foreach (COMPortInfo port in USBCOMPorts)
                    {
                        if (PARENT_FORM.DeviceConn.Open(port.Name))
                        {
                            if (PARENT_FORM.DeviceConn.getRTS())
                            {

                                int device_type = PARENT_FORM.DeviceConn.GetDeviceID();
                                double firmwareVersion = PARENT_FORM.DeviceConn.GetDeviceFirmwareVersion();

                                if((deviceTypeFilter == 0) || (deviceTypeFilter == device_type))
                                {
                                    returnList.Add(new SADevice(PARENT_FORM.GetDeviceName() + " (SN: " + port.SerialNumber + ")", int.Parse(port.Name.Replace("COM", "")), PARENT_FORM.GetDeviceType(), port.SerialNumber, device_type, firmwareVersion));

                                }
                            }

                            PARENT_FORM.DeviceConn.Close();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DeviceManagerForm.GetDeviceList]: " + ex.Message);
            }

            return returnList;

        }


        private void RefreshDeviceList()
        {

            statusLabel.Text = "Scanning for devices...";
            statusLabel.Invalidate();

            listDevices.Text = "";
            listDevices.Items.Clear();
            listDevices.SelectedIndex = -1;

            devicesFound = GetDeviceList(PARENT_FORM.GetDeviceID());

            if (devicesFound.Count > 0)
            {
                statusLabel.Text = devicesFound.Count + " device" + (devicesFound.Count == 1 ? "" : "s") + " found";

                listDevices.Enabled = true;

                foreach (SADevice singleDevice in devicesFound)
                {
                    listDevices.Items.Add(singleDevice.Name);
                }

                listDevices.SelectedIndex = 0;
                listDevices.Refresh();

                btnConnect.Enabled = true;

            }
            else
            {
                btnConnect.Enabled = false;
                listDevices.Enabled = false;
                statusLabel.Text = "No devices found";
            }
        }


        private void btnRefreshDevices_Click(object sender, EventArgs e)
        {
            grpPushPull.Enabled = false;
            progressBar1.Visible = false;
            RefreshDeviceList();
        }

        private void DeviceManagerForm_Shown(object sender, EventArgs e)
        {
            timerClose = false;
            grpPushPull.Enabled = false;
            progressBar1.Visible = false;
            RefreshDeviceList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deviceThumbnail.BackgroundImage = PARENT_FORM.GetDeviceThumbnail();
            deviceThumbnail.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            grpPushPull.Enabled = false;
            progressBar1.Visible = false;
            timer1.Enabled = false;
            RefreshDeviceList();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {

                if(isConnected)
                {
                    btnConnect.Text = "Connect";
                    isConnected = false;
                    PARENT_FORM.DeviceConn.Close();
                    PARENT_FORM.EndLiveMode();
                    grpPushPull.Enabled = false;
                    progressBar1.Visible = false;
                    RefreshDeviceList();
                    return;
                }
                SADevice SelectedDevice = devicesFound[listDevices.SelectedIndex];

                if (PARENT_FORM.DeviceConn.Open("COM" + SelectedDevice.Port))
                {
                    Thread.Sleep(100);
                    if (PARENT_FORM.DeviceConn.getRTS())
                    {
                        statusLabel.Text = "Connected. Downloading...";

                        int device_type = PARENT_FORM.DeviceConn.GetDeviceID();

                        
                        if (device_type == PARENT_FORM.GetDeviceID())
                        {
                            statusLabel.Text = "Connected to " + SelectedDevice.Name;

                            grpPushPull.Enabled = true;
                            btnConnect.Text = "Disconnect";
                            isConnected = true;

                            PARENT_FORM.FIRMWARE_VERSION = devicesFound[listDevices.SelectedIndex].FW;

                            if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.FLX && PARENT_FORM.FIRMWARE_VERSION < 1.8)
                            {
                                MessageBox.Show("FLX devices with firmware version below 1.8 do not support delay. This feature will be disabled once you synchronize the device.", "Device Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            statusLabel.Text = "Device ID not recognized";
                        }
                    }
                    else
                    {
                        statusLabel.Text = "Device not recognized";
                    }
                }
                else
                {
                    statusLabel.Text = "Unable to Connect";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public virtual void DebugDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for(int i = 0; i < 100; i++)
            {
                worker.ReportProgress(i);
                Thread.Sleep(1);
            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            btnOK.Enabled = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;

            if(chkProgram1.Checked)
            {
                ONLYPROGRAM1 = true;
            }

            if (chkDebug.Checked)
            {
                worker.DoWork += DebugDoWork;
            }
            else
            {
                if (radioPull.Checked)
                {
                    worker.DoWork += PARENT_FORM.ReadDevice;
                }
                else
                {
                    worker.DoWork += PARENT_FORM.WriteDevice;
                }
            }

            PARENT_FORM.FIRMWARE_VERSION = devicesFound[listDevices.SelectedIndex].FW;

            worker.RunWorkerCompleted += WorkComplete;

            worker.RunWorkerAsync();
        }

        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;

            int program_index = PARENT_FORM.DeviceConn.GetCurrentProgram();

            PARENT_FORM.ChangeProgram_AfterRead(program_index);

            PARENT_FORM.SetBridgeMode(PARENT_FORM.AmplifierMode);

            PARENT_FORM.BeginLiveMode();
            closeTimer.Enabled = true;

        }

        delegate void SetStatusLabelCallback(string text);

        private void SetStatusLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (statusStrip1.InvokeRequired)
            {
                SetStatusLabelCallback d = new SetStatusLabelCallback(SetStatusLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.statusLabel.Text = text;
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 0)
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            else
            {
                if (e.UserState != null)
                {
                    this.SetStatusLabel(e.UserState.ToString());
                }
            }
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            timerClose = true;
            this.Close();
        }

        private void DeviceManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerClose == false)
            {
                if(grpPushPull.Enabled && PARENT_FORM.DeviceConn.isOpen)
                {
                    // At this point they hit Connect but then hit the Red X to close window
                    // Close connection so that the DeviceConn 
                    PARENT_FORM.DeviceConn.Close();
                }
                
            }
        }

        private void listDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listDevices.SelectedIndex >= 0)
            {
                SADevice SelectedDevice = devicesFound[listDevices.SelectedIndex];

                lblFirmware.Text = "Firmware v" + SelectedDevice.FW.ToString("F");
                lblFirmware.Invalidate();
            }
        }
    }
}
