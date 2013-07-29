using System;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources
{
    public sealed class Helpers
    {
        public static Color Lighten(Color inColor, double inAmount)
        {
            return Color.FromArgb(
              inColor.A,
              (int)Math.Min(255, inColor.R + 255 * inAmount),
              (int)Math.Min(255, inColor.G + 255 * inAmount),
              (int)Math.Min(255, inColor.B + 255 * inAmount));
        }

        public static Color Darken(Color inColor, double inAmount)
        {
            return Color.FromArgb(
                inColor.A,
                (int)(inColor.R * inAmount),
                (int)(inColor.G * inAmount),
                (int)(inColor.B * inAmount)
                );

        }

        public static double geq_to_db(double faderValue) {
           return (-1 * (24 - (0.1 * faderValue)));
        }

        public static int db_to_geq(double db_value)
        {
            return (int)((db_value+24) * 10);
        }

        public static void loadRouterValue(UInt32 value, RadioButton check1, RadioButton check2, RadioButton check3)
        {
            if (value == 0x00000001)
            {
                check1.Checked = true;
            }
            else if (value == 0x00000002)
            {
                check2.Checked = true;
            }
            else
            {
                check3.Checked = true;
            }

        }

        public static void loadRouterValue(UInt32 value, RadioButton check1, RadioButton check2, RadioButton check3, RadioButton check4)
        {
            if (value == 0x00000001)
            {
                check1.Checked = true;
            }
            else if (value == 0x00000002)
            {
                check2.Checked = true;
            }
            else if (value == 0x00000003)
            {
                check3.Checked = true;
            }
            else
            {
                check4.Checked = true;
            }

        }

        public static void loadRouterValue(UInt32 value, RadioButton check1, RadioButton check2, RadioButton check3, RadioButton check4, RadioButton check5, RadioButton check6)
        {
            if (value == 0x00000001)
            {
                check1.Checked = true;
            }
            else if (value == 0x00000002)
            {
                check2.Checked = true;
            }
            else if (value == 0x00000003)
            {
                check3.Checked = true;
            }
            else if (value == 0x00000004)
            {
                check4.Checked = true;
            }
            else if (value == 0x00000005)
            {
                check5.Checked = true;
            }
            else
            {
                check6.Checked = true;
            }

        }

        public static UInt32 bool_to_value(bool in_bool)
        {
            if (in_bool)
            {
                return 0x00000001;
            }

            return 0x00000000;
        }

        public static UInt32 radiogroup_to_value(bool checked1, bool checked2, bool checked3)
        {
            if (checked1)
            {
                return 0x0000001;
            }

            if (checked2)
            {
                return 0x0000002;
            }

            return 0x0000003;
        }

        public static UInt32 radiogroup_to_value(bool checked1, bool checked2, bool checked3, bool checked4)
        {
            if (checked1)
            {
                return 0x0000001;
            }

            if (checked2)
            {
                return 0x0000002;
            }

            if (checked3)
            {
                return 0x0000003;
            }

            return 0x0000004;
        }

        public static UInt32 radiogroup_to_value(bool checked1, bool checked2, bool checked3, bool checked4, bool checked5, bool checked6)
        {
            if (checked1)
            {
                return 0x0000001;
            }

            if (checked2)
            {
                return 0x0000002;
            }

            if (checked3)
            {
                return 0x0000003;
            }

            if (checked4)
            {
                return 0x0000004;
            }

            if (checked5)
            {
                return 0x0000005;
            }

            return 0x0000006;
        }


        public static UInt32 bypass_router_to_checked(bool is_checked)
        {
            if (is_checked)
            {
                return 0x00000001;
            }

            return 0x00000002;
        }

        
    }
}
