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
    public partial class MeterViewForm : Form
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

        private DSP_Primitive_MixerCrosspoint ActiveCrosspoint;

        public MeterViewForm(MainForm_Template _parentForm)
        {
            InitializeComponent();

            try
            {
                PARENT_FORM = _parentForm;

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
                    
                    outMeter1.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 0, 0).Address;
                    outMeter1.PIC_CONN = PARENT_FORM._PIC_Conn;
                    outMeter1.Start();

                    outMeter2.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 1, 0).Address;
                    outMeter2.PIC_CONN = PARENT_FORM._PIC_Conn;
                    outMeter2.Start();

                    outMeter3.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 2, 0).Address;
                    outMeter3.PIC_CONN = PARENT_FORM._PIC_Conn;
                    outMeter3.Start();

                    outMeter4.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, 3, 0).Address;
                    outMeter4.PIC_CONN = PARENT_FORM._PIC_Conn;
                    outMeter4.Start();

                }
                  
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in MeterViewForm]: " + ex.Message);
            }

        }

  
        

        private void MixerForm6x4_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MeterViewForm_Load(object sender, EventArgs e)
        {
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
