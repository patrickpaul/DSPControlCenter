using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
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

        public DSP_Program_Manager[] DSP_PROGRAMS = new DSP_Program_Manager[20];

        public DSP_Meter_Manager DSP_METER_MANAGER = new DSP_Meter_Manager();

        public int CURRENT_PROGRAM = 0;

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

        public SADevice activeDevice;

        public double FIRMWARE_VERSION;

        public Control activeBlockForMenu;

        public int AmplifierMode;
        public int ADC_CALIBRATION_MIN = 30;
        public int ADC_CALIBRATION_MAX = 240;
        public bool SLEEP_ENABLE;
        public int SLEEP_SECONDS = 60;


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

        //public CopyFormType CopyType;
        //public CopyFormType CutType;

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
                _PIC_Conn = new PIC_Bridge(serialPort);

                // Initialize Array of Presets (both Programs and Primitives for now)
                InitializePrograms();
                Initialize_DSP_Programs();

                //TODO - REMOVE ME
                for (int i = 0; i < this.GetNumPresets(); i++)
                {
                    DSP_PROGRAMS[i] = new DSP_Program_Manager(i,"Preset " + i);
                }

                if (this.GetNumPresets() < 2)
                {
                    pbPresetSelection.Visible = false;
                    dropProgramSelection.Visible = false;
                }
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

        

        public virtual void Single_Default_DSP_Program(int program_index = 0)
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


            this.pbtn_Meters.Visible = true;

            if (this.IsAmplifier())
            {
                this.pbtnSettings.Visible = true;
            }
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

            this.pbtn_Meters.Visible = false;
            if (this.IsAmplifier())
            {
                this.pbtnSettings.Visible = false;
            }
        }


        public void AddItemToQueue(LiveQueueItem itemToAdd)
        {
            // TODO - Disable this once done testing
            Console.WriteLine("[DEBUG] Adding item to queue: " + itemToAdd.Index + " - " + itemToAdd.Value.ToString("X8"));

            if (!LIVE_MODE)
            {
                return;
            }

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
                    Thread.Sleep(10);
                    lock (_locker)
                    {
                        if (UPDATE_QUEUE.Count > 0)
                        {
                            LiveQueueItem read_setting = (LiveQueueItem)UPDATE_QUEUE.Dequeue();

                            if (_PIC_Conn.getRTS())
                            {
                                if (read_setting.Index < 900 && read_setting.Value != 0xFFFFFFFF)
                                {
                                    if (_PIC_Conn.SetLiveDSPValue((uint)read_setting.Index, read_setting.Value))
                                    {
                                        //if (read_setting.Index > 500)
                                        //{
                                            Console.WriteLine("Successfully sent queued DSP setting: " + read_setting.Index + " - " + read_setting.Value.ToString("X8"));
                                        //}

                                        if (read_setting.Index == 566)
                                        {
                                            // Phantom power
                                            _PIC_Conn.UpdatePhantomPower();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR sending queued DSP Setting");
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


        #region Default/Read/Write Routines

        public void Default_DSP_Programs()
        {
            try
            {

                for (int i = 0; i < this.GetNumPresets(); i++)
                {
                    Single_Default_DSP_Program(i);
                }

                UpdateTooltips();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION in Default_DSP_Programs]: " + ex.Message);
            }
        }

        public virtual void ReadDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
        }

        public virtual void WriteDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                _PIC_Conn.sendAckdCommand(0x10);

                BackgroundWorker backgroundWorker = sender as BackgroundWorker;

                backgroundWorker.ReportProgress(0, "Putting device into programming mode");

                _PIC_Conn.FlushBuffer();

                UInt32[] Page_array = new UInt32[64];

                int overall_percantage = 0;
                double program_percentage = 0;

                backgroundWorker.ReportProgress(0, "Writing Programs...");

                for (int program_counter = 0; program_counter < this.GetNumPresets(); program_counter++)
                {
                    DSP_PROGRAMS[program_counter].Write_Program_To_Cache(this.GetNumInputChannels());

                    for (int page_counter = 0; page_counter < 12; page_counter++)
                    {
                        program_percentage = (((double)page_counter)/12.0)*10.0;

                        if (_PIC_Conn.InitiateWriteStream(program_counter, page_counter, 256))
                        {
                            Array.Copy(DSP_PROGRAMS[program_counter].WRITE_VALUE_CACHE, (page_counter*64), Page_array, 0, 64);

                            _PIC_Conn.SendStreamNibble(Page_array);
                            backgroundWorker.ReportProgress(overall_percantage + (int)program_percentage);
                            
                        }
                        else
                        {
                            Console.WriteLine("Unable to initiate stream!");
                        }
                    }

                    overall_percantage += 10;

                    backgroundWorker.ReportProgress(overall_percantage);
                }


                if (GetDeviceType() == DeviceType.FLX804)
                {
                    SetBridgeMode(AmplifierMode);
                    _PIC_Conn.SetAmplifierMode(AmplifierMode);
                }
                

                backgroundWorker.ReportProgress(0, "Soft Rebooting device");

                _PIC_Conn.SoftReboot();

                backgroundWorker.ReportProgress(0, "Save complete");
                backgroundWorker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in MainForm_Template.WriteDevice]: " + ex.Message);
            }
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

        public virtual DeviceFamily GetDeviceFamily()
        {
            return DeviceFamily.Unknown;
        }

        public virtual bool isBridgable()
        {
            return false;
        }

        public virtual int GetPermanentAmplifierMode()
        {
            return 0;
        }

        public virtual int GetDisplayOrder()
        {
            return 0;
        }


        public virtual string GetDefaultDeviceFile()
        {
            return "";
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

        public virtual int GetNumPresets()
        {
            return 10;
        }

        public virtual void SetBridgeMode(int BridgeMode)
        {
            
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
                btnMatrixMixer.MouseClick += new MouseEventHandler(this.pbtnMatrixMixer_MouseClick);
            }

        }

        #endregion


        #region Update UI Actions (Tooltips and Connect Button)

        public void UpdatePresetDropdown()
        {
            try
            {
                dropProgramSelection.Items.Clear();

                foreach (DSP_Program_Manager singleProgram in DSP_PROGRAMS)
                {
                    if (singleProgram != null)
                    {
                        dropProgramSelection.Items.Add(singleProgram.Name);
                    }
                    
                }

                if (dropProgramSelection.Items.Count > 0)
                {
                    dropProgramSelection.SelectedIndex = 0;
                }
                dropProgramSelection.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in UpdatePresetDropdown]: " + ex.Message);
            }
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
            DSP_Primitive_Output RecastOutput;
            DSP_Primitive_StandardGain RecastStandardGain;
            DSP_Primitive_Pregain RecastPregain; 
            DSP_Primitive_Ducker4x4 RecastDucker4x4;

            foreach (DSP_Primitive SinglePrimitive in DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES)
            {
                switch(SinglePrimitive.Type)
                {

                    case DSP_Primitive_Types.Ducker4x4:

                        RecastDucker4x4 = (DSP_Primitive_Ducker4x4)SinglePrimitive;

                        PrimitiveButton = ((PictureButton)Controls.Find("pbtnDucker", true).FirstOrDefault());

                        if (PrimitiveButton != null)
                        {
                            PrimitiveButton.Overlay1Visible = (RecastDucker4x4.Bypassed);
                            PrimitiveButton.Invalidate();
                        }

                    break;


                    case DSP_Primitive_Types.Compressor:
                    case DSP_Primitive_Types.Limiter: 
                        RecastCompressor = (DSP_Primitive_Compressor) SinglePrimitive;

                        PrimitiveButton = ((PictureButton)Controls.Find("btnCompressor" + RecastCompressor.Channel + "" + RecastCompressor.PositionA, true).FirstOrDefault());

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
                        PrimitiveLabel.Text = RecastInput.InputName;

                        if (RecastInput.PhantomPower)
                        {
                            PrimitiveLabel.BackColor = Color.ForestGreen;
                        }
                        else
                        {
                            PrimitiveLabel.BackColor = Color.FromArgb(40, 40, 40);
                        }
                        toolTip1.SetToolTip(PrimitiveLabel, RecastInput.ToString());

                    }

                    break;

                    case DSP_Primitive_Types.Output:

                    RecastOutput = (DSP_Primitive_Output)SinglePrimitive;

                    PrimitiveLabel = ((Label)Controls.Find("lblCH" + (RecastOutput.Channel + 1) + "Output", true).FirstOrDefault());

                    if (PrimitiveLabel != null)
                    {
                        PrimitiveLabel.Text = RecastOutput.OutputName;

                    }

                    break;

                    case DSP_Primitive_Types.Pregain:

                    RecastPregain = (DSP_Primitive_Pregain)SinglePrimitive;

                    PrimitiveButton = ((PictureButton)Controls.Find("btnGain" + (RecastPregain.Channel) + (RecastPregain.PositionA), true).FirstOrDefault());

                    if (PrimitiveButton != null)
                    {
                        PrimitiveButton.Overlay2Visible = (RecastPregain.Muted);
                        PrimitiveButton.Invalidate();

                        toolTip1.SetToolTip(PrimitiveButton, RecastPregain.ToString());

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

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1)) - 1;

            DSP_Primitive_Input Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Input, ch_num, 0);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Input at CH=" + ch_num + " and POS = " + 0);
                return;
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Input)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];

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

                            DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Input)Cached_Primitive.Clone();
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
                    Active_Primitive.QueueChange(this);
                    UpdateTooltips(); 
                     
                }
            }
        }


        public void pbtnMatrixMixer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            using (MixerForm6x4 mixerForm = new MixerForm6x4(this))
            {
                if (LIVE_MODE)
                {
                    mixerForm.Width = 499;
                }
                else
                {
                    mixerForm.Width = 228;
                }

                

                DialogResult showBlock = mixerForm.ShowDialog(this);
            }
        }

        public void lblOutput_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1)) - 1;

            DSP_Primitive_Output Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Output, ch_num, 0);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Output at CH=" + ch_num + " and POS = " + 0);
                return;
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Output)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];

            }

            DSP_Primitive_Output Cached_Primitive = (DSP_Primitive_Output)Active_Primitive.Clone();

            using (OutputConfiguration outputForm = new OutputConfiguration(this, Active_Primitive))
            {

                if (!LIVE_MODE)
                {
                    outputForm.Width = 276;

                }
                else
                {
                    outputForm.Width = 320;
                }

                outputForm.Height = 221;


                DialogResult showBlock = outputForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    if (showBlock == DialogResult.Cancel)
                    {
                        if (!Active_Primitive.Equals(Cached_Primitive))
                        {
                            if (LIVE_MODE)
                            {
                                Cached_Primitive.QueueDeltas(this, Active_Primitive);
                            }

                            DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Output)Cached_Primitive.Clone();
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
                    Active_Primitive.QueueChange(this);

                    UpdateTooltips();

                }
            }
        }

        
        public void pbtnDucker4x4_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            DSP_Primitive_Ducker4x4 Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Ducker4x4, 0, 0);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Ducker Primitive");
                return;
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Ducker4x4)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];

            }

            DSP_Primitive_Ducker4x4 Cached_Primitive = (DSP_Primitive_Ducker4x4)Active_Primitive.Clone();


            using (DuckerForm4x4 duckerForm = new DuckerForm4x4(this, Active_Primitive))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form

                if (LIVE_MODE)
                {
                    duckerForm.Width = 556;
                }
                else
                {
                    duckerForm.Width = 386;
                }

                DialogResult showBlock = duckerForm.ShowDialog(this);
                
                if (showBlock == DialogResult.Cancel)
                {
                    if (!Active_Primitive.Equals(Cached_Primitive))
                    {
                        if (LIVE_MODE)
                        {
                            Cached_Primitive.QueueDeltas(this, Active_Primitive);
                        }

                        DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Ducker4x4)Cached_Primitive.Clone();
                    }
                }

                UpdateTooltips();
            }
        }

        // btnFilter_MouseClick
        public void btnFilter_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            int primitive_offset = ((PictureButton)sender).Name.Contains("Pre") ? 0 : 3;
            int num_primitives = ((PictureButton)sender).Name.Contains("Pre") ? 3 : 6;



            using (FilterDesignerForm filterForm = new FilterDesignerForm(this, num_primitives, ch_num, primitive_offset))
            {
                DialogResult showBlock = filterForm.ShowDialog(this);

                if (showBlock == DialogResult.Cancel)
                {
                    //TODO - Handle Filter Cancels
                }
                else
                {
                    UpdateTooltips();
                }
            }
        }


        public void btnComp_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            DSP_Primitive_Compressor Active_Primitive;

            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(13, 1));
            int prim_position = int.Parse(((PictureButton)sender).Name.Substring(14, 1));

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Compressor, ch_num, prim_position);

            if (PrimitiveIndex < 0)
            {
                // Couldn't find a compressor, let's see if we have a limiter there instead
                PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Limiter, ch_num, prim_position);
            }

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Compressor at CH=" + ch_num + " and POS = " + 0);
                return;
            } else
            {
                Active_Primitive = (DSP_Primitive_Compressor)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];

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

                        DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Compressor)Cached_Primitive.Clone();
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
            if (e.Button == MouseButtons.Right)
            {
                return;
            } 
            
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1)) - 1;

            DSP_Primitive_Delay Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Delay, ch_num, 0);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Delay at CH=" + ch_num + " and POS = " + 0);
                return;
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Delay)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];

            }

            DSP_Primitive_Delay Cached_Primitive = (DSP_Primitive_Delay)Active_Primitive.Clone();


            
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

                        DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Delay)Cached_Primitive.Clone();
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

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((PictureButton) sender).Name.Substring(7, 1));
            int pos = int.Parse(((PictureButton)sender).Name.Substring(8, 1));

            DSP_Primitive_StandardGain Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.StandardGain, ch_num, pos);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate StandardGain at CH = " + ch_num + " and POS = " + 0);
            }
            else
            {
                Active_Primitive = (DSP_Primitive_StandardGain)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];
            }

            DSP_Primitive_StandardGain Cached_Primitive = (DSP_Primitive_StandardGain)Active_Primitive.Clone();


            

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

                        DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_StandardGain)Cached_Primitive.Clone();
                    }
                }
                else
                {
                    UpdateTooltips();
                }
            }
        }


        public void btnPregain_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(7, 1));
            int pos = int.Parse(((PictureButton)sender).Name.Substring(8, 1));

            DSP_Primitive_Pregain Active_Primitive;

            int PrimitiveIndex = DSP_PROGRAMS[CURRENT_PROGRAM].LookupIndex(DSP_Primitive_Types.Pregain, ch_num, pos);

            if (PrimitiveIndex < 0)
            {
                Active_Primitive = null;
                Console.WriteLine("[ERROR] Unable to locate Pregain at CH = " + ch_num + " and POS = " + 0);
            }
            else
            {
                Active_Primitive = (DSP_Primitive_Pregain)DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex];
            }

            DSP_Primitive_Pregain Cached_Primitive = (DSP_Primitive_Pregain)Active_Primitive.Clone();




            using (PregainForm gainForm = new PregainForm(this, Active_Primitive))
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

                        DSP_PROGRAMS[CURRENT_PROGRAM].PRIMITIVES[PrimitiveIndex] = (DSP_Primitive_Pregain)Cached_Primitive.Clone();
                    }
                }
                else
                {
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

                if (this.LIVE_MODE)
                {
                    LoadProgramFileForm loadForm = new LoadProgramFileForm(this.openProgramDialog.FileName, this);


                    loadForm.ShowDialog();
                }
                else
                {
                    SCFG_Manager.Read(this.openProgramDialog.FileName, this);
                    UpdateTooltips();
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Unable to load program file.\nMessage: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void WriteSCFG_Event(object sender, EventArgs e)
        {
            try
            {
                if (this.saveProgramDialog.ShowDialog() != DialogResult.OK)
                    return;
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
            new AboutForm("About " + this.GetDeviceName() + " Plugin", "DSP Control Center - " + this.GetDeviceName() + " Plugin", Assembly.GetExecutingAssembly().GetName().Version, "").ShowDialog();
        }

        private void Close_Event(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool disableProgramSwitch = false;

        public void ChangeProgram_AfterRead(int read_index)
        {
            disableProgramSwitch = true;
            dropProgramSelection.SelectedIndex = read_index;
            disableProgramSwitch = false;

            UpdateTooltips();

        }

        public void ChangeProgram_Event(object sender, EventArgs e)
        {
            CURRENT_PROGRAM = ((ListControl)sender).SelectedIndex;
            UpdateTooltips();


            if (!LIVE_MODE || disableProgramSwitch)
            {
                return;
            }

            
            switch (new SwitchProgramForm(this, CURRENT_PROGRAM).ShowDialog())
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
            if (MessageBox.Show("Resetting to Default Settings will overwrite your current configuration. Proceed?", "Overwrite Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (!File.Exists(this.GetDefaultDeviceFile()))
            {
                MessageBox.Show("Unable to locate default configuration file for this device", "Error Loading Default Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (this.LIVE_MODE)
                {
                    LoadProgramFileForm loadForm = new LoadProgramFileForm(this.GetDefaultDeviceFile(), this);


                    loadForm.ShowDialog();
                }
                else
                {
                    SCFG_Manager.Read(this.GetDefaultDeviceFile(), this);
                    UpdateTooltips();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            
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

        /*
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
        */
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
                Console.WriteLine("[Exception in MainForm_Template.viewHelpToolStripMenuItem_Click]: " + ex.Message);
            }
        }

        private void pbtn_Meters_Click(object sender, EventArgs e)
        {
            try
            {
                MeterViewForm mForm = new MeterViewForm(this);
                mForm.ShowDialog();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void pbtnSettings_Click(object sender, EventArgs e)
        {
            FLXConfigurationForm flxForm = new FLXConfigurationForm(this);
            flxForm.ShowDialog();  
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