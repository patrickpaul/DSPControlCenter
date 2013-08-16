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

        MainForm_Template PARENT_FORM;

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


    }
}
