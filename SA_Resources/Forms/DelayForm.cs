using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class DelayForm : Form
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

        private int CH_NUMBER = 0;

        private Dial delayMS, delayFT, delayM;

        private MainForm_Template PARENT_FORM;
        private int SETTINGS_INDEX;

        public DelayForm(MainForm_Template _parent, int chan_num, int _settings_offset)
        {

            InitializeComponent(); 
            
            SETTINGS_INDEX = _settings_offset;
            PARENT_FORM = _parent;

            

            CH_NUMBER = chan_num;

            this.Text = "CH " + CH_NUMBER + " - Delay";

            delayMS = new Dial(TextDelayMS, DialDelayMS, new double[] { 0, 0.018, 0.034, 0.050, 0.067, 0.084, 0.100 }, DialHelpers.Format_String_Delay_MS, Images.knob_red_bg, Images.knob_red_line);
            delayFT = new Dial(TextDelayFT, DialDelayFT, new double[] { 0.11, 19.98, 37.74, 55.5, 74.37, 93.24, 111.5 }, DialHelpers.Format_String_Delay_FT, Images.knob_blue_bg, Images.knob_blue_line);
            delayM = new Dial(TextDelayM, DialDelayM, new double[] { 0, 6.12, 11.56, 17, 22.78, 28.56, 34.0 }, DialHelpers.Format_String_Delay_M, Images.knob_green_bg, Images.knob_green_line);

            delayMS.OnChange += new DialEventHandler(this.DialMS_OnChange);
            delayFT.OnChange += new DialEventHandler(this.DialFT_OnChange);
            delayM.OnChange += new DialEventHandler(this.DialM_OnChange);

            delayMS.Value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay;

            dropAction.SelectedIndex = 0;
            dropAction.Invalidate();

            SetInitialDistances();
        }

        private void SetInitialDistances()
        {

            double new_ms = delayMS.Value;
            delayFT.Value = new_ms * 1110;
            delayM.Value = new_ms * 340;

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay = delayMS.Value;
        }

        private void DialMS_OnChange(object sender, DialEventArgs e)
        {
            double new_ms = delayMS.Value;
            delayFT.Value = new_ms * 1110;
            delayM.Value = new_ms * 340;

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay = delayMS.Value;

            // DSP_Math.double_to_MN(PROGRAMS[program_index].delays[0].Delay, 16, 16);

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, DSP_Math.double_to_MN(delayMS.Value, 16, 16)));
        }

        private void DialFT_OnChange(object sender, DialEventArgs e)
        {
            double new_ft = delayFT.Value;
            delayMS.Value = Math.Min(0.1,new_ft / 1110);
            delayM.Value = new_ft * 0.3048;

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay = delayMS.Value;
        }

        private void DialM_OnChange(object sender, DialEventArgs e)
        {
            double new_m = delayM.Value;
            delayMS.Value = Math.Min(0.1,new_m / 340);
            delayFT.Value = new_m * 3.28;

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay = delayMS.Value;
            
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Bypassed = chkBypass.Checked;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (dropAction.SelectedIndex == 0)
            {
                using (CopyForm copyForm = new CopyForm(PARENT_FORM,CH_NUMBER, CopyFormType.Delay))
                {
                    // passing this in ShowDialog will set the .Owner 
                    // property of the child form
                    copyForm.ShowDialog(this);
                }
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

        
    }
}
