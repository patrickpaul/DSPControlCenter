using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SA_Resources
{
    public partial class SignalMeter_Small : PictureBox
    {
        private double db_value;

        public SignalMeter_Small()
        {
            this.BackgroundImage = SA_Resources.GlobalResources.ui_meter_base_small;
            this.AutoSize = false;
            this.Size = new Size(30,157);
            this.DB = -35;
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
                return 149;
            }
            else if (db_value <= -25)
            {
                return scale_between(db_value, -25.0, -35.0, 135, 149);
            }
            else if (db_value <= -15)
            {
                return scale_between(db_value, -15.0, -25.0, 117, 135);
            }
            else if (db_value <= -10)
            {
                return scale_between(db_value, -10.0, -15.0, 103, 117);
            }
            else if (db_value <= -6)
            {
                return scale_between(db_value, -6.0, -10.0, 88, 103);
            }
            else if (db_value <= -2)
            {
                return scale_between(db_value, -2.0, -6.0, 72, 88);
            }
            else if (db_value <= 0)
            {
                return scale_between(db_value, 0.0, -2.0, 57, 72);
            }
            else if (db_value <= 4)
            {
                return scale_between(db_value, 4.0, 0.0, 43, 57);
            }
            else if (db_value <= 10)
            {
                return scale_between(db_value, 10, 4.0, 28, 43);
            }
            else if (db_value <= 20)
            {
                return scale_between(db_value, 20.0, 10.0, 13, 28);
            }
            else if (db_value <= 35)
            {
                return scale_between(db_value, 35.0, 20.0, 0, 13);
            }

            return 149;
        }

        public double DB
        {
            get
            {
                return this.db_value;
            }
            set
            {
                this.db_value = Math.Min(35,Math.Max(-35,value));
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); 
            
            Rectangle ee = new Rectangle(18,5,9, gain_to_meter());
            using (SolidBrush myBrush = new SolidBrush(Color.FromArgb(80, 80, 80)))
            {
                e.Graphics.FillRectangle(myBrush, ee);
            }

            
        }


    }
}
