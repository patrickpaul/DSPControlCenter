using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources;
using SA_Resources.SAControls;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAForms
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


        private Dial delayMS, delayFT, delayM;

        private MainForm_Template PARENT_FORM;

        private DSP_Primitive_Delay Active_Primitive;


        public DelayForm(MainForm_Template _parent, DSP_Primitive_Delay input_primitive)
        {

            InitializeComponent();

            Active_Primitive = input_primitive;
            PARENT_FORM = _parent;

            this.chkBypass.Checked = Active_Primitive.Bypassed;

            this.Text = "CH " + (Active_Primitive.Channel + 1) + " - Delay";

            delayMS = new Dial(TextDelayMS, DialDelayMS, new double[] { 0, 0.016, 0.031, 0.045, 0.060, 0.076, 0.09 }, DialHelpers.Format_String_Delay_MS, Images.knob_red_bg, Images.knob_red_line);
            delayFT = new Dial(TextDelayFT, DialDelayFT, new double[] { 0.10, 17.98, 33.97, 49.95, 66.93, 83.916, 100.35 }, DialHelpers.Format_String_Delay_FT, Images.knob_blue_bg, Images.knob_blue_line);
            delayM = new Dial(TextDelayM, DialDelayM, new double[] { 0, 5.51, 10.40, 15.3, 20.50, 25.70, 30.6 }, DialHelpers.Format_String_Delay_M, Images.knob_green_bg, Images.knob_green_line);

            delayMS.OnChange += new DialEventHandler(this.DialMS_OnChange);
            delayFT.OnChange += new DialEventHandler(this.DialFT_OnChange);
            delayM.OnChange += new DialEventHandler(this.DialM_OnChange);

            delayMS.Value = Active_Primitive.Delay;

            dropAction.SelectedIndex = 0;
            dropAction.Invalidate();

            SetInitialDistances();
        }

        private void SetInitialDistances()
        {

            double new_ms = delayMS.Value;
            delayFT.Value = new_ms * 1110;
            delayM.Value = new_ms * 340;

            Active_Primitive.Delay = delayMS.Value;
        }

        private void DialMS_OnChange(object sender, DialEventArgs e)
        {
            double new_ms = delayMS.Value;
            delayFT.Value = new_ms * 1110;
            delayM.Value = new_ms * 340;

            Active_Primitive.Delay = delayMS.Value;

            Active_Primitive.QueueChangeByOffset(PARENT_FORM,0);
        }

        private void DialFT_OnChange(object sender, DialEventArgs e)
        {
            double new_ft = delayFT.Value;
            delayMS.Value = Math.Min(0.1,new_ft / 1110);
            delayM.Value = new_ft * 0.3048;

            Active_Primitive.Delay = delayMS.Value;
        }

        private void DialM_OnChange(object sender, DialEventArgs e)
        {
            double new_m = delayM.Value;
            delayMS.Value = Math.Min(0.1,new_m / 340);
            delayFT.Value = new_m * 3.28;

            Active_Primitive.Delay = delayMS.Value;
            
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            Active_Primitive.Bypassed = chkBypass.Checked;
            Active_Primitive.QueueChangeByOffset(PARENT_FORM,1);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {

            Console.WriteLine("btnGo_Click not yet implemented");
            return;
            /*
            if (dropAction.SelectedIndex == 0)
            {
                using (CopyForm copyForm = new CopyForm(PARENT_FORM,CH_NUMBER, CopyFormType.Delay))
                {
                    // passing this in ShowDialog will set the .Owner 
                    // property of the child form
                    copyForm.ShowDialog(this);
                }
            }
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
