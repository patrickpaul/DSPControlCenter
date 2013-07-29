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
    public partial class MixerForm : Form
    {
        public event ConfigChangeEventHandler OnChange;

        GainConfig[][] config = new GainConfig[6][];

        public MixerForm(GainConfig[][] in_config)
        {
            InitializeComponent(); 
            
            for (int i = 0; i < 6; i++)
            {
                config[i] = new GainConfig[4];
            }

            config = in_config;

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
                    if (config[i][j].Muted)
                    {
                        toolTip1.SetToolTip(pbControl, "Muted");
                        pbControl.Overlay3Visible = true;
                    }
                    else
                    {
                        toolTip1.SetToolTip(pbControl, config[i][j].Gain.ToString("N1") + "dB");
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

            using (GainForm gainForm = new GainForm(config[index_in-1][index_out-1]))
            {
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
                if (config[index_in - 1][index_out - 1].Muted)
                {
                    toolTip1.SetToolTip(crosspoint_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(crosspoint_button, config[index_in - 1][index_out - 1].Gain.ToString("N1") + "dB");
                }
                //toolTip1.SetToolTip(this.checkBox1, "My checkBox1"); 
                
                //crosspoint_button.ToolTipText = config[index_in - 1][index_out - 1].Gain.ToString("N1") + "dB";

                if(!config[index_in - 1][index_out - 1].Muted)
                {
                    crosspoint_button.Overlay3Visible = false;
                    crosspoint_button.Overlay1Visible = true; 
                    
                } else
                {
                    //config[index_in - 1][index_out - 1].Gain = -24;
                    crosspoint_button.Overlay3Visible = true;
                }

                crosspoint_button.Invalidate();
            }
        }


    }
}
