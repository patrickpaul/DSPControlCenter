using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.Configurations;
using SA_Resources.DSP.Filters;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    public partial class FilterDesignerForm : Form
    {

        /* Multi-threading helpers */
        private static Thread UIThread;

        public object _threadlock;
        public bool uithread_abort = false;

        public FilterDesignerForm()
        {
            InitializeComponent();

            _threadlock = new Object();

            DSP_Primitive_BiquadFilter TestPrimitive1 = new DSP_Primitive_BiquadFilter("Test filter", 0, 0, 100, FilterType.Peak, false);
            TestPrimitive1.Filter = new PeakFilter(200, 5, 1);

            DSP_Primitive_BiquadFilter TestPrimitive2 = new DSP_Primitive_BiquadFilter("Test filter", 0, 0, 100, FilterType.Peak, false);
            TestPrimitive2.Filter = new PeakFilter(500, 10, 1);

            DSP_Primitive_BiquadFilter TestPrimitive3 = new DSP_Primitive_BiquadFilter("Test filter", 0, 0, 100, FilterType.Peak, false);
            TestPrimitive3.Filter = new PeakFilter(1000, 10, 1);

            DSP_Primitive_BiquadFilter TestPrimitive4 = new DSP_Primitive_BiquadFilter("Test filter", 0, 0, 100, FilterType.Peak, false);
            TestPrimitive4.Filter = new PeakFilter(5000, 10, 1);

            DSP_Primitive_BiquadFilter TestPrimitive5 = new DSP_Primitive_BiquadFilter("Test filter", 0, 0, 100, FilterType.Peak, false);
            TestPrimitive5.Filter = new PeakFilter(10000, 10, 1);

            DSP_Primitive_BiquadFilter TestPrimitive6 = new DSP_Primitive_BiquadFilter("Test filter", 0, 0, 100, FilterType.Peak, false);
            TestPrimitive6.Filter = new PeakFilter(15000, 10, 1);

            filterDesigner.RegisterFilterPrimitive(TestPrimitive1);
            filterDesigner.RegisterFilterPrimitive(TestPrimitive2);
            filterDesigner.RegisterFilterPrimitive(TestPrimitive3);
            filterDesigner.RegisterFilterPrimitive(TestPrimitive4);
            filterDesigner.RegisterFilterPrimitive(TestPrimitive5);
            //filterDesigner.RegisterFilterPrimitive(TestPrimitive6);

            filterDesignBlock0.RegisterPrimitive(0,TestPrimitive1);
            filterDesignBlock1.RegisterPrimitive(1,TestPrimitive2);
            filterDesignBlock2.RegisterPrimitive(2,TestPrimitive3);
            filterDesignBlock3.RegisterPrimitive(3,TestPrimitive4);
            filterDesignBlock4.RegisterPrimitive(4,TestPrimitive5);
            //filterDesignBlock5.RegisterPrimitive(5,TestPrimitive6);

            filterDesigner.SetActiveFilter(0);
        }

        private void filterDesignBlock_OnChange(object sender, FilterEventArgs e)
        {
            filterDesigner.SetActiveFilter(e.Index);
            filterDesigner.RefreshAllFilters();

            Console.WriteLine("filterDesignBlock_OnChange");
        }


        public void UpdateUIToVals(object param)
        {
            try
            {
                MethodInvoker action1, action2, action3;

                int FocusedFilterID = (int) param;

                SAFilterDesignBlock ActiveBlock = (SAFilterDesignBlock)Controls.Find("filterDesignBlock" + FocusedFilterID, true).FirstOrDefault();

                while (true)
                {
                    try
                    {
                        if (ActiveBlock != null)
                        {
                            action1 = delegate
                                          {
                                              ActiveBlock.CenterFrequency = filterDesigner.PrimitiveCache[FocusedFilterID].Filter.CenterFrequency;
                                              ActiveBlock.QValue = filterDesigner.PrimitiveCache[FocusedFilterID].Filter.QValue;
                                              ActiveBlock.Gain = filterDesigner.PrimitiveCache[FocusedFilterID].Filter.Gain;
                                          };

                            ActiveBlock.BeginInvoke(action1);
                        }

                        // Do not delete this Thread.Sleep... you will be banging your head on your desk for an hour wondering why the UI locks up :)
                        Thread.Sleep(5);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception in UpdateUIToVals Level 2: " + ex.Message);
                    }
                    
                    lock (_threadlock)
                    {
                        if (uithread_abort == true)
                        {
                            uithread_abort = false;
                            Console.WriteLine("Broke UI thread");
                            break;
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateUIToVals Level 1: " + ex.Message);

            }

        }

        private void filterDesigner_OnDragBegin(object sender, EventArgs e)
        {
            UIThread = new Thread(UpdateUIToVals);
            UIThread.Name = "UIThread";
            UIThread.IsBackground = true;
            UIThread.Start(filterDesigner.FocusedFilterID);
        }

        private void filterDesigner_OnDragEnd(object sender, EventArgs e)
        {
            try
            {
                lock (_threadlock)
                {
                    uithread_abort = true;
                }


                Console.WriteLine("filterDesigner_OnDragEnd");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in filterDesigner_OnDragEnd: " + ex.Message);
            }
        }
    }
}
