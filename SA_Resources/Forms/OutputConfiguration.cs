using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace SA_Resources
{
    public partial class OutputConfiguration : Form
    {
        private int channel = 0;
        private OutputConfig config;

        public OutputConfiguration(OutputConfig _inConfig, int index)
        {
            InitializeComponent();

            channel = index;
            config = _inConfig;

            this.Text = "CH " + channel.ToString("N0") + " - Output Configuration";

            txtDisplayName.Text = config.Name;
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
            config.Name = txtDisplayName.Text;
        }
    }
}
