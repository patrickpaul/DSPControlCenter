using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

using SA_Resources;

namespace FLX_Tester
{

    public partial class MainForm : Form
    {

        public static String productName = "FLX Tester";

        public bool form_loaded = false;

        /* Settings to put into demo modes */
        // TODO - CHANGE THESE FOR PRODUCTION
        private bool DisableComms = false;
        private readonly bool _vsDebug = System.Diagnostics.Debugger.IsAttached;

        /* Communications Settings */
        string _rxString;

        private const int DelayMs = 50;

        /* Settings Initialization */
        private List<DSP_Setting> _settings = new List<DSP_Setting>();
        
        /* Misc */

        delegate void AddTextCallback(string text);

        readonly Version _currentVersion = new Version(0, 1);
        
        private const int ReadDelayMs = 5;

        private Dial dGain1, dGain2, dGain3, dGain4, dSineFreq, dSineGain, dPinkGain;

        private string DeviceName = "FLX480";

        private int amp_mode = 1;

        private PIC_Bridge _PIC_Conn;

        public MainForm(PIC_Bridge PIC_Conn)
        {
            InitializeComponent();

            _PIC_Conn = PIC_Conn;

            dGain1 = new Dial(TextGain1, DialGain1, new[] { -100.0, -81.4, -62.8, -44, -25.6, -7, 12.0 }, DialHelpers.Format_String_Gain, Images.knob_red_bg, Images.knob_red_line);
            dGain2 = new Dial(TextGain2, DialGain2, new[] { -100.0, -81.4, -62.8, -44, -25.6, -7, 12.0 }, DialHelpers.Format_String_Gain, Images.knob_orange_bg, Images.knob_orange_line);
            dGain3 = new Dial(TextGain3, DialGain3, new[] { -100.0, -81.4, -62.8, -44, -25.6, -7, 12.0 }, DialHelpers.Format_String_Gain, Images.knob_yellow_bg, Images.knob_yellow_line);
            dGain4 = new Dial(TextGain4, DialGain4, new[] { -100.0, -81.4, -62.8, -44, -25.6, -7, 12.0 }, DialHelpers.Format_String_Gain, Images.knob_green_bg, Images.knob_green_line);

            dSineFreq = new Dial(TextSineFreq, DialSineFreq, new[] { 20.0, 60.0, 200.0, 660.0, 2000.0, 6000.0, 20000.00 }, DialHelpers.Format_String_Sine_F, Images.knob_blue_bg, Images.knob_blue_line);
            dSineGain = new Dial(TextSineGain, DialSineGain, new[] { -100.0, -80.0, -60.0, -40.0, -20.0, 0.0, 20.0 }, DialHelpers.Format_String_Gain, Images.knob_purple_bg, Images.knob_purple_line);
            dPinkGain = new Dial(TextPinkGain, DialPinkGain, new[] { -100.0, -81.4, -62.8, -44, -25.6, -7, 12.0 }, DialHelpers.Format_String_Gain, Images.knob_red_bg, Images.knob_red_line);
           
            this.Icon = Icons.App_Icon;
        }


        #region Form Load and Start 

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                DefaultSettings();
                
                //FIXME
                load_dsp_values_into_ui();

                form_loaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading application: \n\n" + ex.Message + "\n\nProgram will now exit.", "Exception During Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();

            }
        }

        private void DefaultSettings()
        {
            amp_mode = 1;
            
            _settings = new List<DSP_Setting>(); 
            
            _settings.Add(new DSP_Setting(0, "Gain Master", 0x20000000));
            _settings.Add(new DSP_Setting(1, "Gain CH1", 0x20000000));
            _settings.Add(new DSP_Setting(2, "Gain CH2", 0x20000000));
            _settings.Add(new DSP_Setting(3, "Gain CH3", 0x20000000));
            _settings.Add(new DSP_Setting(4, "Gain CH4", 0x20000000));

            _settings.Add(new DSP_Setting(5, "Input Router CH1", 0x00000001));
            _settings.Add(new DSP_Setting(6, "Input Router CH2", 0x00000002));
            _settings.Add(new DSP_Setting(7, "Input Router CH3", 0x00000003));
            _settings.Add(new DSP_Setting(8, "Input Router CH4", 0x00000004));

            _settings.Add(new DSP_Setting(9, "Output Router CH1", 0x00000001));
            _settings.Add(new DSP_Setting(10, "Output Router CH2", 0x00000002));
            _settings.Add(new DSP_Setting(11, "Output Router CH3", 0x00000003));
            _settings.Add(new DSP_Setting(12, "Output Router CH4", 0x00000004));

            _settings.Add(new DSP_Setting(13, "Sine Freq", 0x05555555));
            _settings.Add(new DSP_Setting(14, "Sine Gain", 0xfd785d94));
            _settings.Add(new DSP_Setting(15, "Pink Noise Gain", 0x01c9f25c));

        }

        #endregion

        #region Logging

        private void btnClear_Click(object sender, EventArgs e)
        {
            textLog.Text = "";
        }

        #endregion

        #region Save Routine

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!DisableComms)
            {
                serialPort1.DataReceived -= serialPort1_DataReceived;
            }

            loadFormToSettings();
            /*SaveForm saveForm = new SaveForm(_settings, amp_mode,_PIC_Conn, DisableComms, chkDebug.Checked);

            saveForm.ShowDialog();

            if (!DisableComms)
            {
                serialPort1.DataReceived += serialPort1_DataReceived;
            }
             */
        }
        
        private void loadFormToSettings()
        {
            
            _settings[1].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(DialHelpers.string_to_value(TextGain1.Text)), 3, 29);
            _settings[2].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(DialHelpers.string_to_value(TextGain2.Text)), 3, 29);
            _settings[3].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(DialHelpers.string_to_value(TextGain3.Text)), 3, 29);
            _settings[4].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(DialHelpers.string_to_value(TextGain4.Text)), 3, 29);
            
            _settings[5].Value = Helpers.radiogroup_to_value(INROUTER_1_1.Checked, INROUTER_1_2.Checked, INROUTER_1_3.Checked, INROUTER_1_4.Checked, INROUTER_1_5.Checked, INROUTER_1_6.Checked);
            _settings[6].Value = Helpers.radiogroup_to_value(INROUTER_2_1.Checked, INROUTER_2_2.Checked, INROUTER_2_3.Checked, INROUTER_2_4.Checked, INROUTER_2_5.Checked, INROUTER_2_6.Checked);
            _settings[7].Value = Helpers.radiogroup_to_value(INROUTER_3_1.Checked, INROUTER_3_2.Checked, INROUTER_3_3.Checked, INROUTER_3_4.Checked, INROUTER_3_5.Checked, INROUTER_3_6.Checked);
            _settings[8].Value = Helpers.radiogroup_to_value(INROUTER_4_1.Checked, INROUTER_4_2.Checked, INROUTER_4_3.Checked, INROUTER_4_4.Checked, INROUTER_4_5.Checked, INROUTER_4_6.Checked);

            _settings[9].Value = Helpers.radiogroup_to_value(OUTROUTER_1_1.Checked, OUTROUTER_1_2.Checked, OUTROUTER_1_3.Checked, OUTROUTER_1_4.Checked);
            _settings[10].Value = Helpers.radiogroup_to_value(OUTROUTER_2_1.Checked, OUTROUTER_2_2.Checked, OUTROUTER_2_3.Checked, OUTROUTER_2_4.Checked);
            _settings[11].Value = Helpers.radiogroup_to_value(OUTROUTER_3_1.Checked, OUTROUTER_3_2.Checked, OUTROUTER_3_3.Checked, OUTROUTER_3_4.Checked);
            _settings[12].Value = Helpers.radiogroup_to_value(OUTROUTER_4_1.Checked, OUTROUTER_4_2.Checked, OUTROUTER_4_3.Checked, OUTROUTER_4_4.Checked);

            _settings[13].Value = DSP_Math.sine_freq_to_value(DialHelpers.string_to_value(TextSineFreq.Text));
            //_settings[14].Value = Helpers.double_to_MN(DialHelpers.string_to_value(TextSineFreq.Text),1,31);
            _settings[14].Value = 0xfd785d94;
            //_settings[15].Value = Helpers.double_to_MN(DialHelpers.string_to_value(TextSineFreq.Text),1,31);
            _settings[15].Value = 0x01c9f25c;

            amp_mode = dropAmpMode.SelectedIndex + 1;
        }

        


        

        #endregion

        #region RS232 Communications   

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Invoke(new EventHandler(ProcessRS232));
            }
            catch
            {
                textLog.AppendText("Error caught" + Environment.NewLine);
            }
        }

        private void ProcessRS232(object sender, EventArgs e)
        {

            ASCIIEncoding encoding = new ASCIIEncoding();

            _rxString = serialPort1.ReadExisting();

            Byte[] bytes = encoding.GetBytes(_rxString);

            if (bytes.Length == 0)
            {
                //textLog.AppendText("[DEBUG] Captured empty log" + System.Environment.NewLine);
                return;
            }

            textLog.AppendText("String: " + _rxString + System.Environment.NewLine);
        }

        
        

        #endregion

        #region Multithread Helpers


        private void AddTextToLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textLog.InvokeRequired)
            {
                AddTextCallback d = new AddTextCallback(AddTextToLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textLog.Text += text;
            }
        }

        private void AddDebugTextToLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textLog.InvokeRequired)
            {
                AddTextCallback d = new AddTextCallback(AddDebugTextToLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
  
            }
        }

        



        private void ReadWorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            AddTextToLog("Read complete." + Environment.NewLine + "Loading values into UI... "); 
            
            load_dsp_values_into_ui();

            AddTextToLog("Complete!" + Environment.NewLine); 
            progressBar1.Value = 100;
            this.serialPort1.DataReceived += serialPort1_DataReceived;
            
        }
 

        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        #endregion

        #region Read and Load Routine

        /* TODO Redo all read/load to read/write values to textboxes */

        private void btnRead_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            serialPort1.DataReceived -= serialPort1_DataReceived;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += ReadFromDevice;
            worker.RunWorkerCompleted += ReadWorkComplete;

            worker.RunWorkerAsync(_settings);

            AddTextToLog("Reading values from " + DeviceName + "...");

        }

        private void ReadFromDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            /* TODO - Add try/catch */
            UInt32 read_value;

            BackgroundWorker worker = sender as BackgroundWorker;

            List<DSP_Setting> received_settings_list = doWorkEventArgs.Argument as List<DSP_Setting>;

            double total = received_settings_list.Count;
            int count = 0;

            foreach (DSP_Setting single_setting in received_settings_list)
            {
                if (DisableComms)
                {
                    Thread.Sleep(ReadDelayMs);
                    worker.ReportProgress((int)((count / total) * 100.0));

                    count++;

                    continue;
                }

                AddTextToLog("Requesting " + single_setting.Name + "...");

                try
                {
                    read_value = _PIC_Conn.Read_DSP_Value(single_setting.Index);

                    if (read_value == 0xFFFFFFFF)
                    {
                        AddTextToLog("FAILED. Continuing." + Environment.NewLine);

                    }
                    else
                    {
                        AddTextToLog("RECEIVED [" + read_value.ToString("X8") + "]" + Environment.NewLine);
                        _settings[count].Value = read_value;

                    }
                } catch (Exception ex)
                {
                    AddTextToLog("[ERROR] " + ex.Message + Environment.NewLine); 

                }
                worker.ReportProgress((int)((count / total) * 100.0));

                count++;

            }
        }

        private void load_dsp_values_into_ui()
        {

            dGain1.Value = DSP_Math.value_to_gain(_settings[1].Value);
            dGain2.Value = DSP_Math.value_to_gain(_settings[2].Value);
            dGain3.Value = DSP_Math.value_to_gain(_settings[3].Value);
            dGain4.Value = DSP_Math.value_to_gain(_settings[4].Value);


            Helpers.loadRouterValue(_settings[5].Value, INROUTER_1_1, INROUTER_1_2, INROUTER_1_3, INROUTER_1_4, INROUTER_1_5, INROUTER_1_6);
            Helpers.loadRouterValue(_settings[6].Value, INROUTER_2_1, INROUTER_2_2, INROUTER_2_3, INROUTER_2_4, INROUTER_2_5, INROUTER_2_6);
            Helpers.loadRouterValue(_settings[7].Value, INROUTER_3_1, INROUTER_3_2, INROUTER_3_3, INROUTER_3_4, INROUTER_3_5, INROUTER_3_6);
            Helpers.loadRouterValue(_settings[8].Value, INROUTER_4_1, INROUTER_4_2, INROUTER_4_3, INROUTER_4_4, INROUTER_4_5, INROUTER_4_6);

            Helpers.loadRouterValue(_settings[9].Value, OUTROUTER_1_1, OUTROUTER_1_2, OUTROUTER_1_3, OUTROUTER_1_4);
            Helpers.loadRouterValue(_settings[10].Value, OUTROUTER_2_1, OUTROUTER_2_2, OUTROUTER_2_3, OUTROUTER_2_4);
            Helpers.loadRouterValue(_settings[11].Value, OUTROUTER_3_1, OUTROUTER_3_2, OUTROUTER_3_3, OUTROUTER_3_4);
            Helpers.loadRouterValue(_settings[12].Value, OUTROUTER_4_1, OUTROUTER_4_2, OUTROUTER_4_3, OUTROUTER_4_4);


            dSineFreq.Value = DSP_Math.sine_value_to_freq(_settings[13].Value);

            /*
             *
            _settings.Add(new DSP_Setting(14, "Sine Gain", 0xfd785d94));
            _settings.Add(new DSP_Setting(15, "Pink Noise Gain", 0x01c9f25c));
             * */

            dropAmpMode.SelectedIndex = amp_mode - 1;
            dropAmpMode.Invalidate();


        }

        #endregion

        #region Toolstrip

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void commPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string portName = ((ToolStripItem)sender).Text;

            foreach (ToolStripMenuItem item in commPortToolStripMenuItem.DropDownItems)
            {
                if (item.Text != portName)
                {
                    item.Checked = false;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openProgramDialog.ShowDialog() == DialogResult.OK)
                {
                    string tempLine = "";
                    using (System.IO.StreamReader file = new System.IO.StreamReader(openProgramDialog.FileName))
                    {
                        int lineCount = 0, index = 0;
                        UInt32 value = 0x00000000;
                        while (file.Peek() >= 0)
                        {
                            lineCount++;
                            tempLine = file.ReadLine();

                            if ((tempLine.Length != 12) || (tempLine.IndexOf('=') != 2) || (tempLine.IndexOf(';') != 11))
                            {
                                throw new Exception("Invalid format encountered on line " + lineCount);
                            }
                            else
                            {
                                index = int.Parse(tempLine.Substring(0, 2));
                                bool parsedSuccessfully = UInt32.TryParse(tempLine.Substring(3,8), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out value);
                                if (!parsedSuccessfully)
                                {
                                    throw new Exception("Invalid value encountered on line " + lineCount);
                                }
                                else
                                {
                                    _settings[index].Value = value;
                                }
                            }
                        }
                    }

                    //TODO FIXME
                    // load_dsp_values_into_ui();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Save Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveProgramDialog.ShowDialog() == DialogResult.OK)
                {

                    loadFormToSettings();

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveProgramDialog.FileName))
                    {
                        foreach (DSP_Setting single_setting in _settings)
                        {
                            file.WriteLine(single_setting.Index.ToString("D2") + "=" + single_setting.Value.ToString("X8") + ";");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save program file. Message: " + ex.Message, "Save Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chkDisableComm.Checked = debugToolStripMenuItem.Checked;
        }

        private void resetConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefaultSettings();
            //TODO - FIXME
            // load_dsp_values_into_ui();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("About FLX Control Center", "FLX Control Center Software", _currentVersion, "[Alpha]");
            aboutForm.ShowDialog();
        }

        #endregion

        #region Reboot

        private void btnReboot_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.Text = "Attempting reboot";
                if (_PIC_Conn.Reboot())
                {
                    StatusLabel.Text = "Reboot Complete";
                    return;
                }

                StatusLabel.Text = "Reboot failed";
            } catch (Exception)
            {
                StatusLabel.Text = "Error during reboot";
            }
        }

        #endregion
    }
}