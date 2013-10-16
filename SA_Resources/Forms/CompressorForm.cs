using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class CompressorForm : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | 0x200;
                return myCp;
            }
        } 

        private bool dragging_threshold = false;
        private double stored_threshold = -20;

        private bool dragging_ratio = false;
        private double stored_ratio = 100;

        private double max_threshold = 10;
        private double min_threshold = -60;

        private bool is_limiter = false;

        private Series FixedLine = null;
        private Series StraightResponseLine = null;
        private Series MarkerLine = null;
        private Series KneedResponseLine = null;

        private Dial ReleaseDial, AttackDial;

        private MainForm_Template PARENT_FORM;
        private int CH_NUMBER;

        private int COMP_INDEX = 0; // 0 = compressor, 1 = limiter

        double read_gain_value;

        private int ADDR_THRESHOLD;
        private int ADDR_KNEE;
        private int ADDR_RATIO;
        private int ADDR_ATTACK;
        private int ADDR_RELEASE;
        private int ADDR_BYPASS;

        private bool comp_switcher;

        public CompressorForm(MainForm_Template _parentForm, int channel, int _settings_offset, CompressorType compType = CompressorType.Compressor)
        {
            InitializeComponent();

            ADDR_THRESHOLD = _settings_offset;
            ADDR_KNEE = _settings_offset+1;
            ADDR_RATIO = _settings_offset+2;
            ADDR_ATTACK = _settings_offset+3;
            ADDR_RELEASE = _settings_offset+4;
            ADDR_BYPASS = _settings_offset+5;

            dropAction.SelectedIndex = 0;
            dropAction.Invalidate();

            PARENT_FORM = _parentForm;
            CH_NUMBER = channel;

            if (compType == CompressorType.Compressor)
            {
                is_limiter = false;
                COMP_INDEX = 0;
            }
            else
            {
                is_limiter = true;
                COMP_INDEX = 1;
            }

            try
            {
                
                
                FixedLine = dynChart.Series[0];
                MarkerLine = dynChart.Series[1];
                StraightResponseLine = dynChart.Series[2];
                KneedResponseLine = dynChart.Series[3];



                nudCompThreshold.Value = (decimal)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Threshold;
                nudCompRatio.Value = (decimal)(Math.Min(100.0, PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Ratio));

                if (compType == CompressorType.Limiter)
                {
                    lblRatio.Visible = false;
                    lblRatioSuffix.Visible = false;
                    nudCompRatio.Visible = false;

                    MarkerLine.Points[2].MarkerSize = 0;

                    dynChart.Invalidate();

                    this.Text = "Limiter - CH" + channel.ToString();
                }
                else
                {
                    MarkerLine.Points[2].MarkerSize = 12;
                    this.Text = "Compressor - CH" + channel.ToString();
                }

                AttackDial = new Dial(TextCompAttack, DialCompAttack, new double[] {0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0},
                         DialHelpers.Format_String_Comp_Attack, Images.knob_blue_bg, Images.knob_blue_line);

                AttackDial.Value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Attack;
                AttackDial.OnChange += new DialEventHandler(this.AttackDial_OnChange);

                ReleaseDial = new Dial(TextCompRelease, DialCompRelease, new double[] {0.010, 0.038, 0.150, 0.530, 1.250, 7.0, 30.0},
                         DialHelpers.Format_String_Comp_Release, Images.knob_orange_bg, Images.knob_orange_line);
                ReleaseDial.Value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Release;
                ReleaseDial.OnChange += new DialEventHandler(this.ReleaseDial_OnChange);


                var backImage = new NamedImage("DynamicsBackground", GlobalResources.DynamicsGraph_BG_Blue);
                dynChart.Images.Add(backImage);
                dynChart.ChartAreas[0].BackImage = "DynamicsBackground";

                /* Load Image Resources */

                update_soft_knee();

                chkSoftKnee.Checked = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].SoftKnee;

                
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


                chkBypass.Checked = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Bypassed;


                if (_parentForm.LIVE_MODE && _parentForm._PIC_Conn.isOpen)
                {
                    signalTimer.Enabled = true;
                    gainMeterIn.Visible = true;
                    gainMeterOut.Visible = true;
                    lblIn.Visible = true;
                    lblOut.Visible = true;
                    panel1.Location = new Point(35, 366);
                } else
                {
                    signalTimer.Enabled = false;
                    gainMeterIn.Visible = false;
                    gainMeterOut.Visible = false;
                    lblIn.Visible = false;
                    lblOut.Visible = false;
                    panel1.Location = new Point(78, 366);
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void ReleaseDial_OnChange(object sender, DialEventArgs e)
        {

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Release = ReleaseDial.Value;
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RELEASE, DSP_Math.comp_release_to_value(ReleaseDial.Value))); 
        }

        private void AttackDial_OnChange(object sender, DialEventArgs e)
        {

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Attack = AttackDial.Value;
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_ATTACK, DSP_Math.comp_attack_to_value(AttackDial.Value))); 
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
                    var threshold = Math.Round(Math.Min(max_threshold, Math.Max(min_threshold, dynChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X))), 1);

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

                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Threshold = threshold;


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

                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Ratio = ratio;

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
            if (dragging_threshold)
            {

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_THRESHOLD, DSP_Math.double_to_MN((double)nudCompThreshold.Value, 9, 23)));
            }

            if (dragging_ratio)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RATIO, DSP_Math.comp_ratio_to_value((double)nudCompRatio.Value)));
            }


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

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Threshold = threshold;

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
            
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_THRESHOLD, DSP_Math.double_to_MN((double) nudCompThreshold.Value, 9, 23)));

        }

        private void nudCompRatio_ValueChanged(object sender, EventArgs e)
        {
            if (dragging_ratio)
            {
                return;
            } 
            
            var ratio = (double)nudCompRatio.Value;

            stored_ratio = ratio;

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Ratio = ratio;

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

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RATIO, DSP_Math.comp_ratio_to_value((double)nudCompRatio.Value)));


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoutine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRoutine();
        }

        private void SaveRoutine()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void CancelRoutine()
        {
            this.DialogResult = DialogResult.Cancel;
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

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].SoftKnee = chkSoftKnee.Checked;

            if (chkSoftKnee.Checked)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_KNEE, 0x03000000));
            }
            else
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_KNEE, 0x00000000));
            }
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            StraightResponseLine.BorderDashStyle = ChartDashStyle.Solid;

            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][COMP_INDEX].Bypassed = chkBypass.Checked;

            if (chkBypass.Checked)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, 0x00000001)); 
            }
            else
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, 0x00000000)); 
            }
            
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            CopyFormType copyType = CopyFormType.Compressor;
            if (dropAction.SelectedIndex == 0)
            {
                if (is_limiter)
                {
                    copyType = CopyFormType.Limiter;
                }

                using (CopyForm copyForm = new CopyForm(PARENT_FORM, CH_NUMBER, copyType))
                {
                    copyForm.ShowDialog(this);
                }
            }
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {

            if (!PARENT_FORM._PIC_Conn.isOpen || !PARENT_FORM.LIVE_MODE)
            {
                signalTimer.Enabled = false;
                return;
            }

            UInt32 read_value;
            double converted_value;
            double offset = 20 + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_address = 0x00000000;


            if (comp_switcher)
            {
                read_address = PARENT_FORM._comp_in_meters[COMP_INDEX][CH_NUMBER - 1];
            } else
            {
                read_address = PARENT_FORM._comp_out_meters[COMP_INDEX][CH_NUMBER - 1];
            }

            read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);
            converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);

            }
            else
            {
                read_gain_value = -100;
            }

            if (comp_switcher)
            {
                gainMeterIn.DB = read_gain_value;
            }
            else
            {
                gainMeterOut.DB = read_gain_value;
            }
            comp_switcher = !comp_switcher;

        }


    }
}
