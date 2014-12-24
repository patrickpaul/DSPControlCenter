using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;
using SA_Resources.SADevices;

namespace SA_Resources.SAForms
{
    public partial class MeterViewForm4Net : Form
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

        public MeterViewForm4Net(MainForm_Template _parentForm)
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

                if (dType == DeviceType.FLX804Net || dType == DeviceType.FLX804CVNet)
                {
                    // No changes to be made

                }
                else if (dType == DeviceType.FLX1602Net || dType == DeviceType.FLX1602CVNet)
                {

                    outmeter_2_enabled = false;
                    outMeter2.Visible = false;
                    lblOutputMeter2.Visible = false;

                    outMeter3.Location = new Point(outMeter2.Location.X, outMeter3.Location.Y);
                    lblOutputMeter3.Location = new Point(lblOutputMeter2.Location.X, lblOutputMeter2.Location.Y);
                    lblOutputMeter3.Text = "Output 2";

                    outmeter_4_enabled = false;
                    outMeter4.Visible = false;
                    lblOutputMeter4.Visible = false;

                    this.Width = Helpers.NormalizeFormDimension(680);
                    pbtnClose.Location = new Point(305, pbtnClose.Location.Y);
                }
                else if (dType == DeviceType.FLX3201CVNet)
                {

                    outmeter_2_enabled = false;
                    outMeter2.Visible = false;
                    lblOutputMeter2.Visible = false;
                    
                    outmeter_3_enabled = false;
                    outMeter3.Visible = false;
                    lblOutputMeter3.Visible = false;

                    outmeter_4_enabled = false;
                    outMeter4.Visible = false;
                    lblOutputMeter4.Visible = false;

                    this.Width = Helpers.NormalizeFormDimension(618);
                    pbtnClose.Location = new Point(274, pbtnClose.Location.Y);
                }

                if (PARENT_FORM.LIVE_MODE)
                {
                    
                    inMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 0, 0).Address;
                    inMeter1.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter1.Start();

                    inMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 1, 0).Address;
                    inMeter2.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter2.Start();

                    inMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 2, 0).Address;
                    inMeter3.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter3.Start();

                    inMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 3, 0).Address;
                    inMeter4.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter4.Start();

                    inMeter5.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 4, 0).Address;
                    inMeter5.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter5.Start();

                    inMeter6.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 5, 0).Address;
                    inMeter6.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter6.Start();

                    inMeter7.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 6, 0).Address;
                    inMeter7.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter7.Start();

                    inMeter8.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Input, 7, 0).Address;
                    inMeter8.PIC_CONN = PARENT_FORM._PIC_Conn;
                    inMeter8.Start();

                    if (outmeter_1_enabled)
                    {
                        outMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 0, 0).Address;
                        outMeter1.PIC_CONN = PARENT_FORM._PIC_Conn;
                        outMeter1.Start();
                    }
                    else
                    {
                        outMeter1.Visible = false;
                    }

                    if (outmeter_2_enabled)
                    {
                        outMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 1, 0).Address;
                        outMeter2.PIC_CONN = PARENT_FORM._PIC_Conn;
                        outMeter2.Start();
                    }
                    else
                    {
                        outMeter2.Visible = false;
                    }

                    if (outmeter_3_enabled)
                    {
                        outMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 2, 0).Address;
                        outMeter3.PIC_CONN = PARENT_FORM._PIC_Conn;
                        outMeter3.Start();
                    }
                    else
                    {
                        outMeter3.Visible = false;
                    }

                    if (outmeter_4_enabled)
                    {
                        outMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 3, 0).Address;
                        outMeter4.PIC_CONN = PARENT_FORM._PIC_Conn;
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
                Console.WriteLine("[Exception in MeterViewForm]: " + ex.Message);
            }

        }




        private void MeterViewForm4Net_FormClosing(object sender, FormClosingEventArgs e)
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
                Console.WriteLine("[ThreadAbortException in MeterViewForm2Net_FormClosing]: " + taex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in MeterViewForm2Net_FormClosing]: " + ex.Message); 
            }
        }

        private void pictureButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
