using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using SA_Resources;
using SA_Resources.DSP;
using SA_Resources.SAForms;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAForms
{
    public partial class FLXConfigurationForm : Form
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

        private MainForm_Template PARENT_FORM;



        public FLXConfigurationForm(MainForm_Template _parentForm)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            this.Text = "Device Configuration";
        }

        private void FLXConfigurationForm_Load(object sender, EventArgs e)
        {
            try
            {
                chkSleepEnable.Checked = PARENT_FORM.SLEEP_ENABLE;
                nudSleepSeconds.Value = Math.Max(1,(decimal) PARENT_FORM.SLEEP_SECONDS);

                int adc_min = PARENT_FORM.ADC_CALIBRATION_MIN;
                int adc_max = PARENT_FORM.ADC_CALIBRATION_MAX;


                txtRVCMIn.Text = adc_min.ToString();
                txtRVCMax.Text = adc_max.ToString();

                txtRVCValue.Text = PARENT_FORM.DeviceConn.ReadCurrentRVC();
                txtRVCValue.Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in FLXConfiguration.Load: " + ex.Message);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoutine();
        }

        private void SaveRoutine()
        {

            PARENT_FORM.SLEEP_ENABLE = chkSleepEnable.Checked;
            PARENT_FORM.SLEEP_SECONDS = (Int16)nudSleepSeconds.Value;

            PARENT_FORM.DeviceConn.SetSleepModeEnable(PARENT_FORM.SLEEP_ENABLE);
            PARENT_FORM.DeviceConn.SetSleepModeSeconds(PARENT_FORM.SLEEP_SECONDS);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void CancelRoutine()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
        private void pbtnRefresh_Click(object sender, EventArgs e)
        {
            txtRVCValue.Text = PARENT_FORM.DeviceConn.ReadCurrentRVC();
            txtRVCValue.Invalidate();
        }

        private void pbtnCalibrateUpper_Click(object sender, EventArgs e)
        {
            int new_max = PARENT_FORM.DeviceConn.CalibrateUpperRVC();

            txtRVCMax.Text = new_max.ToString();

            PARENT_FORM.ADC_CALIBRATION_MAX = new_max;

            txtRVCValue.Text = PARENT_FORM.DeviceConn.ReadCurrentRVC();
            txtRVCValue.Invalidate();
        }

        private void pbtnCalibrateLower_Click(object sender, EventArgs e)
        {
            int new_min = PARENT_FORM.DeviceConn.CalibrateLowerRVC();

            txtRVCMIn.Text = new_min.ToString();

            PARENT_FORM.ADC_CALIBRATION_MIN = new_min;

            txtRVCValue.Text = PARENT_FORM.DeviceConn.ReadCurrentRVC();
            txtRVCValue.Invalidate();
        }

    }
}
