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
    }
}
