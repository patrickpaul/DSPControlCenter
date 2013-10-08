using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SA_Resources.Forms
{
    public partial class MainForm_Template : Form
    {

        #region Variables

        public List<DSP_Setting>[] _settings = new List<DSP_Setting>[3];
        public List<DSP_Setting>[] _cached_settings = new List<DSP_Setting>[3];

        public List<UInt32>[] _gain_meters = new List<UInt32>[4];
        public List<UInt32> _mix_meters = new List<UInt32>(); 
        public List<UInt32>[] _comp_in_meters = new List<UInt32>[2];
        public List<UInt32>[] _comp_out_meters = new List<UInt32>[2];

        public ProgramConfig[] PROGRAMS = new ProgramConfig[3];

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



        // TODO - Move all DEVICE ID's to a global list
        public string DEVICE_NAME = "";
        public int DEVICE_ID = 0x20;
        public string SERIALNUM = "";

        public string CONFIGFILE = "";

        public bool UnsavedChanges = false;

        public bool LIVE_MODE;

        #endregion


        #region Constructor and InitializePrograms

        public MainForm_Template()
        {
            InitializeComponent();

            menuStrip1.Renderer = new MyRenderer();

        }


        public void InitializePrograms()
        {
            for (int p = 0; p < NUM_PROGRAMS; p++)
            {
                PROGRAMS[p] = new ProgramConfig();
            }

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

        }


        public void AddItemToQueue(LiveQueueItem itemToAdd)
        {

            if (!LIVE_MODE)
            {
                return;
            }

            Console.WriteLine("Added item to queue: " + itemToAdd.Index + " - " + itemToAdd.Value.ToString("X8"));
            lock (_locker)
            {
                UPDATE_QUEUE.Enqueue(itemToAdd);
            }
        }

        protected void Queue_Thread_DoWork(object sender, DoWorkEventArgs e)
        {
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
                                if (read_setting.Index < 1000)
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

                                    if (read_setting.Value == 0x01)
                                    {
                                        _PIC_Conn.SetLivePhantomPower((uint)read_setting.Index - 1000, 1);
                                    }
                                    else
                                    {
                                        _PIC_Conn.SetLivePhantomPower((uint)read_setting.Index - 1000, 0);
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
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            
            double num_settings = (_settings[0].Count - 
                (GetProtectedReadBlock1_End() - GetProtectedReadBlock1_Start()) + 
                (GetProtectedReadBlock2_End() - GetProtectedReadBlock2_Start()) + 
                (GetProtectedReadBlock3_End() - GetProtectedReadBlock3_Start())
                ) * NUM_PROGRAMS;
           
            double total_settings_read = 0.0;

            _PIC_Conn.FlushBuffer();

            backgroundWorker.ReportProgress(0,"Putting device into read mode");

            for (int program_index = 0; program_index < NUM_PROGRAMS; ++program_index)
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
        }

        public virtual void WriteDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                double total_settings_written = 0.0;

                double num_settings = (_settings[0].Count -
                (GetProtectedWriteBlock1_End() - GetProtectedWriteBlock1_Start()) +
                (GetProtectedWriteBlock2_End() - GetProtectedWriteBlock2_Start()) +
                (GetProtectedWriteBlock3_End() - GetProtectedWriteBlock3_Start())
                ) * NUM_PROGRAMS;

                BackgroundWorker backgroundWorker = sender as BackgroundWorker;

                backgroundWorker.ReportProgress(0, "Putting device into programming mode");

                _PIC_Conn.FlushBuffer();

                if (!_PIC_Conn.sendAckdCommand(GetProgrammingSwitchCommandBase()))
                {
                    Console.WriteLine("Error sending ackd GetProgrammingSwitchCommandBase");
                }

                    
                for (int program_index = 0; program_index < this.NUM_PROGRAMS; ++program_index)
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
                            Console.WriteLine("Skipping " + (object)single_setting.Index + " because it is protected");
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
                                    Console.WriteLine("Successfully SetDSPValue at index " + single_setting.Index + " to " + single_setting.Value.ToString("X8"));
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unable to SetDSPValue at index " + single_setting.Index + " to " + single_setting.Value.ToString("X8"));
                            }

                            backgroundWorker.ReportProgress((int)(total_settings_written / num_settings));
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
                                continue;
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
                                continue;
                            }

                            retry_counter++;
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
        }

        #endregion


        #region Virtual Methods

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
            return (byte)32;
        }

        public virtual byte GetEEPROMSwitchCommandBase()
        {
            return (byte)48;
        }

        public virtual int GetProtectedReadBlock1_Start()
        {
            return -1;
        }

        public virtual int GetProtectedReadBlock1_End()
        {
            return -1;
        }

        public virtual int GetProtectedReadBlock2_Start()
        {
            return -1;
        }

        public virtual int GetProtectedReadBlock2_End()
        {
            return -1;
        }

        public virtual int GetProtectedReadBlock3_Start()
        {
            return -1;
        }

        public virtual int GetProtectedReadBlock3_End()
        {
            return -1;
        }

        public virtual int GetProtectedWriteBlock1_Start()
        {
            return -1;
        }

        public virtual int GetProtectedWriteBlock1_End()
        {
            return -1;
        }

        public virtual int GetProtectedWriteBlock2_Start()
        {
            return -1;
        }

        public virtual int GetProtectedWriteBlock2_End()
        {
            return -1;
        }

        public virtual int GetProtectedWriteBlock3_Start()
        {
            return -1;
        }

        public virtual int GetProtectedWriteBlock3_End()
        {
            return -1;
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

        public virtual void SetConnectButtonText(string in_text)
        {
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

       

        protected void AttachUIEvents()
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



            for (int i = 0; i < GetNumInputChannels(); i++)
            {

                // Input Label
                Label lblInput = (Label)(Controls.Find("lblCH" + (i + 1) + "Input", true).FirstOrDefault());

                if (lblInput != null)
                {
                    lblInput.Click += new System.EventHandler(this.lblInput_Click);
                }

                // First Gain Block
                PictureButton btnPreGain1 = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "PreGain", true).FirstOrDefault());

                if (btnPreGain1 != null)
                {
                    btnPreGain1.Click += new System.EventHandler(this.btnPreGain1_Click);
                }

                // Input Filters
                PictureButton btnPreFilters = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "PreFilters", true).FirstOrDefault());

                if (btnPreFilters != null)
                {
                    btnPreFilters.Click += new System.EventHandler(this.btnPreFilters_Click);
                }

                // Compressor
                PictureButton btnCompressor = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "Compressor", true).FirstOrDefault());

                if (btnCompressor != null)
                {
                    btnCompressor.Click += new System.EventHandler(this.btnComp_Click);
                }

                // Premix Gain Block
                PictureButton btnPreGain2 = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "PreGain2", true).FirstOrDefault());

                if (btnPreGain2 != null)
                {
                    btnPreGain2.Click += new System.EventHandler(this.btnPreGain2_Click);
                }


            }


            // Premix Gain Block
            PictureButton btnMatrixMixer = (PictureButton)(Controls.Find("btnMatrixMixer", true).FirstOrDefault());

            if (btnMatrixMixer != null)
            {
                btnMatrixMixer.Click += new System.EventHandler(this.btnMatrixMixer_Click);
            }

            for (int i = 0; i < GetNumOutputChannels(); i++)
            {

                // Trim Block
                PictureButton btnPostTrim = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "PostTrim", true).FirstOrDefault());

                if (btnPostTrim != null)
                {
                    btnPostTrim.Click += new System.EventHandler(this.btnPostTrim_Click);
                }

                // Output Filters
                PictureButton btnPostFilters = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "PostFilters", true).FirstOrDefault());

                if (btnPostFilters != null)
                {
                    btnPostFilters.Click += new System.EventHandler(this.btnPostFilters_Click);
                }

                // Limiter
                PictureButton btnLimiter = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "Limiter", true).FirstOrDefault());

                if (btnLimiter != null)
                {
                    btnLimiter.Click += new System.EventHandler(this.btnLimiter_Click);
                }

                // Delay Block
                PictureButton btnDelay = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "Delay", true).FirstOrDefault());

                if (btnDelay != null)
                {
                    btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
                }
                
                // Output Gain Block
                PictureButton btnPostGain = (PictureButton)(Controls.Find("btnCH" + (i + 1) + "PostGain", true).FirstOrDefault());

                if (btnPostGain != null)
                {
                    btnPostGain.Click += new System.EventHandler(this.BtnPostGainClick);
                }

                // Output Label
                Label lblOutput = (Label)(Controls.Find("lblCH" + (i + 1) + "Output", true).FirstOrDefault());

                if (lblOutput != null)
                {
                    lblOutput.Click += new System.EventHandler(this.lblOutput_Click);
                }


            }

        }
        #endregion


        #region UI Block Actions

        public void UpdateTooltips()
        {

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 50;
            toolTip1.ShowAlways = true;

            // Must be called from somewhere inside SA_Resources. Why not here?
            if (!form_loaded)
            {
                this.SetConnectionPicture(GlobalResources.lblStatus_Blank);
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

                if(btn_PreGain != null)
                {
                    toolTip1.SetToolTip(btn_PreGain, PROGRAMS[CURRENT_PROGRAM].gains[i][0].ToString());
                    btn_PreGain.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][0].Muted;
                    btn_PreGain.Invalidate();
                }

                if(btn_Compressor != null)
                {
                    btn_Compressor.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[i][0].Bypassed;
                    btn_Compressor.Invalidate();
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

                if(btn_Limiter != null)
                {
                    btn_Limiter.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[i][1].Bypassed;
                    btn_Limiter.Invalidate();
                }

                if(btn_Delay != null)
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

        protected void lblInput_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1));

            // Specific to FLX
            bool phantom_power = (ch_num <= num_phantom);
            using (InputConfiguration inputForm = new InputConfiguration(this, ch_num, phantom_power))
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

                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                inputForm.ShowDialog(this);

                UpdateTooltips();

            }
        }


        protected void btnPreGain1_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 0, (0 + ch_num - 1), false, "CH" + ch_num.ToString() + " - Input Gain"))
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

                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                UpdateTooltips();
            }
        }


        protected void btnPreFilters_Click(object sender, EventArgs e)
        {
            int channel = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (FilterForm filterForm = new FilterForm(this, channel))
            {
                filterForm.Height = 500;
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                filterForm.ShowDialog(this);
            }
        }


        protected void btnComp_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            int settings_offset = 220 + (6 * (ch_num - 1));
            using (CompressorForm compressorForm = new CompressorForm(this, ch_num, settings_offset))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                compressorForm.ShowDialog(this);

                UpdateTooltips();
            }
        }


        protected void btnPreGain2_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 1, (28 + ch_num - 1), false, "CH" + ch_num.ToString() + " - Premix Gain"))
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

                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                UpdateTooltips();

            }
        }


        protected void btnMatrixMixer_Click(object sender, EventArgs e)
        {
            using (MixerForm mixerForm = new MixerForm(this))
            {

                if (LIVE_MODE)
                {
                    mixerForm.Width = 496;
                }
                else
                {
                    mixerForm.Width = 228;
                }

                mixerForm.ShowDialog(this);
            }
        }


        protected void btnPostTrim_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 2, (32 + ch_num - 1), false, "CH" + ch_num.ToString() + " - Trim"))
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

                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                UpdateTooltips();
            }
        }


        protected void btnPostFilters_Click(object sender, EventArgs e)
        {
            int channel = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (FilterForm filterForm = new FilterForm(this, channel, true))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                filterForm.ShowDialog(this);
            }
        }


        protected void btnLimiter_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            int settings_offset = 244 + (6 * (ch_num - 1));

            using (CompressorForm compressorForm = new CompressorForm(this, ch_num, settings_offset, CompressorType.Limiter))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                compressorForm.ShowDialog(this);

                UpdateTooltips();
            }
        }


        protected void btnDelay_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (DelayForm delayForm = new DelayForm(this, ch_num, 268 + (ch_num - 1)))
            {

                //delayForm.OnChange += new ConfigChangeEventHandler(this.Config_Changed);
                delayForm.ShowDialog(this);

                UpdateTooltips();

            }
        }


        protected void BtnPostGainClick(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 3, (36 + ch_num - 1), false, "CH" + ch_num.ToString() + " - Output Gain"))
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
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);


                UpdateTooltips();
            }
        }


        protected void lblOutput_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1));

            using (OutputConfiguration outputForm = new OutputConfiguration(this, ch_num))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                outputForm.ShowDialog(this);

                UpdateTooltips();
            }
        }

        #endregion


        #region UI Menu Actions

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
        
        #endregion


        #region Form Actions


        private void MainForm_Template_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.UnsavedChanges)
                return;
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

    }

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
}
