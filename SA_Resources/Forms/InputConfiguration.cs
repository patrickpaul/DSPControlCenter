using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using Controls;

namespace SA_Resources
{
    public partial class InputConfiguration : Form
    {

        private bool form_loaded = false;

        private InputConfig config;

        public InputConfiguration(InputConfig _inputConfig, int channel, bool show_phantom = true)
        {
            InitializeComponent();

            config = _inputConfig;

            this.Text = "CH " + channel.ToString("N0") + " - Input Configuration";

            txtDisplayName.Text = config.Name;

            chkPhantomPower.Checked = config.PhantomPower;
            chkPhantomPower.Invalidate();

            if(config.Type == InputType.Line)
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
                config.Type = InputType.Line;
            } else
            {
                config.Type = InputType.Microphone;
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

            config.PhantomPower = chkPhantomPower.Checked;

        }

        private void txtDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;

                config.Name = txtDisplayName.Text;
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
            config.Name = txtDisplayName.Text;
        }

    }
}
