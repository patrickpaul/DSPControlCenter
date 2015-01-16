using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.DeviceManagement;
using SA_Resources.DSP.Primitives;

using SA_Resources.Utilities;

namespace SA_Resources.SAForms
{
    public partial class MeterViewForm2Net : Form
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

        public MeterViewForm2Net(MainForm_Template _parentForm)
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

                if (dType == DeviceType.DSP1001Net)
                {
                    this.Width = Helpers.NormalizeFormDimension(490);
                    pbtnClose.Location = new Point(210, 278);

                    outmeter_2_enabled = false;
                    outmeter_3_enabled = false;
                    outmeter_4_enabled = false;
                }
                else if (dType == DeviceType.DSP1002Net || dType == DeviceType.DSP1002LZNet)
                {
                    this.Width = Helpers.NormalizeFormDimension(551);
                    pbtnClose.Location = new Point(270, 278);

                    outmeter_3_enabled = false;
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

                    inMeter5.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 4, 0).Address;
                    inMeter5.DeviceConn = PARENT_FORM.DeviceConn;
                    inMeter5.Start();

                    inMeter6.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 5, 0).Address;
                    inMeter6.DeviceConn = PARENT_FORM.DeviceConn;
                    inMeter6.Start();

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




        private void MeterViewForm2Net_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                inMeter1.Stop();
                inMeter2.Stop();
                inMeter3.Stop();
                inMeter4.Stop();
                inMeter5.Stop();
                inMeter6.Stop();

                outMeter1.Stop();
                outMeter2.Stop();
                outMeter3.Stop();
                outMeter4.Stop();
            }
            catch (ThreadAbortException taex)
            {
                Debug.WriteLine("[ThreadAbortException in MeterViewForm2Net_FormClosing]: " + taex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in MeterViewForm2Net_FormClosing]: " + ex.Message); 
            }
        }

        private void pictureButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
