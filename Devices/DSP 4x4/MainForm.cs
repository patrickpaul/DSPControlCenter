using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SA_Resources;
using System.Linq;
using SA_Resources.Configurations;
using SA_Resources.DSP;
using SA_Resources.DSP.Filters;
using SA_Resources.SADevices;
using SA_Resources.SAForms;
using SA_Resources.DSP.Primitives;

namespace DSP_4x4
{
    public partial class MainForm : MainForm_Template
    {

        #region Constructors - Derived

        public MainForm()
            : base("")
        {
            InitializeComponent();
        }

        public MainForm(string configFile = "")
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

        public override void SetConnectionPicture(Image connectionPicture)
        {
            pictureConnectionStatus.BackgroundImage = connectionPicture;
            pictureConnectionStatus.Invalidate();

        }

        #endregion

        #region DefaultSettings

        public override void InitializePrograms()
        {

            for (int p = 0; p < NUM_PROGRAMS; p++)
            {
                PROGRAMS[p] = new ProgramConfig();
            }

            for (int i = 0; i < this.NumPresets(); i++)
            {
                _presetNames.Add("Program " + (i + 1));
            }
        }

        protected override void AttachUIEvents()
        {
            base.AttachUIEvents();

            DSP_PROGRAMS[0].AttachUI_Events(this);

        }

        public override void Initialize_DSP_Programs()
        {
            for (int i = 0; i < 20; i++)
            {
                DSP_PROGRAMS[i] = new DSP_Program_Manager("Program " + i.ToString());
            }
        }

        public override void Initialize_DSP_Meters()
        {
            //DSP_METERS = new DSP
        }

        public override void DefaultSettings()
        {

            
        }

        #endregion    

        #region _settings Read/Load

        public override void LoadSettingsToProgramConfig()
        {
            return;

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
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[m][0].Threshold_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[m][0].SoftKnee_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[m][0].Ratio_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[m][0].Attack_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[m][0].Release_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[m][0].Bypassed_Value;
                }

                // LIMITERS

                for (int n = 0; n < 4; n++)
                {
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[n][1].Threshold_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[n][1].SoftKnee_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[n][1].Ratio_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[n][1].Attack_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[n][1].Release_Value;
                    _settings[program_index][counter++].Value = PROGRAMS[program_index].compressors[n][1].Bypassed_Value;
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

        public override void Default_DSP_Meters()
        {
            try
            {
                DSP_METER_MANAGER.RegisterNewMeter(new DSP_Meter(0xF0C00094, DSP_Primitive_Types.Compressor, 0, 0));
                DSP_METER_MANAGER.RegisterNewMeter(new DSP_Meter(0xF0C00078, DSP_Primitive_Types.Compressor, 0, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION]: " + ex.Message);
            }
        }

        public override void Default_DSP_Programs()
        {
            try
            {
                //Primitive_Manager PManager = new Primitive_Manager();

                /* Gain Blocks */

                DSP_PROGRAMS[0].RegisterNewPrimitive(0, new DSP_Primitive_StandardGain("Input Gain CH 1", 0, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(1, new DSP_Primitive_StandardGain("Input Gain CH 2", 1, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(2, new DSP_Primitive_StandardGain("Input Gain CH 3", 2, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(3, new DSP_Primitive_StandardGain("Input Gain CH 4", 3, 0, StandardGain_Types.Twelve_to_Negative_100));

                DSP_PROGRAMS[0].RegisterNewPrimitive(4, new DSP_Primitive_MixerCrosspoint("Mixer Input 1 - Output 1", 0, 0, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(5, new DSP_Primitive_MixerCrosspoint("Mixer Input 1 - Output 2", 0, 0, 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(6, new DSP_Primitive_MixerCrosspoint("Mixer Input 1 - Output 3", 0, 0, 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(7, new DSP_Primitive_MixerCrosspoint("Mixer Input 1 - Output 4", 0, 0, 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(8, new DSP_Primitive_MixerCrosspoint("Mixer Input 2 - Output 1", 0, 1, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(9, new DSP_Primitive_MixerCrosspoint("Mixer Input 2 - Output 2", 0, 1, 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(10, new DSP_Primitive_MixerCrosspoint("Mixer Input 2 - Output 3", 0, 1, 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(11, new DSP_Primitive_MixerCrosspoint("Mixer Input 2 - Output 4", 0, 1, 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(12, new DSP_Primitive_MixerCrosspoint("Mixer Input 3 - Output 1", 0, 2, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(13, new DSP_Primitive_MixerCrosspoint("Mixer Input 3 - Output 2", 0, 2, 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(14, new DSP_Primitive_MixerCrosspoint("Mixer Input 3 - Output 3", 0, 2, 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(15, new DSP_Primitive_MixerCrosspoint("Mixer Input 3 - Output 4", 0, 2, 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(16, new DSP_Primitive_MixerCrosspoint("Mixer Input 4 - Output 1", 0, 3, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(17, new DSP_Primitive_MixerCrosspoint("Mixer Input 4 - Output 2", 0, 3, 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(18, new DSP_Primitive_MixerCrosspoint("Mixer Input 4 - Output 3", 0, 3, 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(19, new DSP_Primitive_MixerCrosspoint("Mixer Input 4 - Output 4", 0, 3, 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(20, new DSP_Primitive_MixerCrosspoint("Mixer Input 5 - Output 1", 0, 4, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(21, new DSP_Primitive_MixerCrosspoint("Mixer Input 5 - Output 2", 0, 4, 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(22, new DSP_Primitive_MixerCrosspoint("Mixer Input 5 - Output 3", 0, 4, 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(23, new DSP_Primitive_MixerCrosspoint("Mixer Input 5 - Output 4", 0, 4, 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(24, new DSP_Primitive_MixerCrosspoint("Mixer Input 6 - Output 1", 0, 5, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(25, new DSP_Primitive_MixerCrosspoint("Mixer Input 6 - Output 1", 0, 5, 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(26, new DSP_Primitive_MixerCrosspoint("Mixer Input 6 - Output 1", 0, 5, 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(27, new DSP_Primitive_MixerCrosspoint("Mixer Input 6 - Output 1", 0, 5, 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(28, new DSP_Primitive_StandardGain("CH 1 - Pre-Mix Gain", 0, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(29, new DSP_Primitive_StandardGain("CH 2 - Pre-Mix Gain", 1, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(30, new DSP_Primitive_StandardGain("CH 3 - Pre-Mix Gain", 2, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(31, new DSP_Primitive_StandardGain("CH 4 - Pre-Mix Gain", 3, 1, StandardGain_Types.Twelve_to_Negative_100));

                DSP_PROGRAMS[0].RegisterNewPrimitive(32, new DSP_Primitive_StandardGain("CH 1 - Trim", 0, 2, StandardGain_Types.Six_to_Negative_12));
                DSP_PROGRAMS[0].RegisterNewPrimitive(33, new DSP_Primitive_StandardGain("CH 2 - Trim", 1, 2, StandardGain_Types.Six_to_Negative_12));
                DSP_PROGRAMS[0].RegisterNewPrimitive(34, new DSP_Primitive_StandardGain("CH 3 - Trim", 2, 2, StandardGain_Types.Six_to_Negative_12));
                DSP_PROGRAMS[0].RegisterNewPrimitive(35, new DSP_Primitive_StandardGain("CH 4 - Trim", 3, 2, StandardGain_Types.Six_to_Negative_12));

                DSP_PROGRAMS[0].RegisterNewPrimitive(36, new DSP_Primitive_StandardGain("CH 1 - Output Gain", 0, 3, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(37, new DSP_Primitive_StandardGain("CH 2 - Output Gain", 1, 3, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(38, new DSP_Primitive_StandardGain("CH 3 - Output Gain", 2, 3, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[0].RegisterNewPrimitive(39, new DSP_Primitive_StandardGain("CH 4 - Output Gain", 3, 3, StandardGain_Types.Twelve_to_Negative_100));

                DSP_PROGRAMS[0].RegisterNewPrimitive(40, new DSP_Primitive_BiquadFilter("INFILTER_1_1", 1, 0, 300));
                DSP_PROGRAMS[0].RegisterNewPrimitive(45, new DSP_Primitive_BiquadFilter("INFILTER_1_2", 1, 1, 303));
                DSP_PROGRAMS[0].RegisterNewPrimitive(50, new DSP_Primitive_BiquadFilter("INFILTER_1_3", 1, 2, 306));
                DSP_PROGRAMS[0].RegisterNewPrimitive(55, new DSP_Primitive_BiquadFilter("OUTFILTER_1_1", 1, 3, 309));
                DSP_PROGRAMS[0].RegisterNewPrimitive(60, new DSP_Primitive_BiquadFilter("OUTFILTER_1_2", 1, 4, 312));
                DSP_PROGRAMS[0].RegisterNewPrimitive(65, new DSP_Primitive_BiquadFilter("OUTFILTER_1_3", 1, 5, 315));
                DSP_PROGRAMS[0].RegisterNewPrimitive(70, new DSP_Primitive_BiquadFilter("OUTFILTER_1_4", 1, 6, 318));
                DSP_PROGRAMS[0].RegisterNewPrimitive(75, new DSP_Primitive_BiquadFilter("OUTFILTER_1_5", 1, 7, 321));
                DSP_PROGRAMS[0].RegisterNewPrimitive(80, new DSP_Primitive_BiquadFilter("OUTFILTER_1_6", 1, 8, 324));

                DSP_PROGRAMS[0].RegisterNewPrimitive(85, new DSP_Primitive_BiquadFilter("INFILTER_2_1", 2, 0, 327));
                DSP_PROGRAMS[0].RegisterNewPrimitive(90, new DSP_Primitive_BiquadFilter("INFILTER_2_2", 2, 1, 330));
                DSP_PROGRAMS[0].RegisterNewPrimitive(95, new DSP_Primitive_BiquadFilter("INFILTER_2_3", 2, 2, 333));
                DSP_PROGRAMS[0].RegisterNewPrimitive(100, new DSP_Primitive_BiquadFilter("OUTFILTER_2_1", 2, 3, 336));
                DSP_PROGRAMS[0].RegisterNewPrimitive(105, new DSP_Primitive_BiquadFilter("OUTFILTER_2_2", 2, 4, 339));
                DSP_PROGRAMS[0].RegisterNewPrimitive(110, new DSP_Primitive_BiquadFilter("OUTFILTER_2_3", 2, 5, 342));
                DSP_PROGRAMS[0].RegisterNewPrimitive(115, new DSP_Primitive_BiquadFilter("OUTFILTER_2_4", 2, 6, 345));
                DSP_PROGRAMS[0].RegisterNewPrimitive(120, new DSP_Primitive_BiquadFilter("OUTFILTER_2_5", 2, 7, 348));
                DSP_PROGRAMS[0].RegisterNewPrimitive(125, new DSP_Primitive_BiquadFilter("OUTFILTER_2_6", 2, 8, 351));

                DSP_PROGRAMS[0].RegisterNewPrimitive(130, new DSP_Primitive_BiquadFilter("INFILTER_3_1", 3, 0, 354));
                DSP_PROGRAMS[0].RegisterNewPrimitive(135, new DSP_Primitive_BiquadFilter("INFILTER_3_2", 3, 1, 357));
                DSP_PROGRAMS[0].RegisterNewPrimitive(140, new DSP_Primitive_BiquadFilter("INFILTER_3_3", 3, 2, 360));
                DSP_PROGRAMS[0].RegisterNewPrimitive(145, new DSP_Primitive_BiquadFilter("OUTFILTER_3_1", 3, 3, 363));
                DSP_PROGRAMS[0].RegisterNewPrimitive(150, new DSP_Primitive_BiquadFilter("OUTFILTER_3_2", 3, 4, 366));
                DSP_PROGRAMS[0].RegisterNewPrimitive(155, new DSP_Primitive_BiquadFilter("OUTFILTER_3_3", 3, 5, 369));
                DSP_PROGRAMS[0].RegisterNewPrimitive(160, new DSP_Primitive_BiquadFilter("OUTFILTER_3_4", 3, 6, 372));
                DSP_PROGRAMS[0].RegisterNewPrimitive(165, new DSP_Primitive_BiquadFilter("OUTFILTER_3_5", 3, 7, 375));
                DSP_PROGRAMS[0].RegisterNewPrimitive(170, new DSP_Primitive_BiquadFilter("OUTFILTER_3_6", 3, 8, 378));

                DSP_PROGRAMS[0].RegisterNewPrimitive(175, new DSP_Primitive_BiquadFilter("INFILTER_4_1", 4, 0, 381));
                DSP_PROGRAMS[0].RegisterNewPrimitive(180, new DSP_Primitive_BiquadFilter("INFILTER_4_2", 4, 1, 384));
                DSP_PROGRAMS[0].RegisterNewPrimitive(185, new DSP_Primitive_BiquadFilter("INFILTER_4_3", 4, 2, 387));
                DSP_PROGRAMS[0].RegisterNewPrimitive(190, new DSP_Primitive_BiquadFilter("OUTFILTER_4_1", 4, 3, 390));
                DSP_PROGRAMS[0].RegisterNewPrimitive(195, new DSP_Primitive_BiquadFilter("OUTFILTER_4_2", 4, 4, 393));
                DSP_PROGRAMS[0].RegisterNewPrimitive(200, new DSP_Primitive_BiquadFilter("OUTFILTER_4_3", 4, 5, 396));
                DSP_PROGRAMS[0].RegisterNewPrimitive(205, new DSP_Primitive_BiquadFilter("OUTFILTER_4_4", 4, 6, 399));
                DSP_PROGRAMS[0].RegisterNewPrimitive(210, new DSP_Primitive_BiquadFilter("OUTFILTER_4_5", 4, 7, 402));
                DSP_PROGRAMS[0].RegisterNewPrimitive(215, new DSP_Primitive_BiquadFilter("OUTFILTER_4_6", 4, 8, 405));


                DSP_PROGRAMS[0].RegisterNewPrimitive(220, new DSP_Primitive_Compressor("CH 1 - Compressor", 0, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(226, new DSP_Primitive_Compressor("CH 2 - Compressor", 1, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(232, new DSP_Primitive_Compressor("CH 3 - Compressor", 2, 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(238, new DSP_Primitive_Compressor("CH 4 - Compressor", 3, 0));

                DSP_PROGRAMS[0].RegisterNewPrimitive(244, new DSP_Primitive_Compressor("CH 1 - Limiter", 0, 1, DSP_Primitive_Types.Limiter));
                DSP_PROGRAMS[0].RegisterNewPrimitive(250, new DSP_Primitive_Compressor("CH 2 - Limiter", 1, 1, DSP_Primitive_Types.Limiter));
                DSP_PROGRAMS[0].RegisterNewPrimitive(256, new DSP_Primitive_Compressor("CH 3 - Limiter", 2, 1, DSP_Primitive_Types.Limiter));
                DSP_PROGRAMS[0].RegisterNewPrimitive(262, new DSP_Primitive_Compressor("CH 4 - Limiter", 3, 1, DSP_Primitive_Types.Limiter));

                DSP_PROGRAMS[0].RegisterNewPrimitive(268, new DSP_Primitive_Delay("CH 1 - Delay", 0));
                DSP_PROGRAMS[0].RegisterNewPrimitive(270, new DSP_Primitive_Delay("CH 2 - Delay", 1));
                DSP_PROGRAMS[0].RegisterNewPrimitive(272, new DSP_Primitive_Delay("CH 3 - Delay", 2));
                DSP_PROGRAMS[0].RegisterNewPrimitive(274, new DSP_Primitive_Delay("CH 4 - Delay", 3));

                DSP_PROGRAMS[0].RegisterNewPrimitive(400, new DSP_Primitive_Input("Local Input CH 1", 0, 0, 1000, 900, "Local Input #1", InputType.Line, false));
                DSP_PROGRAMS[0].RegisterNewPrimitive(410, new DSP_Primitive_Input("Local Input CH 2", 1, 0, 1000, 900, "Local Input #2", InputType.Line, false));
                DSP_PROGRAMS[0].RegisterNewPrimitive(420, new DSP_Primitive_Input("Local Input CH 3", 2, 0, 1000, 900, "Local Input #3", InputType.Line, false));
                DSP_PROGRAMS[0].RegisterNewPrimitive(430, new DSP_Primitive_Input("Local Input CH 4", 3, 0, 1000, 900, "Local Input #4", InputType.Line, false));

            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION]: " + ex.Message);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            using(FilterDesignerForm fdForm = new FilterDesignerForm())
            {
                fdForm.ShowDialog();
            }
        }
    }
}