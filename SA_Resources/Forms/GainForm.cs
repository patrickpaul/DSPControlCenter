using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SA_Resources
{
    public partial class GainForm : Form
    {
        private GainConfig gainConfig;

        private bool mouseDown;

        private ToolTip toolTip1;
        private int oldX, oldY;

        public GainForm(GainConfig in_gain, string formTitle = "CH1 Gain")
        {
            InitializeComponent();

            gainConfig = in_gain;

            lblGain.Text = gainConfig.Gain.ToString("N1") + "dB";

            this.Text = formTitle;

            this.MouseWheel += new MouseEventHandler(panel1_MouseWheel);

            sliderPB.Location = new Point(18, (int)gain_to_yval(gainConfig.Gain));

            

            toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 50;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(sliderPB, "Muted");

            chkMuted.Checked = gainConfig.Muted;
            
            if (gainConfig.Muted)
            {
                sliderPB.Cursor = Cursors.No;
                toolTip1.SetToolTip(sliderPB, "Muted"); 
                
            }
            else
            {
                sliderPB.Cursor = Cursors.Hand;
                toolTip1.SetToolTip(sliderPB, null);
            }

            

        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
                Console.Out.WriteLine(e.Delta);
        }

        PictureBox thisPB;

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (chkMuted.Checked)
            {
                return;
            }
            mouseDown = true;

            oldX = ((PictureBox)sender).Location.X;
            oldY = e.Y;
        }

        private double yVal_to_gain(double yVal)
        {
            /* REMEMBER THAT WE NORMALIZE THIS BY SUBTRACTING 5 */
            yVal = yVal - 5;

            if (yVal <= 28)
            {
                return (((28.0 - yVal) / 28.0) * 6) + 6;
            }
            else if (yVal <= 58)
            {
                return (((58.0 - yVal) / 30.0) * 6) + 0;
            }
            else if (yVal <= 85)
            {
                return (((85.0 - yVal) / 27.0) * 5) + -5;
            }
            else if (yVal <= 114)
            {
                return (((114.0 - yVal) / 29.0) * 5) + -10;
            }
            else if (yVal <= 142)
            {
                return (((142.0 - yVal) / 28.0) * 5) + -15;
            }
            else if (yVal <= 166)
            {
                return (((166.0 - yVal) / 28.0) * 5) + -20;
            }
            else if (yVal <= 186)
            {
                return (((186.0 - yVal) / 20.0) * 10) + -30;
            }
            else if (yVal <= 198)
            {
                return (((198.0 - yVal) / 12.0) * 10) + -40;
            }
            else if (yVal <= 211)
            {
                return (((211.0 - yVal) / 13.0) * 20) + -60;
            }
            else if (yVal <= 221)
            {
                return (((221.0 - yVal) / 10.0) * 30) + -90;
            }
            else
            {
                return (((231.0 - yVal) / 10.0) * 10) + -100;
            }
        }


        private double gain_to_yval(double gainVal)
        {
            if (gainVal > 6.0)
            {
                return 5 + 28.0 - (((gainVal - 6.0) / 6.0) * 28.0);
            }
            else if (gainVal > 0)
            {
                return 5 + 58.0 - (((gainVal - 0) / 6) * 30.0);
            }
            else if (gainVal > -5)
            {
                return 5 + 85.0 - (((gainVal - -5) / 5) * 27.0);
            }
            else if (gainVal > -10)
            {
                return 5 + 114.0 - (((gainVal - -10) / 5) * 29.0);
            }
            else if (gainVal > -15)
            {
                return 5 + 142.0 - (((gainVal - -15) / 5) * 28.0);
            }
            else if (gainVal > -20)
            {
                return 5 + 166.0 - (((gainVal - -20) / 5.0) * 28.0);
            }
            else if (gainVal > -30)
            {
                return 5 + 186.0 - (((gainVal - -30) / 10.0) * 20.0);
            }
            else if (gainVal > -40)
            {
                return 5 + 198.0 - (((gainVal - -40) / 10.0) * 12.0);
            }
            else if (gainVal > -60)
            {
                return 5 + 211.0 - (((gainVal - -60) / 20.0) * 13.0);
            }
            else if (gainVal > -90)
            {
                return 5 + 221.0 - (((gainVal - -90) / 30.0) * 10.0);
            }
            else
            {
                return 5 + 231.0 - (((gainVal - -100) / 10.0) * 10.0);
            }
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                
                thisPB = (PictureBox)sender;

                int yVal = Math.Min(Math.Max(thisPB.Location.Y - (oldY - e.Y), 5), 236);

                

                gainConfig.Gain = Math.Max(-100.0, yVal_to_gain((double) yVal));

                lblGain.Text = gainConfig.Gain.ToString("N1") + "dB";

                thisPB.Location = new Point(oldX, yVal);
                this.Refresh();

                lblGain.Invalidate();
            }
        }


        private void pic_MouseUp(object sender, MouseEventArgs e)
        {

            mouseDown = false;

        }

        private void GainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainConfig.Muted = chkMuted.Checked;
        }

        private void chkMuted_CheckedChanged(object sender, EventArgs e)
        {
            if(chkMuted.Checked)
            {

                toolTip1.SetToolTip(sliderPB, "Muted");
                lblGain.ForeColor = Color.PaleVioletRed;
            } else
            {
                toolTip1.SetToolTip(sliderPB, null);
                lblGain.ForeColor = Color.White; 
            }

            if (chkMuted.Checked)
            {
                sliderPB.Cursor = Cursors.No;
                lblGain.Text = "0dB";

                lblGain.Invalidate();
            }
            else
            {
                sliderPB.Cursor = Cursors.Hand;
                lblGain.Text = "0dB";

                lblGain.Invalidate();
            }

            gainConfig.Gain = 0;

            sliderPB.Location = new Point(18, (int)gain_to_yval(0));
            sliderPB.Refresh();
        }
    }
}
