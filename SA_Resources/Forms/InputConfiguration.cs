using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using SA_Resources;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class InputConfiguration : Form
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

        private bool form_loaded = false;

        private MainForm_Template PARENT_FORM;
        private int CH_NUMBER;
        private double read_gain_value;

        public InputConfiguration(MainForm_Template _parentForm, int _channelNumber, bool show_phantom = true)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;
            CH_NUMBER = _channelNumber;

            this.Text = "CH " + CH_NUMBER.ToString("N0") + " - Input Configuration";

            txtDisplayName.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER-1].Name;

            chkPhantomPower.Checked = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].PhantomPower;
            chkPhantomPower.Invalidate();

            if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Type == InputType.Line)
            {
                dropInputType.SelectedIndex = 0;
            }
            else if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Type == InputType.Microphone6)
            {
                dropInputType.SelectedIndex = 1;
            } else
            {
                dropInputType.SelectedIndex = 2;
            }

            dropInputType.Invalidate();

            if(!show_phantom)
            {
                lblPhantomPower.Visible = false;
                chkPhantomPower.Visible = false;
            }

            if (PARENT_FORM.LIVE_MODE)
            {
                gainMeter.Visible = true;
                signalTimer.Enabled = true;
            }
            else
            {
                gainMeter.Visible = false;
                signalTimer.Enabled = false;
            }
            

            form_loaded = true;
        }

        private void InputConfiguration_Load(object sender, EventArgs e)
        {
            txtDisplayName.SelectAll();
        }

        

        private void dropInputType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(!form_loaded)
            {
                return;
            }

            if(dropInputType.SelectedIndex == 0)
            {
                chkPhantomPower.Checked = false;
            }

            if(dropInputType.SelectedIndex == 0)
            {
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Type = InputType.Line;
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_NUMBER - 1] = 0;
            } else if(dropInputType.SelectedIndex == 1)
            {
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Type = InputType.Microphone6;
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_NUMBER - 1] = 6;

            } else
            {
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Type = InputType.Microphone20;
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_NUMBER - 1] = 20;
            }

            if (PARENT_FORM.LIVE_MODE)
            {
                UInt32 new_val =
                    DSP_Math.double_to_MN(
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_NUMBER - 1] +
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].gains[CH_NUMBER - 1][0].Gain, 9, 23);

                PARENT_FORM.AddItemToQueue(new LiveQueueItem((0 + CH_NUMBER - 1), new_val));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem((412 + CH_NUMBER - 1), (uint) dropInputType.SelectedIndex));
            }

        }

        private void chkPhantomPower_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loaded)
            {
                return;
            }

            if (chkPhantomPower.Checked == true && MessageBox.Show("Are you sure you wish to enable phantom power on this channel? \nDoing so can damage attached devices not intended to receive phantom power.","Phantom Power Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                chkPhantomPower.Checked = false;
            }

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].PhantomPower = chkPhantomPower.Checked;

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(1000 + (CH_NUMBER - 1), PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].PhantomAsInt()));                

        }

        private void txtDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                e.Handled = true;

                SaveRoutine();
                return;
            }

            string allowedCharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=.:'/\\- \b\n";
            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {
                // Good!
                return;
            }
            else
            {
                // Skip the car
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void InputConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Name = txtDisplayName.Text;
        }

        public bool input_switcher = false;

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            UInt32 read_address;

            read_address = PARENT_FORM._gain_meters[CH_NUMBER - 1][0];

            /*
            if (input_switcher)
            {
                read_address = 0xF0C00005;
            }
            else
            {
                read_address = 0xF0C00077;

            }
            */
            double offset = 20 + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);

            
            double converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);


            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);
            }
            else
            {
                read_gain_value = -100;
            }

            read_gain_value = offset + 10 * Math.Log10(converted_value);
            /*
            if(input_switcher)
            {
                textBox1.Text = read_gain_value.ToString("F1");
                Console.WriteLine("INPUT Read read_value = " + read_value + ", Converted Value = " + converted_value + ", DB Value = " + read_gain_value);
            
            } else
            {
                textBox2.Text = read_gain_value.ToString("F1");
                Console.WriteLine("AFTERGAIN Read read_value = " + read_value + ", Converted Value = " + converted_value + ", DB Value = " + read_gain_value);
            
            }
             * */
            gainMeter.DB = read_gain_value;

            input_switcher = !input_switcher;
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

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
