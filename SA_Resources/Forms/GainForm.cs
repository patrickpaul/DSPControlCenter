using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources.SAControls;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAForms
{
    public partial class GainForm : Form
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
        private DSP_Primitive_StandardGain RecastStandardGain;
        private DSP_Primitive_MixerCrosspoint RecastCrossPoint;

        private bool is_mixer = false;

        public GainForm(MainForm_Template _parentForm, DSP_Primitive _inputPrimitive, DSP_Primitive_Types _primitiveType)
        {
            InitializeComponent();

            try
            {
                if (_primitiveType == DSP_Primitive_Types.StandardGain)
                {
                    is_mixer = false;
                    RecastStandardGain = (DSP_Primitive_StandardGain) _inputPrimitive;
                    saGainFader1.Mode = RecastStandardGain.Mode == StandardGain_Types.Twelve_to_Negative_100 ? 0 : 1;
                }
                else
                {
                    is_mixer = true;
                    RecastCrossPoint = (DSP_Primitive_MixerCrosspoint) _inputPrimitive;
                    saGainFader1.Mode = 0;
                    saGainFader1.MuteDisablesFader = true;
                }

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

                saGainFader1.Gain = is_mixer ? RecastCrossPoint.Gain : RecastStandardGain.Gain;
                saGainFader1.Muted = is_mixer ? RecastCrossPoint.Muted : RecastStandardGain.Muted;



                this.Text = _inputPrimitive.Name;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in GainForm]: " + ex.Message);
            }

        }

        private void saGainFader1_OnChange(object sender, FaderEventArgs e)
        {
            // TODO add QueueChange here

            if (is_mixer)
            {
                RecastCrossPoint.Gain = e.Gain;
                RecastCrossPoint.Muted = e.Muted;

            }
            else
            {
                RecastStandardGain.Gain = e.Gain;
                RecastStandardGain.Muted = e.Muted;
            }
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            // TODO add meter handler here
            /*
            UInt32 read_address;
            
            if (IS_MIXER)
            {
                read_address = PARENT_FORM._mix_meters[GAIN_INDEX];
            }
            else
            {
                read_address = PARENT_FORM._gain_meters[CH_INDEX][GAIN_INDEX];
            }

            double offset = 20 + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);
            double converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

            //Console.WriteLine("Read " + read_value.ToString("X8") + " from " + read_address.ToString("X8"));

            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);
            }
            else
            {
                read_gain_value = -100;
            }

            gainMeter.DB = read_gain_value;
          * */
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
