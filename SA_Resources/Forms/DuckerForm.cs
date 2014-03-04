using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_Resources.DSP;
using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    public partial class DuckerForm : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | 0x200;
                return myCp;
            }
        }

        private Dial ReleaseDial, AttackDial, HoldDial;

        private MainForm_Template PARENT_FORM;


        double read_gain_value;

        private int ADDR_THRESHOLD;
        private int ADDR_HOLD;
        private int ADDR_DEPTH;
        private int ADDR_ATTACK;
        private int ADDR_RELEASE;
        private int ADDR_BYPASS;

        private int cur_meter;

        private bool form_loaded = false;
        public DuckerForm(MainForm_Template _parentForm, int _settings_offset)
        {
            InitializeComponent();

            ADDR_THRESHOLD = _settings_offset;
            ADDR_HOLD = _settings_offset+1;
            ADDR_DEPTH = _settings_offset+2;
            ADDR_ATTACK = _settings_offset+3;
            ADDR_RELEASE = _settings_offset+4;
            ADDR_BYPASS = _settings_offset+5;

            PARENT_FORM = _parentForm;

            try
            {
                dropPriorityChannel.Items.Clear();
                dropPriorityChannel.Text = "";

                for(int i = 0; i < PARENT_FORM.GetNumInputChannels(); i++)
                {
                    dropPriorityChannel.Items.Add(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[i].Name);
                }


                nudDuckThreshold.Value = (decimal)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Threshold;
                nudDuckDepth.Value = (decimal)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Depth;

                dropPriorityChannel.SelectedIndex = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.PriorityChannel;
                dropPriorityChannel.Invalidate();

                HoldDial = new Dial(TextDuckHold, DialDuckHold, new double[] { 0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0 },
                         DialHelpers.Format_String_Duck_Hold, Images.knob_green_bg, Images.knob_green_line);

                HoldDial.Value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Holdtime;
                HoldDial.OnChange += new DialEventHandler(this.HoldDial_OnChange);

                AttackDial = new Dial(TextDuckAttack, DialDuckAttack, new double[] { 0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0 },
                         DialHelpers.Format_String_Comp_Attack, Images.knob_blue_bg, Images.knob_blue_line);

                AttackDial.Value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Attack;
                AttackDial.OnChange += new DialEventHandler(this.AttackDial_OnChange);


                ReleaseDial = new Dial(TextDuckRelease, DialDuckRelease, new double[] {0.010, 0.038, 0.150, 0.530, 1.250, 7.0, 30.0},
                         DialHelpers.Format_String_Comp_Release, Images.knob_orange_bg, Images.knob_orange_line);
                ReleaseDial.Value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Release;
                ReleaseDial.OnChange += new DialEventHandler(this.ReleaseDial_OnChange);

                chkBypass.Checked = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Bypassed;



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
                Console.WriteLine(ex.Message);
            }

            form_loaded = true;


        }

        private void HoldDial_OnChange(object sender, DialEventArgs e)
        {

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Holdtime = HoldDial.Value;
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_HOLD, DSP_Math.dynamic_hold_to_value(HoldDial.Value)));
        }
        
        private void ReleaseDial_OnChange(object sender, DialEventArgs e)
        {

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Release = ReleaseDial.Value;
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RELEASE, DSP_Math.comp_release_to_value(ReleaseDial.Value))); 
        }

        private void AttackDial_OnChange(object sender, DialEventArgs e)
        {

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Attack = AttackDial.Value;
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_ATTACK, DSP_Math.comp_attack_to_value(AttackDial.Value))); 
        }


        private void nudDuckDepth_ValueChanged(object sender, EventArgs e)
        {

            if (!form_loaded)
            {
                return;
            } 

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Depth = (double)nudDuckDepth.Value;

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_DEPTH, DSP_Math.double_to_MN((double)nudDuckDepth.Value, 9, 23)));
        }

        private void nudDuckThreshold_ValueChanged(object sender, EventArgs e)
        {

            if (!form_loaded)
            {
                return;
            } 

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Threshold = (double)nudDuckThreshold.Value;

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_THRESHOLD, DSP_Math.double_to_MN((double)nudDuckThreshold.Value, 9, 23)));
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

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.Bypassed = chkBypass.Checked;

            if (chkBypass.Checked)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, 0x00000001)); 
            }
            else
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, 0x00000000)); 
            }
            
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            UInt32 read_address = 0x00000000;
            double offset = 20 + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_value = 0x00000000;
            double converted_value = 0;


            cur_meter++;
            if (cur_meter == 4)
            {
                cur_meter = 0;
            }

            SignalMeter_Small curMeter = ((SignalMeter_Small)Controls.Find("meter" + (cur_meter + 1), true).First());

            read_address = PARENT_FORM._ducker_meters[cur_meter];


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

            if (!form_loaded)
            {
                return;
            } 
            
            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.PriorityChannel = dropPriorityChannel.SelectedIndex;

            if (PARENT_FORM.LIVE_MODE)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(278, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterInputs[0]));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(279, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterInputs[1]));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(280, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterInputs[2]));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(281, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterInputs[3]));

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(282, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterOutputs[0]));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(283, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterOutputs[1]));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(284, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterOutputs[2]));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(285, (uint) PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].ducker.RouterOutputs[3]));
            }

        }

        


    }
}
