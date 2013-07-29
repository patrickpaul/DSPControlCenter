using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources;

namespace SA_Resources
{
    public partial class DelayForm : Form
    {
        public event ConfigChangeEventHandler OnChange;

        private int ch_index = 0;

        private Dial delayMS, delayFT, delayM;

        private DelayConfig config;

        public DelayForm(int index, DelayConfig in_config)
        {

            config = in_config;

            InitializeComponent();

            ch_index = index;

            this.Text = "CH " + ch_index + " - Delay";

            delayMS = new Dial(TextDelayMS, DialDelayMS, new double[] { 1, 18, 34, 50, 67, 84, 100 }, DialHelpers.Format_String_Delay_MS, Images.knob_red_bg, Images.knob_red_line);
            delayFT = new Dial(TextDelayFT, DialDelayFT, new double[] { 1.11, 19.98, 37.74, 55.5, 74.37, 93.24, 111.0 }, DialHelpers.Format_String_Delay_FT, Images.knob_blue_bg, Images.knob_blue_line);
            delayM = new Dial(TextDelayM, DialDelayM, new double[] { 0.34, 6.12, 11.56, 17, 22.78, 28.56, 34.0 }, DialHelpers.Format_String_Delay_M, Images.knob_green_bg, Images.knob_green_line);

            delayMS.OnChange += new DialEventHandler(this.DialMS_OnChange);
            delayFT.OnChange += new DialEventHandler(this.DialFT_OnChange);
            delayM.OnChange += new DialEventHandler(this.DialM_OnChange);

            delayMS.Value = config.Delay;

            chkBypass.Checked = config.Bypassed;
        }

        private void DialMS_OnChange(object sender, DialEventArgs e)
        {
            double new_ms = delayMS.Value;
            delayFT.Value = new_ms * 1.11;
            delayM.Value = new_ms * 0.34;

            config.Delay = delayMS.Value;

            this.OnChange(this, new ConfigChangeEventArgs(ConfigType.DELAY_MS, delayMS.Value, ch_index));
        }

        private void DialFT_OnChange(object sender, DialEventArgs e)
        {
            double new_ft = delayFT.Value;
            delayMS.Value = new_ft / 1.11;
            delayM.Value = new_ft * 0.3048;

            config.Delay = delayMS.Value;

            this.OnChange(this, new ConfigChangeEventArgs(ConfigType.DELAY_MS, delayMS.Value, ch_index));
        }

        private void DialM_OnChange(object sender, DialEventArgs e)
        {
            double new_m = delayM.Value;
            delayMS.Value = new_m / 0.34;
            delayFT.Value = new_m * 3.28;

            config.Delay = delayMS.Value;

            this.OnChange(this, new ConfigChangeEventArgs(ConfigType.DELAY_MS, delayMS.Value, ch_index));
            
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            config.Bypassed = chkBypass.Checked;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialDelayMS_Click(object sender, EventArgs e)
        {

        }

        
    }
}
