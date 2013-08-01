using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SA_Resources;
using System.Linq;
using System.Globalization;
using System.IO;

/* DEVICE NAME = DSP 4x4 */
namespace DSP_4x4
{
    public partial class MainForm : Form
    {

        #region Variables

        /* Settings to put into demo modes */
        private bool DisableComms = false;
        private readonly bool _vsDebug = System.Diagnostics.Debugger.IsAttached;

        /* Settings Initialization */
        public List<DSP_Setting> _settings = new List<DSP_Setting>();
        public bool form_loaded = false;

        public InputConfig[] inputs = new InputConfig[4];
        public OutputConfig[] outputs = new OutputConfig[4]; 
        public FilterConfig[][] filters = new FilterConfig[4][];
        public GainConfig[][] gains = new GainConfig[4][];
        public CompressorConfig[][] compressors = new CompressorConfig[4][];
        public DelayConfig[] delays = new DelayConfig[4];

        public GainConfig[][] crosspoints = new GainConfig[6][];

        private string[] out_display_names = new string[4];

        private static object updateLock = new object();
        public Queue<Dictionary<String, Object>> ConfigQueue = new Queue<Dictionary<String, Object>>();

        public int ProcessQueueDelay = 2;


        public int num_channels = 4;
        public int num_phantom = 4;

        private static Thread UpdateThread;

        private PIC_Bridge _PIC_Conn;
         
        //private static Thread UIThread;

        // TODO - Move all DEVICE ID's to a global list
        private int DEVICE_ID = 0x20;
        private string SERIALNUM = "";

        private string CONFIGFILE = "";

        #endregion

        #region Constructor and Load

        public MainForm(PIC_Bridge PIC_Conn, string serialNumber, string configFile)
        {
            InitializeComponent();

            SERIALNUM = serialNumber;
            _PIC_Conn = PIC_Conn;

            /* INITIALIZE THE SETTINGS TO DEFAULTS */

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 50;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;


            PictureButton gainControl1, gainControl2, gainControl3, gainControl4;

            for (int i = 0; i < 4; i++)
            {

                


                filters[i] = new FilterConfig[9]; 
                crosspoints[i] = new GainConfig[4];
                gains[i] = new GainConfig[4];
                inputs[i] = new InputConfig(i);
                outputs[i] = new OutputConfig(i);
                

                compressors[i] = new CompressorConfig[2];

                compressors[i][0] = new CompressorConfig(CompressorType.Compressor);
                compressors[i][1] = new CompressorConfig(CompressorType.Limiter);

                delays[i] = new DelayConfig();

                for(int j = 0; j < 4; j++)
                {
                    gains[i][j] = new GainConfig();
                    crosspoints[i][j] = new GainConfig();

                    if(i == j)
                    {
                        crosspoints[i][j].Gain = 0;
                    } else
                    {
                        crosspoints[i][j].Muted = true;
                    }
                }

                gainControl1 = (PictureButton)Controls.Find("btnCH" + (i + 1).ToString() + "PreGain", true).First();
                gainControl2 = (PictureButton)Controls.Find("btnCH" + (i + 1).ToString() + "PreGain2", true).First();
                gainControl3 = (PictureButton)Controls.Find("btnCH" + (i + 1).ToString() + "PostTrim", true).First();
                gainControl4 = (PictureButton)Controls.Find("btnCH" + (i + 1).ToString() + "PostGain", true).First();

                // Set up the ToolTip text for the Button and Checkbox.
                if (gains[i][0].Muted)
                {
                    toolTip1.SetToolTip(gainControl1, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gainControl1, gains[i][1].Gain.ToString("N1") + "dB");
                }

                if (gains[i][1].Muted)
                {
                    toolTip1.SetToolTip(gainControl2, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gainControl2, gains[i][2].Gain.ToString("N1") + "dB");
                }

                if (gains[i][2].Muted)
                {
                    toolTip1.SetToolTip(gainControl3, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gainControl3, gains[i][3].Gain.ToString("N1") + "dB");
                }

                if (gains[i][3].Muted)
                {
                    toolTip1.SetToolTip(gainControl4, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gainControl4, gains[i][3].Gain.ToString("N1") + "dB");
                } 
            }

            for (int j = 4; j < 6; j++)
            {
                crosspoints[j] = new GainConfig[4]; 
                
                for (int k = 0; k < 4; k++)
                {
                    crosspoints[j][k] = new GainConfig(0, true);
                }
            }

            inputs[0].Name = "Local Input #1";
            inputs[1].Name = "Local Input #2";
            inputs[2].Name = "Local Input #3";
            inputs[3].Name = "Local Input #4";

            outputs[0].Name = "Output #1";
            outputs[1].Name = "Output #2";
            outputs[2].Name = "Output #3";
            outputs[3].Name = "Output #4";


            dropProgramSelection.SelectedIndex = 0;

            if (configFile != "")
            {
                CONFIGFILE = configFile;
                
            }


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                DefaultSettings();

                if (CONFIGFILE != "")
                {
                    LoadFromFile(CONFIGFILE);
                }
                //FIXME
                LoadSettingsToForm();

                form_loaded = true;

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading application: \n\n" + ex.Message + "\n\nProgram will now exit.", "Exception During Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();

            }
        }

        #endregion

        #region _settings Read/Load

        private void LoadSettingsToForm()
        {
            lblCH1Input.Text = inputs[0].Name;
            lblCH1Input.Invalidate();

            lblCH2Input.Text = inputs[1].Name;
            lblCH2Input.Invalidate();

            lblCH3Input.Text = inputs[2].Name;
            lblCH3Input.Invalidate();

            lblCH4Input.Text = inputs[3].Name;
            lblCH4Input.Invalidate();

            lblCH1Output.Text = outputs[0].Name;
            lblCH1Output.Invalidate();

            lblCH2Output.Text = outputs[1].Name;
            lblCH2Output.Invalidate();

            lblCH3Output.Text = outputs[2].Name;
            lblCH3Output.Invalidate();

            lblCH4Output.Text = outputs[3].Name;
            lblCH4Output.Invalidate();
            
            int counter = 0;
            int i, j;

            for (i = 0; i < 4; i++)
            {
                gains[i][0].Gain = DSP_Math.value_to_gain(_settings[counter++].Value);
            }

            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (_settings[counter].Value == 0x0000000)
                    {
                        crosspoints[i][j].Muted = true;
                        crosspoints[i][j].Gain = 0;
                    }
                    else
                    {
                        crosspoints[i][j].Muted = false; 
                        crosspoints[i][j].Gain = DSP_Math.value_to_gain(_settings[counter].Value);
                    }

                    counter++;
                }
            }

            // Note that we use i for the second level index and j for the first..
            for (i = 1; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    gains[j][i].Gain = DSP_Math.value_to_gain(_settings[counter++].Value);
                }

                
            }

            
            
            // Counter is now at 40
            // Skip to 220 because 40-219 are the biquad coefficients and absolutely useless to anyone but the DSP

            counter = 220;

            for (int m = 0; m < 4; m++)
            {
                compressors[m][0].Threshold = DSP_Math.MN_to_double_signed(_settings[counter++].Value, 9, 23);
                compressors[m][0].SoftKnee = (_settings[counter++].Value == 0x03000000);
                compressors[m][0].Ratio = DSP_Math.value_to_comp_ratio(_settings[counter++].Value);
                compressors[m][0].Attack = DSP_Math.value_to_comp_attack(_settings[counter++].Value);
                compressors[m][0].Release = DSP_Math.value_to_comp_release(_settings[counter++].Value);
                compressors[m][0].Bypassed = (_settings[counter++].Value == 0x00000001);

            }

            // LIMITERS

            for (int n = 0; n < 4; n++)
            {
                compressors[n][1].Threshold = DSP_Math.MN_to_double_signed(_settings[counter++].Value, 9, 23);
                compressors[n][1].SoftKnee = (_settings[counter++].Value == 0x03000000);
                compressors[n][1].Ratio = DSP_Math.value_to_comp_ratio(_settings[counter++].Value);
                compressors[n][1].Attack = DSP_Math.value_to_comp_attack(_settings[counter++].Value);
                compressors[n][1].Release = DSP_Math.value_to_comp_release(_settings[counter++].Value);
                compressors[n][1].Bypassed = (_settings[counter++].Value == 0x00000001);

            }

            if (!form_loaded)
            {
                return;
            }
            // We return here because the plainfilter settings aren't loaded until the first program or load
            

            int plainfilter_counter = 300;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 9; y++)
                {

                    if (_settings[plainfilter_counter].Value == 0x00000000)
                    {
                        filters[x][y] = null;
                        plainfilter_counter += 3;
                    }
                    else
                    {
                        filters[x][y] = DSP_Math.rebuild_filter(_settings[plainfilter_counter++].Value, _settings[plainfilter_counter++].Value, _settings[plainfilter_counter++].Value);
                    }
                }

            }



            // counter should now be at 40...
            
        }

        private void LoadFormToSettings()
        {

            // Input gain
            _settings[0].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[0][0].Gain), 3, 29);
            _settings[1].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[1][0].Gain), 3, 29);
            _settings[2].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[2][0].Gain), 3, 29);
            _settings[3].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[3][0].Gain), 3, 29);

            //Matrix Mixer
            int counter = 4;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (crosspoints[i][j].Muted == true)
                    {
                        _settings[counter].Value = 0x0000000;
                    }
                    else
                    {
                        _settings[counter].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(crosspoints[i][j].Gain), 3, 29);
                    }

                    counter++;
                }
            }

            // Pre-mix gain
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[0][1].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[1][1].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[2][1].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[3][1].Gain), 3, 29);

            // Post-mix trim
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[0][2].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[1][2].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[2][2].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[3][2].Gain), 3, 29);

            // Output gain
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[0][3].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[1][3].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[2][3].Gain), 3, 29);
            _settings[counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(gains[3][3].Gain), 3, 29);

            // Counter is now 40

            for (int k = 0; k < 4; k++)
            {
                for (int l = 0; l < 9; l++)
                {
                    if (filters[k][l] == null)
                    {
                        _settings[counter++].Value = 0x20000000;
                        _settings[counter++].Value = 0x00000000;
                        _settings[counter++].Value = 0x00000000;
                        _settings[counter++].Value = 0x00000000;
                        _settings[counter++].Value = 0x00000000;
                    }
                    else
                    {
                        if (filters[k][l].Filter == null)
                        {
                            _settings[counter++].Value = 0x20000000;
                            _settings[counter++].Value = 0x00000000;
                            _settings[counter++].Value = 0x00000000;
                            _settings[counter++].Value = 0x00000000;
                            _settings[counter++].Value = 0x00000000;
                        }
                        else
                        {
                            _settings[counter++].Value = DSP_Math.double_to_MN(filters[k][l].Filter.B0, 3, 29);
                            _settings[counter++].Value = DSP_Math.double_to_MN(filters[k][l].Filter.B1, 3, 29);
                            _settings[counter++].Value = DSP_Math.double_to_MN(filters[k][l].Filter.B2, 3, 29);
                            _settings[counter++].Value = DSP_Math.double_to_MN(filters[k][l].Filter.A1 * -1, 2, 30);
                            _settings[counter++].Value = DSP_Math.double_to_MN(filters[k][l].Filter.A2 * -1, 2, 30);
                        }
                    }

                }

            }

            // Counter is now 220

            // COMPRESSORS

            for(int m = 0; m < 4; m++)
            {
                _settings[counter++].Value = DSP_Math.double_to_MN(compressors[m][0].Threshold, 9, 23);

                if (compressors[m][0].SoftKnee)
                {
                    _settings[counter++].Value = 0x03000000;
                } else
                {
                    _settings[counter++].Value = 0x00000000;
                }
                
                _settings[counter++].Value = DSP_Math.comp_ratio_to_value(compressors[m][0].Ratio);
                _settings[counter++].Value = DSP_Math.comp_attack_to_value(compressors[m][0].Attack);
                _settings[counter++].Value = DSP_Math.comp_release_to_value(compressors[m][0].Release);
                _settings[counter++].Value = Convert.ToUInt32(compressors[m][0].Bypassed);

            }

            // LIMITERS

            for (int n = 0; n < 4; n++)
            {
                _settings[counter++].Value = DSP_Math.double_to_MN(compressors[n][1].Threshold, 9, 23);

                if (compressors[n][1].SoftKnee)
                {
                    _settings[counter++].Value = 0x03000000;
                }
                else
                {
                    _settings[counter++].Value = 0x00000000;
                }

                _settings[counter++].Value = DSP_Math.comp_ratio_to_value(compressors[n][1].Ratio);
                _settings[counter++].Value = DSP_Math.comp_attack_to_value(compressors[n][1].Attack);
                _settings[counter++].Value = DSP_Math.comp_release_to_value(compressors[n][1].Release);
                _settings[counter++].Value = Convert.ToUInt32(compressors[n][1].Bypassed);

            }

            _settings[counter++].Value = DSP_Math.double_to_MN(delays[0].Delay, 16, 16);
            _settings[counter++].Value = DSP_Math.double_to_MN(delays[1].Delay, 16, 16);
            _settings[counter++].Value = DSP_Math.double_to_MN(delays[2].Delay, 16, 16);
            _settings[counter++].Value = DSP_Math.double_to_MN(delays[3].Delay, 16, 16);

            int plainfilter_counter = 300;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (filters[x][y] == null)
                    {
                        _settings[plainfilter_counter++].Value = 0x00000000;
                        _settings[plainfilter_counter++].Value = 0x00000000;
                        _settings[plainfilter_counter++].Value = 0x00000000;
                    }
                    else
                    {
                        if (filters[x][y].Filter == null)
                        {
                            _settings[plainfilter_counter++].Value = 0x00000000;
                            _settings[plainfilter_counter++].Value = 0x00000000;
                            _settings[plainfilter_counter++].Value = 0x00000000;
                        }
                        else
                        {
                            _settings[plainfilter_counter++].Value = DSP_Math.filter_to_package(filters[x][y]);
                            _settings[plainfilter_counter++].Value = DSP_Math.double_to_MN(filters[x][y].Filter.Gain, 8, 24);
                            _settings[plainfilter_counter++].Value = DSP_Math.double_to_MN(filters[x][y].Filter.QValue, 8, 24);
                        }
                    }

                }

            }



        }

#endregion

        #region DefaultSettings

        private void DefaultSettings()
        {
            _settings = new List<DSP_Setting>();

            _settings.Add(new DSP_Setting(0, "Gain CH1", 0x20000000));
            _settings.Add(new DSP_Setting(1, "Gain CH2", 0x20000000));
            _settings.Add(new DSP_Setting(2, "Gain CH3", 0x20000000));
            _settings.Add(new DSP_Setting(3, "Gain CH4", 0x20000000));

            _settings.Add(new DSP_Setting(4, "Mixer - IN 1 - OUT 1", 0x20000000));
            _settings.Add(new DSP_Setting(5, "Mixer - IN 1 - OUT 2", 0x0000000));
            _settings.Add(new DSP_Setting(6, "Mixer - IN 1 - OUT 3", 0x0000000));
            _settings.Add(new DSP_Setting(7, "Mixer - IN 1 - OUT 4", 0x0000000));

            _settings.Add(new DSP_Setting(8, "Mixer - IN 2 - OUT 1", 0x0000000));
            _settings.Add(new DSP_Setting(9, "Mixer - IN 2 - OUT 2", 0x20000000));
            _settings.Add(new DSP_Setting(10, "Mixer - IN 2 - OUT 3", 0x0000000));
            _settings.Add(new DSP_Setting(11, "Mixer - IN 2 - OUT 4", 0x0000000));

            _settings.Add(new DSP_Setting(12, "Mixer - IN 3 - OUT 1", 0x0000000));
            _settings.Add(new DSP_Setting(13, "Mixer - IN 3 - OUT 2", 0x0000000));
            _settings.Add(new DSP_Setting(14, "Mixer - IN 3 - OUT 3", 0x20000000));
            _settings.Add(new DSP_Setting(15, "Mixer - IN 3 - OUT 4", 0x0000000));

            _settings.Add(new DSP_Setting(16, "Mixer - IN 4 - OUT 1", 0x0000000));
            _settings.Add(new DSP_Setting(17, "Mixer - IN 4 - OUT 2", 0x0000000));
            _settings.Add(new DSP_Setting(18, "Mixer - IN 4 - OUT 3", 0x0000000));
            _settings.Add(new DSP_Setting(19, "Mixer - IN 4 - OUT 4", 0x20000000));

            _settings.Add(new DSP_Setting(20, "Mixer - IN 5 - OUT 1", 0x0000000));
            _settings.Add(new DSP_Setting(21, "Mixer - IN 5 - OUT 2", 0x0000000));
            _settings.Add(new DSP_Setting(22, "Mixer - IN 5 - OUT 3", 0x0000000));
            _settings.Add(new DSP_Setting(23, "Mixer - IN 5 - OUT 4", 0x0000000));

            _settings.Add(new DSP_Setting(24, "Mixer - IN 6 - OUT 1", 0x0000000));
            _settings.Add(new DSP_Setting(25, "Mixer - IN 6 - OUT 2", 0x0000000));
            _settings.Add(new DSP_Setting(26, "Mixer - IN 6 - OUT 3", 0x0000000));
            _settings.Add(new DSP_Setting(27, "Mixer - IN 6 - OUT 4", 0x0000000));

            _settings.Add(new DSP_Setting(28, "Premix Gain CH1", 0x20000000));
            _settings.Add(new DSP_Setting(29, "Premix Gain CH2", 0x20000000));
            _settings.Add(new DSP_Setting(30, "Premix Gain CH3", 0x20000000));
            _settings.Add(new DSP_Setting(31, "Premix Gain CH4", 0x20000000));

            _settings.Add(new DSP_Setting(32, "Trim CH1", 0x20000000));
            _settings.Add(new DSP_Setting(33, "Trim CH2", 0x20000000));
            _settings.Add(new DSP_Setting(34, "Trim CH3", 0x20000000));
            _settings.Add(new DSP_Setting(35, "Trim CH4", 0x20000000));

            _settings.Add(new DSP_Setting(36, "Output Gain CH1", 0x20000000));
            _settings.Add(new DSP_Setting(37, "Output Gain CH2", 0x20000000));
            _settings.Add(new DSP_Setting(38, "Output Gain CH3", 0x20000000));
            _settings.Add(new DSP_Setting(39, "Output Gain CH4", 0x20000000));

            uint counter = 40;

            uint i,j;

            for(i = 1; i <= 4; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " B0", 0x20000000));
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " B1", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " B2", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " A1", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " A2", 0x00000000));
                }

                for (j = 1; j <= 6; j++)
                {
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " B0", 0x20000000));
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " B1", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " B2", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " A1", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " A2", 0x00000000));
                }
            }

            for (i = 1; i <= 4; i++)
            {
                _settings.Add(new DSP_Setting(counter++, "COMP " + i + " Threshold", 0x00000000));
                _settings.Add(new DSP_Setting(counter++, "COMP " + i + " Knee Size", 0x00000000));
                _settings.Add(new DSP_Setting(counter++, "COMP " + i + " Ratio", 0xC0000000));
                _settings.Add(new DSP_Setting(counter++, "COMP " + i + " Attack", 0x04324349));
                _settings.Add(new DSP_Setting(counter++, "COMP " + i + " Release", 0x00fa8a7d));
                _settings.Add(new DSP_Setting(counter++, "COMP " + i + " Bypass", 0x00000001));
            }

            for (i = 1; i <= 4; i++)
            {
                _settings.Add(new DSP_Setting(counter++, "LIM " + i + " Threshold", 0x00000000));
                _settings.Add(new DSP_Setting(counter++, "LIM " + i + " Knee Size", 0x00000000));
                _settings.Add(new DSP_Setting(counter++, "LIM " + i + " Ratio", 0x8147AE14));
                _settings.Add(new DSP_Setting(counter++, "LIM " + i + " Attack", 0x04324349));
                _settings.Add(new DSP_Setting(counter++, "LIM " + i + " Release", 0x00fa8a7d));
                _settings.Add(new DSP_Setting(counter++, "LIM " + i + " Bypass", 0x00000001));
            }

            _settings.Add(new DSP_Setting(counter++, "DELAY CH1", 0x00000000));
            _settings.Add(new DSP_Setting(counter++, "DELAY CH2", 0x00000000));
            _settings.Add(new DSP_Setting(counter++, "DELAY CH3", 0x00000000));
            _settings.Add(new DSP_Setting(counter++, "DELAY CH4", 0x00000000));

            

            while(counter < 300)
            {
                _settings.Add(new DSP_Setting(counter++, "DUMMY " + counter, 0x00000000));
            }

            for (i = 1; i <= 4; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " Package", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " Gain", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " Q", 0x00000000));
                }

                for (j = 1; j <= 6; j++)
                {
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " Package", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " Gain", 0x00000000));
                    _settings.Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " Q", 0x00000000));
                }
            }

        }






        #endregion

        #region Live Update

        public void AddToUpdateQueue(Dictionary<String, object> data_object)
        {
            // Instead of locking the list we use an arbitrary object shared by the threads
            lock (updateLock)
            {
                ConfigQueue.Enqueue(data_object);
            }

            if (UpdateThread == null)
            {
                // Start the thread on the first request
                UpdateThread = new Thread(ProcessUpdateQueue);
                UpdateThread.IsBackground = true;
                UpdateThread.Start();
            }
        }

        private void ProcessUpdateQueue()
        {
            while (true)
            {
                if (ConfigQueue.Count > 0)
                {
                    lock (updateLock)
                    {
                        Dictionary<String, object> nextItem = ConfigQueue.Dequeue();
                        //string return_string = SendToServer(nextItem);

                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(ProcessQueueDelay));
            }
        }


        private void Config_Changed(object sender, ConfigChangeEventArgs e)
        {
            Console.WriteLine("Config changed! Key = " + e.Type + ", Value = " + e.Value + ", Ch Index = " + e.ChIndex);
        }

        #endregion

        #region Main UI Actions

        private void btnPreFilters_Click(object sender, EventArgs e)
        {
            int channel = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (FilterForm3 filterForm = new FilterForm3(filters[channel - 1], channel))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                filterForm.ShowDialog(this);
            }

            for(int i = 0; i < 3; i ++)
            {
                if (filters[channel - 1][i].Filter == null)
                {
                    Console.WriteLine("Not used");
                }
                else
                {
                    Console.WriteLine(filters[channel - 1][i].Filter.FilterType);
                }
            }

            
        }

        private void btnMatrixMixer_Click(object sender, EventArgs e)
        {
            using (MixerForm mixerForm = new MixerForm(crosspoints))
            {

                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                mixerForm.ShowDialog(this);
            }
        }
        

        private void btnPreGain1_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            
            using (GainForm gainForm = new GainForm(gains[index-1][0], "CH" + index.ToString() + " - Input Gain"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                PictureButton gain_button = (PictureButton)sender;

                if (gains[index - 1][0].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, gains[index - 1][0].Gain.ToString("N1") + "dB");
                }

                gain_button.Overlay2Visible = gains[index - 1][0].Muted;
            }
        }

        private void btnPreGain2_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (GainForm gainForm = new GainForm(gains[index - 1][1], "CH" + index.ToString() + " - Premix Gain"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                PictureButton gain_button = (PictureButton)sender;

                if (gains[index - 1][1].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, gains[index - 1][1].Gain.ToString("N1") + "dB");
                }
                gain_button.Overlay2Visible = gains[index - 1][1].Muted;

            }
        }
        
        
        private void btnComp_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (CompressorForm compressorForm = new CompressorForm(index, compressors[index-1][0]))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                compressorForm.ShowDialog(this);

                PictureButton comp_button = (PictureButton)sender;

                comp_button.Overlay1Visible = compressors[index - 1][0].Bypassed;
            }
        }

        private void btnPostTrim_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (GainForm gainForm = new GainForm(gains[index - 1][2], "CH" + index.ToString() + " - Trim"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                PictureButton gain_button = (PictureButton)sender;

                if (gains[index - 1][2].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, gains[index - 1][2].Gain.ToString("N1") + "dB");
                }
                gain_button.Overlay2Visible = gains[index - 1][2].Muted;
            }
        }
        
        private void btnPostFilters_Click(object sender, EventArgs e)
        {
            int channel = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (FilterForm6 filterForm = new FilterForm6(filters[channel - 1], channel))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                filterForm.ShowDialog(this);
            }
        }


        private void btnLimiter_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (CompressorForm compressorForm = new CompressorForm(index, compressors[index-1][1]))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                compressorForm.ShowDialog(this);

                PictureButton comp_button = (PictureButton)sender;

                comp_button.Overlay1Visible = compressors[index - 1][1].Bypassed;
            }
        }

        private void btnDelay_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (DelayForm delayForm = new DelayForm(index, delays[index-1]))
            {

                delayForm.OnChange += new ConfigChangeEventHandler(this.Config_Changed);
                delayForm.ShowDialog(this);

                PictureButton delay_button = (PictureButton)sender;

                delay_button.Overlay1Visible = delays[index - 1].Bypassed;
                toolTip1.SetToolTip(delay_button, delays[index - 1].Delay.ToString("N1") + "ms");

            }

            
        }

        private void BtnPostGainClick(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (GainForm gainForm = new GainForm(gains[index - 1][3], "CH" + index.ToString() + " - Output Gain"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);


                PictureButton gain_button = (PictureButton)sender;

                if (gains[index - 1][3].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, gains[index - 1][3].Gain.ToString("N1") + "dB");
                }
                gain_button.Overlay2Visible = gains[index - 1][3].Muted;
            }
        }

        private void lblInput_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((Label)sender).Name.Substring(5, 1));

            // Specific to FLX
            bool phantom_power = (index <= num_phantom);
            using (InputConfiguration inputForm = new InputConfiguration(inputs[index - 1], index, phantom_power))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                inputForm.ShowDialog(this);

                ((Label)sender).Text = inputs[index-1].Name;

                if(inputs[index-1].Type == InputType.Line)
                {
                    toolTip1.SetToolTip(((Label)sender), "Line Level\nPhantom Power: " + inputs[index-1].PhantomPower.ToString());
                } else
                {
                    toolTip1.SetToolTip(((Label)sender), "Microphone\nPhantom Power: " + inputs[index - 1].PhantomPower.ToString());
                }
                
            }
        }

        private void lblOutput_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((Label)sender).Name.Substring(5, 1));

            using (OutputConfiguration outputForm = new OutputConfiguration(outputs[index-1], index))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                outputForm.ShowDialog(this);

                ((Label) sender).Text = outputs[index - 1].Name;
                ((Label) sender).Invalidate();
            }
        }


        private void btnPrintConfiguration_Click(object sender, EventArgs e)
        {
            using (ConfigurationPrintout configForm = new ConfigurationPrintout(this))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                configForm.ShowDialog(this);

            } 
        }

        private void pbtnProgramDevice_Click(object sender, EventArgs e)
        {
            LoadFormToSettings();
            SaveForm saveForm = new SaveForm(_settings, inputs, outputs, _PIC_Conn, DisableComms);

            saveForm.ShowDialog();
        }

        private void pbtnReadDevice_Click(object sender, EventArgs e)
        {
            ReadForm readForm = new ReadForm(this, _PIC_Conn);

            readForm.ShowDialog();

            LoadSettingsToForm();
        }

        #endregion

        #region Program/Read/Save/Load UI Actions

        private void pbtnSaveConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveProgramDialog.ShowDialog() == DialogResult.OK)
                {

                    LoadFormToSettings();

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveProgramDialog.FileName))
                    {
                        file.WriteLine("DEVICE-ID:" + DEVICE_ID.ToString("X8") + ";");
                        file.WriteLine("SERIAL:" + SERIALNUM + ";");
                        file.WriteLine("TIMESTAMP:" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + ";");
                        for (int i = 0; i < num_channels; i++)
                        {
                            file.WriteLine("INPUT_" + (i+1) + ":" + inputs[i].Name + ";");
                        }

                        for (int j = 0; j < num_channels; j++)
                        {
                            file.WriteLine("OUTPUT_" + (j + 1) + ":" + outputs[j].Name + ";");
                        }

                        foreach (DSP_Setting single_setting in _settings)
                            {
                                file.WriteLine(single_setting.Index.ToString("D3") + "=" + single_setting.Value.ToString("X8") + ";");
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save program file. Message: " + ex.Message, "Save Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbtnLoadConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
            if (openProgramDialog.ShowDialog() == DialogResult.OK)
            {

                LoadFromFile(openProgramDialog.FileName);
                LoadSettingsToForm();

            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromFile(string filename)
        {

            string tempLine = "";
            string channel_name = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
            {
                int lineCount = 0, index = 0;
                UInt32 value = 0x00000000;
                while (file.Peek() >= 0)
                {
                    lineCount++;
                    tempLine = file.ReadLine();

                    if (tempLine.Contains("DEVICE-ID:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("DEVICE-ID:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("SERIAL:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("TIMESTAMP:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Substring(0, 5) == "INPUT")
                    {
                        index = int.Parse(tempLine.Substring(6, 1));
                        channel_name = tempLine.Substring(8, tempLine.Length - 9);
                        inputs[index - 1].Name = channel_name;
                        continue;
                    }

                    if (tempLine.Substring(0, 6) == "OUTPUT")
                    {
                        index = int.Parse(tempLine.Substring(7, 1));
                        channel_name = tempLine.Substring(9, tempLine.Length - 10);
                        outputs[index - 1].Name = channel_name;
                        continue;
                    }

                    if ((tempLine.Length != 13) || (tempLine.IndexOf('=') != 3) || (tempLine.IndexOf(';') != 12))
                    {
                        throw new Exception("Invalid format encountered on line " + lineCount);
                    }
                    else
                    {
                        index = int.Parse(tempLine.Substring(0, 3));
                        bool parsedSuccessfully = UInt32.TryParse(tempLine.Substring(4, 8), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out value);
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
        }

        #endregion

        #region Metering Demo

        private Bitmap meter_image(int index)
        {
            switch (index)
            {
                case 1:
                    return GlobalResources.meter_1;
                case 2:
                    return GlobalResources.meter_2;
                case 3:
                    return GlobalResources.meter_3;
                case 4:
                    return GlobalResources.meter_4;
                case 5:
                    return GlobalResources.meter_5;
                case 6:
                    return GlobalResources.meter_6;
                case 7:
                    return GlobalResources.meter_7;
                case 8:
                    return GlobalResources.meter_8;
                case 9:
                    return GlobalResources.meter_9;
                case 10:
                    return GlobalResources.meter_10;
                case 11:
                    return GlobalResources.meter_11;
                default :
                    return GlobalResources.meter_0;
            }
        }

        public void MeterDemo(object param)
        {
            MethodInvoker action1, action2, action3, action4;

            int meter_level = (int)param;
            Random rand_gen = new Random();
            while (true)
            {
                try
                {
                    action1 = delegate
                    {
                        pictureBox3.Image = meter_image(rand_gen.Next(0, 12));
                        pictureBox3.Update();
                    };
                    pictureBox3.BeginInvoke(action1);

                    action2 = delegate
                    {
                        pictureBox4.Image = meter_image(rand_gen.Next(0, 12));
                        pictureBox4.Update();
                    };
                    pictureBox4.BeginInvoke(action2);

                    action3 = delegate
                    {
                        pictureBox5.Image = meter_image(rand_gen.Next(0, 12));
                        pictureBox5.Update();
                    };
                    pictureBox5.BeginInvoke(action3);


                    action4 = delegate
                    {
                        pictureBox6.Image = meter_image(rand_gen.Next(0, 12));
                        pictureBox6.Update();
                    };
                    pictureBox6.BeginInvoke(action4);


                    Thread.Sleep(150);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in UpdateUIToVals: " + ex.Message);
                }
            }

        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Save!");
        }

        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openProgramDialog.ShowDialog() == DialogResult.OK)
                {

                    LoadFromFile(openProgramDialog.FileName);

                    tsStatusLabel.Text = "Successfully loaded " + Path.GetFileName(openProgramDialog.FileName);

                    LoadSettingsToForm();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void helpToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            //((ToolStripMenuItem)sender).ForeColor = Color.Black;
        }

        private void helpToolStripMenuItem1_MouseLeave(object sender, EventArgs e)
        {
            //((ToolStripMenuItem)sender).ForeColor = Color.Gainsboro;
        }

        private void helpToolStripMenuItem1_ForeColorChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Changed!");
        }

        private void aboutDSPControlCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }

}
