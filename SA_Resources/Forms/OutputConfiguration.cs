using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class OutputConfiguration : Form
    {
        private MainForm_Template PARENT_FORM;
        private int CH_NUMBER;

        public OutputConfiguration(MainForm_Template _parentForm, int _ch_number)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;
            CH_NUMBER = _ch_number;
            

            this.Text = "CH " + CH_NUMBER.ToString("N0") + " - Output Configuration";

            txtDisplayName.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].outputs[CH_NUMBER-1].Name;

            if (_parentForm.LIVE_MODE && _parentForm._PIC_Conn.isOpen)
            {
                signalTimer.Enabled = true;
                gainMeterOut.Visible = true;
            }
            else
            {
                gainMeterOut.Visible = false;
            }
        }

        private void OutputConfiguration_Load(object sender, EventArgs e)
        {
            txtDisplayName.SelectAll();
        }

        private void SaveRoutine()
        {

            // Temporary...
            this.Close();


        }

        private void CancelRoutine()
        {
            // Temporary
            this.Close();

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoutine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRoutine();
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

        private void OutputConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].outputs[CH_NUMBER - 1].Name = txtDisplayName.Text;
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            if (!PARENT_FORM._PIC_Conn.isOpen || !PARENT_FORM.LIVE_MODE)
            {
                signalTimer.Enabled = false;
                return;
            }

            UInt32 read_value;
            double converted_value;
            double offset = 20 + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_address =  read_address = PARENT_FORM._gain_meters[CH_NUMBER - 1][3];
            double read_gain_value;

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
            
            gainMeterOut.DB = read_gain_value;
        }

        private void pbtnMute_Click(object sender, EventArgs e)
        {
            PARENT_FORM._PIC_Conn.SetRS232Mute(CH_NUMBER,2);
            bool mute_status = PARENT_FORM._PIC_Conn.ReadRS232Mute(CH_NUMBER);

            if(mute_status == true)
            {
                lblMuteStatus.Text = "Muted";
                lblMuteStatus.Invalidate();
                pbtnMute.Image = GlobalResources.ui_btn_blue_unmute;
                pbtnMute.Invalidate();
            } else
            {
                lblMuteStatus.Text = "Unmuted";
                lblMuteStatus.Invalidate();
                pbtnMute.Image = GlobalResources.ui_btn_blue_mute;
                pbtnMute.Invalidate();
            }

        }
    }
}
