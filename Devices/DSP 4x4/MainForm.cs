﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SA_Resources;
using System.Linq;
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

        public override int GetNumPresets()
        {
            return 10;
        }

        public override void SetConnectionPicture(Image connectionPicture)
        {
            pictureConnectionStatus.BackgroundImage = connectionPicture;
            pictureConnectionStatus.Invalidate();

        }

        #endregion

        #region DefaultSettings

        protected override void AttachUIEvents()
        {
            base.AttachUIEvents();

            DSP_PROGRAMS[0].AttachUI_Events(this);

        }

        public override void Initialize_DSP_Programs()
        {
            for (int i = 0; i < this.GetNumPresets(); i++)
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


        public override void Default_DSP_Meters()
        {
            try
            {
                DSP_METER_MANAGER.RegisterNewMeter(new DSP_Meter(0xF0C00094, DSP_Primitive_Types.Compressor, 0, 0));
                DSP_METER_MANAGER.RegisterNewMeter(new DSP_Meter(0xF0C00078, DSP_Primitive_Types.Compressor, 0, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION in Default_DSP_Meters]: " + ex.Message);
            }
        }

        public override void Single_Default_DSP_Program(int program_index = 0)
        {
            try {
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(0, new DSP_Primitive_StandardGain("Input Gain CH 1", 0, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(2, new DSP_Primitive_StandardGain("Input Gain CH 2", 1, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(4, new DSP_Primitive_StandardGain("Input Gain CH 3", 2, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(6, new DSP_Primitive_StandardGain("Input Gain CH 4", 3, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(8, new DSP_Primitive_StandardGain("Input Gain CH 5", 4, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(10, new DSP_Primitive_StandardGain("Input Gain CH 6", 5, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(12, new DSP_Primitive_StandardGain("Input Gain CH 7", 6, 0, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(14, new DSP_Primitive_StandardGain("Input Gain CH 8", 7, 0, StandardGain_Types.Twelve_to_Negative_100));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(16, new DSP_Primitive_StandardGain("CH 1 - Pre-Mix Gain", 0, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(18, new DSP_Primitive_StandardGain("CH 2 - Pre-Mix Gain", 1, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(20, new DSP_Primitive_StandardGain("CH 3 - Pre-Mix Gain", 2, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(22, new DSP_Primitive_StandardGain("CH 4 - Pre-Mix Gain", 3, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(24, new DSP_Primitive_StandardGain("CH 5 - Pre-Mix Gain", 4, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(26, new DSP_Primitive_StandardGain("CH 6 - Pre-Mix Gain", 5, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(28, new DSP_Primitive_StandardGain("CH 7 - Pre-Mix Gain", 6, 1, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(30, new DSP_Primitive_StandardGain("CH 8 - Pre-Mix Gain", 7, 1, StandardGain_Types.Twelve_to_Negative_100));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(32, new DSP_Primitive_StandardGain("CH 1 - Trim", 0, 2, StandardGain_Types.Six_to_Negative_12));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(34, new DSP_Primitive_StandardGain("CH 2 - Trim", 1, 2, StandardGain_Types.Six_to_Negative_12));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(36, new DSP_Primitive_StandardGain("CH 3 - Trim", 2, 2, StandardGain_Types.Six_to_Negative_12));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(38, new DSP_Primitive_StandardGain("CH 4 - Trim", 3, 2, StandardGain_Types.Six_to_Negative_12));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(40, new DSP_Primitive_StandardGain("CH 1 - Output Gain", 0, 3, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(42, new DSP_Primitive_StandardGain("CH 2 - Output Gain", 1, 3, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(44, new DSP_Primitive_StandardGain("CH 3 - Output Gain", 2, 3, StandardGain_Types.Twelve_to_Negative_100));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(46, new DSP_Primitive_StandardGain("CH 4 - Output Gain", 3, 3, StandardGain_Types.Twelve_to_Negative_100));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(48, new DSP_Primitive_Ducker4x4("Ducker 4x4", 0, 0));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(70, new DSP_Primitive_BiquadFilter("INFILTER_1_1", 0, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(75, new DSP_Primitive_BiquadFilter("INFILTER_1_2", 0, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(80, new DSP_Primitive_BiquadFilter("INFILTER_1_3", 0, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(85, new DSP_Primitive_BiquadFilter("INFILTER_2_1", 1, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(90, new DSP_Primitive_BiquadFilter("INFILTER_2_2", 1, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(95, new DSP_Primitive_BiquadFilter("INFILTER_2_3", 1, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(100, new DSP_Primitive_BiquadFilter("INFILTER_3_1", 2, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(105, new DSP_Primitive_BiquadFilter("INFILTER_3_2", 2, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(110, new DSP_Primitive_BiquadFilter("INFILTER_3_3", 2, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(115, new DSP_Primitive_BiquadFilter("INFILTER_4_1", 3, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(120, new DSP_Primitive_BiquadFilter("INFILTER_4_2", 3, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(125, new DSP_Primitive_BiquadFilter("INFILTER_4_3", 3, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(130, new DSP_Primitive_BiquadFilter("INFILTER_5_1", 4, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(135, new DSP_Primitive_BiquadFilter("INFILTER_5_2", 4, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(140, new DSP_Primitive_BiquadFilter("INFILTER_5_3", 4, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(145, new DSP_Primitive_BiquadFilter("INFILTER_6_1", 5, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(150, new DSP_Primitive_BiquadFilter("INFILTER_6_2", 5, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(155, new DSP_Primitive_BiquadFilter("INFILTER_6_3", 5, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(160, new DSP_Primitive_BiquadFilter("INFILTER_7_1", 6, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(165, new DSP_Primitive_BiquadFilter("INFILTER_7_2", 6, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(170, new DSP_Primitive_BiquadFilter("INFILTER_7_3", 6, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(175, new DSP_Primitive_BiquadFilter("INFILTER_8_1", 7, 0, 300));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(180, new DSP_Primitive_BiquadFilter("INFILTER_8_2", 7, 1, 303));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(185, new DSP_Primitive_BiquadFilter("INFILTER_8_3", 7, 2, 306));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(190, new DSP_Primitive_Compressor("CH 1 - Compressor", 0, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(196, new DSP_Primitive_Compressor("CH 2 - Compressor", 1, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(202, new DSP_Primitive_Compressor("CH 3 - Compressor", 2, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(208, new DSP_Primitive_Compressor("CH 4 - Compressor", 3, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(214, new DSP_Primitive_Compressor("CH 5 - Compressor", 4, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(220, new DSP_Primitive_Compressor("CH 6 - Compressor", 5, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(226, new DSP_Primitive_Compressor("CH 7 - Compressor", 6, 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(232, new DSP_Primitive_Compressor("CH 8 - Compressor", 7, 0));

                int index_counter = 238;
                int crosspoint_gain = 0;

                for (int in_channel = 0; in_channel < 10; in_channel++)
                {
                    for (int out_channel = 0; out_channel < 8; out_channel++)
                    {
                        crosspoint_gain = ((in_channel == out_channel) && (in_channel < 8)) ? 0 : -100;

                        DSP_PROGRAMS[program_index].RegisterNewPrimitive(index_counter++, new DSP_Primitive_MixerCrosspoint("Mixer Input " + (in_channel + 1) + " - Output " + (out_channel + 1) + "", in_channel, out_channel, crosspoint_gain));
                    }
                }

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(318, new DSP_Primitive_BiquadFilter("OUTFILTER_1_1", 0, 3, 309));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(323, new DSP_Primitive_BiquadFilter("OUTFILTER_1_2", 0, 4, 312));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(328, new DSP_Primitive_BiquadFilter("OUTFILTER_1_3", 0, 5, 315));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(333, new DSP_Primitive_BiquadFilter("OUTFILTER_1_4", 0, 6, 318));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(338, new DSP_Primitive_BiquadFilter("OUTFILTER_1_5", 0, 7, 321));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(343, new DSP_Primitive_BiquadFilter("OUTFILTER_1_6", 0, 8, 324));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(348, new DSP_Primitive_BiquadFilter("OUTFILTER_2_1", 1, 3, 309));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(353, new DSP_Primitive_BiquadFilter("OUTFILTER_2_2", 1, 4, 312));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(358, new DSP_Primitive_BiquadFilter("OUTFILTER_2_3", 1, 5, 315));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(363, new DSP_Primitive_BiquadFilter("OUTFILTER_2_4", 1, 6, 318));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(368, new DSP_Primitive_BiquadFilter("OUTFILTER_2_5", 1, 7, 321));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(373, new DSP_Primitive_BiquadFilter("OUTFILTER_2_6", 1, 8, 324));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(378, new DSP_Primitive_BiquadFilter("OUTFILTER_3_1", 2, 3, 309));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(383, new DSP_Primitive_BiquadFilter("OUTFILTER_3_2", 2, 4, 312));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(388, new DSP_Primitive_BiquadFilter("OUTFILTER_3_3", 2, 5, 315));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(393, new DSP_Primitive_BiquadFilter("OUTFILTER_3_4", 2, 6, 318));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(398, new DSP_Primitive_BiquadFilter("OUTFILTER_3_5", 2, 7, 321));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(403, new DSP_Primitive_BiquadFilter("OUTFILTER_3_6", 2, 8, 324));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(408, new DSP_Primitive_BiquadFilter("OUTFILTER_4_1", 3, 3, 309));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(413, new DSP_Primitive_BiquadFilter("OUTFILTER_4_2", 3, 4, 312));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(418, new DSP_Primitive_BiquadFilter("OUTFILTER_4_3", 3, 5, 315));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(423, new DSP_Primitive_BiquadFilter("OUTFILTER_4_4", 3, 6, 318));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(428, new DSP_Primitive_BiquadFilter("OUTFILTER_4_5", 3, 7, 321));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(433, new DSP_Primitive_BiquadFilter("OUTFILTER_4_6", 3, 8, 324));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(438, new DSP_Primitive_Compressor("CH 1 - Limiter", 0, 1, DSP_Primitive_Types.Limiter));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(444, new DSP_Primitive_Compressor("CH 2 - Limiter", 1, 1, DSP_Primitive_Types.Limiter));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(450, new DSP_Primitive_Compressor("CH 3 - Limiter", 2, 1, DSP_Primitive_Types.Limiter));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(456, new DSP_Primitive_Compressor("CH 4 - Limiter", 3, 1, DSP_Primitive_Types.Limiter));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(462, new DSP_Primitive_Delay("CH 1 - Delay", 0));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(464, new DSP_Primitive_Delay("CH 2 - Delay", 1));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(466, new DSP_Primitive_Delay("CH 3 - Delay", 2));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(468, new DSP_Primitive_Delay("CH 4 - Delay", 3));

                

                
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1000, new DSP_Primitive_Input("Local Input CH 1", 0, 0, 1000, 900, "Local Input #1", InputType.Line, false));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1010, new DSP_Primitive_Input("Local Input CH 2", 1, 0, 1000, 900, "Local Input #2", InputType.Line, false));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1020, new DSP_Primitive_Input("Local Input CH 3", 2, 0, 1000, 900, "Local Input #3", InputType.Line, false));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1030, new DSP_Primitive_Input("Local Input CH 4", 3, 0, 1000, 900, "Local Input #4", InputType.Line, false));

                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1040, new DSP_Primitive_Output("Output CH 1", 0, 0, "Output #1"));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1050, new DSP_Primitive_Output("Output CH 1", 1, 0, "Output #2"));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1060, new DSP_Primitive_Output("Output CH 1", 2, 0, "Output #3"));
                DSP_PROGRAMS[program_index].RegisterNewPrimitive(1070, new DSP_Primitive_Output("Output CH 1", 3, 0, "Output #4"));
               

            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION in Single_Default_DSP_Program]: " + ex.Message);
            }
        }

        public override void Default_DSP_Programs()
        {
            try
            {

                for (int i = 0; i < this.GetNumPresets(); i++)
                {
                    Single_Default_DSP_Program(i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION in Default_DSP_Programs]: " + ex.Message);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            using (FilterDesignerForm fdForm = new FilterDesignerForm(this, 3, 0, 0))
            {
                fdForm.ShowDialog();
            }
        }
    }
}