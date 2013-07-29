﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_Resources;
using Controls;
using SA_Resources.Filters;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class FilterForm3 : Form
    {

        #region Variables

        private const int NOT_USED = 0;
        private const int LOW_PASS = 1;
        private const int HIGH_PASS = 2;
        private const int LOW_SHELF = 3;
        private const int HIGH_SHELF = 4;
        private const int PEAK = 5;
        private const int NOTCH = 6;

        private FilterConfig[] filters = new FilterConfig[9];

        private Color[] filterColors = new Color[10];


        private List<double> center_frequencies = new List<double>();

        private int active_filter = 0;

        private bool draw_grabber_points = true;

        public bool graph_loaded = false;

        public bool dragging_lowcutoff, dragging_center, dragging_highcutoff, dragging_crosshairs;

        private double filterSelectorFade = 0.3;
        private double grabberPrecision = 1.02;
        private double minimumQ = 0.707;

        private double singleFilterStep = 1.04;
        private double masterFilterStep = 1.02;

        private const double max_filters = 3;

        private double minFreq = 10;
        private double maxFreq = 20000;

        private double last_dragging_x = 0;
        private double last_dragging_y = 0;

        private int total_filters = 3;
        private int starting_filter = 0;

        private Series MasterResponseLine;
        private Series MasterMarkerLine;

        private int chartarea_min_x = 73;
        private int chartarea_max_x = 860;
        private int chartarea_min_y = 9;
        private int chartarea_max_y = 260;

        private bool editing_textbox = false;
        private string starting_text_value = "";

        private object parent = null;

        private static Thread UIThread;

        private bool show_all_filters = true;

        /* Dial Settings */

        private delegate void SetTextCallback(TextBox tbControl, string text);

        #endregion

        private void SetText(TextBox tbControl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (tbControl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { tbControl,text });
            }
            else
            {
                tbControl.Text = text;
                tbControl.Invalidate();
            }
        }


        #region Constructor and Load

        public FilterForm3(FilterConfig[] in_filters, int chan_number = 1)
        {

            InitializeComponent();

            MasterResponseLine = filterChart.Series[6];
            MasterMarkerLine = filterChart.Series[7];

            try
            {
                filterColors[0] = Color.Chocolate;
                filterColors[1] = Color.Chartreuse;
                filterColors[2] = Color.DarkMagenta;

                this.Text = "Filter Designer - CH " + chan_number.ToString();
                this.DoubleBuffered = true;

                filters = in_filters;

                for (int i = starting_filter; i < starting_filter + total_filters; i++)
                {
                    if (filters[i] == null || filters[i].Type == FilterType.None)
                    {
                        // We don't care. Let's reinstantiate even the FilterType.None's
                        filters[i] = new FilterConfig(FilterType.None, false);

                        ((ComboBox)Controls.Find("dropFilter" + i.ToString(), true)[0]).SelectedIndex = 0;

                        filterChart.Series[i].Enabled = false;
                    }
                    else
                    {

                        ((TextBox) Controls.Find("txtGain" + i.ToString(), true)[0]).Text = filters[i].Filter.Gain.ToString("#.##");
                        ((TextBox)Controls.Find("txtFreq" + i.ToString(), true)[0]).Text = filters[i].Filter.CenterFrequency.ToString("#.");
                        ((TextBox)Controls.Find("txtQVal" + i.ToString(), true)[0]).Text = filters[i].Filter.QValue.ToString("#.###");


                        if (((int)filters[i].Filter.FilterType == 6) || ((int)filters[i].Filter.FilterType == 7))
                        {
                            if ((int)filters[i].Filter.FilterType == 6)
                            {
                                //LP 2nd order
                                ((ComboBox)Controls.Find("dropSlope" + i.ToString(), true)[0]).SelectedIndex = 1;
                                ((ComboBox)Controls.Find("dropSlope" + i.ToString(), true)[0]).Invalidate();

                                ((ComboBox)Controls.Find("dropFilter" + i.ToString(), true)[0]).SelectedIndex = LOW_PASS;
                                
                            }
                            else
                            {
                                //HP 2nd order
                                ((ComboBox)Controls.Find("dropSlope" + i.ToString(), true)[0]).SelectedIndex = 1;
                                ((ComboBox)Controls.Find("dropSlope" + i.ToString(), true)[0]).Invalidate();

                                ((ComboBox)Controls.Find("dropFilter" + i.ToString(), true)[0]).SelectedIndex = HIGH_PASS;                               

                            }
                        }
                        else
                        {
                            ((ComboBox)Controls.Find("dropSlope" + i.ToString(), true)[0]).SelectedIndex = 0;
                            ((ComboBox)Controls.Find("dropSlope" + i.ToString(), true)[0]).Invalidate();

                            ((ComboBox)Controls.Find("dropFilter" + i.ToString(), true)[0]).SelectedIndex = (int)filters[i].Filter.FilterType + 1;
                        }

                        

                        //filterColors[i] = filters[i].Filter.Color;

                        filterChart.Series[i].Enabled = true;
                    }

                }

                RefreshAllPoints();

                filterChart.ChartAreas[0].RecalculateAxesScale();

                var backImage = new NamedImage("FilterBackground", GlobalResources.FilterGraph_BG_Blue);
                filterChart.Images.Add(backImage);
                filterChart.ChartAreas[0].BackImage = "FilterBackground";

                dropFilter0.BackColor = Helpers.Lighten(filterColors[0], 0.30);
                dropFilter1.BackColor = Helpers.Lighten(filterColors[1], 0.30);
                dropFilter2.BackColor = Helpers.Lighten(filterColors[2], 0.50);

                lblFilterSelector1.BackColor = Helpers.Darken(filterColors[1], filterSelectorFade);
                lblFilterSelector2.BackColor = Helpers.Darken(filterColors[2], filterSelectorFade);


                dropAction.SelectedIndex = 0;

                graph_loaded = true;

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void FilterForm3_Load(object sender, EventArgs e)
        {
            parent = this.Owner;
        }


        #endregion

        #region Point Drawing


        private void RefreshAllPoints()
        {
            for (int i = starting_filter; i < starting_filter + total_filters; i++)
            {
                // Do not check for FilterType.None here because we want RefreshPointsInSingleFilter() to clear the points
                if (filters[i] == null)
                {
                    continue;
                }

                RefreshPointsInSingleFilter(i);
            }

            RefreshMasterPoints();

        }

        // If the type is FilterType.None then we will have ZERO POINTS when we're done.
        private void RefreshPointsInSingleFilter(int filter_index)
        {

            Series filterSeries = filterChart.Series[filter_index];
            BiquadFilter singleFilter = filters[filter_index].Filter;

            filterSeries.Points.Clear();

            // Check if there are no points due to no filter
            if (filters[filter_index] == null || filters[filter_index].Type == FilterType.None)
            {
                filterChart.Refresh();
                return;
            }

            double center_freq = singleFilter.CenterFrequency;

            center_frequencies.Add(center_freq);

            
            double value = 0.0;

            for (double freq = minFreq; freq < maxFreq; freq *= singleFilterStep)
            {

                value = singleFilter.LogValueAt(freq);
                filterSeries.Points.AddXY(freq, value);

                // Force it to add a point at the center frequency if it is between steps
                if (freq < center_freq && center_freq < (freq * singleFilterStep))
                {
                    singleFilter.CenterIndex = filterSeries.Points.Count;
                    value = singleFilter.LogValueAt(center_freq);
                    filterSeries.Points.AddXY(center_freq, value);
                }

            }

            // Add a point to the center index which was saved when we force added it
            filterSeries.Points[singleFilter.CenterIndex].MarkerColor = filterColors[filter_index];
            filterSeries.Points[singleFilter.CenterIndex].MarkerStyle = MarkerStyle.Circle;
            filterSeries.Points[singleFilter.CenterIndex].MarkerSize = 8;


            filterSeries.Color = filterColors[filter_index];
            filterSeries.BorderColor = filterColors[filter_index];
            filterChart.Refresh();

        }

        private double MasterFilterLogValue(double f)
        {
            double return_value = 0;

            for (int i = starting_filter; i < starting_filter + total_filters; i++)
            {
                if (filters[i] == null || filters[i].Type == FilterType.None || filters[i].Bypassed == true)
                {
                    continue;
                }

                return_value += filters[i].Filter.LogValueAt(f);
            }

            return Math.Max(-24, Math.Min(return_value, 24));

        }

        private void RefreshMasterPoints()
        {


            // First step is to paint just the markers...
            MasterMarkerLine.Points.Clear();

            if (filters[active_filter] == null || filters[active_filter].Type == FilterType.None)
            {
                // We should never get here except for if we sloppily code the form loading.. 

                MasterMarkerLine.Points.AddXY(0, 0);
            }
            else
            {
                BiquadFilter activeFilter = filters[active_filter].Filter;
                if (activeFilter is NotchFilter)
                {
                    MasterMarkerLine.Points.AddXY(activeFilter.CenterFrequency, -24);
                }
                else
                {
                    MasterMarkerLine.Points.AddXY(activeFilter.CenterFrequency, activeFilter.LogValueAt(activeFilter.CenterFrequency));
                }

                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[active_filter];
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 10;
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;

                if ((activeFilter is PeakFilter || activeFilter is LowShelfFilter || activeFilter is HighShelfFilter || activeFilter is NotchFilter) && draw_grabber_points)
                {
                    MasterMarkerLine.Points.AddXY(activeFilter.LowerCutoffFrequency, activeFilter.LogValueAt(activeFilter.LowerCutoffFrequency));
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[active_filter]; ;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 7;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;

                    MasterMarkerLine.Points.AddXY(activeFilter.UpperCutoffFrequency, activeFilter.LogValueAt(activeFilter.UpperCutoffFrequency));
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[active_filter]; ;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 7;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;
                }
            }


            // Add a fake point for our crosshairs
            MasterMarkerLine.Points.AddXY(0, 0);

            filterChart.Refresh();

            // STEP 2 - Update the response line...

            MasterResponseLine.Points.Clear();

            for (double i = minFreq; i < maxFreq; i *= masterFilterStep)
            {
                MasterResponseLine.Points.AddXY(i, MasterFilterLogValue(i));

                for (int j = starting_filter; j < starting_filter + total_filters; j++)
                {

                    if (filters[j] == null || filters[j].Type == FilterType.None || filters[j].Bypassed == true)
                    {
                        continue;

                    }

                    if (i < filters[j].Filter.CenterFrequency && filters[j].Filter.CenterFrequency < (i * masterFilterStep))
                    {

                        MasterResponseLine.Points.AddXY(filters[j].Filter.CenterFrequency,
                                                           MasterFilterLogValue(filters[j].Filter.CenterFrequency));

                    }
                }
            }

            filterChart.Refresh();
        }

        private void AddCrosshairMarker(double f)
        {
            int lastPoint = MasterMarkerLine.Points.Count - 1;
            MasterMarkerLine.Points[lastPoint].SetValueXY(f, MasterFilterLogValue(f));

            MasterMarkerLine.Points[lastPoint].MarkerColor = Color.Red;
            MasterMarkerLine.Points[lastPoint].MarkerSize = 10;
            MasterMarkerLine.Points[lastPoint].MarkerBorderColor = Color.White;
            MasterMarkerLine.Points[lastPoint].MarkerStyle = MarkerStyle.Cross;

            lblStatus.SuspendLayout();
            double crosshair_master_gain = MasterFilterLogValue(f);
            lblStatus.Text = f.ToString("N0") + "Hz, " + crosshair_master_gain.ToString("N2") + "dB";
            lblStatus.Invalidate();

        }

        #endregion

        #region Form Events (Everything but Chart)

        
        private void UpdateActiveFilter()
        {
            for (int i = 0; i < max_filters; i++)
            {
                Label filterLabel = ((Label)(Controls.Find("lblFilterSelector" + i.ToString(), true)[0]));

                if (i == active_filter)
                {
                    if(!show_all_filters)
                    {
                        filterChart.Series[i].Enabled = true; 
                    }

                    filterLabel.BackColor = filterColors[i];
                }
                else
                {
                    if (!show_all_filters)
                    {
                        filterChart.Series[i].Enabled = false;
                    }
                    filterLabel.BackColor = Color.FromArgb(filterColors[i].A,
                                                           (int)(filterColors[i].R * filterSelectorFade),
                                                           (int)(filterColors[i].G * filterSelectorFade),
                                                           (int)(filterColors[i].B * filterSelectorFade));
                }
            }

            RefreshMasterPoints();
        }

        private void dropFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (graph_loaded)
            {
                lblFilterSelector0.Focus(); 
            }
            
            int filterID = int.Parse(((ComboBox) sender).Name.Substring(10));

            // Change the handles to be on the one selected
            

            Label filterSelector = (Label) (Controls.Find("lblFilterSelector" + filterID.ToString(), true)[0]);

            ComboBox dropSlope = (ComboBox) (Controls.Find("dropSlope" + filterID.ToString(), true)[0]);

            TextBox txtFreq = (TextBox)(Controls.Find("txtFreq" + filterID.ToString(), true)[0]);
            TextBox txtGain = (TextBox)(Controls.Find("txtGain" + filterID.ToString(), true)[0]);
            TextBox txtQval = (TextBox)(Controls.Find("txtQval" + filterID.ToString(), true)[0]);

            Label lblFreq = (Label) (Controls.Find("lblFreq" + filterID.ToString(), true)[0]);
            Label lblGain = (Label) (Controls.Find("lblGain" + filterID.ToString(), true)[0]);
            Label lblQ = (Label) (Controls.Find("lblQ" + filterID.ToString(), true)[0]);
            Label lblSlope = (Label) (Controls.Find("lblSlope" + filterID.ToString(), true)[0]);

            Label lblFilterSelector = (Label) (Controls.Find("lblFilterSelector" + filterID.ToString(), true)[0]);


            filterSelector.Visible = true;

            BandPassFilter toolFilter = new BandPassFilter(0, 0, 0);

            switch (((ComboBox) sender).SelectedIndex)
            {
                case LOW_PASS:

                    if (graph_loaded)
                    {
                        if (dropSlope.SelectedIndex == 0)
                        {
                            filters[filterID] = new FilterConfig(FilterType.FirstOrderLowPass, false);
                            filters[filterID].Filter = new FirstOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                        else
                        {
                            filters[filterID] = new FilterConfig(FilterType.SecondOrderLowPass, false);
                            filters[filterID].Filter = new SecondOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                    }


                    
                    filterChart.Series[filterID].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = false;
                    txtGain.Visible = false;

                    lblQ.Visible = false;
                    txtQval.Visible = false;

                    lblSlope.Visible = true;
                    dropSlope.Visible = true;

                    if (graph_loaded)
                    {
                        dropSlope.SelectedIndex = 0;
                        dropSlope.Invalidate();
                    }

                    

                    break;

                case HIGH_PASS:
                    if (graph_loaded)
                    {
                        if (dropSlope.SelectedIndex == 0)
                        {
                            filters[filterID] = new FilterConfig(FilterType.FirstOrderHighPass, false);
                            filters[filterID].Filter = new FirstOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                        else
                        {
                            filters[filterID] = new FilterConfig(FilterType.SecondOrderHighPass, false);
                            filters[filterID].Filter = new SecondOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                    }
                    
                    filterChart.Series[filterID].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = false;
                    txtGain.Visible = false;

                    lblQ.Visible = false;
                    txtQval.Visible = false;

                    lblSlope.Visible = true;
                    dropSlope.Visible = true;

                    if (graph_loaded)
                    {
                        dropSlope.SelectedIndex = 0;
                        dropSlope.Invalidate();
                    }

                    

                    break;

                case LOW_SHELF:
                    filters[filterID] = new FilterConfig(FilterType.LowShelf, false);
                    filters[filterID].Filter = new LowShelfFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[filterID].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = true;
                    txtGain.Visible = true;

                    lblQ.Visible = true;
                    txtQval.Visible = true;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    break;

                case HIGH_SHELF:
                    filters[filterID] = new FilterConfig(FilterType.HighShelf, false);
                    filters[filterID].Filter = new HighShelfFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[filterID].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = true;
                    txtGain.Visible = true;

                    lblQ.Visible = true;
                    txtQval.Visible = true;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    break;

                case PEAK:
                    filters[filterID] = new FilterConfig(FilterType.Peak, false);
                    filters[filterID].Filter = new PeakFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[filterID].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = true;
                    txtGain.Visible = true;

                    lblQ.Visible = true;
                    txtQval.Visible = true;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    break;

                case NOTCH:
                    filters[filterID] = new FilterConfig(FilterType.Notch, false);
                    filters[filterID].Filter = new NotchFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[filterID].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = false;
                    txtGain.Visible = false;

                    lblQ.Visible = true;
                    txtQval.Visible = true;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    break;

                default:
                    filters[filterID] = new FilterConfig(FilterType.None, false);

                    filterChart.Series[filterID].Enabled = false;

                    lblFreq.Visible = false;
                    txtFreq.Visible = false;

                    lblGain.Visible = false;
                    txtGain.Visible = false;

                    lblQ.Visible = false;
                    txtQval.Visible = false;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    filterSelector.Visible = false;
                    break;

            }

            if (graph_loaded)
            {
                active_filter = filterID;
                UpdateActiveFilter();

                RefreshPointsInSingleFilter(filterID);
                RefreshMasterPoints();
            }
        }

        private void lblFilterSelector_Click(object sender, EventArgs e)
        {
            // lblFilterSelector5
            int filterIndex = int.Parse(((Label) sender).Name.Substring(17));

            if (filters[filterIndex] == null)
            {
                return;
            }

            active_filter = filterIndex;

            UpdateActiveFilter();
        }

        private void lblFilterSelector_Paint(object sender, PaintEventArgs e)
        {
            if (active_filter == int.Parse(((Label) sender).Name.Substring(17)))
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.White, ButtonBorderStyle.Solid);
            }
        }

        private void dropSlope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!graph_loaded)
            {
                return;
            }

            int filterIndex = int.Parse(((ComboBox)sender).Name.Substring(9));
            
            
            BandPassFilter toolFilter = new BandPassFilter(0, 0, 0);

            
            TextBox txtFreq = (TextBox)(Controls.Find("txtFreq" + filterIndex.ToString(), true)[0]);

            ComboBox dropFilter = (ComboBox)(Controls.Find("dropFilter" + filterIndex.ToString(), true)[0]);

            if(((ComboBox)sender).SelectedIndex == 0)
            {
                if (dropFilter.SelectedIndex == 1)
                {
                    filters[filterIndex].Filter = new FirstOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    filters[filterIndex].Type = FilterType.FirstOrderLowPass;
                } else
                {
                    filters[filterIndex].Filter = new FirstOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    filters[filterIndex].Type = FilterType.FirstOrderHighPass;
                }
                
            } else
            {
                if (dropFilter.SelectedIndex == 1)
                {
                    filters[filterIndex].Filter = new SecondOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    filters[filterIndex].Type = FilterType.SecondOrderLowPass;
                }
                else
                {
                    filters[filterIndex].Filter = new SecondOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    filters[filterIndex].Type = FilterType.SecondOrderHighPass;
                }
            }

            lblFilterSelector0.Focus(); 

            RefreshAllPoints();
        }

        #endregion

        #region Chart Events

        /*public void UpdateUIToVals(object param)
        {
            TextBox active_textbox = (TextBox) param;
            MethodInvoker action;
            bool is_freq = false;
            bool is_qval = false;
            bool is_gain = false;

            int filterID = int.Parse(active_textbox.Name.Substring(7));

            if (active_textbox.Name.Contains("Freq"))
            {
                is_freq = true;
            }

            if (active_textbox.Name.Contains("Gain"))
            {
                is_gain = true;
            }

            if (active_textbox.Name.Contains("Qval"))
            {
                is_qval = true;
            }

            while (true)
            {
                //SetText(active_textbox, filters[filterID].Filter.CenterFrequency.ToString("0"));

                if(is_freq)
                {
                    action = delegate { active_textbox.Text = filters[filterID].Filter.CenterFrequency.ToString("0"); active_textbox.Update(); };
                } else if(is_gain) {
                    action = delegate { active_textbox.Text = filters[filterID].Filter.Gain.ToString("0.0"); active_textbox.Update(); };
                
                } else {
                    action = delegate { active_textbox.Text = filters[filterID].Filter.QValue.ToString("0.000"); active_textbox.Update(); };
                
                }

                active_textbox.BeginInvoke(action);
                
                Thread.Sleep(5);
            }

        }
         * */


        public void UpdateUIToVals(object param)
        {
            MethodInvoker action1, action2, action3;

            int filterID = (int) param;

            TextBox freqTextbox = (TextBox)(Controls.Find("txtFreq" + filterID.ToString(), true)[0]);
            TextBox gainTextbox = (TextBox)(Controls.Find("txtGain" + filterID.ToString(), true)[0]);
            TextBox qvalTextbox = (TextBox)(Controls.Find("txtQval" + filterID.ToString(), true)[0]); 

            while (true)
            {
                try
                {
                    action1 = delegate
                                  {
                                      freqTextbox.Text = filters[filterID].Filter.CenterFrequency.ToString("0");
                                      freqTextbox.Update();
                                  };
                    freqTextbox.BeginInvoke(action1);

                    action2 = delegate
                                  {
                                      gainTextbox.Text = filters[filterID].Filter.Gain.ToString("0.00");
                                      gainTextbox.Update();
                                  };
                    gainTextbox.BeginInvoke(action2);

                    action3 = delegate
                                  {
                                      qvalTextbox.Text = filters[filterID].Filter.QValue.ToString("0.000");
                                      qvalTextbox.Update();
                                  };
                    qvalTextbox.BeginInvoke(action3);

                    Thread.Sleep(5);
                } catch (Exception ex)
                {
                    Console.WriteLine("Exception in UpdateUIToVals: " + ex.Message);
                }
            }

        }


        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {

            if (filterChart.HitTest(e.X, e.Y).Series == MasterMarkerLine)
            {

                filterChart.Cursor = Cursors.Hand;

                int point_index = filterChart.HitTest(e.X, e.Y).PointIndex;

                if (filters[active_filter].Filter is PeakFilter || filters[active_filter].Filter is LowShelfFilter || filters[active_filter].Filter is HighShelfFilter ||
                         filters[active_filter].Filter is NotchFilter)
                {
                    dragging_lowcutoff = false;
                    dragging_center = false;
                    dragging_highcutoff = false;

                    if (point_index == 1)
                    {
                        dragging_lowcutoff = true;
                        //StartTheThread(txtFreq0, txtQval0);
                    }
                    else if (point_index == 0)
                    {
                        dragging_center = true;
                        //UIThread.Start(txtFreq0);
                    }
                    else
                    {
                        dragging_highcutoff = true;
                        //StartTheThread(txtFreq0, txtQval0);
                    }

                    
                }
                else if (filters[active_filter].Filter is FirstOrderLowPassFilter ||
                         filters[active_filter].Filter is SecondOrderLowPassFilter ||
                         filters[active_filter].Filter is FirstOrderHighPassFilter ||
                         filters[active_filter].Filter is SecondOrderHighPassFilter )
                {
                    dragging_center = true;

                    
                    
                    
                }

                UIThread = new Thread(UpdateUIToVals);
                UIThread.IsBackground = true;
                UIThread.Start(active_filter);



            }
            else
            {
                AddCrosshairMarker(Math.Pow(filterChart.ChartAreas[0].AxisX.LogarithmBase,
                                            filterChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X)));
                dragging_crosshairs = true;
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

            try
            {
                var xVal = 0.0;

                var yVal = 0.0;

                var xPos = e.X;
                var yPos = e.Y;

                // Dragging everything but the crosshairs
                if (dragging_lowcutoff || dragging_center || dragging_highcutoff)
                {


                    //Console.WriteLine(yPos);
                    // We are no longer in the chart area... Let's set to max/min values as necessary

                    //if (filterChart.HitTest(e.X, e.Y).ChartArea != filterChart.ChartAreas[0])
                    //{

                        if (xPos < chartarea_min_x)
                        {
                            xPos = chartarea_min_x;
                        }

                        if (xPos > chartarea_max_x)
                        {
                            xPos = chartarea_max_x;
                        }

                        if (yPos < chartarea_min_y)
                        {
                            yPos = chartarea_min_y;
                        }

                        if (yPos > chartarea_max_y)
                        {
                            yPos = chartarea_max_y;
                        }
                    //}


                    // This must be when we know we're on the chart
                    //mouseOffYFlag = false;

                    Axis x_axis = filterChart.ChartAreas[0].AxisX;

                    xVal = Math.Min(filterChart.ChartAreas[0].AxisX.Maximum, Math.Max(filterChart.ChartAreas[0].AxisX.Minimum, Math.Pow(x_axis.LogarithmBase, x_axis.PixelPositionToValue(xPos))));

                    yVal = filterChart.ChartAreas[0].AxisY.PixelPositionToValue(yPos);

                    // Store the y val before its re-filtered


                    yVal = Math.Max(filters[active_filter].Filter.GainMin, Math.Min(yVal, filters[active_filter].Filter.GainMax));


                    if (dragging_lowcutoff)
                    {
                        if (xVal > filters[active_filter].Filter.CenterFrequency/grabberPrecision)
                        {
                            return;
                        }

                        lock (filters)
                        {
                            filters[active_filter].Filter.QValue = Math.Max(minimumQ,
                                                                            filters[active_filter].Filter.CenterFrequency/
                                                                            ((filters[active_filter].Filter.CenterFrequency - xVal)*2));
                        }
                    }
                    else if (dragging_highcutoff)
                    {
                        if (xVal < filters[active_filter].Filter.CenterFrequency*grabberPrecision)
                        {
                            return;
                        }


                        lock (filters)
                        {
                            filters[active_filter].Filter.QValue = Math.Max(minimumQ,
                                                                            filters[active_filter].Filter.CenterFrequency/
                                                                            ((xVal - filters[active_filter].Filter.CenterFrequency)*2));
                        }
                    }

                    else
                    {
                        lock (filters)
                        {
                            filters[active_filter].Filter.CenterFrequency = xVal;
                            filters[active_filter].Filter.Gain = yVal;
                        }
                    }

                    RefreshPointsInSingleFilter(active_filter);
                    RefreshMasterPoints();


                }
                else if (dragging_crosshairs)
                {

                    if (filterChart.HitTest(xPos, yPos).ChartArea != filterChart.ChartAreas[0])
                    {
                        if (last_dragging_y > (filterChart.ChartAreas[0].AxisY.Maximum - 6) ||
                            last_dragging_y < filterChart.ChartAreas[0].AxisY.Minimum)
                        {
                            return;
                        }

                        if (last_dragging_x < 1000)
                        {
                            // Assume we went off the left-hand side;
                            AddCrosshairMarker(filterChart.ChartAreas[0].AxisX.Minimum + 0.4);
                            return;
                        }
                        else
                        {
                            AddCrosshairMarker(filterChart.ChartAreas[0].AxisX.Maximum - 50);
                            return;
                        }


                    }

                    // This must be when we know we're on the chart
                    //mouseOffYFlag = false;

                    xVal = Math.Pow(filterChart.ChartAreas[0].AxisX.LogarithmBase,
                                    filterChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X));

                    last_dragging_y = filterChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                    if (xVal < (filterChart.ChartAreas[0].AxisX.Minimum + 0.4))
                    {
                        AddCrosshairMarker(filterChart.ChartAreas[0].AxisX.Minimum + 0.4);
                        return;
                    }

                    if (xVal > (filterChart.ChartAreas[0].AxisX.Maximum - 50))
                    {
                        AddCrosshairMarker(filterChart.ChartAreas[0].AxisX.Maximum - 50);
                        return;
                    }

                    last_dragging_x = xVal;

                    AddCrosshairMarker(xVal);



                }
                else
                {
                    if (filterChart.HitTest(e.X, e.Y).Series == MasterMarkerLine)
                    {
                        filterChart.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        filterChart.Cursor = Cursors.Default;
                    }
                }
            } catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging_lowcutoff = false;
            dragging_center = false;
            dragging_highcutoff = false;

            try
            {
                UIThread.Abort();
            } catch
            {
            }

            if(dragging_crosshairs)
            {
                int lastPoint = MasterMarkerLine.Points.Count - 1;
                MasterMarkerLine.Points[lastPoint].MarkerSize = 0;
            }
            dragging_crosshairs = false;

            // TODO - FIX NOTIFYME
            // parent.NotifyParent(active_filter, filters[active_filter]);
        }


        #endregion

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {
            int filterIndex = int.Parse(((PictureCheckbox)sender).Name.Substring(9));

            Series active_filter_series = filterChart.Series[filterIndex];
            filters[filterIndex].Bypassed = ((PictureCheckbox) sender).Checked;

            if (filters[filterIndex].Bypassed)
            {
                active_filter_series.BorderWidth = 2;
                active_filter_series.BorderDashStyle = ChartDashStyle.Dot;
                filterChart.Invalidate();
            }
            else
            {
                active_filter_series.BorderWidth = 2;
                active_filter_series.BorderDashStyle = ChartDashStyle.Solid;
                filterChart.Invalidate();
            }
            RefreshAllPoints();
        }

        /*private void nudFreq_ValueChanged(object sender, EventArgs e)
        {
            if (dragging_lowcutoff || dragging_center || dragging_highcutoff)
            {
                return;
            }

            int filterIndex = int.Parse(((NumericUpDown)sender).Name.Substring(7));

            filters[filterIndex].Filter.CenterFrequency = (double) ((NumericUpDown) sender).Value;

            RefreshAllPoints();



        }
        */

        private void Event_Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox active_textbox = (TextBox) sender;

            int filterIndex = int.Parse(active_textbox.Name.Substring(7));

            bool is_freq = false;
            bool is_gain = false;
            bool is_q = false;

            if (active_textbox.Name.Contains("Freq"))
            {
                is_freq = true;
            }

            if (active_textbox.Name.Contains("Gain"))
            {
                is_gain = true;
            }

            if (active_textbox.Name.Contains("Qval"))
            {
                is_q = true;
            }

            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                editing_textbox = false;
                active_textbox.Select(0, 0);

                UpdateFilterValuefromTextbox(active_textbox);
            }


            string allowedCharacterSet = "0123456789.-\b\n";

            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {
                if(is_freq && (e.KeyChar.ToString() == "." || e.KeyChar.ToString() == "-"))
                {
                    // Frequency doesn't allow negative or decimal
                    SystemSounds.Beep.Play();
                    e.Handled = true;

                }
                else if (is_gain && (e.KeyChar.ToString() == "." && active_textbox.Text.Contains(".")) || (e.KeyChar.ToString() == "-" && active_textbox.Text != "" && active_textbox.SelectedText == ""))
                {
                    // Gain allows one decimal and a negative at the beginning
                    SystemSounds.Beep.Play(); 
                    e.Handled = true;
                }
                else if (is_q && (e.KeyChar.ToString() == "-" || (e.KeyChar.ToString() == "." && active_textbox.Text.Contains("."))))
                {
                    // QVal allows no negative but one decimal
                    SystemSounds.Beep.Play(); 
                    e.Handled = true;

                }
            }
            else
            {
                SystemSounds.Beep.Play(); 
                e.Handled = true;
            }
        }

        private void Event_Textbox_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox active_textbox = (TextBox)sender;

            int filterIndex = int.Parse(active_textbox.Name.Substring(7));

            starting_text_value = active_textbox.Text;
            active_textbox.SelectAll();
            editing_textbox = true;
        }

        private void Event_Textbox_Leave(object sender, EventArgs e)
        {
            if (editing_textbox)
            {
                TextBox active_textbox = (TextBox)sender;

                int filterIndex = int.Parse(active_textbox.Name.Substring(7)); 
                
                editing_textbox = false;
                active_textbox.Select(0, 0);
                UpdateFilterValuefromTextbox(active_textbox);

            }
        }

        private void UpdateFilterValuefromTextbox(TextBox active_textbox)
        {

            int filterIndex = int.Parse(active_textbox.Name.Substring(7));

            double parsed_value;

            if (active_textbox.Name.Contains("Freq"))
            {
                if (!double.TryParse(active_textbox.Text, out parsed_value))
                {
                    // Can't parse. Switch to the original text
                    active_textbox.Text = starting_text_value;
                    return;
                } else
                {
                    active_textbox.Text = filters[filterIndex].Filter.SuggestedFrequency(parsed_value).ToString();
                    filters[filterIndex].Filter.CenterFrequency = filters[filterIndex].Filter.SuggestedFrequency(parsed_value);
                    
                    RefreshPointsInSingleFilter(filterIndex);
                    RefreshMasterPoints();
                }
            }

            if (active_textbox.Name.Contains("Gain"))
            {
                if (!double.TryParse(active_textbox.Text, out parsed_value))
                {
                    // Can't parse. Switch to the original text
                    active_textbox.Text = starting_text_value;
                    return;
                }
                else
                {
                    active_textbox.Text = filters[filterIndex].Filter.SuggestedGain(parsed_value).ToString("##.#");
                    filters[filterIndex].Filter.Gain = filters[filterIndex].Filter.SuggestedGain(parsed_value);

                    RefreshPointsInSingleFilter(filterIndex);
                    RefreshMasterPoints();
                }
            }

            if (active_textbox.Name.Contains("Qval"))
            {

                if (!double.TryParse(active_textbox.Text, out parsed_value))
                {
                    // Can't parse. Switch to the original text
                    active_textbox.Text = starting_text_value;
                    return;
                }
                else
                {
                    active_textbox.Text = filters[filterIndex].Filter.SuggestedQ(parsed_value).ToString("##.###");
                    filters[filterIndex].Filter.QValue = filters[filterIndex].Filter.SuggestedQ(parsed_value);

                    RefreshPointsInSingleFilter(filterIndex);
                    RefreshMasterPoints();
                }

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine(filters);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            using (CopyForm copyForm = new CopyForm())
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                copyForm.ShowDialog(this);
            }
        }


    }

    

}
