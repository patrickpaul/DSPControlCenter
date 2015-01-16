using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.DSP;
using SA_Resources.USB;


namespace SA_Resources.SAControls
{
    public partial class SignalMeter : PictureBox
    {
        private double db_value;
        public UInt32 Address;

        private object _threadlock;
        private bool meterthread_abort = false;
        private Thread MeterThread;
        public DeviceBridge DeviceConn = null;

        public SignalMeter()
        {
            _threadlock = new object();
            this.BackgroundImage = SA_GFXLib.SA_GFXLib_Resources.bg_meter_large;
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

        public void Start()
        {

            if (DeviceConn == null || Address == 0x00)
            {
                return;
            }

            MeterThread = new Thread(MeterThread_Worker);
            MeterThread.Name = "MeterThread";
            MeterThread.IsBackground = true;
            MeterThread.Start();


        }

        public void Stop()
        {
            try
            {
                if (MeterThread != null)
                {
                    meterthread_abort = true;
                    //MeterThread.Abort();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in SignalMeter.Stop]: " + ex.Message);
            }
        }

        public void MeterThread_Worker(object param)
        {
            try
            {
                UInt32 read_value;
                double converted_value, read_gain_value;


                double offset = (20 - 20 + 3.8) + 10 * Math.Log10(2) + 20 * Math.Log10(16);

                while (true && !meterthread_abort)
                {
                    try
                    {

                        read_value = DeviceConn.Read_Live_DSP_Value(this.Address);

                        if (read_value != 0xFFFFFFFF)
                        {
                            converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

                            if (converted_value > (0.000001*0.000001))
                            {
                                read_gain_value = offset + 10*Math.Log10(converted_value);
                            }
                            else
                            {
                                read_gain_value = -100;
                            }

                            this.DB = read_gain_value;
                        }
                        // Do not delete this Thread.Sleep... you will be banging your head on your desk for an hour wondering why the UI locks up :)
                        Thread.Sleep(5);




                        lock (_threadlock)
                        {
                            if (meterthread_abort == true)
                            {
                                meterthread_abort = false;
                                //Debug.WriteLine("Broke Meter thread");
                                MeterThread.Abort();
                                break;

                            }


                        }

                    }
                    catch (ThreadAbortException taex)
                    {
                        Debug.WriteLine("[ThreadAbortException in MeterThread_Worker Level 2]: " + taex.Message);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception in MeterThread_Worker Level 2: " + ex.Message);
                    }
                }

            }
            catch (ThreadAbortException taex)
            {
                Debug.WriteLine("[ThreadAbortException in MeterThread_Worker Level 1]: " + taex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateUIToVals Level 1: " + ex.Message);

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
