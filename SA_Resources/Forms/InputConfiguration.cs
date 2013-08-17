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

        private bool form_loaded = false;

        private MainForm_Template PARENT_FORM;
        private int CH_NUMBER;


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
            } else
            {
                dropInputType.SelectedIndex = 1;
            }

            dropInputType.Invalidate();

            if(!show_phantom)
            {
                lblPhantomPower.Visible = false;
                chkPhantomPower.Visible = false;
            }
            

            form_loaded = true;
        }

        private void InputConfiguration_Load(object sender, EventArgs e)
        {
            txtDisplayName.Select(txtDisplayName.Text.Length, 0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoutine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRoutine();
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
            } else
            {
                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Type = InputType.Microphone;
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

            if (chkPhantomPower.Checked)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(1000 + (CH_NUMBER - 1), 1));
            }
            else
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(1000 + (CH_NUMBER - 1), 0));
            }
                    

        }

        private void txtDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;

                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Name = txtDisplayName.Text;
                return;
                /*
                SaveRoutine();
                return;
                 * * */
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

        private void SaveRoutine()
        {
            this.Close();

        }

        private void CancelRoutine()
        {
            this.Close();

        }

        private void InputConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].inputs[CH_NUMBER - 1].Name = txtDisplayName.Text;
        }

    }
}
