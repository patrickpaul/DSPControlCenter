using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.USB;

namespace SA_Resources.SAControls
{
    public partial class SASignalMeter : UserControl
    {

        private double db_value;
        private DSP_Meter MeterPrimitive;
        private PIC_Bridge PicBridge;

        public SASignalMeter()
        {
            InitializeComponent();

            this.pictureBox1.BackgroundImage = SA_Resources.GlobalResources.ui_meter_base;
            this.DB = 0;
        }

        public double DB
        {
            get
            {
                return this.db_value;
            }
            set
            {
                this.db_value = Math.Min(35, Math.Max(-35, value));
                this.pictureBox1.Invalidate();
            }
        }

        public Boolean MeterEnabled
        {
            get
            {
                return this.timer1.Enabled;
            }
            set
            {
                this.timer1.Enabled = value;

            }
        }

        public int UpdateInterval
        {
            get
            {
                return this.timer1.Interval;

            }
            set
            {
                this.timer1.Interval = value;

            }
        }

        public void Register_Meter_Primitive(DSP_Meter in_meter)
        {
            this.MeterPrimitive = in_meter;
        }

        public void Register_PIC_Bridge(PIC_Bridge in_bridge)
        {
            this.PicBridge = in_bridge;
        }
    
        private int scale_between(double value, double upper, double lower, int pixel_upper, int pixel_lower)
        {
            int pixel_diff = Math.Abs(pixel_lower - pixel_upper);

            double percentage = Math.Abs(value - upper) / Math.Abs(upper - lower);

            return (int)(percentage * pixel_diff) + pixel_upper;
        }

        private int gain_to_meter()
        {
            if (db_value <= -35)
            {
                return 214;
            }
            else if (db_value <= -25)
            {
                return scale_between(db_value, -25.0, -35.0, 192, 214);
            }
            else if (db_value <= -15)
            { 
                return scale_between(db_value, -15.0, -25.0, 170, 192);
            }
            else if (db_value <= -10)
            {
                return scale_between(db_value, -10.0, -15.0, 149, 170);
            }
            else if (db_value <= -6)
            {
                return scale_between(db_value, -6.0, -10.0, 127, 149);
            }
            else if (db_value <= -2)
            {
                return scale_between(db_value, -2.0, -6.0, 106, 127);
            }
            else if (db_value <= 0)
            {
                return scale_between(db_value, 0.0, -2.0, 84, 106);
            }
            else if (db_value <= 4)
            {
                return scale_between(db_value, 4.0, 0.0, 63, 84);
            }
            else if (db_value <= 10)
            {
                return scale_between(db_value, 10, 4.0, 41, 63);
            }
            else if (db_value <= 20)
            {
                return scale_between(db_value, 20.0, 10.0, 20, 41);
            }
            else if (db_value <= 35)
            {
                return scale_between(db_value, 35.0, 20.0, 0, 20);
            }

            return 214;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle ee = new Rectangle(26, 7, 13, gain_to_meter());
            using (SolidBrush myBrush = new SolidBrush(Color.FromArgb(80, 80, 80)))
            {
                e.Graphics.FillRectangle(myBrush, ee);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double offset = 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_value = this.PicBridge.Read_Live_DSP_Value(this.MeterPrimitive.Address);
            double converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

            if (converted_value > (0.000001 * 0.000001))
            {
                this.DB = offset + 10 * Math.Log10(converted_value);
            }
            else
            {
                this.DB = -100;
            }

            pictureBox1.Invalidate();
        }
    }
}
