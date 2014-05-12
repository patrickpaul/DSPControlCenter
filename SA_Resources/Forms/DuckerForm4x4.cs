using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    public partial class DuckerForm4x4 : Form
    {

        /* Hides the [X] in the window */
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | 0x200;
                return myCp;
            }
        }

        private MainForm_Template PARENT_FORM; 
        
        private Dial ReleaseDial, AttackDial, HoldDial;

        private double read_gain_value = 0;
        private int cur_meter;

        private bool flagSwitchingPriorityOrder = false;

        private bool form_loaded = false;

        private DSP_Primitive_Ducker4x4 RecastDucker;

        private List<bool> BypassCache = new List<bool>();
        private int[] chkOrderCache = new int[3];

        public DuckerForm4x4(MainForm_Template _parentForm, DSP_Primitive _inputPrimitive)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            try
            {
                dropPriorityChannel.Items.Clear();
                dropPriorityChannel.Text = "";


                RecastDucker = (DSP_Primitive_Ducker4x4) _inputPrimitive;


                for (int i = 0; i < PARENT_FORM.GetNumInputChannels(); i++)
                {
                    BypassCache.Add(RecastDucker.CH_Bypasses[i]);
                    dropPriorityChannel.Items.Add(((DSP_Primitive_Input)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Input, i, 0)).InputName);
                }

                dropPriorityChannel.SelectedIndex = RecastDucker.PriorityChannel;
                dropPriorityChannel.Invalidate();



                nudDuckThreshold.Value = (decimal)RecastDucker.Threshold;
                nudDuckDepth.Value = (decimal) RecastDucker.Depth;

                HoldDial = new Dial(TextDuckHold, DialDuckHold, new double[] { 0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0 },
                         DialHelpers.Format_String_Duck_Hold, Images.knob_green_bg, Images.knob_green_line);

                HoldDial.Value = RecastDucker.HoldTime;
                HoldDial.OnChange += new DialEventHandler(this.HoldDial_OnChange);

                AttackDial = new Dial(TextDuckAttack, DialDuckAttack, new double[] { 0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0 },
                         DialHelpers.Format_String_Comp_Attack, Images.knob_blue_bg, Images.knob_blue_line);

                AttackDial.Value = RecastDucker.Attack;
                AttackDial.OnChange += new DialEventHandler(this.AttackDial_OnChange);


                ReleaseDial = new Dial(TextDuckRelease, DialDuckRelease, new double[] {0.010, 0.038, 0.150, 0.530, 1.250, 7.0, 30.0},
                         DialHelpers.Format_String_Comp_Release, Images.knob_orange_bg, Images.knob_orange_line);

                ReleaseDial.Value = RecastDucker.Release;
                ReleaseDial.OnChange += new DialEventHandler(this.ReleaseDial_OnChange);

                chkBypass.Checked = RecastDucker.Bypassed;



                if (_parentForm.LIVE_MODE && _parentForm._PIC_Conn.isOpen)
                {
                    signalTimer.Enabled = true;
                    //gainMeterIn.Visible = true;
                    //gainMeterOut.Visible = true;
                } else
                {
                    signalTimer.Enabled = false;
                    //gainMeterIn.Visible = false;
                    //gainMeterOut.Visible = false;
                }

            } catch (Exception ex)
            {
                Console.WriteLine("[Exception in DuckerForm4x4]: " + ex.Message);
            }

            form_loaded = true;


        }

        private void DuckerForm_Load(object sender, EventArgs e)
        {
        }

        private void HoldDial_OnChange(object sender, DialEventArgs e)
        {

            RecastDucker.HoldTime = HoldDial.Value;
            RecastDucker.QueueChangeByOffset(PARENT_FORM,17);
        }
        
        private void ReleaseDial_OnChange(object sender, DialEventArgs e)
        {

            RecastDucker.Release = ReleaseDial.Value;
            RecastDucker.QueueChangeByOffset(PARENT_FORM, 20);
        }

        private void AttackDial_OnChange(object sender, DialEventArgs e)
        {

            RecastDucker.Attack = AttackDial.Value;
            RecastDucker.QueueChangeByOffset(PARENT_FORM, 19);
        }


        private void nudDuckDepth_ValueChanged(object sender, EventArgs e)
        {

            if (!form_loaded)
            {
                return;
            } 

            RecastDucker.Depth = (double)nudDuckDepth.Value;
            RecastDucker.QueueChangeByOffset(PARENT_FORM, 18);
        }

        private void nudDuckThreshold_ValueChanged(object sender, EventArgs e)
        {

            if (!form_loaded)
            {
                return;
            } 

            RecastDucker.Threshold = (double)nudDuckThreshold.Value;
            RecastDucker.QueueChangeByOffset(PARENT_FORM, 16);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoutine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRoutine();
        }

        private void SaveRoutine()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void CancelRoutine()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {

            RecastDucker.Bypassed = chkBypass.Checked;
            RecastDucker.QueueChangeByOffset(PARENT_FORM, 21);
            
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            UInt32 read_address = 0x00000000;

            double offset = (20 - 20 + 3.8) + 10 * Math.Log10(2) + 20 * Math.Log10(16);

            UInt32 read_value = 0x00000000;
            double converted_value = 0;


            cur_meter++;
            if (cur_meter == 4)
            {
                cur_meter = 0;
            }

            SignalMeter_Small curMeter = ((SignalMeter_Small)Controls.Find("duckMeter" + (cur_meter + 1), true).First());

            read_address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Ducker4x4, cur_meter,0).Address;


            read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);
            converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);
            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);
            }
            else
            {
                read_gain_value = -100;
            }

            curMeter.DB = read_gain_value;
            curMeter.Refresh();
        }

        private void dropPriorityChannel_SelectedIndexChanged(object sender, EventArgs e)
        {

            flagSwitchingPriorityOrder = true;

            int label_counter = 0;

            DSP_Primitive_Input Temp_Primitive = null;
            Label temp_label = null;

            for (int i = 0; i < PARENT_FORM.GetNumInputChannels(); i++)
            {
                if (i != dropPriorityChannel.SelectedIndex)
                {
                    PictureCheckbox temp_checkbox = ((PictureCheckbox)Controls.Find("chkBypass" + label_counter.ToString(), true).FirstOrDefault());
                    temp_checkbox.Checked = !BypassCache[i];

                    Temp_Primitive = ((DSP_Primitive_Input)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Input, i, 0));
                    temp_label = ((Label)Controls.Find("lblDuckInput" + label_counter.ToString(), true).FirstOrDefault());
                    temp_label.Text = Temp_Primitive.InputName;
                    temp_label.Invalidate();
                    

                    chkOrderCache[label_counter] = i;

                    label_counter++;
                }
            }

            flagSwitchingPriorityOrder = false;

            if (!form_loaded)
            {
                return;
            } 
            
            RecastDucker.PriorityChannel = dropPriorityChannel.SelectedIndex;

            if (PARENT_FORM.LIVE_MODE)
            {
                RecastDucker.QueueChange(PARENT_FORM);
            }

            

        }

        private void chkChannelBypass_CheckedChanged(object sender, EventArgs e)
        {

            if (flagSwitchingPriorityOrder)
            {
                return;
            }

            PictureCheckbox senderCheckbox = (PictureCheckbox) sender;

            int checkIndex = int.Parse(senderCheckbox.Name.Substring(9, 1));

            int ch_num = chkOrderCache[checkIndex];

            RecastDucker.SetChannelBypass(ch_num,!(senderCheckbox.Checked));
            BypassCache[ch_num] = !senderCheckbox.Checked;

            RecastDucker.QueueChange(PARENT_FORM);
        }

        private void signalMeter_Small1_Click(object sender, EventArgs e)
        {

        }
    }
}
