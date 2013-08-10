using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SA_Resources;
using System.Linq;
using System.Globalization;
using System.IO;
using SA_Resources.Forms;

/* DEVICE NAME = DSP 4x4 */
namespace DSP_4x4
{
    public partial class MainForm : SA_Resources.Forms.MainForm_Template
    {

        #region Variables

        private bool demo_mode = false;
        

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

            InitializePrograms();

            UpdateTooltips();

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

                LoadSettingsToProgramConfig();

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


        private void LoadSettingsToProgramConfig()
        {
            
            int counter = 0;
            int i, j;


            for (int program_index = 0; program_index < NUM_PROGRAMS; program_index++)
            {

                for (i = 0; i < 4; i++)
                {
                    PROGRAMS[program_index].gains[i][0].Gain = DSP_Math.value_to_gain(_settings[program_index][counter++].Value);
                }

                for (i = 0; i < 6; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (_settings[program_index][counter].Value == 0x0000000)
                        {
                            PROGRAMS[program_index].crosspoints[i][j].Muted = true;
                            PROGRAMS[program_index].crosspoints[i][j].Gain = 0;
                        }
                        else
                        {
                            PROGRAMS[program_index].crosspoints[i][j].Muted = false;
                            PROGRAMS[program_index].crosspoints[i][j].Gain = DSP_Math.value_to_gain(_settings[program_index][counter].Value);
                        }

                        counter++;
                    }
                }

                // Note that we use i for the second level index and j for the first..
                for (i = 1; i < 4; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        PROGRAMS[program_index].gains[j][i].Gain = DSP_Math.value_to_gain(_settings[program_index][counter++].Value);
                    }


                }



                // Counter is now at 40
                // Skip to 220 because 40-219 are the biquad coefficients and absolutely useless to anyone but the DSP

                counter = 220;

                for (int m = 0; m < 4; m++)
                {

                    PROGRAMS[program_index].compressors[m][0].Threshold = DSP_Math.MN_to_double_signed(_settings[program_index][counter++].Value, 9, 23);
                    PROGRAMS[program_index].compressors[m][0].SoftKnee = (_settings[program_index][counter++].Value == 0x03000000);
                    PROGRAMS[program_index].compressors[m][0].Ratio = DSP_Math.value_to_comp_ratio(_settings[program_index][counter++].Value);
                    PROGRAMS[program_index].compressors[m][0].Attack = DSP_Math.value_to_comp_attack(_settings[program_index][counter++].Value);
                    PROGRAMS[program_index].compressors[m][0].Release = DSP_Math.value_to_comp_release(_settings[program_index][counter++].Value);
                    PROGRAMS[program_index].compressors[m][0].Bypassed = (_settings[program_index][counter++].Value == 0x00000001);

                    ((PictureButton)Controls.Find("btnCH" + (m + 1).ToString() + "Compressor", true).First()).Overlay1Visible = PROGRAMS[program_index].compressors[m][0].Bypassed;
                    ((PictureButton)Controls.Find("btnCH" + (m + 1).ToString() + "Compressor", true).First()).Invalidate();
                }

                // LIMITERS

                for (int n = 0; n < 4; n++)
                {
                    PROGRAMS[program_index].compressors[n][1].Threshold = DSP_Math.MN_to_double_signed(_settings[program_index][counter++].Value, 9, 23);
                    PROGRAMS[program_index].compressors[n][1].SoftKnee = (_settings[program_index][counter++].Value == 0x03000000);
                    PROGRAMS[program_index].compressors[n][1].Ratio = DSP_Math.value_to_comp_ratio(_settings[program_index][counter++].Value);
                    PROGRAMS[program_index].compressors[n][1].Attack = DSP_Math.value_to_comp_attack(_settings[program_index][counter++].Value);
                    PROGRAMS[program_index].compressors[n][1].Release = DSP_Math.value_to_comp_release(_settings[program_index][counter++].Value);
                    PROGRAMS[program_index].compressors[n][1].Bypassed = (_settings[program_index][counter++].Value == 0x00000001);

                    ((PictureButton)Controls.Find("btnCH" + (n + 1).ToString() + "Limiter", true).First()).Overlay1Visible = PROGRAMS[program_index].compressors[n][1].Bypassed;
                    ((PictureButton)Controls.Find("btnCH" + (n + 1).ToString() + "Limiter", true).First()).Invalidate();
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

                        if (_settings[program_index][plainfilter_counter].Value == 0x00000000)
                        {
                            PROGRAMS[program_index].filters[x][y] = null;
                            plainfilter_counter += 3;
                        }
                        else
                        {
                            PROGRAMS[program_index].filters[x][y] = DSP_Math.rebuild_filter(_settings[program_index][plainfilter_counter++].Value, _settings[program_index][plainfilter_counter++].Value, _settings[program_index][plainfilter_counter++].Value);
                        }
                    }

                }
            }
        }

        private void LoadProgramConfigToSettings()
        {

            for (int program_index = 0; program_index < NUM_PROGRAMS; program_index++)
            {
                // Input gain
                _settings[program_index][0].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[0][0].Gain), 3, 29);
                _settings[program_index][1].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[1][0].Gain), 3, 29);
                _settings[program_index][2].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[2][0].Gain), 3, 29);
                _settings[program_index][3].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[3][0].Gain), 3, 29);

                //Matrix Mixer
                int counter = 4;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (PROGRAMS[program_index].crosspoints[i][j].Muted == true)
                        {
                            _settings[program_index][counter].Value = 0x0000000;
                        }
                        else
                        {
                            _settings[program_index][counter].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].crosspoints[i][j].Gain), 3, 29);
                        }

                        counter++;
                    }
                }

                // Pre-mix gain
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[0][1].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[1][1].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[2][1].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[3][1].Gain), 3, 29);

                // Post-mix trim
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[0][2].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[1][2].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[2][2].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[3][2].Gain), 3, 29);

                // Output gain
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[0][3].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[1][3].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[2][3].Gain), 3, 29);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PROGRAMS[program_index].gains[3][3].Gain), 3, 29);

                // Counter is now 40

                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        if (PROGRAMS[program_index].filters[k][l] == null)
                        {
                            _settings[program_index][counter++].Value = 0x20000000;
                            _settings[program_index][counter++].Value = 0x00000000;
                            _settings[program_index][counter++].Value = 0x00000000;
                            _settings[program_index][counter++].Value = 0x00000000;
                            _settings[program_index][counter++].Value = 0x00000000;
                        }
                        else
                        {
                            if (PROGRAMS[program_index].filters[k][l].Filter == null)
                            {
                                _settings[program_index][counter++].Value = 0x20000000;
                                _settings[program_index][counter++].Value = 0x00000000;
                                _settings[program_index][counter++].Value = 0x00000000;
                                _settings[program_index][counter++].Value = 0x00000000;
                                _settings[program_index][counter++].Value = 0x00000000;
                            }
                            else
                            {
                                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[k][l].Filter.B0, 3, 29);
                                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[k][l].Filter.B1, 3, 29);
                                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[k][l].Filter.B2, 3, 29);
                                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[k][l].Filter.A1 * -1, 2, 30);
                                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[k][l].Filter.A2 * -1, 2, 30);
                            }
                        }

                    }

                }

                // Counter is now 220

                // PROGRAMS[program_index].compressors

                for (int m = 0; m < 4; m++)
                {
                    _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].compressors[m][0].Threshold, 9, 23);

                    if (PROGRAMS[program_index].compressors[m][0].SoftKnee)
                    {
                        _settings[program_index][counter++].Value = 0x03000000;
                    }
                    else
                    {
                        _settings[program_index][counter++].Value = 0x00000000;
                    }

                    _settings[program_index][counter++].Value = DSP_Math.comp_ratio_to_value(PROGRAMS[program_index].compressors[m][0].Ratio);
                    _settings[program_index][counter++].Value = DSP_Math.comp_attack_to_value(PROGRAMS[program_index].compressors[m][0].Attack);
                    _settings[program_index][counter++].Value = DSP_Math.comp_release_to_value(PROGRAMS[program_index].compressors[m][0].Release);
                    _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].compressors[m][0].Bypassed);

                }

                // LIMITERS

                for (int n = 0; n < 4; n++)
                {
                    _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].compressors[n][1].Threshold, 9, 23);

                    if (PROGRAMS[program_index].compressors[n][1].SoftKnee)
                    {
                        _settings[program_index][counter++].Value = 0x03000000;
                    }
                    else
                    {
                        _settings[program_index][counter++].Value = 0x00000000;
                    }

                    _settings[program_index][counter++].Value = DSP_Math.comp_ratio_to_value(PROGRAMS[program_index].compressors[n][1].Ratio);
                    _settings[program_index][counter++].Value = DSP_Math.comp_attack_to_value(PROGRAMS[program_index].compressors[n][1].Attack);
                    _settings[program_index][counter++].Value = DSP_Math.comp_release_to_value(PROGRAMS[program_index].compressors[n][1].Release);
                    _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].compressors[n][1].Bypassed);

                }

                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].delays[0].Delay, 16, 16);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].delays[1].Delay, 16, 16);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].delays[2].Delay, 16, 16);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].delays[3].Delay, 16, 16);

                int plainfilter_counter = 300;
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        if (PROGRAMS[program_index].filters[x][y] == null)
                        {
                            _settings[program_index][plainfilter_counter++].Value = 0x00000000;
                            _settings[program_index][plainfilter_counter++].Value = 0x00000000;
                            _settings[program_index][plainfilter_counter++].Value = 0x00000000;
                        }
                        else
                        {
                            if (PROGRAMS[program_index].filters[x][y].Filter == null)
                            {
                                _settings[program_index][plainfilter_counter++].Value = 0x00000000;
                                _settings[program_index][plainfilter_counter++].Value = 0x00000000;
                                _settings[program_index][plainfilter_counter++].Value = 0x00000000;
                            }
                            else
                            {
                                _settings[program_index][plainfilter_counter++].Value = DSP_Math.filter_to_package(PROGRAMS[program_index].filters[x][y]);
                                _settings[program_index][plainfilter_counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[x][y].Filter.Gain, 8, 24);
                                _settings[program_index][plainfilter_counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].filters[x][y].Filter.QValue, 8, 24);
                            }
                        }

                    }

                }

            }

        }

#endregion

        #region DefaultSettings

        private void DefaultSettings()
        {
            _settings[0] = new List<DSP_Setting>();
            _settings[1] = new List<DSP_Setting>();
            _settings[2] = new List<DSP_Setting>();

            for (int x = 0; x < 3; x++)
            {
                _settings[x].Add(new DSP_Setting(0, "Gain CH1", 0x20000000));
                _settings[x].Add(new DSP_Setting(1, "Gain CH2", 0x20000000));
                _settings[x].Add(new DSP_Setting(2, "Gain CH3", 0x20000000));
                _settings[x].Add(new DSP_Setting(3, "Gain CH4", 0x20000000));

                _settings[x].Add(new DSP_Setting(4, "Mixer - IN 1 - OUT 1", 0x20000000));
                _settings[x].Add(new DSP_Setting(5, "Mixer - IN 1 - OUT 2", 0x0000000));
                _settings[x].Add(new DSP_Setting(6, "Mixer - IN 1 - OUT 3", 0x0000000));
                _settings[x].Add(new DSP_Setting(7, "Mixer - IN 1 - OUT 4", 0x0000000));

                _settings[x].Add(new DSP_Setting(8, "Mixer - IN 2 - OUT 1", 0x0000000));
                _settings[x].Add(new DSP_Setting(9, "Mixer - IN 2 - OUT 2", 0x20000000));
                _settings[x].Add(new DSP_Setting(10, "Mixer - IN 2 - OUT 3", 0x0000000));
                _settings[x].Add(new DSP_Setting(11, "Mixer - IN 2 - OUT 4", 0x0000000));

                _settings[x].Add(new DSP_Setting(12, "Mixer - IN 3 - OUT 1", 0x0000000));
                _settings[x].Add(new DSP_Setting(13, "Mixer - IN 3 - OUT 2", 0x0000000));
                _settings[x].Add(new DSP_Setting(14, "Mixer - IN 3 - OUT 3", 0x20000000));
                _settings[x].Add(new DSP_Setting(15, "Mixer - IN 3 - OUT 4", 0x0000000));

                _settings[x].Add(new DSP_Setting(16, "Mixer - IN 4 - OUT 1", 0x0000000));
                _settings[x].Add(new DSP_Setting(17, "Mixer - IN 4 - OUT 2", 0x0000000));
                _settings[x].Add(new DSP_Setting(18, "Mixer - IN 4 - OUT 3", 0x0000000));
                _settings[x].Add(new DSP_Setting(19, "Mixer - IN 4 - OUT 4", 0x20000000));

                _settings[x].Add(new DSP_Setting(20, "Mixer - IN 5 - OUT 1", 0x0000000));
                _settings[x].Add(new DSP_Setting(21, "Mixer - IN 5 - OUT 2", 0x0000000));
                _settings[x].Add(new DSP_Setting(22, "Mixer - IN 5 - OUT 3", 0x0000000));
                _settings[x].Add(new DSP_Setting(23, "Mixer - IN 5 - OUT 4", 0x0000000));

                _settings[x].Add(new DSP_Setting(24, "Mixer - IN 6 - OUT 1", 0x0000000));
                _settings[x].Add(new DSP_Setting(25, "Mixer - IN 6 - OUT 2", 0x0000000));
                _settings[x].Add(new DSP_Setting(26, "Mixer - IN 6 - OUT 3", 0x0000000));
                _settings[x].Add(new DSP_Setting(27, "Mixer - IN 6 - OUT 4", 0x0000000));

                _settings[x].Add(new DSP_Setting(28, "Premix Gain CH1", 0x20000000));
                _settings[x].Add(new DSP_Setting(29, "Premix Gain CH2", 0x20000000));
                _settings[x].Add(new DSP_Setting(30, "Premix Gain CH3", 0x20000000));
                _settings[x].Add(new DSP_Setting(31, "Premix Gain CH4", 0x20000000));

                _settings[x].Add(new DSP_Setting(32, "Trim CH1", 0x20000000));
                _settings[x].Add(new DSP_Setting(33, "Trim CH2", 0x20000000));
                _settings[x].Add(new DSP_Setting(34, "Trim CH3", 0x20000000));
                _settings[x].Add(new DSP_Setting(35, "Trim CH4", 0x20000000));

                _settings[x].Add(new DSP_Setting(36, "Output Gain CH1", 0x20000000));
                _settings[x].Add(new DSP_Setting(37, "Output Gain CH2", 0x20000000));
                _settings[x].Add(new DSP_Setting(38, "Output Gain CH3", 0x20000000));
                _settings[x].Add(new DSP_Setting(39, "Output Gain CH4", 0x20000000));

                uint counter = 40;

                uint i, j;

                for (i = 1; i <= 4; i++)
                {
                    for (j = 1; j <= 3; j++)
                    {
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " B0", 0x20000000));
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " B1", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " B2", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " A1", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " A2", 0x00000000));
                    }

                    for (j = 1; j <= 6; j++)
                    {
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " B0", 0x20000000));
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " B1", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " B2", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " A1", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " A2", 0x00000000));
                    }
                }

                for (i = 1; i <= 4; i++)
                {
                    _settings[x].Add(new DSP_Setting(counter++, "COMP " + i + " Threshold", 0x00000000));
                    _settings[x].Add(new DSP_Setting(counter++, "COMP " + i + " Knee Size", 0x00000000));
                    _settings[x].Add(new DSP_Setting(counter++, "COMP " + i + " Ratio", 0xC0000000));
                    _settings[x].Add(new DSP_Setting(counter++, "COMP " + i + " Attack", 0x04324349));
                    _settings[x].Add(new DSP_Setting(counter++, "COMP " + i + " Release", 0x00fa8a7d));
                    _settings[x].Add(new DSP_Setting(counter++, "COMP " + i + " Bypass", 0x00000001));
                }

                for (i = 1; i <= 4; i++)
                {
                    _settings[x].Add(new DSP_Setting(counter++, "LIM " + i + " Threshold", 0x00000000));
                    _settings[x].Add(new DSP_Setting(counter++, "LIM " + i + " Knee Size", 0x00000000));
                    _settings[x].Add(new DSP_Setting(counter++, "LIM " + i + " Ratio", 0x8147AE14));
                    _settings[x].Add(new DSP_Setting(counter++, "LIM " + i + " Attack", 0x04324349));
                    _settings[x].Add(new DSP_Setting(counter++, "LIM " + i + " Release", 0x00fa8a7d));
                    _settings[x].Add(new DSP_Setting(counter++, "LIM " + i + " Bypass", 0x00000001));
                }

                _settings[x].Add(new DSP_Setting(counter++, "DELAY CH1", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "DELAY CH2", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "DELAY CH3", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "DELAY CH4", 0x00000000));



                while (counter < 300)
                {
                    _settings[x].Add(new DSP_Setting(counter++, "DUMMY " + counter, 0x00000000));
                }

                for (i = 1; i <= 4; i++)
                {
                    for (j = 1; j <= 3; j++)
                    {
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " Package", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " Gain", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "IN FILTER " + i + " - " + j + " Q", 0x00000000));
                    }

                    for (j = 1; j <= 6; j++)
                    {
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " Package", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " Gain", 0x00000000));
                        _settings[x].Add(new DSP_Setting(counter++, "OUT FILTER " + i + " - " + j + " Q", 0x00000000));
                    }
                }
            }

        }






        #endregion

        #region Live Update

        /*
         * 
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

        */

        private void Config_Changed(object sender, ConfigChangeEventArgs e)
        {
            Console.WriteLine("Config changed! Key = " + e.Type + ", Value = " + e.Value + ", Ch Index = " + e.ChIndex);
        }

        #endregion

        #region Main UI Actions and Updates

        public override void UpdateTooltips()
        {
            lblCH1Input.Text = PROGRAMS[CURRENT_PROGRAM].inputs[0].Name;
            lblCH1Input.Invalidate();

            lblCH2Input.Text = PROGRAMS[CURRENT_PROGRAM].inputs[1].Name;
            lblCH2Input.Invalidate();

            lblCH3Input.Text = PROGRAMS[CURRENT_PROGRAM].inputs[2].Name;
            lblCH3Input.Invalidate();

            lblCH4Input.Text = PROGRAMS[CURRENT_PROGRAM].inputs[3].Name;
            lblCH4Input.Invalidate();

            lblCH1Output.Text = PROGRAMS[CURRENT_PROGRAM].outputs[0].Name;
            lblCH1Output.Invalidate();

            lblCH2Output.Text = PROGRAMS[CURRENT_PROGRAM].outputs[1].Name;
            lblCH2Output.Invalidate();

            lblCH3Output.Text = PROGRAMS[CURRENT_PROGRAM].outputs[2].Name;
            lblCH3Output.Invalidate();

            lblCH4Output.Text = PROGRAMS[CURRENT_PROGRAM].outputs[3].Name;
            lblCH4Output.Invalidate();

            for (int i = 0; i < num_channels; i++)
            {
                PictureButton btn_gain1 = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PreGain", true).First());
                PictureButton btn_gain2 = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PreGain2", true).First());
                PictureButton btn_gain3 = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PostTrim", true).First());
                PictureButton btn_gain4 = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "PostGain", true).First());

                PictureButton btn_compressor = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "Compressor", true).First());
                PictureButton btn_limiter = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "Limiter", true).First());


                PictureButton btn_delay = ((PictureButton)Controls.Find("btnCH" + (i + 1) + "Delay", true).First());


                if (PROGRAMS[CURRENT_PROGRAM].gains[i][0].Muted)
                {
                    toolTip1.SetToolTip(btn_gain1, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(btn_gain1, PROGRAMS[CURRENT_PROGRAM].gains[i][0].Gain.ToString("N1") + "dB");
                }

                btn_gain1.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][0].Muted;


                if (PROGRAMS[CURRENT_PROGRAM].gains[i][1].Muted)
                {
                    toolTip1.SetToolTip(btn_gain2, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(btn_gain2, PROGRAMS[CURRENT_PROGRAM].gains[i][1].Gain.ToString("N1") + "dB");
                }

                btn_gain2.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][1].Muted;


                if (PROGRAMS[CURRENT_PROGRAM].gains[i][2].Muted)
                {
                    toolTip1.SetToolTip(btn_gain3, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(btn_gain3, PROGRAMS[CURRENT_PROGRAM].gains[i][2].Gain.ToString("N1") + "dB");
                }

                btn_gain3.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][2].Muted;


                if (PROGRAMS[CURRENT_PROGRAM].gains[i][3].Muted)
                {
                    toolTip1.SetToolTip(btn_gain4, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(btn_gain4, PROGRAMS[CURRENT_PROGRAM].gains[i][3].Gain.ToString("N1") + "dB");
                }

                btn_gain4.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[i][3].Muted;

                btn_compressor.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[i][0].Bypassed;
                btn_limiter.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[i][1].Bypassed;

                btn_gain1.Invalidate();
                btn_gain2.Invalidate();
                btn_gain3.Invalidate();
                btn_gain4.Invalidate();
                btn_compressor.Invalidate();
                btn_limiter.Invalidate();

                toolTip1.SetToolTip(btn_delay, (PROGRAMS[CURRENT_PROGRAM].delays[i].Delay * 1000).ToString("N1") + "ms");


            }

        }
        
        
        private void btnPreFilters_Click(object sender, EventArgs e)
        {
            int channel = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (FilterForm3 filterForm = new FilterForm3(this,PROGRAMS[CURRENT_PROGRAM].filters[channel - 1], channel))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                filterForm.ShowDialog(this);
            }

            for(int i = 0; i < 3; i ++)
            {
                if (PROGRAMS[CURRENT_PROGRAM].filters[channel - 1][i].Filter == null)
                {
                    Console.WriteLine("Not used");
                }
                else
                {
                    Console.WriteLine(PROGRAMS[CURRENT_PROGRAM].filters[channel - 1][i].Filter.FilterType);
                }
            }

            
        }

        private void btnMatrixMixer_Click(object sender, EventArgs e)
        {
            using (MixerForm mixerForm = new MixerForm(this))
            {

                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                mixerForm.ShowDialog(this);
            }
        }
        

        private void btnPreGain1_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num-1, 0, false, "CH" + ch_num.ToString() + " - Input Gain"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                PictureButton gain_button = (PictureButton)sender;

                if (PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].Gain.ToString("N1") + "dB");
                }

                gain_button.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][0].Muted;
            }
        }

        private void btnPreGain2_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 1, false, "CH" + ch_num.ToString() + " - Premix Gain"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                PictureButton gain_button = (PictureButton)sender;

                if (PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][1].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][1].Gain.ToString("N1") + "dB");
                }
                gain_button.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][1].Muted;

            }
        }
        
        
        private void btnComp_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (CompressorForm compressorForm = new CompressorForm(this, ch_num))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                compressorForm.ShowDialog(this);

                PictureButton comp_button = (PictureButton)sender;

                comp_button.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[ch_num - 1][0].Bypassed;
            }
        }

        private void btnPostTrim_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 2, false, "CH" + ch_num.ToString() + " - Trim"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);

                PictureButton gain_button = (PictureButton)sender;

                if (PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][2].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][2].Gain.ToString("N1") + "dB");
                }
                gain_button.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][2].Muted;
            }
        }
        
        private void btnPostFilters_Click(object sender, EventArgs e)
        {
            int channel = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (FilterForm6 filterForm = new FilterForm6(this, PROGRAMS[CURRENT_PROGRAM].filters[channel - 1], channel))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                filterForm.ShowDialog(this);
            }
        }


        private void btnLimiter_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (CompressorForm compressorForm = new CompressorForm(this, ch_num,CompressorType.Limiter))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                compressorForm.ShowDialog(this);

                PictureButton comp_button = (PictureButton)sender;

                comp_button.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].compressors[ch_num - 1][1].Bypassed;
            }
        }

        private void btnDelay_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));

            using (DelayForm delayForm = new DelayForm(this, ch_num))
            {

                delayForm.OnChange += new ConfigChangeEventHandler(this.Config_Changed);
                delayForm.ShowDialog(this);

                PictureButton delay_button = (PictureButton)sender;

                //delay_button.Overlay1Visible = PROGRAMS[CURRENT_PROGRAM].delays[index - 1].Bypassed;
                toolTip1.SetToolTip(delay_button, (PROGRAMS[CURRENT_PROGRAM].delays[ch_num - 1].Delay*1000).ToString("N1") + "ms");

            }

            
        }

        private void BtnPostGainClick(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((PictureButton)sender).Name.Substring(5, 1));


            using (GainForm gainForm = new GainForm(this, ch_num - 1, 3, false, "CH" + ch_num.ToString() + " - Output Gain"))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                gainForm.ShowDialog(this);


                PictureButton gain_button = (PictureButton)sender;

                if (PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][3].Muted)
                {
                    toolTip1.SetToolTip(gain_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(gain_button, PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][3].Gain.ToString("N1") + "dB");
                }
                gain_button.Overlay2Visible = PROGRAMS[CURRENT_PROGRAM].gains[ch_num - 1][3].Muted;
            }
        }

        private void lblInput_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1));

            // Specific to FLX
            bool phantom_power = (ch_num <= num_phantom);
            using (InputConfiguration inputForm = new InputConfiguration(this, ch_num, phantom_power))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                inputForm.ShowDialog(this);

                ((Label)sender).Text = PROGRAMS[CURRENT_PROGRAM].inputs[ch_num - 1].Name;

                if (PROGRAMS[CURRENT_PROGRAM].inputs[ch_num - 1].Type == InputType.Line)
                {
                    toolTip1.SetToolTip(((Label)sender), "Line Level\nPhantom Power: " + PROGRAMS[CURRENT_PROGRAM].inputs[ch_num - 1].PhantomPower.ToString());
                } else
                {
                    toolTip1.SetToolTip(((Label)sender), "Microphone\nPhantom Power: " + PROGRAMS[CURRENT_PROGRAM].inputs[ch_num - 1].PhantomPower.ToString());
                }
                
            }
        }

        private void lblOutput_Click(object sender, EventArgs e)
        {
            int ch_num = int.Parse(((Label)sender).Name.Substring(5, 1));

            using (OutputConfiguration outputForm = new OutputConfiguration(this, ch_num))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                outputForm.ShowDialog(this);

                ((Label)sender).Text = PROGRAMS[CURRENT_PROGRAM].outputs[ch_num - 1].Name;
                ((Label) sender).Invalidate();
            }
        }


        private void btnPrintConfiguration_Click(object sender, EventArgs e)
        {
            /*using (ConfigurationPrintout configForm = new ConfigurationPrintout(this))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                configForm.ShowDialog(this);

            } */
        }

        private void pbtnProgramDevice_Click(object sender, EventArgs e)
        {
            if (demo_mode)
            {
                MessageBox.Show("This feature has been disabled in evaluation mode.","Feature Disabled",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            
            LoadProgramConfigToSettings();
            SaveForm saveForm = new SaveForm(this, DisableComms);

            saveForm.ShowDialog();
        }

        private void pbtnReadDevice_Click(object sender, EventArgs e)
        {
            if (demo_mode)
            {
                MessageBox.Show("This feature has been disabled in evaluation mode.", "Feature Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ReadForm readForm = new ReadForm(this, _PIC_Conn);

            readForm.ShowDialog();

            LoadSettingsToProgramConfig();
        }

        #endregion

        #region Program/Read/Save/Load UI Actions

        private void pbtnSaveConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveProgramDialog.ShowDialog() == DialogResult.OK)
                {

                    LoadProgramConfigToSettings(); 
                    SaveToFile(saveProgramDialog.FileName);
                    
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

                    LoadSettingsToProgramConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveToFile(string outputFile)
        {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile))
            {
                file.WriteLine("DEVICE-ID:" + DEVICE_ID.ToString("X8") + ";");
                file.WriteLine("SERIAL:" + SERIALNUM + ";");
                file.WriteLine("TIMESTAMP:" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + ";");
                for (int k = 0; k < 3; k++)
                {
                    file.WriteLine("PRESET" + (k + 1) + ";"); 
                    
                    for (int i = 0; i < num_channels; i++)
                    {
                        file.WriteLine("INPUT_" + (i + 1) + ":" + PROGRAMS[k].inputs[i].Name + ";");
                    }

                    for (int j = 0; j < num_channels; j++)
                    {
                        file.WriteLine("OUTPUT_" + (j + 1) + ":" + PROGRAMS[k].outputs[j].Name + ";");
                    }

                
                    foreach (DSP_Setting single_setting in _settings[k])
                    {
                        file.WriteLine(single_setting.Index.ToString("D3") + "=" + single_setting.Value.ToString("X8") + ";");
                    }
                }
            }

        }
        
        
        private void LoadFromFile(string filename)
        {

            string tempLine = "";
            string channel_name = "";
            int cur_program = 0;

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

                    if (tempLine.Contains("PRESET") && tempLine.Substring(7,1) == ";")
                    {
                        cur_program = int.Parse(tempLine.Substring(6, 1)) - 1;
                        Console.WriteLine("Changing current program to " + cur_program);
                        continue;
                    }

                    if (tempLine.Substring(0, 5) == "INPUT")
                    {
                        index = int.Parse(tempLine.Substring(6, 1));
                        channel_name = tempLine.Substring(8, tempLine.Length - 9);
                        PROGRAMS[cur_program].inputs[index - 1].Name = channel_name;
                        continue;
                    }

                    if (tempLine.Substring(0, 6) == "OUTPUT")
                    {
                        index = int.Parse(tempLine.Substring(7, 1));
                        channel_name = tempLine.Substring(9, tempLine.Length - 10);
                        PROGRAMS[cur_program].outputs[index - 1].Name = channel_name;
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
                            _settings[cur_program][index].Value = value;
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

        #region Toolstrip
        private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveProgramDialog.ShowDialog() == DialogResult.OK)
                {

                    LoadProgramConfigToSettings();

                    SaveToFile(saveProgramDialog.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save program file. Message: " + ex.Message, "Save Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openProgramDialog.ShowDialog() == DialogResult.OK)
                {

                    LoadFromFile(openProgramDialog.FileName);

                    tsStatusLabel.Text = "Successfully loaded " + Path.GetFileName(openProgramDialog.FileName);

                    LoadSettingsToProgramConfig();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load program file. Message: " + ex.Message, "Load Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutDSPControlCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((new AboutForm("About DSP Control Center", "Stewart Audio DSP Control Center", new Version(1, 2, 0), "-BETA").ShowDialog() == DialogResult.OK))
            {
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO - check if we want to save configuration
            
            this.Close();
        }

        private void dropProgramSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CURRENT_PROGRAM = dropProgramSelection.SelectedIndex;
            UpdateTooltips();
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }

    }

}
