using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources.DSP;
using SA_Resources.SAControls;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAForms
{
    public partial class PregainForm : Form
    {

        // Disable form's closing button
        protected override CreateParams CreateParams
        {
            get
            {
                int CP_NOCLOSE_BUTTON = 0x200;
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 
        
        private MainForm_Template PARENT_FORM;
        private DSP_Primitive_Pregain RecastPregain;


        public PregainForm(MainForm_Template _parentForm, DSP_Primitive _inputPrimitive)
        {
            InitializeComponent();

            try
            {
                RecastPregain = (DSP_Primitive_Pregain)_inputPrimitive;

                PARENT_FORM = _parentForm;

                if (PARENT_FORM.LIVE_MODE)
                {
                    gainMeter.Visible = true;
                    gainMeter.DeviceConn = PARENT_FORM.DeviceConn;
                    gainMeter.Address = RecastPregain._Meter;
                    gainMeter.Start();
                }
                else
                {
                    gainMeter.Visible = false;
                }

                switch (RecastPregain.Pregain)
                {
                    case 0:
                        saGainFader1.Mode = 2;
                        break;

                    case 6:
                        saGainFader1.Mode = 3;
                        break;

                    case 20:
                        saGainFader1.Mode = 4;
                        break;

                }
                saGainFader1.Gain = RecastPregain.Gain;
                saGainFader1.Muted = RecastPregain.Muted;
                


                this.Text = _inputPrimitive.Name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in GainForm]: " + ex.Message);
            }

        }

        private void saGainFader1_OnChange(object sender, FaderEventArgs e)
        {

                RecastPregain.Gain = e.Gain;

                RecastPregain.Muted = e.Muted;
                RecastPregain.QueueChange(PARENT_FORM);
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

        private void PregainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeter.Stop();
        }

        
    }
}
