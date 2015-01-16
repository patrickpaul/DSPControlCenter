using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.DeviceManagement;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;

using SA_Resources.Utilities;

namespace SA_Resources.SAForms
{
    public partial class MeterViewForm : Form
    {

        #region Disable form's closing button
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

        #endregion

        private MainForm_Template PARENT_FORM;

        public MeterViewForm(MainForm_Template _parentForm)
        {
            InitializeComponent();

            try
            {
                PARENT_FORM = _parentForm;

                bool outmeter_1_enabled = true;
                bool outmeter_2_enabled = true;
                bool outmeter_3_enabled = true;
                bool outmeter_4_enabled = true;

                DeviceType dType = PARENT_FORM.GetDeviceType();
                DeviceFamily dFamily = PARENT_FORM.GetDeviceFamily();

                if (dType == DeviceType.DSP1001 || dType == DeviceType.FLX3201)
                {
                    this.Width = Helpers.NormalizeFormDimension(377);
                    pbtnClose.Location = new Point(153, 278);

                    outmeter_2_enabled = false;
                    outmeter_3_enabled = false;
                    outmeter_4_enabled = false;

                }
                else if (dType == DeviceType.DSP1002 || dType == DeviceType.DSP1002LZ)
                {
                    this.Width = Helpers.NormalizeFormDimension(439);
                    pbtnClose.Location = new Point(184, 278);

                    outmeter_3_enabled = false;
                    outmeter_4_enabled = false;
                }
                else if (dType == DeviceType.FLX1602 || dType == DeviceType.FLX1602CV)
                {
                    this.Width = Helpers.NormalizeFormDimension(439);
                    pbtnClose.Location = new Point(184, 278);

                    outmeter_2_enabled = false;
                    // Hide this meter since Channel 3 will go on top of it
                    outMeter2.Visible = false;
                    lblOutputMeter2.Visible = false;

                    outmeter_3_enabled = true;
                    // Move Channel 3 output meter to Channel 2 location
                    outMeter3.Location = new Point(outMeter2.Location.X, outMeter2.Location.Y);
                    lblOutputMeter3.Location = new Point(lblOutputMeter2.Location.X, lblOutputMeter2.Location.Y);
                    lblOutputMeter3.Text = "Output 2";

                    outmeter_4_enabled = false;
                }

                if (PARENT_FORM.LIVE_MODE)
                {
                    
                    inMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 0, 0).Address;
                    inMeter1.DeviceConn = PARENT_FORM.DeviceConn;
                    inMeter1.Start();

                    inMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 1, 0).Address;
                    inMeter2.DeviceConn = PARENT_FORM.DeviceConn;
                    inMeter2.Start();

                    inMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 2, 0).Address;
                    inMeter3.DeviceConn = PARENT_FORM.DeviceConn;
                    inMeter3.Start();

                    inMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 3, 0).Address;
                    inMeter4.DeviceConn = PARENT_FORM.DeviceConn;
                    inMeter4.Start();

                    if (outmeter_1_enabled)
                    {
                        outMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 0, 0).Address;
                        outMeter1.DeviceConn = PARENT_FORM.DeviceConn;
                        outMeter1.Start();
                    }
                    else
                    {
                        outMeter1.Visible = false;
                    }

                    if (outmeter_2_enabled)
                    {
                        outMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 1, 0).Address;
                        outMeter2.DeviceConn = PARENT_FORM.DeviceConn;
                        outMeter2.Start();
                    }
                    else
                    {
                        outMeter2.Visible = false;
                    }

                    if (outmeter_3_enabled)
                    {
                        outMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 2, 0).Address;
                        outMeter3.DeviceConn = PARENT_FORM.DeviceConn;
                        outMeter3.Start();
                    }
                    else
                    {
                        outMeter3.Visible = false;
                    }

                    if (outmeter_4_enabled)
                    {
                        outMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 3, 0).Address;
                        outMeter4.DeviceConn = PARENT_FORM.DeviceConn;
                        outMeter4.Start();
                    }
                    else
                    {
                        outMeter4.Visible = false;
                    }


                }
                  
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in MeterViewForm]: " + ex.Message);
            }

        }

  
        

        private void MixerForm6x4_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                inMeter1.Stop();
                inMeter2.Stop();
                inMeter3.Stop();
                inMeter4.Stop();

                outMeter1.Stop();
                outMeter2.Stop();
                outMeter3.Stop();
                outMeter4.Stop();
            }
            catch (ThreadAbortException taex)
            {
                Debug.WriteLine("[ThreadAbortException in Mixerform6x4_FormClosing]: " + taex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in Mixerform6x4_FormClosing]: " + ex.Message); 
            }
        }

        private void pictureButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
