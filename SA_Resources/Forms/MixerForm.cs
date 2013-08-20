using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class MixerForm : Form
    {

        private MainForm_Template PARENT_FORM;

        private double read_gain_value = 0;
        private int cur_meter;

        public MixerForm(MainForm_Template _parentForm)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureButton pbControl = (PictureButton)Controls.Find("btnRouter" + (i + 1).ToString() + "_" + (j + 1).ToString(), true).First();

                    // Create the ToolTip and associate with the Form container.
                    ToolTip toolTip1 = new ToolTip();

                    // Set up the delays for the ToolTip.
                    toolTip1.AutoPopDelay = 5000;
                    toolTip1.InitialDelay = 10;
                    toolTip1.ReshowDelay = 50;
                    // Force the ToolTip text to be displayed whether or not the form is active.
                    toolTip1.ShowAlways = true;

                    // Set up the ToolTip text for the Button and Checkbox.
                    if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[i][j].Muted)
                    {
                        toolTip1.SetToolTip(pbControl, "Muted");
                        pbControl.Overlay3Visible = true;
                    }
                    else
                    {
                        toolTip1.SetToolTip(pbControl, PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[i][j].Gain.ToString("N1") + "dB");
                        pbControl.Overlay1Visible = true;
                    }
                    pbControl.Invalidate();
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMixerCrosspoint_Click(object sender, EventArgs e)
        {

            int index_in = int.Parse(((PictureButton)sender).Name.Substring(9, 1));
            int index_out = int.Parse(((PictureButton)sender).Name.Substring(11, 1));

            using (GainForm gainForm = new GainForm(PARENT_FORM, index_in - 1, index_out - 1, (index_in * 4) + (index_out-1), true))
            {

                gainForm.Width = 132;

                gainForm.Height = 414;

                gainForm.ShowDialog(this);

                PictureButton crosspoint_button = (PictureButton)sender;

                // Create the ToolTip and associate with the Form container.
                ToolTip toolTip1 = new ToolTip();

                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 10;
                toolTip1.ReshowDelay = 50;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;

                // Set up the ToolTip text for the Button and Checkbox.
                if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Muted)
                {
                    toolTip1.SetToolTip(crosspoint_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(crosspoint_button, PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Gain.ToString("N1") + "dB");
                }

                //toolTip1.SetToolTip(this.checkBox1, "My checkBox1"); 
                
                //crosspoint_button.ToolTipText = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Gain.ToString("N1") + "dB";

                if(!PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Muted)
                {
                    crosspoint_button.Overlay3Visible = false;
                    crosspoint_button.Overlay1Visible = true; 
                    
                } else
                {
                    //PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Gain = -24;
                    crosspoint_button.Overlay3Visible = true;
                }

                crosspoint_button.Invalidate();
            }
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {
            UInt32 read_address = 0x00000000;
            double offset = 20 + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_value = 0x00000000;
            double converted_value = 0;

            
            cur_meter++;
            if (cur_meter == 4)
            {
                cur_meter = 0;
            }

            SignalMeter curMeter = ((SignalMeter)Controls.Find("gainMeter" + (cur_meter + 1), true).First());

            read_address = PARENT_FORM._mix_meters[cur_meter];


            read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);
            converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);
            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);
            }
            else
            {
                read_gain_value = -100;
            }

            //Console.WriteLine("Read value of " + read_gain_value + "dB");


            curMeter.DB = read_gain_value;
        }

    }
}
