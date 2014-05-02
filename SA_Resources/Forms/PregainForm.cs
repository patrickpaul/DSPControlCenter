using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private bool is_mixer = false;

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
                    signalTimer.Enabled = true;
                }
                else
                {
                    gainMeter.Visible = false;
                    signalTimer.Enabled = false;
                }

                saGainFader1.Gain = RecastPregain.Gain;
                saGainFader1.Muted = RecastPregain.Muted;



                this.Text = _inputPrimitive.Name;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in GainForm]: " + ex.Message);
            }

        }

        private void saGainFader1_OnChange(object sender, FaderEventArgs e)
        {

                RecastPregain.Gain = e.Gain;
                Console.WriteLine("Setting gain to " + e.Gain);

                RecastPregain.Muted = e.Muted;
                RecastPregain.QueueChange(PARENT_FORM);
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            // TODO add meter handler here


            UInt32 read_address = (RecastPregain.Meter);
            
            
            double offset = (20-20+3.8) + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);

            
            double converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);
            double read_gain_value;

            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);
            }
            else
            {
                read_gain_value = -100;
            }


            gainMeter.DB = read_gain_value;
          
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
