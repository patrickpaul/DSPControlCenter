using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using SA_Resources;
using SA_Resources.SADevices;
using SA_Resources.SAForms;

namespace SA_Resources
{
    public partial class StartupForm : Form
    {

        private readonly bool _vsDebug = System.Diagnostics.Debugger.IsAttached;

        private bool standalone_build = false;

        private List<SADevicePlugin> DevicePlugins = new List<SADevicePlugin>();


        private string CONFIGFILE = "";

        public StartupForm()
        {


            InitializeComponent();

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
                    standalone_build = true;
                }

                if (args.Length > 2)
                {
                    if (args[2].Contains(".scfg"))
                    {
                        CONFIGFILE = args[2];
                    }

                    if (args[2] == "/standalone")
                    {
                        standalone_build = true;
                    }
                }
            }

            if (!_vsDebug && !standalone_build)
            {
                string InstallPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\StewartAudioDSP", "Path", null);
                // Do stuff
                Directory.SetCurrentDirectory(InstallPath);
            }

            //CONFIGFILE = @"c:\users\patrick\desktop\manager107.scfg";
        }


        private void StartupForm_Load(object sender, EventArgs e)
        {
            //PIC_Conn = new PIC_Bridge(serialPort);
            
            string[] device_plugins = Directory.GetFiles(@"Devices\", "*.sadev"); // <-- Case-insensitive

            foreach (string single_plugin in device_plugins)
            {
                InitializeDevicePlugin(single_plugin);
                
            }

            DeviceListBox.SelectedIndex = 0;
            DeviceListBox.Invalidate();
            
        }

        private void StartupForm_Shown(object sender, EventArgs e)
        {
            int device_id = 0;

            SADevicePlugin foundPlugin = null;

            automaticUpdater1.ForceCheckForUpdate();

            if (CONFIGFILE != "")
            {
                //device_id = SCFG_Manager.GetDeviceID(CONFIGFILE);

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
                        Console.WriteLine("Given file is for a " + foundPlugin.Name);

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

                String assembly_path = Directory.GetCurrentDirectory() + @"\" + plugin_filepath;
                String assembly_name = plugin_filepath.Replace(" ", "_").Replace("-", "_").Replace("Devices\\","").Replace(".sadev","");
                Assembly a = null;
                a = Assembly.LoadFrom(assembly_path);
                Type mainFormType = a.GetType(assembly_name + ".MainForm");

                Form form = Activator.CreateInstance(mainFormType) as Form;

                string DeviceName = (string)mainFormType.GetMethod("GetDeviceName").Invoke(form, null);
                int DeviceID = (int)mainFormType.GetMethod("GetDeviceID").Invoke(form, null);

                DevicePlugins.Add(new SADevicePlugin(DeviceID, DeviceName, assembly_path, assembly_name));

                DeviceListBox.Items.Add(DeviceName);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading device plugin at " + plugin_filepath + ". Message was: " + ex.Message);
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

                MainForm_Template form = (MainForm_Template)Activator.CreateInstance(classType, CONFIGFILE);

                form.ShowDialog();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("device attached"))
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
            
            if (DeviceListBox.SelectedIndex == -1)
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
