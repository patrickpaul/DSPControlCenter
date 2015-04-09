using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources.DeviceManagement;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;

using SA_Resources.Utilities;

namespace SA_Resources.SAForms
{
    public partial class MixerForm8x2 : Form
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

        public MixerForm8x2(MainForm_Template _parentForm)
        {
            InitializeComponent();

            try
            {
                PARENT_FORM = _parentForm;

                DeviceType dType = PARENT_FORM.GetDeviceType();
                DeviceFamily dFamily = PARENT_FORM.GetDeviceFamily();


                if (dType == DeviceType.DSP1001Net)
                {
                    // Only panelCH1 visible.
                    CH2_hidden = true;

                    panelCH2.Visible = false;

                    gainMeter2.Visible = false;

                    if (PARENT_FORM.LIVE_MODE)
                    {
                        this.Width = Helpers.NormalizeFormDimension(211);
                        gainMeter1.Location = new Point(136, gainMeter1.Location.Y);
                        lblOutputMeter1.Location = new Point(131, lblOutputMeter1.Location.Y);
                        btnSave.Location = new Point(73, btnSave.Location.Y);
                    }
                    else
                    {
                        this.Width = Helpers.NormalizeFormDimension(142);
                        btnSave.Location = new Point(37, btnSave.Location.Y);
                    }
                }
                else if (dType == DeviceType.DSP1002Net || dType == DeviceType.DSP1002LZNet)
                {
                    CH2_hidden = false;

                    gainMeter2.Visible = true;

                    if (PARENT_FORM.LIVE_MODE)
                    {
                        this.Width = Helpers.NormalizeFormDimension(286);
                        gainMeter1.Location = new Point(155, gainMeter1.Location.Y);
                        lblOutputMeter1.Location = new Point(150, lblOutputMeter1.Location.Y);
                        gainMeter2.Location = new Point(216, gainMeter2.Location.Y);
                        lblOutputMeter2.Location = new Point(211, lblOutputMeter2.Location.Y);
                        btnSave.Location = new Point(111, btnSave.Location.Y);
                    }
                    else
                    {
                        this.Width = Helpers.NormalizeFormDimension(171);
                        btnSave.Location = new Point(53, btnSave.Location.Y);
                    }
                }

                if (PARENT_FORM.LIVE_MODE)
                {
                    gainMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Type.MixerCrosspoint, 0, 0, 0).Address;
                    gainMeter1.DeviceConn = PARENT_FORM.DeviceConn;
                    gainMeter1.Start();

                    if (!CH2_hidden)
                    {
                        gainMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Type.MixerCrosspoint, 0, 0, 1).Address;
                        gainMeter2.DeviceConn = PARENT_FORM.DeviceConn;
                        gainMeter2.Start();
                    }

                }


                DSP_Primitive_Input InputPrimitive = null;
                Label InputLabel = null;

                for (int i = 0; i < 4; i++)
                {
                    InputPrimitive = (DSP_Primitive_Input) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.Input, i, 0);
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

                lblOutput0.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.Output, 0, 0)).OutputName;
                if (CH2_hidden)
                {
                    lblOutput1.Text = PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100 ? "" : "(Bridged)";
                }
                else
                {
                    lblOutput1.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.Output, 1, 0)).OutputName;

                }
            
                int inputIndex = 0;

                DSP_Primitive_MixerCrosspoint SingleCrosspoint;

                // Inputs 
                for (int i = 0; i < 8; i++)
                {

                    inputIndex = i < 6 ? i : i + 2;
                    // Outputs
                    for (int j = 0; j < 2; j++)
                    {
                        PictureButton pbControl = (PictureButton)Controls.Find("btnRouter" + (inputIndex).ToString() + "" + (j).ToString(), true).First();


                        if ((CH2_hidden && j == 1))
                        {
                            //pbControl.Visible = false;
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

                            SingleCrosspoint = (DSP_Primitive_MixerCrosspoint) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.MixerCrosspoint, inputIndex, j);

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
                Debug.WriteLine("[Exception in MixerForm8x2]: " + ex.Message + " - Trace - " + ex.StackTrace);
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

            ActiveCrosspoint = (DSP_Primitive_MixerCrosspoint)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.MixerCrosspoint, index_in, index_out);


            using (GainForm gainForm = new GainForm(PARENT_FORM, ActiveCrosspoint,DSP_Primitive_Type.MixerCrosspoint))
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

        private void MixerForm8x2_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeter1.Stop();
            gainMeter2.Stop();
        }


    }
}
