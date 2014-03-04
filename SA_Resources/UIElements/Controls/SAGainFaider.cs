using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SA_Resources.SAControls
{
    public class FaderEventArgs : EventArgs
    {
        private readonly double _gain;
        private readonly bool _muted;

        // Constructor.
        public FaderEventArgs(double in_gain, bool in_muted)
        {
            _gain = in_gain;
            _muted = in_muted;
        }

        public double Gain
        {
            get { return _gain; }
        }

        public bool Muted
        {
            get { return _muted; }
        }
        // Properties.



    }

    public delegate void FaderEventHandler(object sender, FaderEventArgs e);

    public partial class SAGainFader : UserControl
    {

        public event FaderEventHandler OnChange;

        private double _gain;
        private bool _muted;

        private int mode = 1;

        private double _maxGain;
        private double _minGain;

        private bool mouseDown;

        private int oldY;

        private bool has_changed = false;


        private ToolTip toolTip1;

        public SAGainFader()
        {
            InitializeComponent();

            this.Mode = 0;

            this.Gain = 0;

            InitializeTooltip();
        }

        public SAGainFader(int in_mode, int cur_gain = 0)
        {
            InitializeComponent();

            this.Mode = in_mode;

            this.Gain = cur_gain;

            InitializeTooltip();

        }

        private void InitializeTooltip()
        {
            toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 50;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(sliderPB, "Muted");
        }

        protected void OnChangeEvent(FaderEventArgs e)
        {
            if (this.OnChange != null)
            {
                try
                {
                    OnChange(this, e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in SAGainFader.OnChangeEvent: " + ex.Message);
                }
            }
        } 


        public double Gain
        {
            get { return this._gain; }

            set
            {
                if (value > _maxGain)
                {
                    this._gain = _maxGain;
                }
                else if (value < _minGain)
                {
                    this._gain = _minGain;
                }
                else
                {
                    this._gain = value;
                }


                lblGain.Text = _gain.ToString("N1") + "dB";
                lblGain.Invalidate();

                sliderPB.Location = new Point(sliderPB.Location.X, (int)gain_to_yval(this.Gain));

            }
        }


        public bool Muted
        {
            get
            {
                return this._muted;
            }

            set
            {
                this._muted = value;

                chkMuted.Checked = this._muted;

                if(this._muted)
                {
                    
                    sliderPB.Image = SA_Resources.GlobalResources.ui_fader_slider_muted;
                    //sliderPB.Cursor = Cursors.No;
                    toolTip1.SetToolTip(sliderPB, "Muted");
                    lblGain.ForeColor = Color.PaleVioletRed;
                } else
                {
                    sliderPB.Image = SA_Resources.GlobalResources.ui_fader_slider;
                    //sliderPB.Cursor = Cursors.Hand;
                    toolTip1.SetToolTip(sliderPB, null);
                    lblGain.ForeColor = Color.White;
                }
            }


        }
        public int Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;

                if (this.mode == 1)
                {
                    _maxGain = 6;
                    _minGain = -12;
                    this.panel1.BackgroundImage = SA_Resources.GlobalResources.gainfader_6_to__12;
                }
                else
                {
                    _maxGain = 12;
                    _minGain = -100;
                    this.panel1.BackgroundImage = SA_Resources.GlobalResources.gainfader_12_to__100;
                }

                this.Gain = 0;

            }
        }

        PictureBox thisPB;



        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            /*if (chkMuted.Checked)
            {
                return;
            }*/
            mouseDown = true;
            has_changed = false;

            oldY = e.Y;
        }

        private double yVal_to_gain_scale(double y_val, double y_upper, double y_lower, double gain_upper, double gain_lower)
        {
            double percent = (y_val - y_upper)/(y_lower-y_upper);

            double return_gain = gain_upper - (gain_upper - gain_lower)*percent;

            return return_gain;
        }

        private double yVal_to_gain(double yVal)
        {
            if (this.mode == 1)
            {
                return yVal_to_gain_mode1(yVal);
            }
            else
            {
                return yVal_to_gain_mode0(yVal);
            }
        }
        private double yVal_to_gain_mode0(double yVal)
        {

            if (yVal < 32)
            {
                return yVal_to_gain_scale(yVal, 5.0, 32.0, 12.0, 6.0);
            }

            if (yVal < 62)
            {
                return yVal_to_gain_scale(yVal, 32.0, 62.0, 6.0, 0.0);
            }

            if (yVal < 89)
            {
                return yVal_to_gain_scale(yVal, 62.0, 89.0, 0.0, -5.0);
            }

            if (yVal < 118)
            {
                return yVal_to_gain_scale(yVal, 89.0, 118.0, -5.0, -10.0);
            }

            if (yVal < 146)
            {
                return yVal_to_gain_scale(yVal, 118.0, 146.0, -10.0, -15.0);
            }

            if (yVal < 170)
            {
                return yVal_to_gain_scale(yVal, 146.0, 170.0, -15.0, -20.0);
            }

            if (yVal < 191)
            {
                return yVal_to_gain_scale(yVal, 170.0, 191.0, -20.0, -30.0);
            }

            if (yVal < 203)
            {
                return yVal_to_gain_scale(yVal, 191.0, 203.0, -30.0, -40.0);
            }

            if (yVal < 216)
            {
                return yVal_to_gain_scale(yVal, 203.0, 216.0, -40.0, -60.0);
            }

            if (yVal < 226)
            {
                return yVal_to_gain_scale(yVal, 216.0, 226.0, -60.0, -90.0);
            }

            if (yVal < 235)
            {
                return yVal_to_gain_scale(yVal, 226.0, 235.0, -90.0, -100.0);
            }

            return _minGain;
        }

        private double yVal_to_gain_mode1(double yVal)
        {

            if (yVal < 29)
            {
                return yVal_to_gain_scale(yVal, 5.0, 29.0, 6.0, 4.0);
            }

            if (yVal < 53)
            {
                return yVal_to_gain_scale(yVal, 29.0, 53.0, 4.0, 2.0);
            }

            if (yVal < 78)
            {
                return yVal_to_gain_scale(yVal, 53.0, 78.0, 2.0, 0.0);
            }

            if (yVal < 104)
            {
                return yVal_to_gain_scale(yVal, 78.0, 104.0, 0.0, -2.0);
            }

            if (yVal < 128)
            {
                return yVal_to_gain_scale(yVal, 104.0, 128.0, -2.0, -4.0);
            }

            if (yVal < 153)
            {
                return yVal_to_gain_scale(yVal, 128.0, 153.0, -4.0, -6.0);
            }

            if (yVal < 177)
            {
                return yVal_to_gain_scale(yVal, 153.0, 177.0, -6.0, -8.0);
            }

            if (yVal < 202)
            {
                return yVal_to_gain_scale(yVal, 177.0, 202.0, -8.0, -10.0);
            }

            if (yVal < 225)
            {
                return yVal_to_gain_scale(yVal, 202.0, 225.0, -10.0, -12.0);
            }


            return _minGain;
        }

        private double gain_to_yval_scale(double gainVal, double gain_upper, double gain_lower, double y_upper, double y_lower)
        {
            double percent = (gain_upper - gainVal) / (gain_upper - gain_lower);

            double return_gain = y_upper + (y_lower - y_upper) * percent;

            return return_gain;
        }


        public double gain_to_yval(double gainVal)
        {
            if(this.mode == 1)
            {
                return gain_to_yval_mode1(gainVal);
            } else
            {
                return gain_to_yval_mode0(gainVal);
            }
        }

        private double gain_to_yval_mode0(double gainVal)
        {
            if(gainVal > 6)
            {
                return gain_to_yval_scale(gainVal, 12.0, 6.0, 5.0, 32.0);
            }

            if (gainVal > 0)
            {
                return gain_to_yval_scale(gainVal, 6.0, 0.0, 32.0, 62.0);
            }

            if (gainVal > -5)
            {
                return gain_to_yval_scale(gainVal, 0.0, -5.0, 62.0, 89.0);
            }

            if (gainVal > -10)
            {
                return gain_to_yval_scale(gainVal, -5.0, -10.0, 89.0, 118.0);
            }

            if (gainVal > -15)
            {
                return gain_to_yval_scale(gainVal, -10.0, -15.0, 118.0, 146.0);
            }

            if (gainVal > -20)
            {
                return gain_to_yval_scale(gainVal, -15.0, -20.0, 146.0, 170.0);
            }

            if (gainVal > -30)
            {
                return gain_to_yval_scale(gainVal, -20.0, -30.0, 170.0, 191.0);
            }

            if (gainVal > -40)
            {
                return gain_to_yval_scale(gainVal, -30.0, -40.0, 191.0, 203.0);
            }

            if (gainVal > -60)
            {
                return gain_to_yval_scale(gainVal, -40.0, -60.0, 203.0, 216.0);
            }

            if (gainVal > -90)
            {
                return gain_to_yval_scale(gainVal, -60.0, -90.0, 216.0, 226.0);
            }

            if (gainVal > -100)
            {
                return gain_to_yval_scale(gainVal, -90.0, -100.0, 226.0, 235.0);
            }

            return 235.0;

        }

        private double gain_to_yval_mode1(double gainVal)
        {
            if (gainVal > 4)
            {
                return gain_to_yval_scale(gainVal, 6.0, 4.0, 5.0, 29.0);
            }

            if (gainVal > 2)
            {
                return gain_to_yval_scale(gainVal, 4.0, 2.0, 29.0, 53.0);
            }

            if (gainVal > 0)
            {
                return gain_to_yval_scale(gainVal, 2.0, 0.0, 53.0, 78.0);
            }

            if (gainVal > -2)
            {
                return gain_to_yval_scale(gainVal, 0.0, -2.0, 78.0, 104.0);
            }

            if (gainVal > -4)
            {
                return gain_to_yval_scale(gainVal, -2.0, -4.0, 104.0, 128.0);
            }

            if (gainVal > -6)
            {
                return gain_to_yval_scale(gainVal, -4.0, -6.0, 128.0, 153.0);
            }

            if (gainVal > -8)
            {
                return gain_to_yval_scale(gainVal, -6.0, -8.0, 153.0, 177.0);
            }

            if (gainVal > -10)
            {
                return gain_to_yval_scale(gainVal, -8.0, -10.0, 177.0, 202.0);
            }

            if (gainVal > -12)
            {
                return gain_to_yval_scale(gainVal, -10.0, -12.0, 202.0, 225.0);
            }


            return 225.0;

        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                has_changed = true;

                thisPB = (PictureBox)sender;

                int yVal = Math.Min(Math.Max(thisPB.Location.Y - (oldY - e.Y), 5), 235);

                this.Gain = yVal_to_gain((double) yVal);

                lblGain.Invalidate();
            }
        }


        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            // Call handler here

            if (has_changed)
            {
                FaderEventArgs args = new FaderEventArgs(Gain, Muted);
                OnChangeEvent(args);
            }

            has_changed = false;
            mouseDown = false;

        }

        private void chkMuted_CheckedChanged(object sender, EventArgs e)
        {
            this.Muted = chkMuted.Checked;

            FaderEventArgs args = new FaderEventArgs(Gain, Muted);
            OnChangeEvent(args);

        }
    }
}