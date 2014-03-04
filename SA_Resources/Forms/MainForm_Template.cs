using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.Configurations;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;
using SA_Resources.SADevices;
using SA_Resources.USB;

namespace SA_Resources.SAForms
{
    public partial class MainForm_Template : Form
    {

        #region Variables

        /* Program Configurations */

        public ProgramConfig[] PROGRAMS = new ProgramConfig[3];
        public ProgramConfig PROGRAM_CACHE = new ProgramConfig();

        public DSP_Program_Manager[] DSP_PROGRAMS = new DSP_Program_Manager[20];

        public DSP_Meter_Manager DSP_METER_MANAGER = new DSP_Meter_Manager();

        public int NUM_PRESETS = 3;

        /* Settings Lists */

        public List<DSP_Setting>[] _settings = new List<DSP_Setting>[3];
        public List<DSP_Setting>[] _cached_settings = new List<DSP_Setting>[3];

        public List<UInt32>[] _gain_meters = new List<UInt32>[4];
        public List<UInt32> _mix_meters = new List<UInt32>(); 
        public List<UInt32>[] _comp_in_meters = new List<UInt32>[2];
        public List<UInt32>[] _comp_out_meters = new List<UInt32>[2];
        public List<UInt32> _ducker_meters = new List<UInt32>();
       
        public List<string> _presetNames = new List<string>(); 

        /* Queue Processing */
        
        public Queue UPDATE_QUEUE = new Queue();
        public object _locker = new Object();


        /* Settings to put into demo modes */

        public bool DisableComms = false;
        public readonly bool _vsDebug = System.Diagnostics.Debugger.IsAttached;

        /* Settings Initialization */

        public bool form_loaded = false;

        public int num_channels = 4;
        public int num_phantom = 4;

        public PIC_Bridge _PIC_Conn;

        public int CURRENT_PROGRAM = 0;
        public int NUM_PROGRAMS = 3;

        public SADevice activeDevice;

        public double FIRMWARE_VERSION;

        public Control activeBlockForMenu;


        public object TempConfig;
        public object CopyConfig;
        public object CutConfig;

        public bool has_copy_config = false;
        public bool has_cut_config = false;

        public int temp_from_preset = 0;
        public int temp_from_ch = 0;
        public int temp_from_index = 0;

        public int copy_from_preset = 0;
        public int copy_from_ch = 0;
        public int copy_from_index = 0;

        public int cut_from_preset = 0;
        public int cut_from_ch = 0;
        public int cut_from_index = 0;

        public CopyFormType CopyType;
        public CopyFormType CutType;

        // TODO - Move all DEVICE ID's to a global list
        public string DEVICE_NAME = "";
        public int DEVICE_ID = 0x20;
        public string SERIALNUM = "";

        public string CONFIGFILE = "";

        public bool UnsavedChanges = false;

        public bool LIVE_MODE;

        public bool PRIMITIVES_LOADED = false;
        #endregion


        #region Constructor and InitializePrograms

        public MainForm_Template()
        {
            // Initialize all User Controls
            InitializeComponent();
        }

        public MainForm_Template(string configFile)
        {  
            // Initialize all User Controls
            InitializeComponent();

            menuStrip1.Renderer = new MyRenderer();

            

            // TODO - Should these still be disabled?

            /* INITIALIZE THE SETTINGS TO DEFAULTS */
            /*
            DefaultSettings(); 
            
            
            

            if (configFile != "")
            {
                CONFIGFILE = configFile;

            }

            InitializePrograms();
            InitializeDSPPrimitives();
             * */

        }
        protected void MainForm_Template_Load(object sender, EventArgs e)
        {

            try
            {
                // Starting up not connected, set LIVE_MODE to false
                LIVE_MODE = false;
                // Initialize the PIC_Bridge (starts up closed)
                _PIC_Conn = new PIC_Bridge(serialPort1);

                // Initialize Array of Presets (both Programs and Primitives for now)
                InitializePrograms();
                Initialize_DSP_Programs();

                //TODO - REMOVE ME
                DefaultSettings();
                Default_DSP_Programs();
                Default_DSP_Meters();

                // This prevents Visual Studio from throwing an exception when loading derived forms in designer
                if (DSP_PROGRAMS.Count() < 1 || DSP_PROGRAMS[0] == null)
                {
                    form_loaded = true;
                    return;
                }
                // We have to attach these here because it must occur after the derived forms' InitializeComponent() happens in their Constructors

                AttachUIEvents();
                UpdateTooltips();

                 
                if (CONFIGFILE != "" && CONFIGFILE != " ")
                {
                    SCFG_Manager.Read(CONFIGFILE, this);
                }

                LoadSettingsToProgramConfig();

                UpdateTooltips();

                UpdatePresetDropdown();

                form_loaded = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading application: \n\n" + ex.Message + System.Environment.NewLine + ex.StackTrace + "\n\nProgram will now exit.", "Exception During Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //Application.Exit();

            }
        }


        public virtual void InitializePrograms()
        {
            //NUM_PRESETS
        }

        public virtual void Initialize_DSP_Programs()
        {
            
        }

        public virtual void Initialize_DSP_Meters()
        {

        }

        public virtual void Default_DSP_Programs()
        {
            
        }

        public virtual void Default_DSP_Meters()
        {
            
        }

        #endregion


        #region Live Update (Queue and Process)

        public void BeginLiveMode()
        {

            if (Queue_Thread.IsBusy != true)
            {
                Queue_Thread.RunWorkerAsync();
            }

            LIVE_MODE = true;

            this.SetConnectionPicture((Image)GlobalResources.lblStatus_Connected);
            this.SetConnectButtonText("Disconnect");

            readFromDeviceToolStripMenuItem.Enabled = true;

            // TODO - implement USB detection so heartbeat timer isn't necessary
            //HeartbeatTimer.Enabled = true;

        }

        public void EndLiveMode()
        {
            if (Queue_Thread.WorkerSupportsCancellation == true)
            {
                Queue_Thread.CancelAsync();
            }

            LIVE_MODE = false;

            _PIC_Conn.Close();

            this.SetConnectionPicture((Image)GlobalResources.lblStatus_Disconnected);
            this.SetConnectButtonText("Connect");

            readFromDeviceToolStripMenuItem.Enabled = false;

            //HeartbeatTimer.Enabled = false;

        }


        public void AddItemToQueue(LiveQueueItem itemToAdd)
        {
            // TODO - Disable this once done testing
            Console.WriteLine("Added item to queue: " + itemToAdd.Index + " - " + itemToAdd.Value.ToString("X8"));
            lock (_locker)
            {
                UPDATE_QUEUE.Enqueue(itemToAdd);
            }
        }

        protected void Queue_Thread_DoWork(object sender, DoWorkEventArgs e)
        {

            // RE-EVALUATE QUEUE_THREAD_DOWORK
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    //Console.WriteLine("Cancellation Pending...");
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(50);
                    lock (_locker)
                    {
                        if (UPDATE_QUEUE.Count > 0)
                        {
                            LiveQueueItem read_setting = (LiveQueueItem)UPDATE_QUEUE.Dequeue();

                            if (_PIC_Conn.getRTS())
                            {
                                if (read_setting.Index < 1000 && read_setting.Value != 0xFFFFFFFF)
                                {
                                    if (_PIC_Conn.SetLiveDSPValue((uint)read_setting.Index, read_setting.Value))
                                    {
                                        Console.WriteLine("Successfully sent queued DSP setting: " + read_setting.Index + " - " + read_setting.Value.ToString("X8"));
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR sending queued DSP Setting");
                                    }
                                }
                                else
                                {
                                    // THIS IS A UTILITY SUCH AS PHANTOM POWER OR INPUT/OUTPUT NAME

                                    if (read_setting.ValueString != "")
                                    {
                                        if (read_setting.Index < GetNumInputChannels()+1)
                                        {
                                            
                                            if(_PIC_Conn.SendChannelName(read_setting.Index, read_setting.ValueString, false))
                                            {
                                                Console.WriteLine("Successfully set Input CH " + read_setting.Index.ToString() + " name to " + read_setting.ValueString);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error setting Input CH " + read_setting.Index.ToString() + " name to " + read_setting.ValueString);
                                            }
                                        } else
                                        {
                                            if(_PIC_Conn.SendChannelName(read_setting.Index - GetNumInputChannels(), read_setting.ValueString, true))
                                            {
                                                Console.WriteLine("Successfully set Output CH " + read_setting.Index.ToString() + " name to " + read_setting.ValueString);
                                            } else
                                            {
                                                Console.WriteLine("Error setting Output CH " + read_setting.Index.ToString() + " name to " + read_setting.ValueString);
                                            }
                                        }
                                        
                                    } else if (read_setting.Value == 0x01)
                                    {
                                        if(_PIC_Conn.SetLivePhantomPower((uint)read_setting.Index - 1000, 1))
                                        {
                                            Console.WriteLine("Successfully set Phantom Power CH " + (read_setting.Index - 1000) + " to ON");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error setting set Phantom Power CH " + (read_setting.Index - 1000) + " to ON");
                                        }
                                    }
                                    else
                                    {
                                        if (_PIC_Conn.SetLivePhantomPower((uint)read_setting.Index - 1000, 0))
                                        {
                                            Console.WriteLine("Successfully set Phantom Power CH " + (read_setting.Index - 1000) + " to OFF");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error setting set Phantom Power CH " + (read_setting.Index - 1000) + " to OFF");
                                        }
                                    }


                                }

                            }
                            else
                            {
                                //Console.WriteLine("Couldn't get RTS from BW");
                            }
                        }
                        else
                        {

                            //Console.WriteLine("There are no items in the queue.");
                        }
                    }
                }
            }
        }

        protected void Queue_Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine("BW reported work is complete.");
        }

        protected void Queue_Thread_stop()
        {
            if (Queue_Thread.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                Queue_Thread.CancelAsync();
                //Console.WriteLine("Requested Background worker stop.");
            }
        }

        #endregion


        #region Read/Write Routines

        public virtual void ReadDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            /*
            int programs_to_run = NUM_PROGRAMS;

            if(doWorkEventArgs.Argument != null)
            {
                if((bool)doWorkEventArgs.Argument == true)
                {
                    programs_to_run = 1;
                }
            }
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            
            double num_settings = (_settings[0].Count - 
                (GetProtectedReadBlock1_End() - GetProtectedReadBlock1_Start()) + 
                (GetProtectedReadBlock2_End() - GetProtectedReadBlock2_Start()) + 
                (GetProtectedReadBlock3_End() - GetProtectedReadBlock3_Start())
                ) * programs_to_run;
           
            double total_settings_read = 0.0;

            _PIC_Conn.FlushBuffer();

            backgroundWorker.ReportProgress(0,"Putting device into read mode");

            for (int program_index = 0; program_index < programs_to_run; ++program_index)
            {
                if (_PIC_Conn.sendAckdCommand((byte)(GetEEPROMSwitchCommandBase() + (uint)program_index), 3000))
                {
                    Console.WriteLine("Successfully switched to program " +  (program_index + 1));

                    backgroundWorker.ReportProgress(0, ("Reading program " + (program_index + 1)));

                    Console.WriteLine("Started main read routine");

                    for (int input_index = 0; input_index < GetNumInputChannels(); ++input_index)
                    {
                        PROGRAMS[program_index].inputs[input_index].Name = _PIC_Conn.ReadChannelName(input_index + 1);
                        Thread.Sleep(100);
                    }


                    for (int output_index = 0; output_index < GetNumOutputChannels(); ++output_index)
                    {
                        PROGRAMS[program_index].outputs[output_index].Name = _PIC_Conn.ReadChannelName(output_index + 1, true);
                        Thread.Sleep(100);
                    }
                    for (int phantom_index = 0; phantom_index < GetNumPhantomPowerChannels(); ++phantom_index)
                    {
                        PROGRAMS[program_index].inputs[phantom_index].PhantomPower = _PIC_Conn.ReadPhantomPower(phantom_index);
                        Thread.Sleep(10);
                    }

                    int setting_index_counter = 0;

                    foreach (DSP_Setting singleSetting in _settings[program_index])
                    {
                        if (
                            (singleSetting.Index > GetProtectedReadBlock1_Start() && singleSetting.Index < GetProtectedReadBlock1_End())
                            ||
                            (singleSetting.Index > GetProtectedReadBlock2_Start() && singleSetting.Index < GetProtectedReadBlock2_End())
                            ||
                            (singleSetting.Index > GetProtectedReadBlock3_Start() && singleSetting.Index < GetProtectedReadBlock3_End())
                            )
                        {
                            ++setting_index_counter;
                        }
                        else
                        {
                            if (!_PIC_Conn.sendAckdCommand(1))
                            {
                                // error
                                return;
                            }

                            uint read_value = _PIC_Conn.Read_DSP_Value(singleSetting.Index);

                            _settings[program_index][setting_index_counter].Value = read_value;
                            _cached_settings[program_index][setting_index_counter].Value = read_value;

                            setting_index_counter++;
                            total_settings_read++;

                            backgroundWorker.ReportProgress(Math.Min(100, (int)(total_settings_read / num_settings * 100.0)));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unable to switch to program " + (program_index + 1) + ". Exiting.");
                    return;
                }
            }

            backgroundWorker.ReportProgress(0, "Read device complete");
            backgroundWorker.ReportProgress(100);

            */
        }

        public virtual void WriteDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            /*try
            {
                
                int programs_to_run = NUM_PROGRAMS;

                if (doWorkEventArgs.Argument != null)
                {
                    if ((bool)doWorkEventArgs.Argument == true)
                    {
                        programs_to_run = 1;
                    }
                }

                double total_settings_written = 0.0;

                double num_settings = (_settings[0].Count -
                (GetProtectedWriteBlock1_End() - GetProtectedWriteBlock1_Start()) +
                (GetProtectedWriteBlock2_End() - GetProtectedWriteBlock2_Start()) +
                (GetProtectedWriteBlock3_End() - GetProtectedWriteBlock3_Start())
                ) * programs_to_run;

                BackgroundWorker backgroundWorker = sender as BackgroundWorker;

                backgroundWorker.ReportProgress(0, "Putting device into programming mode");

                _PIC_Conn.FlushBuffer();

                if (!_PIC_Conn.sendAckdCommand(GetProgrammingSwitchCommandBase()))
                {
                    Console.WriteLine("Error sending ackd GetProgrammingSwitchCommandBase");
                }


                for (int program_index = 0; program_index < programs_to_run; ++program_index)
                {
                    backgroundWorker.ReportProgress(0, ("Saving configuration for Program " + (program_index + 1)));

                    int setting_index = 0;

                    foreach (DSP_Setting single_setting in _settings[program_index])
                    {
                        total_settings_written++;
                        setting_index++;

                        if (
                            (single_setting.Index > GetProtectedWriteBlock1_Start() && single_setting.Index < GetProtectedWriteBlock1_End())
                            ||
                            (single_setting.Index > GetProtectedWriteBlock2_Start() && single_setting.Index < GetProtectedWriteBlock2_End())
                            ||
                            (single_setting.Index > GetProtectedWriteBlock3_Start() && single_setting.Index < GetProtectedWriteBlock3_End())
                        )
                        {
                            //Console.WriteLine("Skipping " + (object)single_setting.Index + " because it is protected");
                            continue;
                        }
                        

                        if (_PIC_Conn.sendAckdCommand(GetCommand_RTS()))
                        {
                            if (_PIC_Conn.SetDSPValue(single_setting.Index, single_setting.Value))
                            {
                                if (!_PIC_Conn.verifyLastCommand())
                                {
                                    Console.WriteLine("Unable to verifyLastCommand at index " + single_setting.Index);
                                }
                                else
                                {
                                    //Console.WriteLine("Successfully SetDSPValue at index " + single_setting.Index + " to " + single_setting.Value.ToString("X8"));
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unable to SetDSPValue at index " + single_setting.Index + " to " + single_setting.Value.ToString("X8"));
                            }

                            backgroundWorker.ReportProgress(Math.Min(100,(int)((total_settings_written / num_settings)*100)));
                        }
                        else
                        {
                            Console.WriteLine("Unable to GetCommand_RTS at index " + (object)single_setting.Index);
                            return;
                        }
                    }

                    for (int phantom_index = 0; phantom_index < GetNumPhantomPowerChannels(); phantom_index++)
                    {
                        if (!_PIC_Conn.sendAckdData(GetCommand_SetPhantomPower(), (byte)phantom_index, 100, Convert.ToByte(PROGRAMS[program_index].inputs[phantom_index].PhantomPower)))
                        {
                            // error
                        }
                    }

                     
                    int num_retries = 5;

                    for (int input_index = 0; input_index < this.GetNumInputChannels(); input_index++)
                    {
                        
                        int retry_counter = 0;
                        while (retry_counter < num_retries)
                        {
                            if (_PIC_Conn.SendChannelName(input_index + 1, PROGRAMS[program_index].inputs[input_index].Name))
                            {
                                break;
                            }

                            retry_counter++;
                        }
                    }

                    for (int output_index = 0; output_index < this.GetNumOutputChannels(); output_index++)
                    {

                        
                        int retry_counter = 0;
                        while (retry_counter < num_retries)
                        {
                            if (_PIC_Conn.SendChannelName(output_index + 1, PROGRAMS[program_index].inputs[output_index].Name, true))
                            {
                                break;
                            }

                            retry_counter++;
                        }
                    }


                    if (program_index < (programs_to_run - 1))
                    {
                        backgroundWorker.ReportProgress(0, "Switching to Program " + (program_index + 2));

                        if (_PIC_Conn.sendAckdCommand((byte)(GetProgrammingSwitchCommandBase() + program_index)))
                        {
                            Console.WriteLine("Successfully switched to program " + (program_index + 2));
                        }
                        else
                        {
                            Console.WriteLine("Unable to switch to program " + (program_index + 2));
                        }
                    }
                    
                }


                backgroundWorker.ReportProgress(0, "Switching back to Preset 1");

                if (!_PIC_Conn.sendAckdCommand(GetLiveSwitchCommandBase()))
                {
                    // error
                }

                backgroundWorker.ReportProgress(95);

                backgroundWorker.ReportProgress(0, "Rebooting device");

                if (!_PIC_Conn.sendAckdCommand(GetCommand_RebootDevice()))
                {
                    // error
                }

                backgroundWorker.ReportProgress(0, "Save complete");
                backgroundWorker.ReportProgress(100);
            }
            catch (Exception ex)
            {
            }
                 * */
        }

        #endregion


        #region Virtual Methods

        public virtual void DefaultSettings()
        {
        }
        
        
        public virtual int GetNumInputChannels()
        {
            return 0;
        }

        public virtual int GetNumOutputChannels()
        {
            return 0;
        }

        public virtual int GetNumPhantomPowerChannels()
        {
            return 0;
        }

        public virtual int GetDeviceID()
        {
            return 0;
        }

        public virtual Image GetDeviceThumbnail()
        {
            return (Image)null;
        }

        public virtual string GetDeviceName()
        {
            return (string)null;
        }

        public virtual DeviceType GetDeviceType()
        {
            return DeviceType.Unknown;
        }

        public virtual bool IsAmplifier()
        {
            return false;
        }

        public virtual byte GetLiveSwitchCommandBase()
        {
            return (byte)16;
        }

        public virtual byte GetProgrammingSwitchCommandBase()
        {
            return (byte)0x20;
        }

        public virtual byte GetEEPROMSwitchCommandBase()
        {
            return (byte)48;
        }

        public virtual byte GetCommand_RTS()
        {
            return (byte)1;
        }

        public virtual byte GetCommand_RebootDevice()
        {
            return (byte)7;
        }

        public virtual byte GetCommand_SetPhantomPower()
        {
            return (byte)9;
        }

        public virtual void LoadSettingsToProgramConfig()
        {
        }

        public virtual void LoadProgramConfigToSettings()
        {
        }

        public virtual void SetConnectionPicture(Image connectionImage)
        {

        }

        public virtual int NumPresets()
        {
            return 3;
        }

        

        #endregion


        #region Program Undo Changes


        public void CacheCurrentProgram()
        {
            //PROGRAM_CACHE = (ProgramConfig)PROGRAMS[CURRENT_PROGRAM].Clone();
        }

        public void RestoreProgramCache()
        {
            //PROGRAMS[CURRENT_PROGRAM] = (ProgramConfig)PROGRAM_CACHE.Clone(); ;
            //UpdateTooltips();
        }

        #endregion


        #region UnsavedChangesHandler

        protected void SetUnsavedChanges(object sender, EventArgs e)
        {
            this.UnsavedChanges = true;
        }

        #endregion


        #region AttachUIEvents

        public IEnumerable<Control> GetControlsOfType(Control control, System.Type type)
        {
            IEnumerable<Control> enumerable = Enumerable.Cast<Control>((IEnumerable)control.Controls);
            return Enumerable.Where<Control>(Enumerable.Concat<Control>(Enumerable.SelectMany<Control, Control>(enumerable, (Func<Control, IEnumerable<Control>>)(ctrl => this.GetControlsOfType(ctrl, type))), enumerable), (Func<Control, bool>)(c => c.GetType() == type));
        }

       

        protected virtual void AttachUIEvents()
        {
            
            foreach (Control control in GetControlsOfType(this, typeof(PictureButton)))
            {
                control.Click += new EventHandler(this.SetUnsavedChanges);
            }

            PictureButton pbtnReadDevice = (PictureButton)Enumerable.FirstOrDefault(Controls.Find("pbtnReadDevice", true));

            if (pbtnReadDevice != null)
            {
                pbtnReadDevice.Click += new EventHandler(ReadDevice_Event);
            }

            PictureButton pbtnLoadConfiguration = (PictureButton)Enumerable.FirstOrDefault(Controls.Find("pbtnLoadConfiguration", true));
            if (pbtnLoadConfiguration != null)
            {
                pbtnLoadConfiguration.Click += new EventHandler(ReadSCFG_Event);
            }

            PictureButton pbtnSaveConfiguration = (PictureButton)Enumerable.FirstOrDefault(Controls.Find("pbtnSaveConfiguration", true));
            if (pbtnSaveConfiguration != null)
            {
                pbtnSaveConfiguration.Click += new EventHandler(this.WriteSCFG_Event);
            }

            Button btnConnectToDevice = (Button)Enumerable.FirstOrDefault(Controls.Find("btnConnectToDevice", true));

            if (btnConnectToDevice != null)
            {
                btnConnectToDevice.Click += new EventHandler(this.Connect_Event);
            }

            ComboBox dropProgramSelection = (ComboBox) Enumerable.FirstOrDefault(Controls.Find("dropProgramSelection", true));

            if(dropProgramSelection != null)
            {
                dropProgramSelection.SelectedIndexChanged += new EventHandler(this.ChangeProgram_Event);
            }


            PictureButton btnMatrixMixer = (PictureButton)(Controls.Find("btnMatrixMixer", true).FirstOrDefault());

            if (btnMatrixMixer != null)
            {
                btnMatrixMixer.MouseClick += new MouseEventHandler(this.btnMatrixMixer_MouseClick);
            }

            PictureButton pbtnDucker = (PictureButton)Enumerable.FirstOrDefault(Controls.Find("pbtnDucker", true));

            if (pbtnDucker != null)
            {
                pbtnDucker.MouseClick += new MouseEventHandler(pbtnDucker_MouseClick);
            }

        }

        #endregion


        #region Update UI Actions (Tooltips and Connect Button)

        public void UpdatePresetDropdown()
        {
            dropProgramSelection.Items.Clear();
            foreach (string singlePresetName in _presetNames)
            {
                dropProgramSelection.Items.Add(singlePresetName);
            }

            if (dropProgramSelection.Items.Count > 0)
            {
                dropProgramSelection.SelectedIndex = 0;
            }
            dropProgramSelection.Invalidate();
        }
        
        public void UpdateTooltips()
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 50;
            toolTip1.ShowAlways = true;

            // Must be called from somewhere inside SA_Resources. Why not here?
            if (!form_loaded)
            {
                this.SetConnectionPicture(GlobalResources.lblStatus_Disconnected);
            }


            PictureButton PrimitiveButton = null;
            Label PrimitiveLabel = null;

            DSP_Primitive_Compressor RecastCompressor;
            DSP_Primitive_Delay RecastDelay;
            DSP_Primitive_Input RecastInput;
            DSP_Primitive_StandardGain RecastStandardGain;

            foreach (DSP_Primitive SinglePrimitive in DSP_PROGRAMS[0].PRIMITIVES)
            {
                switch(SinglePrimitive.Type)
                {
                    case DSP_Primitive_Types.Compressor: 
    
                        RecastCompressor = (DSP_Primitive_Compressor) SinglePrimitive;

                        PrimitiveButton = ((PictureButton)Controls.Find("btnCH" + (RecastCompressor.Channel + 1) + "Compressor", true).FirstOrDefault());

                        if(PrimitiveButton != null)
                        {
                            PrimitiveButton.Overlay1Visible = (RecastCompressor.Bypassed);
                            PrimitiveButton.Invalidate();
                        }

                    break;

                    case DSP_Primitive_Types.Delay:

                    RecastDelay = (DSP_Primitive_Delay)SinglePrimitive;

                    PrimitiveButton = ((PictureButton)Controls.Find("btnCH" + (RecastDelay.Channel + 1) + "Delay", true).FirstOrDefault());

                    if (PrimitiveButton != null)
                    {
                        PrimitiveButton.Overlay1Visible = (RecastDelay.Bypassed);
                        PrimitiveButton.Invalidate();

                        toolTip1.SetToolTip(PrimitiveButton, RecastDelay.ToString());

                    }

                    break;

                    case DSP_Primitive_Types.Input:

                    RecastInput = (DSP_Primitive_Input)SinglePrimitive;

                    PrimitiveLabel = ((Label)Controls.Find("lblCH" + (RecastInput.Channel + 1) + "Input", true).FirstOrDefault());

                    if (PrimitiveLabel != null)
                    {
                        PrimitiveLabel.Text = RecastInput.Name;

                        toolTip1.SetToolTip(PrimitiveLabel, RecastInput.ToString());

                    }

                    break;

                    case DSP_Primitive_Types.StandardGain:

                    RecastStandardGain = (DSP_Primitive_StandardGain)SinglePrimitive;

                    PrimitiveButton = ((PictureButton)Controls.Find("btnGain" + (RecastStandardGain.Channel) + (RecastStandardGain.PositionA), true).FirstOrDefault());

                    if (PrimitiveButton != null)
                    {
                        PrimitiveButton.Overlay2Visible = (RecastStandardGain.Muted);
                        PrimitiveButton.Invalidate();

                        toolTip1.SetToolTip(PrimitiveButton, RecastStandardGain.ToString());

                    }

                    break;


                    default :
                        
                    break;
                }

            }

            return;

            PictureButton pbtnDucker = ((PictureButton)Controls.Find("pbtnDucker", true).FirstOrDefault());

            if (pbtnDucker != null)
            {
                pbtnDucker.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].ducker.Bypassed;
                pbtnDucker.Invalidate();
            }


            for (int i = 0; i < GetNumInputChannels(); i++)
            {

                PictureButton btn_PreGain = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PreGain", true).FirstOrDefault());
                PictureButton btn_PreGain2 = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PreGain2", true).FirstOrDefault());
                PictureButton btn_Compressor = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "Compressor", true).FirstOrDefault());

                Label inputLabel = (Label)(Controls.Find("lblCH" + (i + 1) + "Input", true).FirstOrDefault());

                if (inputLabel != null)
                {
                    inputLabel.Text = PROGRAMS[CURRENT_PROGRAM].inputs[i].Name;
                    inputLabel.Invalidate();
                    toolTip1.SetToolTip(inputLabel, PROGRAMS[CURRENT_PROGRAM].inputs[i].ToString());
                }

                if (btn_PreGain != null)
                {
                    toolTip1.SetToolTip(btn_PreGain, PROGRAMS[CURRENT_PROGRAM].gains[i][0].ToString());
                    btn_PreGain.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][0].Muted;
                    btn_PreGain.Invalidate();
                }

                if (btn_Compressor != null)
                {
                    if (DSP_PROGRAMS[0].LookupIndex(DSP_Primitive_Types.Compressor, i, 0) > 0)
                    {
                        btn_Compressor.Overlay1Visible = ((DSP_Primitive_Compressor)DSP_PROGRAMS[0].LookupPrimitive(DSP_Primitive_Types.Compressor, i, 0)).Bypassed;
                        btn_Compressor.Invalidate();
                    }
                }

                if (btn_PreGain2 != null)
                {
                    toolTip1.SetToolTip(btn_PreGain2, PROGRAMS[CURRENT_PROGRAM].gains[i][1].ToString());
                    btn_PreGain2.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][1].Muted;
                    btn_PreGain2.Invalidate();
                }
            }

            for (int i = 0; i < GetNumOutputChannels(); i++)
            {

                PictureButton btn_PostTrim = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PostTrim", true).FirstOrDefault());
                PictureButton btn_Limiter = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "Limiter", true).FirstOrDefault());
                PictureButton btn_Delay = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "Delay", true).FirstOrDefault());
                PictureButton btn_PostGain = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PostGain", true).FirstOrDefault());

                Label outputLabel = (Label)(Controls.Find("lblCH" + (i + 1) + "Output", true).FirstOrDefault());

                if (btn_PostTrim != null)
                {
                    toolTip1.SetToolTip(btn_PostTrim, PROGRAMS[CURRENT_PROGRAM].gains[i][2].ToString());
                    btn_PostTrim.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][2].Muted;
                    btn_PostTrim.Invalidate();
                }

                if (btn_Limiter != null)
                {
                    btn_Limiter.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[i][1].Bypassed;
                    btn_Limiter.Invalidate();
                }

                if (btn_Delay != null)
                {
                    toolTip1.SetToolTip(btn_Delay, (PROGRAMS[CURRENT_PROGRAM].delays[i].Delay * 1000).ToString("N1") + "ms");
                    btn_Delay.Invalidate();
                }

                if (btn_PostGain != null)
                {
                    toolTip1.SetToolTip(btn_PostGain, PROGRAMS[CURRENT_PROGRAM].gains[i][3].ToString());
                    btn_PostGain.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][3].Muted;
                    btn_PostGain.Invalidate();
                }

                if (outputLabel != null)
                {
                    outputLabel.Text = PROGRAMS[CURRENT_PROGRAM].outputs[i].Name;
                    outputLabel.Invalidate();
                }

            }

        }

        public void SetConnectButtonText(string in_text)
        {
            btnConnectToDevice.Text = in_text;
            btnConnectToDevice.Invalidate();
        }

        #endregion


        #region UI Block Actions

        
        public void lblInput_MouseClick(object sender, MouseEventArgs e)
        {
            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1)) - 1;

            DSP_Primitive_Input Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[0].LookupIndex(DSP_Primitive_Types.Input, ch_num, 0);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Input at CH=" + ch_num + " and POS = " + 0);
                return;
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Input)DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex];

            }

            DSP_Primitive_Input Cached_Primitive = (DSP_Primitive_Input)Active_Primitive.Clone();

            using (InputConfiguration inputForm = new InputConfiguration(this,Active_Primitive))
            {
                
                if (!LIVE_MODE)
                {
                    inputForm.Width = 276;

                }
                else
                {
                    inputForm.Width = 320;
                }

                inputForm.Height = 221;
                

                DialogResult showBlock = inputForm.ShowDialog(this);

                if(showBlock == DialogResult.Cancel)
                {
                    if (showBlock == DialogResult.Cancel)
                    {
                        if (!Active_Primitive.Equals(Cached_Primitive))
                        {
                            if (LIVE_MODE)
                            {
                                Cached_Primitive.QueueDeltas(this, Active_Primitive);
                            }

                            DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Input)Cached_Primitive.Clone();
                        }
                    }
                    else
                    {
                        UpdateTooltips();
                    }
                }
                else
                {
                    /*
                   if (LIVE_MODE && (PROGRAMS[CURRENT_PROGRAM].inputs[ch_num - 1].Name != cached_input.Name))
                   {
                       // Check if this has changed
                       // Don't update this until we hit save since it takes so long.
                       AddItemToQueue(new LiveQueueItem(ch_num, PROGRAMS[CURRENT_PROGRAM].inputs[ch_num - 1].Name));
                   }
                   * */
                    
                    UpdateTooltips(); 
                     
                }
            }
        }

        protected void btnPreGain1_MouseClick(object sender, MouseEventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            int settings_index = (0 + ch_num - 1); 
            
            GainConfig cached_gain = (GainConfig)PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].Clone();
            
            if(e.Button == MouseButtons.Right)
            {
                TempConfig = cached_gain;

                temp_from_ch = ch_num;
                temp_from_index = 0;
                temp_from_preset = CURRENT_PROGRAM;

                ShowCopyMenu(sender);
                return;
            }
            /*

            using (GainForm gainForm = new GainForm(this, ch_num - 1, 0, settings_index, false, "CH" + ch_num.ToString() + " - Input Gain"))
            {

                if (!LIVE_MODE)
                {
                    gainForm.Width = 132;
                }
                else
                {
                    gainForm.Width = 187;
                }

                gainForm.Height = 414;

                DialogResult showBlock = gainForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (!PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].Equals(cached_gain))
                    {
                        PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0] = (GainConfig) cached_gain.Clone();
                        PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].QueueChange(this, settings_index, true, ch_num - 1);

                        if (LIVE_MODE && FIRMWARE_VERSION > 2.5)
                        {

                            UInt32 new_input_gain = DSP_Math.double_to_MN(PROGRAMS[CURRENT_PROGRAM].pregains[ch_num - 1] +
                                                                          PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].Gain, 9, 23);

                            AddItemToQueue(new LiveQueueItem((0 + ch_num - 1), new_input_gain));
                        }
                    }

                }
                else
                {
                    UpdateTooltips();
                }
            }
             * */
        }


        protected void btnPreFilters_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            int settings_index, plainfilter_index;

            FilterConfig[] cached_filters = new FilterConfig[3];

            for (int i = 0; i < 3; i ++)
            {
                if (PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i] != null)
                {
                    if (PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i] != null)
                    {
                        cached_filters[i] = (FilterConfig) PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i].Clone();
                    }
                }
            }

            
            using (FilterForm filterForm = new FilterForm(this, ch_num))
            {
                filterForm.Height = 500;

                DialogResult showBlock = filterForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        if (cached_filters[i] != null)
                        {
                            settings_index = (40) + ((ch_num - 1) * 45) + (i * 5);
                            plainfilter_index = (300) + ((ch_num - 1) * 27) + (i * 3);

                            PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i] = (FilterConfig) cached_filters[i].Clone();

                            if(LIVE_MODE)
                            {
                                PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i].QueueChange(this, settings_index, plainfilter_index, ch_num);
                            }
                        }
                    }

                }
                else
                {
                    UpdateTooltips();
                }
            }
        }

        protected void pbtnDucker_MouseClick(object sender, MouseEventArgs e)
        {

            if(e.Button == MouseButtons.Right)
            {
                return;
            }

            DuckerConfig cached_ducker = (DuckerConfig)PROGRAMS[CURRENT_PROGRAM].ducker.Clone();

            int settings_offset = 272;

            using (DuckerForm duckerForm = new DuckerForm(this, settings_offset))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                DialogResult showBlock = duckerForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (!PROGRAMS[CURRENT_PROGRAM].ducker.Equals(cached_ducker))
                    {
                        PROGRAMS[CURRENT_PROGRAM].ducker = (DuckerConfig)cached_ducker.Clone();


                        if (LIVE_MODE)
                        {
                            PROGRAMS[CURRENT_PROGRAM].ducker.QueueChange(this, settings_offset);
                        }
                    }
                }
                else
                {
                    UpdateTooltips();
                }
            }
        }

        public void btnComp_MouseClick(object sender, MouseEventArgs e)
        {
            DSP_Primitive_Compressor Active_Primitive;

            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(13, 1));
            int prim_position = int.Parse(((PictureButton)sender).Name.Substring(14, 1));

            int PrimitiveIndex = DSP_PROGRAMS[0].LookupIndex(DSP_Primitive_Types.Compressor, ch_num, prim_position);

            if (PrimitiveIndex < 0)
            {
                // Couldn't find a compressor, let's see if we have a limiter there instead
                PrimitiveIndex = DSP_PROGRAMS[0].LookupIndex(DSP_Primitive_Types.Limiter, ch_num, prim_position);
            }

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Compressor at CH=" + ch_num + " and POS = " + 0);
                return;
            } else
            {
                Active_Primitive = (DSP_Primitive_Compressor)DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex];

            }
            
            DSP_Primitive_Compressor Cached_Primitive = (DSP_Primitive_Compressor)Active_Primitive.Clone();

            if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Right-click not yet implemented");
                /*
                TempConfig = cached_comp;

                temp_from_ch = ch_num;
                temp_from_index = 0;
                temp_from_preset = CURRENT_PROGRAM;

                ShowCopyMenu(sender);
                return;
                 * */
            }

            using (CompressorForm compressorForm = new CompressorForm(this, Active_Primitive))
            {
                DialogResult showBlock = compressorForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (!Active_Primitive.Equals(Cached_Primitive))
                    {
                        if (LIVE_MODE)
                        {
                            Cached_Primitive.QueueDeltas(this, Active_Primitive);
                        }

                        DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Compressor)Cached_Primitive.Clone();
                    }
                }
                else
                {
                    UpdateTooltips();
                }
            }
        }


        protected void btnMatrixMixer_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            GainConfig[][] crosspoint_cache = new GainConfig[6][];

            for (int i = 0; i < 6; i++)
            {
                crosspoint_cache[i] = new GainConfig[4];

                for(int j = 0; j < 4; j++)
                {
                    crosspoint_cache[i][j] = (GainConfig) PROGRAMS[CURRENT_PROGRAM].crosspoints[i][j].Clone();
                }
            }

                using (MixerForm6x4 mixerForm6X4 = new MixerForm6x4(this))
                {

                    if (LIVE_MODE)
                    {
                        mixerForm6X4.Width = 496;
                    }
                    else
                    {
                        mixerForm6X4.Width = 228;
                    }

                    DialogResult showBlock = mixerForm6X4.ShowDialog(this);

                    if (showBlock == DialogResult.Cancel)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                PROGRAMS[CURRENT_PROGRAM].crosspoints[i][j] = (GainConfig)crosspoint_cache[i][j].Clone();
                            }
                        }

                    }
                }
        }



        protected void btnPostFilters_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            int settings_index, plainfilter_index;
            FilterConfig[] cached_filters = new FilterConfig[6];

            for (int i = 3; i < 9; i++)
            {
                if (PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i] != null)
                {
                    cached_filters[i-3] = (FilterConfig)PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i].Clone();
                }
            }

            using (FilterForm filterForm = new FilterForm(this, ch_num, true))
            {
                DialogResult showBlock = filterForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {

                    for (int i = 3; i < 9; i++)
                    {
                        settings_index = (40) + ((ch_num - 1) * 45) + (i * 5);
                        plainfilter_index = (300) + ((ch_num - 1) * 27) + (i * 3);

                        if (cached_filters[i - 3] != null)
                        {
                            PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i] = (FilterConfig) cached_filters[i - 3].Clone();
                            if (LIVE_MODE)
                            {
                                PROGRAMS[CURRENT_PROGRAM].filters[ch_num - 1][i].QueueChange(this, settings_index, plainfilter_index, ch_num);
                            }
                        } else
                        {
                            if (LIVE_MODE)
                            {
                                // Filter doesn't exist... SO... let's just send a blank one
                                new FilterConfig(FilterType.None, true).QueueChange(this, settings_index, plainfilter_index, ch_num);
                            }
                        }
                    }

                }
                else
                {
                    UpdateTooltips();
                }

            }
        }


        public void btnDelay_MouseClick(object sender, MouseEventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1)) - 1;

            DSP_Primitive_Delay Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[0].LookupIndex(DSP_Primitive_Types.Delay, ch_num, 0);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Delay at CH=" + ch_num + " and POS = " + 0);
                return;
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Delay)DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex];

            }

            DSP_Primitive_Delay Cached_Primitive = (DSP_Primitive_Delay)Active_Primitive.Clone();


            if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Right-click not yet implemented");
                return;
                /*TempConfig = cached_delay;

                temp_from_ch = ch_num;
                temp_from_index = 0;
                temp_from_preset = CURRENT_PROGRAM;

                ShowCopyMenu(sender);
                return;
                 * */
            }
            using (DelayForm delayForm = new DelayForm(this, Active_Primitive))
            {

                DialogResult showBlock = delayForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (!Active_Primitive.Equals(Cached_Primitive))
                    {
                        if (LIVE_MODE)
                        {
                            Cached_Primitive.QueueDeltas(this, Active_Primitive);
                        }

                        DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Delay)Cached_Primitive.Clone();
                    }
                }
                else
                {
                    UpdateTooltips();
                }

            }
        }


        public void btnStandardGain_MouseClick(object sender, MouseEventArgs e)
        {
            int ch_num = int.Parse(((PictureButton) sender).Name.Substring(7, 1));
            int pos = int.Parse(((PictureButton)sender).Name.Substring(8, 1));

            DSP_Primitive_StandardGain Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[0].LookupIndex(DSP_Primitive_Types.StandardGain, ch_num, pos);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate StandardGain at CH = " + ch_num + " and POS = " + 0);
            }
            else
            {
                Active_Primitive = (DSP_Primitive_StandardGain)DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex];
            }

            DSP_Primitive_StandardGain Cached_Primitive = (DSP_Primitive_StandardGain)Active_Primitive.Clone();


            if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Right-click not yet implemented");
                return;
                /*TempConfig = cached_delay;

                temp_from_ch = ch_num;
                temp_from_index = 0;
                temp_from_preset = CURRENT_PROGRAM;

                ShowCopyMenu(sender);
                return;
                 * */
            }

            using (GainForm gainForm = new GainForm(this, Active_Primitive, DSP_Primitive_Types.StandardGain))
            {

                gainForm.Width = LIVE_MODE ? 187 : 132;
                gainForm.Height = 414;

                DialogResult showBlock = gainForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (!Active_Primitive.Equals(Cached_Primitive))
                    {
                        if (LIVE_MODE)
                        {
                            Cached_Primitive.QueueDeltas(this, Active_Primitive);
                        }

                        DSP_PROGRAMS[0].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_StandardGain)Cached_Primitive.Clone();
                    }
                }
                else
                {
                    UpdateTooltips();
                }
            }
        }


        protected void lblOutput_MouseClick(object sender, MouseEventArgs e)
        {
            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1));

            OutputConfig cached_output = (OutputConfig)PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1].Clone();

            if (e.Button == MouseButtons.Right)
            {
                temp_from_ch = ch_num;
                temp_from_index = 0;
                temp_from_preset = CURRENT_PROGRAM;

                TempConfig = cached_output;
                ShowCopyMenu(sender);
                return;
            }

            using (OutputConfiguration outputForm = new OutputConfiguration(this, ch_num))
            {

                DialogResult showBlock = outputForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (!PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1].Equals(cached_output))
                    {
                        PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1] = (OutputConfig)cached_output.Clone();

                        if (LIVE_MODE)
                        {
                            AddItemToQueue(new LiveQueueItem(ch_num + GetNumInputChannels(), PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1].Name));
                        }
                    }
                }
                else
                {
                    if (!PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1].Equals(cached_output))
                    {
                        if (LIVE_MODE)
                        {
                            AddItemToQueue(new LiveQueueItem(ch_num + GetNumInputChannels(), PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1].Name));
                        }
                    }

                    UpdateTooltips();
                }
            }
        }

        #endregion


        #region UI Menu Events

        public void ReadSCFG_Event(object sender, EventArgs e)
        {
            try
            {
                if (this.openProgramDialog.ShowDialog() != DialogResult.OK)
                    return;
                SCFG_Manager.Read(this.openProgramDialog.FileName, this);
                this.LoadSettingsToProgramConfig();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void WriteSCFG_Event(object sender, EventArgs e)
        {
            try
            {
                if (this.saveProgramDialog.ShowDialog() != DialogResult.OK)
                    return;
                this.LoadProgramConfigToSettings();
                SCFG_Manager.Write(this.saveProgramDialog.FileName, this);
                this.UnsavedChanges = false;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Unable to save program file. Message: " + ex.Message, "Save Program Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ReadDevice_Event(object sender, EventArgs e)
        {
            ReadForm readForm = new ReadForm(this);
            readForm.Height = 84;
            int num = (int)readForm.ShowDialog();
            this.LoadSettingsToProgramConfig();
            this.UpdateTooltips();
        }

        public void WriteDevice_Event(object sender, EventArgs e)
        {
        }

        public void Connect_Event(object sender, EventArgs e)
        {
            if (this.LIVE_MODE)
            {
                this.EndLiveMode();
            }
            else
            {
                using (DeviceManagerForm deviceManagerForm = new DeviceManagerForm(this))
                {
                    int num = (int)deviceManagerForm.ShowDialog((IWin32Window)this);
                }
            }
        }

        public void About_Event(object sender, EventArgs e)
        {
            // TODO - CHECK - WHY IS THIS EMPTY
            if (new AboutForm("About " + this.GetDeviceName() + " Plugin", "DSP Control Center - " + this.GetDeviceName() + " Plugin", Assembly.GetExecutingAssembly().GetName().Version, "").ShowDialog() != DialogResult.OK);
        }

        private void Close_Event(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ChangeProgram_Event(object sender, EventArgs e)
        {
            CURRENT_PROGRAM = ((ListControl)sender).SelectedIndex;
            UpdateTooltips();

            if (!LIVE_MODE)
            {
                return;
            }

            switch (new SwitchProgramForm(this, (byte)(GetLiveSwitchCommandBase() + (uint)CURRENT_PROGRAM)).ShowDialog())
            {
                case DialogResult.No:
                    Console.WriteLine("Unable to switch program. Switch command responded with an error.");
                    break;
                case DialogResult.Abort:
                    Console.WriteLine("Unable to switch program. No RTS");
                    break;
                case DialogResult.OK:
                    Console.WriteLine("Successfully switched program");
                    break;
            }
        }

        public void ResetInterface_Event(object sender, EventArgs e)
        {

            if(MessageBox.Show("Resetting to Default Settings will overwrite your current configuration. Proceed?","Overwrite Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            InitializePrograms();

            DefaultSettings();
            
            dropProgramSelection.SelectedIndex = 0;

            LoadSettingsToProgramConfig();

            UpdateTooltips();

        }

        public void FactoryReset_Event(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restoring to factory settings will take around 2 minutes.\n" +
                                "Do not power down the device during this process.\n" +
                                "The clip lights will indicate restore progress.\n" +
                                "Proceed?", "Restore Factory Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            using (RestoreSettingsForm restoreForm = new RestoreSettingsForm(this))
            {
                restoreForm.ShowDialog(this);
            }

        }
        
        #endregion

        #region UI ContextMenu Actions

        public CopyFormType CopyTypeFromControl(Control control)
        {
            if(control.Name.Contains("Input"))
            {
                return CopyFormType.InputConfiguration;
            }

            if(control.Name.Contains("Gain"))
            {
                return CopyFormType.Gain;
            }

            if(control.Name.Contains("PreFilter"))
            {
                return CopyFormType.Filter3;
            }

            if (control.Name.Contains("Compressor"))
            {
                return CopyFormType.Compressor;
            }

            if (control.Name.Contains("PostFilter"))
            {
                return CopyFormType.Filter6;
            }

            if (control.Name.Contains("Limiter"))
            {
                return CopyFormType.Limiter;
            }

            if (control.Name.Contains("Delay"))
            {
                return CopyFormType.Delay;
            }

            if (control.Name.Contains("Output"))
            {
                return CopyFormType.OutputConfiguration;
            }

            return CopyFormType.Unknown;
        }

        public void ShowCopyMenu(object sender)
        {

            return; // DISABLE THIS. NOT READY FOR RELEASE.

            activeBlockForMenu = (Control)sender;            

            if(!has_copy_config)
            {
                menuItem_Paste.Enabled = false;
            } else
            {
                if(CopyType != CopyTypeFromControl(activeBlockForMenu))
                {
                    menuItem_Paste.Enabled = false;
                } else
                {
                    menuItem_Paste.Enabled = true;
                }
            }

            menuBlockCopy.Show((Control)sender, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
        }

        public void ContextMenu_Copy(object sender, EventArgs e)
        {
            CopyType = CopyTypeFromControl(activeBlockForMenu);
            CopyConfig = TempConfig;
            
            copy_from_preset = temp_from_preset;
            copy_from_index = temp_from_index;
            has_copy_config = true;

        }

        public void ContextMenu_Cut(object sender, EventArgs e)
        {


        }

        public void ContextMenu_Paste(object sender, EventArgs e)
        {
            // We have already checked that we're good to copy by showing/hiding the Paste menuitem

            switch(CopyType)
            {
                case CopyFormType.Gain:
                    PROGRAMS[CURRENT_PROGRAM].gains[temp_from_ch - 1][temp_from_index] = (GainConfig)((GainConfig)CopyConfig).Clone();
                break;

                case CopyFormType.InputConfiguration:
                    PROGRAMS[CURRENT_PROGRAM].inputs[temp_from_ch - 1] = (InputConfig)((InputConfig)CopyConfig).Clone();
                break;

                case CopyFormType.Compressor:
                    PROGRAMS[CURRENT_PROGRAM].compressors[temp_from_ch - 1][0] = (CompressorConfig)((CompressorConfig)CopyConfig).Clone();
                break;

                case CopyFormType.Limiter:
                    PROGRAMS[CURRENT_PROGRAM].compressors[temp_from_ch - 1][1] = (CompressorConfig)((CompressorConfig)CopyConfig).Clone();
                break;

                case CopyFormType.Delay:
                    PROGRAMS[CURRENT_PROGRAM].delays[temp_from_ch - 1] = (DelayConfig)((DelayConfig)CopyConfig).Clone();
                break;

                case CopyFormType.OutputConfiguration:
                    PROGRAMS[CURRENT_PROGRAM].outputs[temp_from_ch - 1] = (OutputConfig)((OutputConfig)CopyConfig).Clone();
                break;

                default :
                    // Do nothing. How did we get here?

                    break;

            }

            UpdateTooltips();

        }

        #endregion

        #region Form Actions




        private void MainForm_Template_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (_PIC_Conn != null)
            {
                if (_PIC_Conn.isOpen)
                {
                    _PIC_Conn.Close();
                }
            }

            

            if (!this.UnsavedChanges)
            {
                return;
            }

            switch (MessageBox.Show("Would you like to save your current configuration before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    this.WriteSCFG_Event(sender, (EventArgs)e);
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        #endregion

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("DSP Control Center Manual.pdf");
            } catch (Exception ex)
            {
                
            }
        }

        private void dropProgramSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void presetManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PresetManager presetForm = new PresetManager(this))
            {


                DialogResult showBlock = presetForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                }
                else
                {
                }
            }
        }


    }

    #region Toolstrip Custom Renderer Class

    public class MyRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            //Console.WriteLine(e.Item.AccessibilityObject.ToString());
            if (!e.Item.Selected && !e.Item.IsOnDropDown)
            {
                // Unselected top level item
                if(e.Item.Pressed)
                {
                    e.Item.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    e.Item.ForeColor = Color.Gainsboro;
                }
                
                base.OnRenderMenuItemBackground(e);
            }
            else if (!e.Item.Selected)
            {
                // Unselected dropdown or overflow
                base.OnRenderMenuItemBackground(e);
            }
            else if(!e.Item.IsOnDropDown)
            {
                // Selected top menu item
                e.Item.ForeColor = Color.FromArgb(64,64,64);
                
                Rectangle rc = new Rectangle(1,1, e.Item.Size.Width-2,e.Item.Size.Height-2);
                e.Graphics.FillRectangle(Brushes.LightGray, rc);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(100, 128, 128, 128)), 0, 0, e.Item.Size.Width - 1, e.Item.Size.Height);
            }
            else
            {
                // Selected dropdown or overflow item
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(Brushes.LightGray, rc);
            }
        }
    }

    #endregion

}