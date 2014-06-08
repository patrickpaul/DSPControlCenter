using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

        private DSP_Primitive_MixerCrosspoint ActiveCrosspoint;

        private bool CH2_hidden = false;
        private bool CH3_hidden = false; 
        private bool CH4_hidden = false;

        public MixerForm6x4(MainForm_Template _parentForm)
        {
            InitializeComponent();

            try
            {
                PARENT_FORM = _parentForm;

                if (PARENT_FORM.IsAmplifier() && PARENT_FORM.AmplifierMode != 0)
                {
                    if (PARENT_FORM.AmplifierMode != 0)
                    {
                        if (PARENT_FORM.AmplifierMode == 1)
                        {
                            CH2_hidden = true;
                            CH4_hidden = true;
                            gainMeter2.Visible = false;
                            gainMeter4.Visible = false;
                        }
                        else if (PARENT_FORM.AmplifierMode == 2)
                        {
                            CH2_hidden = true;
                            gainMeter2.Visible = false;
                        }
                        else if (PARENT_FORM.AmplifierMode == 3)
                        {
                            CH2_hidden = true;
                            CH3_hidden = true; 
                            CH4_hidden = true;
                            gainMeter2.Visible = false;
                            gainMeter3.Visible = false; 
                            gainMeter4.Visible = false;
                        }
                    }
                }



                if (PARENT_FORM.LIVE_MODE)
                {
                    gainMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 0).Address;
                    gainMeter1.PIC_CONN = PARENT_FORM._PIC_Conn;
                    gainMeter1.Start();

                    if (!CH2_hidden)
                    {
                        gainMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 1).Address;
                        gainMeter2.PIC_CONN = PARENT_FORM._PIC_Conn;
                        gainMeter2.Start();
                    }

                    if (!CH3_hidden)
                    {
                        gainMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 2).Address;
                        gainMeter3.PIC_CONN = PARENT_FORM._PIC_Conn;
                        gainMeter3.Start();
                    }
                    if (!CH4_hidden)
                    {
                        gainMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 3).Address;
                        gainMeter4.PIC_CONN = PARENT_FORM._PIC_Conn;
                        gainMeter4.Start();
                    }

                }


                DSP_Primitive_Input InputPrimitive = null;
                Label InputLabel = null;

                for (int i = 0; i < 4; i++)
                {
                    InputPrimitive = (DSP_Primitive_Input) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Input, i, 0);
                    InputLabel = (Label) Controls.Find("lblInput" + i, true).FirstOrDefault();

                    if (InputLabel != null)
                    {
                        if (InputPrimitive != null)
                        {
                            InputLabel.Text = InputPrimitive.InputName;
                        }
                        else
                        {
                            InputLabel.Text = "Input " + (i + 1).ToString();
                        }
                    }

                }

                lblOutput0.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 0, 0)).OutputName;
                if (CH2_hidden)
                {
                    lblOutput1.Text = "(Bridged)";
                }
                else
                {
                    lblOutput1.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 1, 0)).OutputName;

                }

                if (CH3_hidden)
                {
                    lblOutput2.Text = "(Bridged)";
                }
                else
                {
                    lblOutput2.Text = ((DSP_Primitive_Output)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 2, 0)).OutputName;
                }

                
                if (CH4_hidden)
                {
                    lblOutput3.Text = "(Bridged)";
                }
                else
                {
                    lblOutput3.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 3, 0)).OutputName;
                }
            
                int inputIndex = 0;

                DSP_Primitive_MixerCrosspoint SingleCrosspoint;

                // Inputs
                for (int i = 0; i < 6; i++)
                {

                    inputIndex = i < 4 ? i : i + 4;
                    // Outputs
                    for (int j = 0; j < 4; j++)
                    {
                        PictureButton pbControl = (PictureButton)Controls.Find("btnRouter" + (inputIndex).ToString() + "" + (j).ToString(), true).First();


                        if ((CH2_hidden && j == 1) || (CH3_hidden && j == 2) || (CH4_hidden && j == 3))
                        {
                            pbControl.Visible = false;
                        }
                        else
                        {
                            // Create the ToolTip and associate with the Form container.
                            ToolTip toolTip1 = new ToolTip();

                            // Set up the delays for the ToolTip.
                            toolTip1.AutoPopDelay = 5000;
                            toolTip1.InitialDelay = 10;
                            toolTip1.ReshowDelay = 50;
                            // Force the ToolTip text to be displayed whether or not the form is active.
                            toolTip1.ShowAlways = true;

                            SingleCrosspoint = (DSP_Primitive_MixerCrosspoint) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.MixerCrosspoint, inputIndex, j);

                            if (SingleCrosspoint.Muted)
                            {
                                toolTip1.SetToolTip(pbControl, "Muted");
                                pbControl.Overlay3Visible = true;
                            }
                            else
                            {
                                toolTip1.SetToolTip(pbControl, SingleCrosspoint.Gain.ToString("N1") + "dB");
                                pbControl.Overlay1Visible = true;
                            }
                        }
                        pbControl.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in MixerForm6x4]: " + ex.Message);
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
            int index_out = int.Parse(((PictureButton)sender).Name.Substring(10, 1));

            ActiveCrosspoint = (DSP_Primitive_MixerCrosspoint)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.MixerCrosspoint, index_in, index_out);


            using (GainForm gainForm = new GainForm(PARENT_FORM, ActiveCrosspoint,DSP_Primitive_Types.MixerCrosspoint))
            {
                gainForm.Width = 132;
                DialogResult showResult = gainForm.ShowDialog(this);

                if(showResult == DialogResult.Cancel)
                {
                    //PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].crosspoints[index_in - 1][index_out - 1] = (GainConfig) cached_gain.Clone();
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
                if (ActiveCrosspoint.Muted)
                {
                    toolTip1.SetToolTip(crosspoint_button, "Muted");
                }
                else
                {
                    toolTip1.SetToolTip(crosspoint_button, ActiveCrosspoint.Gain.ToString("N1") + "dB");
                }
                if (!ActiveCrosspoint.Muted)
                {
                    crosspoint_button.Overlay3Visible = false;
                    crosspoint_button.Overlay1Visible = true; 
                    
                } else
                {
                    crosspoint_button.Overlay3Visible = true;
                }

                crosspoint_button.Invalidate();
            }
        }

        private void MixerForm6x4_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeter1.Stop();
            gainMeter2.Stop();
            gainMeter3.Stop();
            gainMeter4.Stop();
        }

    }
}
