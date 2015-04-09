using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.DSP;
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

        public object _threadlock;
        public bool meterthread_abort = false;
        public Thread MeterThread;

        private bool is_mixer = false;

        public GainForm(MainForm_Template _parentForm, DSP_Primitive _inputPrimitive, DSP_Primitive_Type primitiveType)
        {
            InitializeComponent();

            _threadlock = new object();
            try
            {
                if (primitiveType == DSP_Primitive_Type.StandardGain)
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

                if (PARENT_FORM.LIVE_MODE && !is_mixer)
                {
                    gainMeter.Visible = true;
                    gainMeter.DeviceConn = PARENT_FORM.DeviceConn;
                    gainMeter.Address = RecastStandardGain._Meter;
                    gainMeter.Start();
                }
                else
                {
                    gainMeter.Visible = false;
                }

                saGainFader1.Gain = is_mixer ? RecastCrossPoint.Gain : RecastStandardGain.Gain;
                saGainFader1.Muted = is_mixer ? RecastCrossPoint.Muted : RecastStandardGain.Muted;



                this.Text = _inputPrimitive.Name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in GainForm]: " + ex.Message);
            }

        }

        private void saGainFader1_OnChange(object sender, FaderEventArgs e)
        {
            // TODO add QueueChange here

            if (is_mixer)
            {
                RecastCrossPoint.Gain = e.Gain;
                RecastCrossPoint.Muted = e.Muted;
                RecastCrossPoint.QueueChange(PARENT_FORM);
            }
            else
            {
                RecastStandardGain.Gain = e.Gain;

                RecastStandardGain.Muted = e.Muted;
                RecastStandardGain.QueueChange(PARENT_FORM);
            }
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

        private void GainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeter.Stop();
        }

        
    }
}
