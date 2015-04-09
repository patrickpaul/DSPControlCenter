using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using SA_Resources.DeviceManagement;

using SA_Resources.SAForms;

namespace SA_Resources
{
    public partial class StartupForm : Form
    {

        private readonly bool _vsDebug = Debugger.IsAttached;

#if STANDALONE

        private readonly bool _standaloneBuild = true;
#else
        private readonly bool _standaloneBuild = false;
#endif
        private List<SADevicePlugin> DevicePlugins = new List<SADevicePlugin>();
        private List<SADevicePlugin> OrderedDevicePlugins = new List<SADevicePlugin>();


        private string CONFIGFILE = "";

        public StartupForm()
        {


            InitializeComponent();

            try
            {
                EventLog appLog = new EventLog();
                appLog.Source = "DSP Control Center";
                appLog.WriteEntry("Started DSP Control Center");      

                string[] args = Environment.GetCommandLineArgs();



                CONFIGFILE = "";
                if (args.Length > 1)
                {
                    if (args[0].Contains(".scfg"))
                    {
                        CONFIGFILE = args[0];
                    }

                    if (args[1].Contains(".scfg"))
                    {
                        CONFIGFILE = args[1];
                    }

                    if (args[1] == "/standalone")
                    {
                        _standaloneBuild = true;
                    }

                    if (args.Length > 2)
                    {
                        if (args[2].Contains(".scfg"))
                        {
                            CONFIGFILE = args[2];
                        }

                        if (args[2] == "/standalone")
                        {
                            _standaloneBuild = true;
                        }
                    }
                }

                if (!_vsDebug && !_standaloneBuild)
                {
                    string installPath = (string) Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\StewartAudioDSP", "Path", null);
                    // Do stuff

                    if (String.IsNullOrEmpty(installPath))
                    {
                        MessageBox.Show("Note: Application is not installed, running in portable mode.","Application Not Installed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //this.Close();
                    } else
                    {
                        Directory.SetCurrentDirectory(installPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program: " + ex.StackTrace);
            }
        }


        private void StartupForm_Load(object sender, EventArgs e)
        {
            //PIC_Conn = new DeviceBridge(serialPort);
            try
            {
                string[] device_plugins = Directory.GetFiles(@"Devices\", "*.sadev"); // <-- Case-insensitive

                if (device_plugins.Count() < 1)
                {
                    MessageBox.Show("There are no device plugins available!");
                }

 
                foreach (string single_plugin in device_plugins)
                {
                    InitializeDevicePlugin(single_plugin);

                }

                if (DevicePlugins.Count > 0)
                {
                    OrderedDevicePlugins = DevicePlugins.OrderBy(x => x.DisplayOrder).ToList();

                    foreach (SADevicePlugin singlePlugin in OrderedDevicePlugins)
                    {
                        DeviceListBox.Items.Add(singlePlugin.Name);
                    }
                }

 
                DeviceListBox.SelectedIndex = 0;
                DeviceListBox.Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in StartupForm.StartupForm_Load]: " + ex.Message);
            }
        }

        private void StartupForm_Shown(object sender, EventArgs e)
        {
            int device_id = 0;

            SADevicePlugin foundPlugin = null;

            automaticUpdater1.ForceCheckForUpdate();

            if (CONFIGFILE != "")
            {
                if (device_id > 0)
                {
                    foreach (SADevicePlugin singlePlugin in DevicePlugins)
                    {
                        if (singlePlugin.MatchesDeviceID(device_id))
                        {
                            foundPlugin = singlePlugin;
                            break;
                        }
                    }

                    if (foundPlugin != null)
                    {
                        Debug.WriteLine("Given file is for a " + foundPlugin.Name);

                        OpenDeviceInterface(foundPlugin.Name, CONFIGFILE);
                    }
                    else
                    {
                        MessageBox.Show("Unable to locate device plugin found in " + Path.GetFileName(CONFIGFILE), "SCFG Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Unable to locate device plugin found in " + Path.GetFileName(CONFIGFILE), "SCFG Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }
        
        private void InitializeDevicePlugin(string plugin_filepath)
        {
            try
            {

                String assemblyPath = Directory.GetCurrentDirectory() + @"\" + plugin_filepath;
                String assemblyName = plugin_filepath.Replace(" ", "_").Replace("-", "_").Replace("Devices\\","").Replace(".sadev","");
                Assembly a = null;
                a = Assembly.LoadFrom(assemblyPath);
                Type mainFormType = a.GetType(assemblyName + ".MainForm");

                var form = Activator.CreateInstance(mainFormType) as Form;

                string deviceName = (string)mainFormType.GetMethod("GetDeviceName").Invoke(form, null);
                var deviceId = (int)mainFormType.GetMethod("GetDeviceID").Invoke(form, null);
                var displayOrder = (int)mainFormType.GetMethod("GetDisplayOrder").Invoke(form, null);

                DevicePlugins.Add(new SADevicePlugin(deviceId, deviceName, assemblyPath, assemblyName, displayOrder));

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error loading device plugin at " + plugin_filepath + ". Message was: " + ex.Message);
            }
        }


        private void OpenDeviceInterface(string deviceName, string configFile = "")
        {

            try
            {
                SADevicePlugin activePlugin = null;

                foreach (SADevicePlugin singlePlugin in DevicePlugins)
                {
                    if (singlePlugin.Name == deviceName)
                    {
                        activePlugin = singlePlugin;
                    }
                }

                if (activePlugin == null)
                {
                    MessageBox.Show("Error encountered: Unable to find device file for selected device.", "Device Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                Assembly a = null;
                a = Assembly.LoadFrom(activePlugin.Filepath);
                Type classType = a.GetType(activePlugin.Assembly + ".MainForm");

                var form = (MainForm_Template)Activator.CreateInstance(classType, CONFIGFILE);

                form.ShowDialog();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("device attached") || ex.Message.Contains("device timeout"))
                {
                    MessageBox.Show("Communication with device lost. Please re-connect.", "Device Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // We no longer call CloseAndClearComm() because it is called after LoadDevice in the Connect routine
                    // When this exception is caught, it'll continue execution of Connect routine and call CloseAndClearComm()

                }
                else
                {
                    MessageBox.Show("Error encountered: " + ex.Message, "Device Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }


        private void btnLaunch_Click(object sender, EventArgs e)
        {
            //loadingWithoutConnecting = true;
            
            if(DeviceListBox.SelectedIndex == -1)
            {
                OpenDeviceInterface(DeviceListBox.Items[0].ToString());
            }
            else
            {
                OpenDeviceInterface(DeviceListBox.Items[DeviceListBox.SelectedIndex].ToString());
            }
            
        }

    }
}
