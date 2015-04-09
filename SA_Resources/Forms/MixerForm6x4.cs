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

                DeviceType dType = PARENT_FORM.GetDeviceType();
                DeviceFamily dFamily = PARENT_FORM.GetDeviceFamily();
                
                if (dType == DeviceType.DSP4x4)
                {
                    if (PARENT_FORM.LIVE_MODE)
                    {
                        this.Width = Helpers.NormalizeFormDimension(499);
                    }
                    else
                    {
                        this.Width = Helpers.NormalizeFormDimension(231);
                    }
                }
                else if (dFamily == DeviceFamily.FLX)
                {
                    if (dType == DeviceType.FLX804)
                    {
                        switch (PARENT_FORM.AmplifierBridgeMode)
                        {
                            case BridgeMode.FourChannel:
                                if (PARENT_FORM.LIVE_MODE)
                                {
                                    this.Width = Helpers.NormalizeFormDimension(499);
                                }
                                else
                                {
                                    this.Width = Helpers.NormalizeFormDimension(231);
                                }
                                break;

                            case BridgeMode.TwoChannel:

                                CH2_hidden = true;
                                CH3_hidden = false;
                                CH4_hidden = true;

                                panelCH2.Visible = false; 
                                panelCH3.Visible = true;
                                panelCH4.Visible = false;

                                panelCH3.Location = new Point(panelCH2.Location.X, panelCH2.Location.Y);

                                string secondBridgedName = ((DSP_Primitive_Output)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.Output, 2, 0)).OutputName;


                                lblOutputMeter3.Text = secondBridgedName.Substring(0, Math.Min(secondBridgedName.Length, 10));
                                lblOutputMeter3.Invalidate();
                                lblOutputMeter3.Refresh();

                                toolTip1.SetToolTip(lblOutputMeter3,secondBridgedName);

                                gainMeter2.Visible = false;
                                gainMeter3.Visible = true;
                                gainMeter4.Visible = false;

                                if (PARENT_FORM.LIVE_MODE)
                                {
                                    this.Width = Helpers.NormalizeFormDimension(290);
                                    gainMeter1.Location = new Point(155, gainMeter1.Location.Y);
                                    lblOutputMeter1.Location = new Point(150, lblOutputMeter1.Location.Y);
                                    gainMeter3.Location = new Point(216, gainMeter3.Location.Y);
                                    lblOutputMeter3.Location = new Point(211, lblOutputMeter3.Location.Y);
                                    btnSave.Location = new Point(111, btnSave.Location.Y);
                                }
                                else
                                {
                                    this.Width = Helpers.NormalizeFormDimension(171);
                                    btnSave.Location = new Point(53, btnSave.Location.Y);
                                }

                                break;


                        }
                    }
                    else if (dType == DeviceType.FLX804CV)
                    {
                        if (PARENT_FORM.LIVE_MODE)
                        {
                            this.Width = Helpers.NormalizeFormDimension(499);
                        }
                        else
                        {
                            this.Width = Helpers.NormalizeFormDimension(231);
                        }
                    }
                    else if (dType == DeviceType.FLX1602 || dType == DeviceType.FLX1602CV)
                    {
                        CH2_hidden = true;
                        CH3_hidden = false;
                        CH4_hidden = true;

                        panelCH2.Visible = false; 
                        panelCH3.Visible = true;
                        panelCH4.Visible = false;

                        panelCH3.Location = new Point(panelCH2.Location.X, panelCH2.Location.Y);
                        lblOutputMeter3.Text = "Output 2";

                        gainMeter2.Visible = false;
                        gainMeter3.Visible = true;
                        gainMeter4.Visible = false;

                        if (PARENT_FORM.LIVE_MODE)
                        {
                            this.Width = Helpers.NormalizeFormDimension(290);
                            gainMeter1.Location = new Point(155, gainMeter1.Location.Y);
                            lblOutputMeter1.Location = new Point(150, lblOutputMeter1.Location.Y);
                            gainMeter3.Location = new Point(216, gainMeter3.Location.Y);
                            lblOutputMeter3.Location = new Point(211, lblOutputMeter3.Location.Y);
                            btnSave.Location = new Point(111, btnSave.Location.Y);
                        }
                        else
                        {
                            this.Width = Helpers.NormalizeFormDimension(171);
                            btnSave.Location = new Point(53, btnSave.Location.Y);
                        }
                    }
                    else if (dType == DeviceType.FLX3201)
                    {
                        CH2_hidden = true;
                        CH3_hidden = true;
                        CH4_hidden = true;

                        panelCH2.Visible = false;
                        panelCH3.Visible = false;
                        panelCH4.Visible = false;

                        panelCH3.Location = new Point(panelCH2.Location.X, panelCH2.Location.Y);
                        lblOutputMeter3.Visible = false;

                        gainMeter2.Visible = false;
                        gainMeter3.Visible = false;
                        gainMeter4.Visible = false;

                        if (PARENT_FORM.LIVE_MODE)
                        {
                            this.Width = Helpers.NormalizeFormDimension(230);
                            gainMeter1.Location = new Point(155, gainMeter1.Location.Y);
                            lblOutputMeter1.Location = new Point(150, lblOutputMeter1.Location.Y);
                            gainMeter3.Location = new Point(216, gainMeter3.Location.Y);
                            lblOutputMeter3.Location = new Point(211, lblOutputMeter3.Location.Y);
                            btnSave.Location = new Point(111, btnSave.Location.Y);
                        }
                        else
                        {
                            this.Width = Helpers.NormalizeFormDimension(171);
                            btnSave.Location = new Point(53, btnSave.Location.Y);
                        }
                    }
                }
                else if (dType == DeviceType.DSP1001)
                {
                    // Only panelCH1 visible.
                    CH2_hidden = true;
                    CH3_hidden = true;
                    CH4_hidden = true;

                    panelCH2.Visible = false;
                    panelCH3.Visible = false;
                    panelCH4.Visible = false;

                    gainMeter2.Visible = false;
                    gainMeter3.Visible = false;
                    gainMeter4.Visible = false;

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
                else if (dType == DeviceType.DSP1002 || dType == DeviceType.DSP1002LZ)
                {
                    CH2_hidden = false;
                    CH3_hidden = true;
                    CH4_hidden = true;

                    panelCH3.Visible = false;
                    panelCH4.Visible = false;

                    gainMeter2.Visible = true;
                    gainMeter3.Visible = false;
                    gainMeter4.Visible = false;

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

                    if (!CH3_hidden)
                    {
                        gainMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Type.MixerCrosspoint, 0, 0, 2).Address;
                        gainMeter3.DeviceConn = PARENT_FORM.DeviceConn;
                        gainMeter3.Start();
                    }
                    if (!CH4_hidden)
                    {
                        gainMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Type.MixerCrosspoint, 0, 0, 3).Address;
                        gainMeter4.DeviceConn = PARENT_FORM.DeviceConn;
                        gainMeter4.Start();
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

                if (CH3_hidden)
                {
                    lblOutput2.Text = PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100 ? "" : "(Bridged)";
                }
                else
                {
                    lblOutput2.Text = ((DSP_Primitive_Output)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.Output, 2, 0)).OutputName;
                }

                
                if (CH4_hidden)
                {
                    lblOutput3.Text = PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100 ? "" : "(Bridged)";
                }
                else
                {
                    lblOutput3.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Type.Output, 3, 0)).OutputName;
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
                Debug.WriteLine("[Exception in MixerForm6x4]: " + ex.Message + " - Trace - " + ex.StackTrace);
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

        private void MixerForm6x4_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeter1.Stop();
            gainMeter2.Stop();
            gainMeter3.Stop();
            gainMeter4.Stop();
        }

        private void MixerForm6x4_Load(object sender, EventArgs e)
        {

        }


    }
}
