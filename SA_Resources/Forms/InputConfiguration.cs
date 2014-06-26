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
using SA_Resources.DSP;
using SA_Resources.SADevices;
using SA_Resources.SAForms;
using SA_Resources.DSP.Primitives;

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

        private DSP_Primitive_Input Active_Primitive;


        public InputConfiguration(MainForm_Template _parentForm, DSP_Primitive_Input in_primitive)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;


            if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100)
            {
                txtDisplayName.Enabled = false;
            }

            Active_Primitive = in_primitive;

            this.Text = "CH " + (Active_Primitive.Channel+1).ToString("N0") + " - Input Configuration";

            txtDisplayName.Text = Active_Primitive.InputName;

            chkPhantomPower.Checked = Active_Primitive.PhantomPower;
            chkPhantomPower.Invalidate();

            if (Active_Primitive.InputType == InputType.Line)
            {
                dropInputType.SelectedIndex = 0;
            }
            else if (Active_Primitive.InputType == InputType.Microphone6)
            {
                dropInputType.SelectedIndex = 1;
            } else
            {
                dropInputType.SelectedIndex = 2;
            }

            dropInputType.Invalidate();

            if (!Active_Primitive.PhantomAvailable)
            {
                lblPhantomPower.Visible = false;
                chkPhantomPower.Visible = false;
            }

            if (PARENT_FORM.LIVE_MODE)
            {
                //read_address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, Active_Primitive.Channel, 0).Address;
            
                gainMeter.Visible = true;
                gainMeter.PIC_CONN = PARENT_FORM._PIC_Conn;
                gainMeter.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, Active_Primitive.Channel, 0).Address;
                gainMeter.Start();
            }
            else
            {
                gainMeter.Visible = false;
            }
            

            form_loaded = true;
        }

        private void InputConfiguration_Load(object sender, EventArgs e)
        {
            txtDisplayName.SelectAll();
        }

        

        private void dropInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSP_Primitive_Pregain Pregain_Primitive;

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
                Active_Primitive.InputType = InputType.Line;
            } else if(dropInputType.SelectedIndex == 1)
            {
                Active_Primitive.InputType = InputType.Microphone6;

            } else
            {
                Active_Primitive.InputType = InputType.Microphone20;
            }

            Pregain_Primitive = (DSP_Primitive_Pregain)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Pregain, Active_Primitive.Channel, 0);

            Pregain_Primitive.Pregain = (int)Active_Primitive.Pregain;

            if (PARENT_FORM.LIVE_MODE)
            {
                Active_Primitive.QueuePregain(PARENT_FORM);
                Pregain_Primitive.QueueChange(PARENT_FORM);
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

            
            Active_Primitive.PhantomPower = chkPhantomPower.Checked;

            Active_Primitive.QueuePhantom(PARENT_FORM);             

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
            gainMeter.Stop();
            Active_Primitive.InputName = txtDisplayName.Text;
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
    }
}
