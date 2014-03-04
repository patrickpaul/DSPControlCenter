using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SA_Resources.SAControls
{
    public partial class SignalMeter : PictureBox
    {
        private double db_value;

        public SignalMeter()
        {
            this.BackgroundImage = SA_Resources.GlobalResources.ui_meter_base;
            this.AutoSize = false;
            this.Size = new Size(43,225);
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
            
            Rectangle ee = new Rectangle(26, 7, 13, gain_to_meter());
            using (SolidBrush myBrush = new SolidBrush(Color.FromArgb(80, 80, 80)))
            {
                e.Graphics.FillRectangle(myBrush, ee);
            }

            
        }


    }
}
