using System;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources.SAControls
{
    public class DialEventArgs : EventArgs
    {
        // Constructor.
        public DialEventArgs() {}
        // Properties.

    }

    public class Dial
    {

        public event DialEventHandler OnChange;

        public string Textbox_Name;
        public TextBox Textbox;

        public string Picturebox_Name;
        public PictureBox Picturebox;

        public double[] Dial_Ticks;

        public Func<double, string> Format_Callback;

        public Image DialBackground;

        public Image DialLine;

        public int Rotations;

        private int startingY, startingRotation, deltaY;
        private bool is_dragging = false;

        private string startingTextValue;
        private bool editingTextbox = false;

        public Image unrotatedLine = null;

        public Dial(TextBox in_textbox, PictureBox in_picturebox, double[] in_ticks, Func<double, string> in_callback, Image in_background, Image in_line, int in_rotations = 0)
        {
            Textbox = in_textbox;
            Textbox_Name = in_textbox.Name;

            Picturebox = in_picturebox;
            Picturebox_Name = in_picturebox.Name;

            DialLine = in_line; 
            
            Picturebox.BackgroundImage = in_background;
            Picturebox.Image = DialLine;

            unrotatedLine = new Bitmap(in_line);
            Dial_Ticks = in_ticks;

            Format_Callback = in_callback;

            DialBackground = in_background;
            

            Rotations = in_rotations;

            in_picturebox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Event_MouseDown);
            in_picturebox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Event_MouseMove);
            in_picturebox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_MouseUp);

            in_textbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_Textbox_MouseUp);
            in_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            in_textbox.Leave += new System.EventHandler(this.Event_Textbox_Leave);
            
        }

        protected void OnChangeEvent(DialEventArgs e)
        {
            if(this.OnChange != null)
            {
                try
                {
                    OnChange(this, e);
                } catch (Exception ex)
                {
                    Console.WriteLine("Exception in Dial.OnChangeEvent: " + ex.Message);
                }
            }
        } 

        private void Event_MouseDown(object sender, MouseEventArgs e)
        {
            startingY = e.Y;
            is_dragging = true;

            startingRotation = Rotations;
        }

        private void Event_MouseMove(object sender, MouseEventArgs e)
        {
            if (!is_dragging) return;

            deltaY = startingY - e.Y;

            Rotations = startingRotation + deltaY;

            if (Rotations > 250)
            {
                Rotations = 250;
            }

            if (Rotations < 0)
            {
                Rotations = 0;
            }

            Textbox.Text = DialHelpers.rotations_to_value(Rotations, Format_Callback, Dial_Ticks);
            

            Image oldImage = Picturebox.Image;

            Picturebox.Image = ImageHelpers.RotateImage(unrotatedLine, (float)(DialHelpers.rotations_to_degrees(Rotations)));

            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        private void Event_MouseUp(object sender, MouseEventArgs e)
        {
            is_dragging = false;

            DialEventArgs args = new DialEventArgs();
            OnChangeEvent(args);
            
        }

        private void Event_Textbox_MouseUp(object sender, MouseEventArgs e)
        {
            startingTextValue = Textbox.Text;
            Textbox.SelectAll();
            editingTextbox = true;
        }

        private void Event_Textbox_Leave(object sender, EventArgs e)
        {
            if (editingTextbox)
            {
                editingTextbox = false;
                Textbox.Select(0, 0);
                Update_Dial_From_Textbox();

            }
        }

        private void Update_Dial_From_Textbox()
        {
            double new_value = UI_Helpers.validate_textbox_value(Textbox.Text, Dial_Ticks[0], Dial_Ticks[6]);
            Rotations = DialHelpers.value_to_rotations(new_value, Dial_Ticks);

            Picturebox.Image = ImageHelpers.RotateImage(unrotatedLine, (float)DialHelpers.rotations_to_degrees(Rotations));
            Textbox.Text = Format_Callback(new_value);
            Picturebox.Focus();

            DialEventArgs args = new DialEventArgs();
            OnChangeEvent(args);
        }

        private void Event_Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                editingTextbox = false;
                Textbox.Select(0, 0);
                Update_Dial_From_Textbox();
            }


            string allowedCharacterSet = "0123456789.-\b\n";
            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {
                if ((e.KeyChar.ToString() == "." && Textbox.Text.Contains(".")) || (e.KeyChar.ToString() == "-" && Textbox.Text != "" && Textbox.SelectedText == ""))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public void Disable()
        {
            Picturebox.Enabled = false;
            Textbox.Enabled = false;
        }

        public void Enable()
        {
            Picturebox.Enabled = true;
            Textbox.Enabled = true;

        }

        public double Value
        {
            get
            {
                return ValueFromString(Textbox.Text);
            }

            set
            {
                Rotations = DialHelpers.value_to_rotations(value, Dial_Ticks);
                Picturebox.Image = ImageHelpers.RotateImage(unrotatedLine, (float) DialHelpers.rotations_to_degrees(Rotations));
                Textbox.Text = Format_Callback(value);
            }
        }

        public double ValueFromString(string in_string)
        {
            if (in_string.Contains("kHz"))
            {
                return (Double.Parse(in_string.Replace("kHz", "")) * 1000.0);
            }
            else if (in_string.Contains("Hz"))
            {
                return Double.Parse(in_string.Replace("Hz", ""));
            }
            else if (in_string.Contains("ms"))
            {
                return (Double.Parse(in_string.Replace("ms", "")) / 1000.0);
            }
            else if (in_string.Contains("ft"))
            {
                return (Double.Parse(in_string.Replace("ft", "")));
            }
            else if (in_string.Contains("m"))
            {
                return (Double.Parse(in_string.Replace("m", "")));
            }

            else if (in_string.Contains("s"))
            {
                return Double.Parse(in_string.Replace("s", ""));
            }
            else if (in_string.Contains("dB"))
            {
                return Double.Parse(in_string.Replace("dB", ""));
            }
            else
            {
                return Double.Parse(in_string);
            }
        }
    }

    public delegate void DialEventHandler(object sender, DialEventArgs e);
}