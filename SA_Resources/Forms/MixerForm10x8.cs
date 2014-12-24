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
using SA_Resources.SADevices;

namespace SA_Resources.SAForms
{
    public partial class MixerForm10x8 : Form
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

        public MixerForm10x8(MainForm_Template _parentForm)
        {
            InitializeComponent();

            try
            {
                PARENT_FORM = _parentForm;

                DeviceType dType = PARENT_FORM.GetDeviceType();
                DeviceFamily dFamily = PARENT_FORM.GetDeviceFamily();

                if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.FLX || PARENT_FORM.GetDeviceFamily() == DeviceFamily.FLXNET)
                {
                    if (dType == DeviceType.FLX804Net || dType == DeviceType.FLX804CVNet)
                    {

                        if (PARENT_FORM.LIVE_MODE)
                        {
                            this.Width = Helpers.NormalizeFormDimension(531);
                        }
                        else
                        {
                            this.Width = Helpers.NormalizeFormDimension(327);
                            this.btnSave.Location = new Point(131, btnSave.Location.Y);

                        }
                    }
                    else if (dType == DeviceType.FLX1602Net || dType == DeviceType.FLX1602CVNet)
                    {
                        CH2_hidden = true;
                        CH3_hidden = false;
                        CH4_hidden = true;

                        panelCH2.Visible = false; 
                        panelCH3.Visible = true;
                        panelCH4.Visible = false;

                        panelCH3.Location = new Point(120, panelCH3.Location.Y); // Put in Channel 2's position
                        panelNetCH1.Location = new Point(146, panelNetCH1.Location.Y); // Put in Channel 3's position
                        panelNetCH2.Location = new Point(172, panelNetCH2.Location.Y); // Put in Channel 4's position
                        panelNetCH3.Location = new Point(198, panelNetCH3.Location.Y); // Put in Net Channel 1's position
                        panelNetCH4.Location = new Point(224, panelNetCH4.Location.Y); // Put in Net Channel 2's position

                        //lblOutputMeter3.Text = "Output 2";

                        gainMeter2.Visible = false;
                        gainMeter3.Visible = true;
                        gainMeter4.Visible = false;

                        if (PARENT_FORM.LIVE_MODE)
                        {
                            this.Width = Helpers.NormalizeFormDimension(477);

                            gainMeter2.Visible = false;
                            lblOutputMeter2.Visible = false;

                            gainMeter3.Location = new Point(gainMeter2.Location.X, gainMeter2.Location.Y);
                            lblOutputMeter3.Location = new Point(lblOutputMeter2.Location.X, lblOutputMeter2.Location.Y);
                            lblOutputMeter3.Text = "2";

                            gainMeter4.Visible = false;
                            lblOutputMeter4.Visible = false;

                            btnSave.Location = new Point(111, btnSave.Location.Y);
                            this.panelMeters.Location = new Point(274, panelMeters.Location.Y);
                        }
                        else
                        {
                            this.panelMeters.Visible = false;
                            this.Width = Helpers.NormalizeFormDimension(271);
                            btnSave.Location = new Point(103, btnSave.Location.Y);
                        }
                    }
                    else if (dType == DeviceType.FLX3201CVNet)
                    {
                        CH2_hidden = true;
                        CH3_hidden = true;
                        CH4_hidden = true;

                        panelCH2.Visible = false;
                        panelCH3.Visible = false;
                        panelCH4.Visible = false;

                        panelNetCH1.Location = new Point(120, panelNetCH1.Location.Y); // Put in Channel 2's position
                        panelNetCH2.Location = new Point(146, panelNetCH2.Location.Y); // Put in Channel 3's position
                        panelNetCH3.Location = new Point(172, panelNetCH3.Location.Y); // Put in Channel 4's position
                        panelNetCH4.Location = new Point(198, panelNetCH4.Location.Y); // Put in Net Channel 1's position

                        gainMeter2.Visible = false;
                        gainMeter3.Visible = false;
                        gainMeter4.Visible = false;

                        if (PARENT_FORM.LIVE_MODE)
                        {
                            this.Width = Helpers.NormalizeFormDimension(450);

                            gainMeter2.Visible = false;
                            lblOutputMeter2.Visible = false;

                            gainMeter3.Visible = false;
                            lblOutputMeter3.Visible = false;

                            gainMeter4.Visible = false;
                            lblOutputMeter4.Visible = false;

                            btnSave.Location = new Point(111, btnSave.Location.Y);
                            this.panelMeters.Location = new Point(247, panelMeters.Location.Y);
                        }
                        else
                        {
                            this.panelMeters.Visible = false;
                            this.Width = Helpers.NormalizeFormDimension(244);
                            btnSave.Location = new Point(90, btnSave.Location.Y);
                        }
                    }
                }
               

                if (PARENT_FORM.LIVE_MODE)
                {
                    
                    //netGainMeter1
                    
                    netGainMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 4).Address;
                    netGainMeter1.PIC_CONN = PARENT_FORM._PIC_Conn;
                    netGainMeter1.Start();

                    netGainMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 5).Address;
                    netGainMeter2.PIC_CONN = PARENT_FORM._PIC_Conn;
                    netGainMeter2.Start();

                    netGainMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 6).Address;
                    netGainMeter3.PIC_CONN = PARENT_FORM._PIC_Conn;
                    netGainMeter3.Start();

                    netGainMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.MixerCrosspoint, 0, 0, 7).Address;
                    netGainMeter4.PIC_CONN = PARENT_FORM._PIC_Conn;
                    netGainMeter4.Start();
                    
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
                    lblOutput1.Text = PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100 ? "" : "(Bridged)";
                }
                else
                {
                    lblOutput1.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 1, 0)).OutputName;

                }

                if (CH3_hidden)
                {
                    lblOutput2.Text = PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100 ? "" : "(Bridged)";
                }
                else
                {
                    lblOutput2.Text = ((DSP_Primitive_Output)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 2, 0)).OutputName;
                }

                
                if (CH4_hidden)
                {
                    lblOutput3.Text = PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100 ? "" : "(Bridged)";
                }
                else
                {
                    lblOutput3.Text = ((DSP_Primitive_Output) PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Output, 3, 0)).OutputName;
                }
            
                int inputIndex = 0;

                DSP_Primitive_MixerCrosspoint SingleCrosspoint;

                // Inputs 
                for (int i = 0; i < 10; i++)
                {

                    inputIndex = i;
                    // Outputs
                    for (int j = 0; j < 8; j++)
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
                Console.WriteLine("[Exception in MixerForm10x8]: " + ex.Message);
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

        private void MixerForm10x8_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeter1.Stop();
            gainMeter2.Stop();
            gainMeter3.Stop();
            gainMeter4.Stop();
        }

    }
}
