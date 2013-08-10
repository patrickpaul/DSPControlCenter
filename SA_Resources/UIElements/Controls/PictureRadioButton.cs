using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources;

namespace SA_Resources
{


    public class PictureRadioButton : RadioButton
    {
        Image checkedImage, uncheckedImage;

        public PictureRadioButton()
        {
            this.Cursor = Cursors.Hand;
        }

        public Image CheckedImage
        {
            get
            {
                return this.checkedImage;
            }
            set
            {
                this.checkedImage = value;
            }
        }

        // Property for the background image to be drawn behind the button text when
        // the button is pressed.
        public Image UncheckedImage
        {
            get
            {
                return this.uncheckedImage;
            }
            set
            {
                this.uncheckedImage = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //this.AutoSize = false;
            //this.Text = "";
            //this.Margin = new Padding(0);
            //this.Padding = new Padding(0);
            //this.Size = new Size(16, 16);

            if (this.Text.Length > 0)
            {
                base.OnPaint(e);
            }
            else
            {
                this.AutoSize = false;
                if (this.uncheckedImage != null)
                {
                    this.Size = new Size(this.uncheckedImage.Width, this.uncheckedImage.Height);
                }
                else
                {
                    this.Size = new Size(17, 17);
                }
            } 
            
            if (this.Checked)
            {
                if (this.checkedImage != null)
                {
                    e.Graphics.DrawImageUnscaled(this.checkedImage, 0, 0);
                }
                else
                {
                    e.Graphics.DrawImageUnscaled(ControlIcons.RadioButton_Checked, 0, 0);
                }
            }
            else
            {
                if (this.uncheckedImage != null)
                {
                    e.Graphics.DrawImageUnscaled(this.uncheckedImage, 0, 0);
                }
                else
                {
                    e.Graphics.DrawImageUnscaled(ControlIcons.RadioButton_Unchecked, 0, 0);
                }
            }
        }
    }
}
