using System;
using System.Collections.Generic;
using System.Drawing;
using SA_Resources;
using System.Linq;
using SA_Resources.Forms;

namespace DSP_4x4
{
    public partial class MainForm : MainForm_Template
    {

        #region Constructors - Derived

        public MainForm()
            : base()
        {
            InitializeComponent();
        }

        public MainForm(string configFile)
            : base(configFile)
        {
            InitializeComponent();
        }

        #endregion

        #region Editable Portion

        #region Device Specific Settings

        public override int GetDeviceID()
        {
            return 0x20;
        }

        public override string GetDeviceName()
        {
            return "DSP 4x4";
        }

        public override DeviceType GetDeviceType()
        {
            return DeviceType.DSP4x4;
        }
        
        
        public override int GetNumInputChannels()
        {
            return 4;
        }

        public override int GetNumOutputChannels()
        {
            return 4;
        }

        public override int GetNumPhantomPowerChannels()
        {
            return 4;
        }


        public override bool IsAmplifier()
        {
            return false;
        }

        public override bool DuckerEnabled()
        {
            return true;
        }

        public override int GetProtectedReadBlock1_Start()
        {
            return 39;
        }

        public override int GetProtectedReadBlock1_End()
        {
            return 220;
        }

        public override int GetProtectedReadBlock2_Start()
        {
            return 285;
        }

        public override int GetProtectedReadBlock2_End()
        {
            return 300;
        }

        public override int GetProtectedReadBlock3_Start()
        {
            return -1;
        }

        public override int GetProtectedReadBlock3_End()
        {
            return -1;
        }

        public override int GetProtectedWriteBlock1_Start()
        {
            return 285;
        }

        public override int GetProtectedWriteBlock1_End()
        {
            return 300;
        }

        public override int GetProtectedWriteBlock2_Start()
        {
            return -1;
        }

        public override int GetProtectedWriteBlock2_End()
        {
            return -1;
        }

        public override int GetProtectedWriteBlock3_Start()
        {
            return -1;
        }

        public override int GetProtectedWriteBlock3_End()
        {
            return -1;
        }

        public override Image GetDeviceThumbnail()
        {
            return Properties.Resources.DSP4x4_Thumbnail;
        }

        public override void SetConnectionPicture(Image connectionPicture)
        {
            pictureConnectionStatus.BackgroundImage = connectionPicture;
            pictureConnectionStatus.Invalidate();

        }

        #endregion

        #region DefaultSettings

        public override void DefaultSettings()
        {
            _settings[0] = new List<DSP_Setting>();
            _settings[1] = new List<DSP_Setting>();
            _settings[2] = new List<DSP_Setting>();

            _cached_settings[0] = new List<DSP_Setting>();
            _cached_settings[1] = new List<DSP_Setting>();
            _cached_settings[2] = new List<DSP_Setting>();

            for (int x = 0; x < 3; x++)
            {
                _settings[x].Add(new DSP_Setting(0, "Gain CH1", 0x00000000));
                _settings[x].Add(new DSP_Setting(1, "Gain CH2", 0x00000000));
                _settings[x].Add(new DSP_Setting(2, "Gain CH3", 0x00000000));
                _settings[x].Add(new DSP_Setting(3, "Gain CH4", 0x00000000));

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

                _settings[x].Add(new DSP_Setting(counter++, "DUCK Threshold", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Hold Time", 0x0000012c));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Depth", 0xf8800000));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Attack", 0x04324349));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Release", 0x00FA8A7D));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Bypass", 0x00000001));

                _settings[x].Add(new DSP_Setting(counter++, "DUCK In Router 1", 0x00000002));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK In Router 2", 0x00000003));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK In Router 3", 0x00000004));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK In Router 4", 0x00000001));

                _settings[x].Add(new DSP_Setting(counter++, "DUCK Out Router 1", 0x00000004));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Out Router 2", 0x00000001));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Out Router 3", 0x00000002));
                _settings[x].Add(new DSP_Setting(counter++, "DUCK Out Router 4", 0x00000003));

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

                _settings[x].Add(new DSP_Setting(counter++, "Volume Group 1-2", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Volume Group 3-4", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Volume Group 5-6", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Volume Group 7-8", 0x00000000));

                _settings[x].Add(new DSP_Setting(counter++, "Pregain 1", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Pregain 2", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Pregain 3", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Pregain 4", 0x00000000));

                _settings[x].Add(new DSP_Setting(counter++, "Input Gain 1", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Input Gain 2", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Input Gain 3", 0x00000000));
                _settings[x].Add(new DSP_Setting(counter++, "Input Gain 4", 0x00000000));


                foreach (DSP_Setting single_setting in _settings[x])
                {
                    _cached_settings[x].Add(new DSP_Setting(single_setting.Index, single_setting.Name, single_setting.Value));
                }

            }

            // CH1 METERS
            _gain_meters[0] = new List<UInt32>();
            _gain_meters[0].Add(0xF0C00005);
            _gain_meters[0].Add(0xF4C00004);
            _gain_meters[0].Add(0xF6C00004);
            _gain_meters[0].Add(0xFAC00004);

            // CH2 METERS
            _gain_meters[1] = new List<UInt32>();
            _gain_meters[1].Add(0xF0C00009);
            _gain_meters[1].Add(0xF4C00008);
            _gain_meters[1].Add(0xF6C00008);
            _gain_meters[1].Add(0xFAC00008);

            // CH3 METERS
            _gain_meters[2] = new List<UInt32>();
            _gain_meters[2].Add(0xF0C0000D);
            _gain_meters[2].Add(0xF4C0000C);
            _gain_meters[2].Add(0xF6C0000C);
            _gain_meters[2].Add(0xFAC0000C);

            // CH4 METERS
            _gain_meters[3] = new List<UInt32>();
            _gain_meters[3].Add(0xF0C00011);
            _gain_meters[3].Add(0xF4C00010);
            _gain_meters[3].Add(0xF6C00010);
            _gain_meters[3].Add(0xFAC00010);

            // COMPRESSORS
            _comp_in_meters[0] = new List<UInt32>();
            _comp_in_meters[0].Add(0xF3C00004);
            _comp_in_meters[0].Add(0xF3C00008);
            _comp_in_meters[0].Add(0xF3C0000C);
            _comp_in_meters[0].Add(0xF3C00010);

            _comp_out_meters[0] = new List<UInt32>();
            _comp_out_meters[0].Add(0xF3C00018);
            _comp_out_meters[0].Add(0xF3C0001C);
            _comp_out_meters[0].Add(0xF3C00020);
            _comp_out_meters[0].Add(0xF3C00024);

            // LIMITERS
            _comp_in_meters[1] = new List<UInt32>();
            _comp_in_meters[1].Add(0xF8C00004);
            _comp_in_meters[1].Add(0xF8C00008);
            _comp_in_meters[1].Add(0xF8C0000C);
            _comp_in_meters[1].Add(0xF8C00010);

            _comp_out_meters[1] = new List<UInt32>();
            _comp_out_meters[1].Add(0xF8C00018);
            _comp_out_meters[1].Add(0xF8C0001C);
            _comp_out_meters[1].Add(0xF8C00020);
            _comp_out_meters[1].Add(0xF8C00024);

            // MIXERS
            _mix_meters = new List<UInt32>();
            _mix_meters.Add(0xF5C0003E);
            _mix_meters.Add(0xF5C00042);
            _mix_meters.Add(0xF5C00046);
            _mix_meters.Add(0xF5C0004A);

            // DUCKER
            _ducker_meters = new List<UInt32>();
            _ducker_meters.Add(0xF1C00004);
            _ducker_meters.Add(0xF1C00008);
            _ducker_meters.Add(0xF1C0000C);
            _ducker_meters.Add(0xF1C00010);

        }

        #endregion    

        #region _settings Read/Load

        public override void LoadSettingsToProgramConfig()
        {
            
            int counter = 0;
            int i, j;


            for (int program_index = 0; program_index < NUM_PROGRAMS; program_index++)
            {

                for (i = 0; i < 4; i++)
                {
                    PROGRAMS[program_index].gains[i][0].Gain = DSP_Math.value_to_simple_gain(_settings[program_index][counter++].Value);

                    if (PROGRAMS[program_index].gains[i][0].Gain < -90)
                    {
                        PROGRAMS[program_index].gains[i][0].Gain = 0;
                        PROGRAMS[program_index].gains[i][0].Muted = true;
                    }
                    else
                    {
                        PROGRAMS[program_index].gains[i][0].Muted = false;
                    }

                }

                for (i = 0; i < 6; i++)
                {
                    for (j = 0; j < 4; j++)
                    {

                        PROGRAMS[program_index].crosspoints[i][j].Gain = DSP_Math.value_to_gain(_settings[program_index][counter++].Value);

                        // Note we use -90 because in live mode it will often not set gain to a true -100
                        if (PROGRAMS[program_index].crosspoints[i][j].Gain < -90)
                        {
                            PROGRAMS[program_index].crosspoints[i][j].Gain = 0;
                            PROGRAMS[program_index].crosspoints[i][j].Muted = true;
                        }
                        else
                        {
                            PROGRAMS[program_index].crosspoints[i][j].Muted = false;
                        }

                        //counter++;
                    }
                }

                // Note that we use i for the second level index and j for the first..
                for (i = 1; i < 4; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        PROGRAMS[program_index].gains[j][i].Gain = DSP_Math.value_to_gain(_settings[program_index][counter++].Value);

                        if (PROGRAMS[program_index].gains[j][i].Gain < -90)
                        {
                            PROGRAMS[program_index].gains[j][i].Gain = 0;
                            PROGRAMS[program_index].gains[j][i].Muted = true;
                        }
                        else
                        {
                            PROGRAMS[program_index].gains[j][i].Muted = false;
                        }
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

                for (int o = 0; o < 4; o++)
                {
                    PROGRAMS[program_index].delays[o].Delay = DSP_Math.MN_to_double_signed(_settings[program_index][counter++].Value, 16,16);
                }

                // counter is at 272, ready for DUCK features


                PROGRAMS[program_index].ducker.Threshold = DSP_Math.MN_to_double_signed(_settings[program_index][counter++].Value, 9, 23);
                PROGRAMS[program_index].ducker.Holdtime = DSP_Math.value_to_dynamic_hold(_settings[program_index][counter++].Value);
                PROGRAMS[program_index].ducker.Depth = DSP_Math.MN_to_double_signed(_settings[program_index][counter++].Value, 9, 23);
                PROGRAMS[program_index].ducker.Attack = DSP_Math.value_to_comp_attack(_settings[program_index][counter++].Value);
                PROGRAMS[program_index].ducker.Release = DSP_Math.value_to_comp_release(_settings[program_index][counter++].Value);
                PROGRAMS[program_index].ducker.Bypassed = (_settings[program_index][counter++].Value == 0x00000001);

                // To get the priority channel, we simply see what channel goes into the sidechain of the ducker. This is DUCK_IN_ROUTER_4
                // The router is not zero-based so add 1
                PROGRAMS[program_index].ducker.PriorityChannel = (int)(_settings[program_index][281].Value-1);

                counter = 0;

                // NOTE - WE JUUUUMP!

                // pregain stored 412-415 (int 0 = 0dB, 1 = 20dB, 2 = 40dB
                // input gain is 416-419 and stored in 6.26 format

                for(int in_count = 0; in_count < 4; in_count++)
                {
                    if(_settings[program_index][412+in_count].Value == 0)
                    {
                        PROGRAMS[program_index].inputs[in_count].Type = InputType.Line;
                    } else if(_settings[program_index][412+in_count].Value == 1)
                    {
                        PROGRAMS[program_index].inputs[in_count].Type = InputType.Microphone6;
                    } else
                    {
                        PROGRAMS[program_index].inputs[in_count].Type = InputType.Microphone20;
                    }

                     PROGRAMS[program_index].gains[in_count][0].Gain = DSP_Math.MN_to_double_signed(_settings[program_index][416+in_count].Value,6,26);

                        if (PROGRAMS[program_index].gains[in_count][0].Gain < -90)
                        {
                            PROGRAMS[program_index].gains[in_count][0].Gain = 0;
                            PROGRAMS[program_index].gains[in_count][0].Muted = true;
                        }
                        else
                        {
                            PROGRAMS[program_index].gains[in_count][0].Muted = false;
                        }

                }
                

                if (!form_loaded && CONFIGFILE == "")
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

        public override void LoadProgramConfigToSettings()
        {

            for (int program_index = 0; program_index < NUM_PROGRAMS; program_index++)
            {
                // Input gain
                _settings[program_index][0].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[0][0].Gain, 9, 23);
                _settings[program_index][1].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[1][0].Gain, 9, 23);
                _settings[program_index][2].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[2][0].Gain, 9, 23);
                _settings[program_index][3].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[3][0].Gain, 9, 23);

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

                // Ducker will use 272-285

                // counter is at 272, ready for DUCK features

                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].ducker.Threshold, 9, 23);
                _settings[program_index][counter++].Value = DSP_Math.dynamic_hold_to_value(PROGRAMS[program_index].ducker.Holdtime);
                _settings[program_index][counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].ducker.Depth, 9, 23);
                _settings[program_index][counter++].Value = DSP_Math.comp_attack_to_value(PROGRAMS[program_index].ducker.Attack);
                _settings[program_index][counter++].Value = DSP_Math.comp_release_to_value(PROGRAMS[program_index].ducker.Release);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.Bypassed);

                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterInputs[0]);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterInputs[1]);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterInputs[2]);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterInputs[3]);

                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterOutputs[0]);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterOutputs[1]);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterOutputs[2]);
                _settings[program_index][counter++].Value = Convert.ToUInt32(PROGRAMS[program_index].ducker.RouterOutputs[3]);

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

                int post_plainfilter_counter = 408;

                // Volume Groups
                // TODO - Implement me

                _settings[program_index][post_plainfilter_counter++].Value = 0x00000000;
                _settings[program_index][post_plainfilter_counter++].Value = 0x00000000;
                _settings[program_index][post_plainfilter_counter++].Value = 0x00000000;
                _settings[program_index][post_plainfilter_counter++].Value = 0x00000000;

                // Pregain (Input Type, ENUM)

                _settings[program_index][post_plainfilter_counter++].Value = PROGRAMS[program_index].inputs[0].TypeToValue();
                _settings[program_index][post_plainfilter_counter++].Value = PROGRAMS[program_index].inputs[1].TypeToValue();
                _settings[program_index][post_plainfilter_counter++].Value = PROGRAMS[program_index].inputs[2].TypeToValue();
                _settings[program_index][post_plainfilter_counter++].Value = PROGRAMS[program_index].inputs[3].TypeToValue();

                // Input Gain (gain in 6.26)

                // Input gain
                _settings[program_index][post_plainfilter_counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[0][0].Gain, 6, 26);
                _settings[program_index][post_plainfilter_counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[1][0].Gain, 6, 26);
                _settings[program_index][post_plainfilter_counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[2][0].Gain, 6, 26);
                _settings[program_index][post_plainfilter_counter++].Value = DSP_Math.double_to_MN(PROGRAMS[program_index].gains[3][0].Gain, 6, 26);



            }

        }

#endregion


        #endregion


    }
}