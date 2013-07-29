using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources
{
    public class PictureButton : Control
    {
        Image backgroundImage, pressedImage, overImage, overlay1Image, overlay2Image, overlay3Image;
        bool pressed = false;
        bool autosize = true;
        bool mouseover = false;
        private bool overlay1Visible = false, overlay2Visible = false, overlay3Visible = false;
        private ToolTip ToolTip;
        private string int_tooltip_text;
        public PictureButton()
        {
            this.Cursor = Cursors.Hand;

            ToolTip = new ToolTip();
            ToolTipText = "";
        }

        

        public string ToolTipText
        {
            get { return int_tooltip_text.Replace("\n", "\\n"); }
            set
            {
                try
                {
                    if(value != null)
                    {
                       int_tooltip_text = value.Replace("\\n", "\n"); ; ToolTip.SetToolTip(this, int_tooltip_text); 
                    }
                } catch (Exception ex)
                {
                    // do nothing with it for now...
                }
                
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        
        
        public bool AutoResize
        {
            get
            {
                return this.autosize;
            }
            set
            {
                this.autosize = value;
            }
        }
        
        // Property for the background image to be drawn behind the button text.
        public override Image BackgroundImage
        {
            get
            {
                return this.backgroundImage;
            }
            set
            {
                this.backgroundImage = value;
            }
        }

        // Property for the background image to be drawn behind the button text.
        public Image Overlay1Image
        {
            get
            {
                return this.overlay1Image;
            }
            set
            {
                this.overlay1Image = value;
            }
        }

        // Property for the background image to be drawn behind the button text.
        public bool Overlay1Visible
        {
            get
            {
                return this.overlay1Visible;
            }
            set
            {
                this.overlay1Visible = value;
            }
        }

        // Property for the background image to be drawn behind the button text.
        public Image Overlay2Image
        {
            get
            {
                return this.overlay2Image;
            }
            set
            {
                this.overlay2Image = value;
            }
        }

        // Property for the background image to be drawn behind the button text.
        public bool Overlay2Visible
        {
            get
            {
                return this.overlay2Visible;
            }
            set
            {
                this.overlay2Visible = value;
            }
        }

        // Property for the background image to be drawn behind the button text.
        public Image Overlay3Image
        {
            get
            {
                return this.overlay3Image;
            }
            set
            {
                this.overlay3Image = value;
            }
        }

        // Property for the background image to be drawn behind the button text.
        public bool Overlay3Visible
        {
            get
            {
                return this.overlay3Visible;
            }
            set
            {
                this.overlay3Visible = value;
            }
        }

        // Property for the background image to be drawn behind the button text when
        // the button is pressed.
        public Image PressedImage
        {
            get
            {
                return this.pressedImage;
            }
            set
            {
                this.pressedImage = value;
            }
        }

        public Image OverImage
        {
            get
            {
                return this.overImage;
            }
            set
            {
                this.overImage = value;
            }
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            this.mouseover = true;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.mouseover = false;
            this.Invalidate();
            
            base.OnMouseLeave(e);
        }

        // When the mouse button is pressed, set the "pressed" flag to true 
        // and invalidate the form to cause a repaint.  The .NET Compact Framework 
        // sets the mouse capture automatically.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.pressed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        // When the mouse is released, reset the "pressed" flag 
        // and invalidate to redraw the button in the unpressed state.
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.pressed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        // Override the OnPaint method to draw the background image and the text.
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.pressed && this.pressedImage != null)
            {
                e.Graphics.DrawImage(this.pressedImage, 0, 0);
            }
            else
            {
                if(this.mouseover && this.overImage != null)
                {
                    e.Graphics.DrawImage(this.overImage, 0, 0);
                }
                else if (backgroundImage != null)
                {
                        e.Graphics.DrawImage(this.backgroundImage, 0, 0);
                }
            }

            if (this.autosize && this.backgroundImage != null)
            {
                this.Size = new Size(this.backgroundImage.Width, this.backgroundImage.Height);
            }

            if (this.overlay1Image != null && this.overlay1Visible)
            {
                e.Graphics.DrawImage(this.overlay1Image, 0, 0);
            }

            if (this.overlay2Image != null && this.overlay2Visible)
            {
                e.Graphics.DrawImage(this.overlay2Image, 0, 0);
            }

            if (this.overlay3Image != null && this.overlay3Visible)
            {
                e.Graphics.DrawImage(this.overlay3Image, 0, 0);
            }

            base.OnPaint(e);
        }
    }
}
