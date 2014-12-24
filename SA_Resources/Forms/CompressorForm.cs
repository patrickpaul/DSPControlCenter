using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_Resources.DSP;
using SA_Resources.SAControls;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAForms
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

        private bool form_loaded = false;

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

        private bool approx_threshold_override = false;

        private DSP_Primitive_Compressor Active_Primitive;
        private UInt32 in_gain_address;

        private object _threadlock;
        private bool livesignalthread_abort = false;
        private Thread LiveSignalThread;

        public CompressorForm(MainForm_Template _parentForm, DSP_Primitive_Compressor input_primitive)
        {

            _threadlock = new object();


            Active_Primitive = input_primitive;

            InitializeComponent();

            dropAction.SelectedIndex = 0;
            dropAction.Invalidate();

            PARENT_FORM = _parentForm;

            if (Active_Primitive.Type == DSP_Primitive_Types.Compressor)
            {
                is_limiter = false;
            }
            else
            {
                is_limiter = true;
            }

            try
            {
                int compLimOffset = is_limiter ? 1 : 0;

                FixedLine = dynChart.Series[0];
                MarkerLine = dynChart.Series[1];
                StraightResponseLine = dynChart.Series[2];
                KneedResponseLine = dynChart.Series[3];



                nudCompThreshold.Value = (decimal)Active_Primitive.Threshold;
                nudCompRatio.Value = (decimal)(Math.Min(100.0, Active_Primitive.Ratio));

                if (is_limiter)
                {
                    lblRatio.Visible = false;
                    lblRatioSuffix.Visible = false;
                    nudCompRatio.Visible = false;

                    MarkerLine.Points[2].MarkerSize = 0;

                    dynChart.Invalidate();

                    this.Text = "Limiter - CH" + (Active_Primitive.Channel+1).ToString();
                }
                else
                {
                    MarkerLine.Points[2].MarkerSize = 12;
                    this.Text = "Compressor - CH" + (Active_Primitive.Channel + 1).ToString();
                }

                AttackDial = new Dial(TextCompAttack, DialCompAttack, new double[] {0.001, 0.003, 0.01, 0.03, 0.08, 0.3, 1.0},
                         DialHelpers.Format_String_Comp_Attack, Images.knob_blue_bg, Images.knob_blue_line);

                AttackDial.Value = Active_Primitive.Attack;
                AttackDial.OnChange += new DialEventHandler(this.AttackDial_OnChange);

                ReleaseDial = new Dial(TextCompRelease, DialCompRelease, new double[] {0.010, 0.038, 0.150, 0.530, 1.250, 7.0, 30.0},
                         DialHelpers.Format_String_Comp_Release, Images.knob_orange_bg, Images.knob_orange_line);
                ReleaseDial.Value = Active_Primitive.Release;
                ReleaseDial.OnChange += new DialEventHandler(this.ReleaseDial_OnChange);


                var backImage = new NamedImage("DynamicsBackground", GlobalResources.DynamicsGraph_BG_Blue);
                dynChart.Images.Add(backImage);
                dynChart.ChartAreas[0].BackImage = "DynamicsBackground";

                /* Load Image Resources */

                update_soft_knee();

                if (Active_Primitive.SoftKnee)
                {
                    chkSoftKnee.Checked = true;
                    KneedResponseLine.Enabled = true;
                    StraightResponseLine.Enabled = false;
                }
                else
                {
                    chkSoftKnee.Checked = false;
                    KneedResponseLine.Enabled = false;
                    StraightResponseLine.Enabled = true;
                }


                chkBypass.Checked = Active_Primitive.Bypassed;


                if (_parentForm.LIVE_MODE && _parentForm._PIC_Conn.isOpen)
                {
                    gainMeterIn.Visible = true;
                    gainMeterOut.Visible = true;

                    gainMeterIn.PIC_CONN = PARENT_FORM._PIC_Conn;
                    gainMeterIn.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Compressor, Active_Primitive.Channel, compLimOffset, 0).Address;
                    gainMeterIn.Start();

                    gainMeterOut.PIC_CONN = PARENT_FORM._PIC_Conn;
                    gainMeterOut.Address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Compressor, Active_Primitive.Channel, compLimOffset, 1).Address;
                    gainMeterOut.Start();

                    lblIn.Visible = true;
                    lblOut.Visible = true;
                    panel1.Location = new Point(35, 366);

                    in_gain_address = gainMeterIn.Address;

                } else
                {
                    gainMeterIn.Visible = false;
                    gainMeterOut.Visible = false;
                    lblIn.Visible = false;
                    lblOut.Visible = false;
                    panel1.Location = new Point(78, 366);
                }

                if (_parentForm.LIVE_MODE)
                {
                    LiveSignalThread = new Thread(LiveSignal_Worker);
                    LiveSignalThread.Name = "LiveSignalThread";
                    LiveSignalThread.IsBackground = true;
                    LiveSignalThread.Start();
                }

            } catch (Exception ex)
            {
                Console.WriteLine("[Exception in CompressorForm.Constructor]:" +  ex.Message);
            }

            form_loaded = true;

        }

        private void ReleaseDial_OnChange(object sender, DialEventArgs e)
        {

            Active_Primitive.Release = ReleaseDial.Value;
            if (PARENT_FORM.LIVE_MODE)
            {
                Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.RELEASE_OFFSET);
            }
        }

        private void AttackDial_OnChange(object sender, DialEventArgs e)
        {

            Active_Primitive.Attack = AttackDial.Value;

            if (PARENT_FORM.LIVE_MODE)
            {
                Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.ATTACK_OFFSET);
            }
        }

        private void dynChart_MouseMove(object sender, MouseEventArgs e)
        {
            // By removing the following, we must do some error handling...


            
            //if (dynChart.HitTest(e.X, e.Y).ChartArea != dynChart.ChartAreas[0])
            //{
            //    return;
            //}
            if(e.Y > 230 && e.Y < 245 && e.X > 65 && e.X < 80 && stored_threshold < 58)
            {
                dynChart.Cursor = Cursors.Hand;
                dynChart.Invalidate();
                approx_threshold_override = true;
                return;
                //Console.WriteLine("X = " + e.X + ", Y = " + e.Y);
            } else if (e.Y < 25 && e.X > 283 && e.X < 300 && stored_threshold > 9)
            {
                dynChart.Cursor = Cursors.Hand;
                dynChart.Invalidate();
                approx_threshold_override = true;
                return;
                //Console.WriteLine("X = " + e.X + ", Y = " + e.Y);
            }
            else
            {
                approx_threshold_override = false;
            }

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
                    var threshold = Math.Round(Math.Min(max_threshold, Math.Max(min_threshold, dynChart.ChartAreas[0].AxisX.PixelPositionToValue(Math.Min(300,e.X)))), 1);

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

                    Active_Primitive.Threshold = threshold;


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

                    Active_Primitive.Ratio = ratio;

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

                if (point_index == 3)
                {
                    return;
                }
                // If we're at the upper-right corner, the hit test will give us the ratio point (point_index = 2)
                // Force threshold point (point_index = 1) if we detect that the points are likely stacked 
                if(approx_threshold_override && stored_threshold > 9)
                {
                    point_index = 1;
                }


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

            } else if(approx_threshold_override)
            {
                dragging_threshold = true;
            }
        }

        private void dynChart_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging_threshold)
            {
                Active_Primitive.Threshold = (double) nudCompThreshold.Value;
                if (PARENT_FORM.LIVE_MODE)
                {
                    Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.THRESHOLD_OFFSET);
                }
            }

            if (dragging_ratio)
            {
                Active_Primitive.Ratio = (double)nudCompRatio.Value;
                if (PARENT_FORM.LIVE_MODE)
                {
                    Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.RATIO_OFFSET);
                }
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

            Active_Primitive.Threshold = threshold;

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
            
            Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.THRESHOLD_OFFSET);

        }

        private void nudCompRatio_ValueChanged(object sender, EventArgs e)
        {
            if (dragging_ratio)
            {
                return;
            } 
            
            var ratio = (double)nudCompRatio.Value;

            stored_ratio = ratio;

            Active_Primitive.Ratio = ratio;

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

            Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.RATIO_OFFSET);
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

            Active_Primitive.SoftKnee = chkSoftKnee.Checked;

            Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.SOFTKNEE_OFFSET);
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {

                KneedResponseLine.BorderDashStyle = chkBypass.Checked ? ChartDashStyle.Dash : ChartDashStyle.Solid;
                StraightResponseLine.BorderDashStyle = chkBypass.Checked ? ChartDashStyle.Dash : ChartDashStyle.Solid;
            
             dynChart.Invalidate();

            if (!form_loaded)
            {
                return;
            }
            Active_Primitive.Bypassed = chkBypass.Checked;

            Active_Primitive.QueueChangeByOffset(PARENT_FORM, DSP_Primitive_Compressor.BYPASSED_OFFSET);
            
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            /*
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
             * */
        }

       

        private void CompressorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gainMeterIn.Stop();
            gainMeterOut.Stop();
            if(LiveSignalThread != null) {
                LiveSignalThread.Abort();
                }
        }

 

        public void LiveSignal_Worker(object param)
        {
            try
            {
                MethodInvoker action1;
                UInt32 read_value;
                double converted_value, read_gain_value;


                double offset = (20 - 20 + 3.8) + 10 * Math.Log10(2) + 20 * Math.Log10(16);

                while (true)
                {
                    try
                    {

                        read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(in_gain_address);

                        if (read_value != 0xFFFFFFFF)
                        {

                            converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

                            if (converted_value > (0.000001 * 0.000001))
                            {
                                read_gain_value = offset + 10 * Math.Log10(converted_value);
                            }
                            else
                            {
                                read_gain_value = -100;
                            }

                            if (read_value != 0xFFFFFFFF)
                            {

                                converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

                                if (converted_value > (0.000001 * 0.000001))
                                {
                                    read_gain_value = offset + 10 * Math.Log10(converted_value);
                                }
                                else
                                {
                                    read_gain_value = -100;
                                }

                                if (MarkerLine != null)
                                {
                                    action1 = delegate
                                    {
                                        if (Active_Primitive.Bypassed || (read_gain_value < stored_threshold))
                                        {
                                            MarkerLine.Points[3].SetValueXY(read_gain_value, read_gain_value);
                                        }
                                        else
                                        {
                                            MarkerLine.Points[3].SetValueXY(read_gain_value, ((read_gain_value - stored_threshold) / stored_ratio) + stored_threshold);
                                        }


                                        dynChart.Invalidate();
                                    };

                                    dynChart.BeginInvoke(action1);
                                }

                                
                            }
                        }
                        // Do not delete this Thread.Sleep... you will be banging your head on your desk for an hour wondering why the UI locks up :)
                        Thread.Sleep(5);


                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Exception in LiveSignal_Thread Level 2: " + ex.Message);
                    }

                    lock (_threadlock)
                    {
                        if (livesignalthread_abort == true)
                        {
                            livesignalthread_abort = false;
                            //Console.WriteLine("Broke LiveSignal thread");
                            LiveSignalThread.Abort();
                            break;

                        }


                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in LiveSignal_Thread Level 1: " + ex.Message);

            }

        }

    }
}
