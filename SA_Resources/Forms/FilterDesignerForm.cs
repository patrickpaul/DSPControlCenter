using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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

        public MainForm_Template PARENT_FORM;

        private bool form_loaded = false;

        // public DelayForm(MainForm_Template _parent, DSP_Primitive_Delay input_primitive)
        public FilterDesignerForm(MainForm_Template _parent, int num_filters, int ch_num, int starting_filter_index)
        {
            InitializeComponent();

            _threadlock = new Object();

            PARENT_FORM = _parent;

            DSP_Primitive_BiquadFilter SingleFilterPrimitive;
            SAFilterDesignBlock DesignBlock;

            bool firstFilterFound = false;

            for (int i = 0; i < num_filters; i++)
            {
                SingleFilterPrimitive = (DSP_Primitive_BiquadFilter)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.BiquadFilter,ch_num,starting_filter_index++);
                filterDesigner.RegisterFilterPrimitive(SingleFilterPrimitive);

                if (SingleFilterPrimitive.Filter != null)
                {
                    if (SingleFilterPrimitive.FType != FilterType.None)
                    {
                        filterDesigner.SetActiveFilter(i);
                        firstFilterFound = true;
                    }

                }
                DesignBlock = (SAFilterDesignBlock)Controls.Find("filterDesignBlock" + i, true).First();
                DesignBlock.RegisterPrimitive(i, SingleFilterPrimitive);
            }

            if (!firstFilterFound)
            {
                filterDesigner.SetActiveFilter(0);
            }

            form_loaded = true;
        }

        private void filterDesignBlock_OnChange(object sender, FilterEventArgs e)
        {
            
            filterDesigner.SetActiveFilter(e.Index);

            SendActivePrimitive();

            filterDesigner.RefreshAllFilters();

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

                SendActivePrimitive();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in filterDesigner_OnDragEnd: " + ex.Message);
            }
        }

        private void SendActivePrimitive()
        {
            if (!form_loaded)
            {
                return;
            }

            filterDesigner.PrimitiveCache[filterDesigner.FocusedFilterID].QueueChange(PARENT_FORM);
        }

        private void filterDesignBlock0_Click(object sender, EventArgs e)
        {

        }
    }
}
