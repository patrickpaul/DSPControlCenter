using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SA_Resources
{
    public partial class CompressorForm : Form
    {

        private bool dragging_threshold = false;
        private double stored_threshold = -20;

        private bool dragging_ratio = false;
        private double stored_ratio = 100;

        private bool is_limiter = false;
        private bool bypassed = false;

        private Series FixedLine = null;
        private Series StraightResponseLine = null;
        private Series MarkerLine = null;
        private Series KneedResponseLine = null;

        private CompressorConfig config;

        private Dial ReleaseDial, AttackDial;

        public CompressorForm(int channel, CompressorConfig in_config)
        {

            config = in_config;

            try
            {
                InitializeComponent(); 
                
                FixedLine = dynChart.Series[0];
                MarkerLine = dynChart.Series[1];
                StraightResponseLine = dynChart.Series[2];
                KneedResponseLine = dynChart.Series[3];

                nudCompThreshold.Value = (decimal)config.Threshold;
                nudCompRatio.Value = (decimal)(Math.Min(100.0,config.Ratio));

                if (config.Type == CompressorType.Limiter)
                {
                    lblRatio.Visible = false;
                    lblRatioSuffix.Visible = false;
                    nudCompRatio.Visible = false;

                    MarkerLine.Points[2].MarkerSize = 0;

                    dynChart.Invalidate();
                    is_limiter = true;

                    this.Text = "Limiter - CH" + channel.ToString();
                }
                else
                {
                    MarkerLine.Points[2].MarkerSize = 12;
                    this.Text = "Compressor - CH" + channel.ToString();
                }

                AttackDial = new Dial(TextCompAttack, DialCompAttack, new double[] {0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0},
                         DialHelpers.Format_String_Comp_Attack, Images.knob_blue_bg, Images.knob_blue_line);

                AttackDial.Value = config.Attack;
                AttackDial.OnChange += new DialEventHandler(this.AttackDial_OnChange);

                ReleaseDial = new Dial(TextCompRelease, DialCompRelease, new double[] {0.010, 0.038, 0.150, 0.530, 1.250, 7.0, 30.0},
                         DialHelpers.Format_String_Comp_Release, Images.knob_orange_bg, Images.knob_orange_line);
                ReleaseDial.Value = config.Release;
                ReleaseDial.OnChange += new DialEventHandler(this.ReleaseDial_OnChange);


                var backImage = new NamedImage("DynamicsBackground", GlobalResources.DynamicsGraph_BG_Blue);
                dynChart.Images.Add(backImage);
                dynChart.ChartAreas[0].BackImage = "DynamicsBackground";

                /* Load Image Resources */

                update_soft_knee();

                chkSoftKnee.Checked = config.SoftKnee;

                
                if (chkSoftKnee.Checked)
                {
                    KneedResponseLine.Enabled = true;
                    StraightResponseLine.Enabled = false;
                }
                else
                {
                    KneedResponseLine.Enabled = false;
                    StraightResponseLine.Enabled = true;
                }


                chkBypass.Checked = config.Bypassed;


            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void ReleaseDial_OnChange(object sender, DialEventArgs e)
        {

            config.Release = ReleaseDial.Value;
        }

        private void AttackDial_OnChange(object sender, DialEventArgs e)
        {

            config.Attack = AttackDial.Value;
        }

        private void dynChart_MouseMove(object sender, MouseEventArgs e)
        {
            // By removing the following, we must do some error handling...

            //if (dynChart.HitTest(e.X, e.Y).ChartArea != dynChart.ChartAreas[0])
            //{
            //    return;
            //}

            if (e.Y <= 0 || e.Y >= dynChart.Height)
            {
                return;
            }

            try
            {
                if (dragging_threshold)
                {
                    // Fixed ratio

                    // New threshold
                    var threshold = Math.Round(Math.Min(0, Math.Max(-60, dynChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X))),1);

                    stored_threshold = threshold;
                    /* Update the threshold point.. this is always (threshold,threshold) */
                    MarkerLine.Points[1].SetValueXY(threshold, threshold);
                    StraightResponseLine.Points[1].SetValueXY(threshold, threshold);


                    // New solution here to commented out portion below..
                    // We store a more accurate ratio instead of comparing to the inaccurate YValues[0]

                    if(stored_ratio == 100)
                    {
                        MarkerLine.Points[2].SetValueXY(10, threshold);
                        StraightResponseLine.Points[2].SetValueXY(10, threshold);
                    } else
                    {
                        MarkerLine.Points[2].SetValueXY(10, ((10 - threshold) / stored_ratio) + threshold);
                        StraightResponseLine.Points[2].SetValueXY(10, ((10 - threshold) / stored_ratio) + threshold);
                    }

                    config.Threshold = threshold;


                    update_soft_knee();

                    nudCompThreshold.Value = (decimal) threshold;

                    dynChart.Invalidate();

                    return;
                }

                if (dragging_ratio)
                {
                    var yVal = Math.Min(10,
                                        Math.Max(MarkerLine.Points[1].XValue,
                                                 dynChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y)));

                    MarkerLine.Points[2].SetValueXY(10, yVal);
                    StraightResponseLine.Points[2].SetValueXY(10, yVal);

                    var threshold = MarkerLine.Points[1].XValue;
                    var ratio = 1.0;

                    // No one likes dividing by zero ;)
                    if (yVal == threshold)
                    {
                        ratio = 100;
                    }
                    else
                    {
                        ratio = Math.Min(100.0, ((10 - threshold)/(yVal - threshold)));
                    }

                    config.Ratio = ratio;

                    stored_ratio = ratio;

                    update_soft_knee();


                    nudCompRatio.Value = (decimal)stored_ratio;

                    dynChart.Invalidate();

                    return;


                }

                if (dynChart.HitTest(e.X, e.Y).Series == dynChart.Series[1])
                {
                    dynChart.Cursor = Cursors.Hand;
                }
                else
                {
                    dynChart.Cursor = Cursors.Default;
                }
            } catch (Exception)
            {
                // User dragged outside of chart area...
                return;
            }
        }

        private void update_soft_knee()
        {
            // Point to left of threshold
            Line leftPoint = new Line(-60, -60, stored_threshold + 2, (stored_threshold) + 2 / stored_ratio);
            KneedResponseLine.Points[1].XValue = stored_threshold - 4;
            KneedResponseLine.Points[1].YValues[0] = leftPoint.ValueAt(stored_threshold - 4);


            // Center point under threshold
            Line centerPoint = new Line(-60, -60, stored_threshold + 3, (stored_threshold) + 3 / stored_ratio);
            KneedResponseLine.Points[2].XValue = stored_threshold;
            KneedResponseLine.Points[2].YValues[0] = centerPoint.ValueAt(stored_threshold);

            // Point to right of threshold
            Line rightPoint = new Line(-60, -60, stored_threshold + 5, (stored_threshold) + 5 / stored_ratio);
            KneedResponseLine.Points[3].XValue = stored_threshold + 4;
            KneedResponseLine.Points[3].YValues[0] = rightPoint.ValueAt(stored_threshold + 4);

            // Point at end of knee
            KneedResponseLine.Points[4].SetValueXY(stored_threshold + 10, ((stored_threshold + 10 - stored_threshold) / stored_ratio) + stored_threshold);

            // Point at end of chart
            KneedResponseLine.Points[5].SetValueXY(10, ((10 - stored_threshold) / stored_ratio) + stored_threshold);


        }

        private void dynChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (dynChart.HitTest(e.X, e.Y).Series == dynChart.Series[1])
            {
                dynChart.Cursor = Cursors.Hand;

                int point_index = dynChart.HitTest(e.X, e.Y).PointIndex;

                dragging_threshold = false;
                dragging_ratio = false;

                if(point_index == 1)
                {
                    dragging_threshold = true;
                }
                else
                {
                    if (!is_limiter)
                    {
                        dragging_ratio = true;
                    }
                }

            }
        }

        private void dynChart_MouseUp(object sender, MouseEventArgs e)
        {
            dragging_threshold = false;
            dragging_ratio = false;
            dynChart.Cursor = Cursors.Default;
        }

        private void nudCompThreshold_ValueChanged(object sender, EventArgs e)
        {
            if(dragging_threshold)
            {
                return;
            }

            var threshold = (double)nudCompThreshold.Value;

            stored_threshold = threshold;
            MarkerLine.Points[1].SetValueXY(threshold, threshold);
            StraightResponseLine.Points[1].SetValueXY(threshold, threshold);

            if (stored_ratio == 100)
            {
                MarkerLine.Points[2].SetValueXY(10, threshold);
                StraightResponseLine.Points[2].SetValueXY(10, threshold);
            }
            else
            {
                MarkerLine.Points[2].SetValueXY(10, ((10 - threshold) / stored_ratio) + threshold);
                StraightResponseLine.Points[2].SetValueXY(10, ((10 - threshold) / stored_ratio) + threshold);
            }


            update_soft_knee();

            dynChart.Invalidate();

            config.Threshold = threshold;
        }

        private void nudCompRatio_ValueChanged(object sender, EventArgs e)
        {
            if (dragging_ratio)
            {
                return;
            } 
            
            var ratio = (double)nudCompRatio.Value;

            stored_ratio = ratio;

            config.Ratio = ratio;

            update_soft_knee();

            var threshold = (double)nudCompThreshold.Value; 

            MarkerLine.Points[2].SetValueXY(10, ((10 - threshold) / ratio) + threshold);
            StraightResponseLine.Points[2].SetValueXY(10, ((10 - threshold) / ratio) + threshold);

            dynChart.Invalidate();

            if(ratio <= 10)
            {
                nudCompRatio.Increment = (decimal)0.1;
            }
            else
            {
                nudCompRatio.Increment = (decimal)5.0;
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSoftKnee_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSoftKnee.Checked)
            {
                KneedResponseLine.Enabled = true;
                StraightResponseLine.Enabled = false;
            } else
            {
                KneedResponseLine.Enabled = false;
                StraightResponseLine.Enabled = true;
            }

            config.SoftKnee = chkSoftKnee.Checked;
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            StraightResponseLine.BorderDashStyle = ChartDashStyle.Solid;

            config.Bypassed = chkBypass.Checked;
        }

    }
}
