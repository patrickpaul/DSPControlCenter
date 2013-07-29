using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SA_Resources
{
    public class UI_Helpers
    {
        public static void disable_all_controls_in_tab(TabPage tabPage, bool skip_checkboxes = false)
        {
            foreach (Control control in tabPage.Controls)
            {
                if (control.Name.Contains("chk") && skip_checkboxes)
                {
                    control.Enabled = true;
                }
                else
                {
                    control.Enabled = false;
                }
            }
        }

        public static void enable_all_controls_in_tab(TabPage tabPage, bool skip_checkboxes = false)
        {
            foreach (Control control in tabPage.Controls)
            {
                if (control.Name.Contains("chk") && skip_checkboxes)
                {
                    control.Enabled = true;
                }
                else
                {
                    control.Enabled = true;
                }
            }
        }

        public static double validate_textbox_value(string input, double minimum, double maximum)
        {
            double parse_value;

            if (!double.TryParse(input, out parse_value))
            {
                return minimum;
            }

            if (parse_value < minimum)
            {
                return minimum;
            }

            if (parse_value > maximum)
            {
                return maximum;
            }

            return parse_value;
        }
    }
}
