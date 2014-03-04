using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources.Configurations;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    public partial class MixerForm6x4 : Form
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

        private double read_gain_value = 0;
        private int cur_meter;

        public MixerForm6x4(MainForm_Template _parentForm)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            DSP_Primitive_Input InputPrimitive = null;
            Label InputLabel = null;

            for(int i = 0; i < 4; i++)
            {
                InputPrimitive = (DSP_Primitive_Input)PARENT_FORM.DSP_PROGRAMS[0].LookupPrimitive(DSP_Primitive_Types.Input, i, 0);
                InputLabel = (Label) Controls.Find("lblInput" + i, true).FirstOrDefault();

                if(InputLabel != null)
                {
                    if(InputPrimitive != null)
                    {
                        InputLabel.Text = InputPrimitive.InputName;
                    } else
                    {
                        InputLabel.Text = "Input " + (i + 1).ToString();
                    }
                }

            }


            lblOutput0.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].outputs[0].Name;
            lblOutput1.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].outputs[1].Name;
            lblOutput2.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].outputs[2].Name;
            lblOutput3.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].outputs[3].Name;

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

        private void btnMixerCrosspoint_Click(object sender, EventArgs e)
        {

            int index_in = int.Parse(((PictureButton)sender).Name.Substring(9, 1));
            int index_out = int.Parse(((PictureButton)sender).Name.Substring(11, 1));

            GainConfig cached_gain = (GainConfig)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Clone();
            /*
            using (GainForm gainForm = new GainForm(PARENT_FORM, index_in - 1, index_out - 1, (index_in * 4) + (index_out-1), true))
            {

                gainForm.Width = 132;

                gainForm.Height = 414;

                DialogResult showResult = gainForm.ShowDialog(this);

                if(showResult == DialogResult.Cancel)
                {
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1] = (GainConfig) cached_gain.Clone();
                    return;
                }

                // This code below is equivalent to UpdateTooltips() in MainForm_Template, so just leave it.

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
                if(!PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1].Muted)
                {
                    crosspoint_button.Overlay3Visible = false;
                    crosspoint_button.Overlay1Visible = true; 
                    
                } else
                {
                    crosspoint_button.Overlay3Visible = true;
                }

                crosspoint_button.Invalidate();
            }
             * */
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

            curMeter.DB = read_gain_value;
            curMeter.Refresh();
        }

    }
}
