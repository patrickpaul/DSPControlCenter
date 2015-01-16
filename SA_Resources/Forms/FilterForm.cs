using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_Resources.Configurations;
using SA_Resources.DSP;
using SA_Resources.DSP.Filters;
using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    public partial class FilterForm : Form
    {

        // Disable form's closing button
        protected override CreateParams CreateParams
        {
            get
            {
                int CP_NOCLOSE_BUTTON = 0x200;
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        #region Variables

        public bool graph_loaded = false;
        private MainForm_Template PARENT_FORM;
        private int CH_NUMBER = 0;
        private static Thread UIThread;

        public object _threadlock;
        public bool uithread_abort = false;


        /* FILTER TYPES */
        private const int NOT_USED = 0;
        private const int LOW_PASS = 1;
        private const int HIGH_PASS = 2;
        private const int LOW_SHELF = 3;
        private const int HIGH_SHELF = 4;
        private const int PEAK = 5;
        private const int NOTCH = 6;
        private const int BANDPASS = 7;

        /* Caches */

        private List<double> center_frequencies = new List<double>();

        /* Colors and Display */
        private Color[] filterColors = new Color[10];
        private double filterSelectorFade = 0.3;

        /* Dragging */
        public bool dragging_lowcutoff, dragging_center, dragging_highcutoff, dragging_crosshairs;
        private double grabberPrecision = 1.02;
        private double minimumQ = 0.707;
        private bool draw_grabber_points = true;

        /* Configuration */
        private double minFreq = 10;
        private double maxFreq = 20000;
        private int total_filters = 6;
        private int starting_filter = 3;
        private int active_global_filter_index = 3;

        private bool show_all_filters = true; // Whether or not to show just active filter and master or all filters and master


        /* Chart Area */
        private double singleFilterStep = 1.04;
        private double masterFilterStep = 1.02;
        private double last_dragging_x = 0;
        private double last_dragging_y = 0;

        private Series MasterResponseLine;
        private Series MasterMarkerLine;

        private int chartarea_min_x = 73;
        private int chartarea_max_x = 860;
        private int chartarea_min_y = 9;
        private int chartarea_max_y = 260;

        private bool editing_textbox = false;
        private string starting_text_value = "";

        private bool IS_SIX_CHANNEL = false;

        /* Dial Delegates */

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

        public FilterForm(MainForm_Template _parentForm, int chan_number = 1, bool is_six = false)
        {
            CH_NUMBER = chan_number;
            PARENT_FORM = _parentForm;

            InitializeComponent();

            _threadlock = new Object();

            MasterResponseLine = filterChart.Series[6];
            MasterMarkerLine = filterChart.Series[7];

            if (is_six)
            {
                total_filters = 6;
                starting_filter = 3;
                active_global_filter_index = 3;
                pnlSecondRowFilters.Visible = true;
                pnlButtons.Location = new Point(18, 511);
                IS_SIX_CHANNEL = true;

            }
            else
            {
                total_filters = 3;
                starting_filter = 0;
                active_global_filter_index = 0;
                pnlSecondRowFilters.Visible = false;

                lblFilterSelector3.Visible = false;
                lblFilterSelector4.Visible = false;
                lblFilterSelector5.Visible = false;

                pnlButtons.Location = new Point(18, 407);
            }

            try
            {
                filterColors[0] = Color.Chocolate;
                filterColors[1] = Color.Chartreuse;
                filterColors[2] = Color.DarkMagenta;
                filterColors[3] = Color.SandyBrown;
                filterColors[4] = Color.PaleGreen;
                filterColors[5] = Color.Plum;
                filterColors[6] = Color.SandyBrown;
                filterColors[7] = Color.PaleGreen;
                filterColors[8] = Color.Plum;

                this.Text = "Filter Designer - CH " + chan_number.ToString();
                this.DoubleBuffered = true;

                bool first_active_selected = false;

                for (int i = starting_filter; i < starting_filter + total_filters; i++)
                {
                    int localized_starting_filter = i - starting_filter;
                    if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i] == null || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Type == FilterType.None)
                    {
                        // We don't care. Let's reinstantiate even the FilterType.None's
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i] = new FilterConfig(FilterType.None, false);

                        ((ComboBox)Controls.Find("dropFilter" + (localized_starting_filter).ToString(), true)[0]).SelectedIndex = 0;

                        filterChart.Series[localized_starting_filter].Enabled = false;
                    }
                    else
                    {
                        if (!first_active_selected)
                        {
                            active_global_filter_index = i;
                            
                            first_active_selected = true;
                        }

                        ((PictureCheckbox)Controls.Find("chkBypass" + localized_starting_filter.ToString(), true)[0]).Checked = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Bypassed;
                        ((PictureCheckbox)Controls.Find("chkBypass" + localized_starting_filter.ToString(), true)[0]).Invalidate();

                        ((TextBox)Controls.Find("txtGain" + localized_starting_filter.ToString(), true)[0]).Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.Gain.ToString("F1");
                        ((TextBox)Controls.Find("txtFreq" + localized_starting_filter.ToString(), true)[0]).Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.CenterFrequency.ToString("#.");
                        ((TextBox)Controls.Find("txtQval" + localized_starting_filter.ToString(), true)[0]).Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.QValue.ToString("F3");

                        if (((int)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.FilterType == 6) || ((int)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.FilterType == 7))
                        {
                            if ((int)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.FilterType == 6)
                            {
                                //LP 2nd order
                                ((ComboBox)Controls.Find("dropSlope" + localized_starting_filter.ToString(), true)[0]).SelectedIndex = 1;
                                ((ComboBox)Controls.Find("dropSlope" + localized_starting_filter.ToString(), true)[0]).Invalidate();

                                ((ComboBox)Controls.Find("dropFilter" + localized_starting_filter.ToString(), true)[0]).SelectedIndex = LOW_PASS;

                            }
                            else
                            {
                                //HP 2nd order
                                ((ComboBox)Controls.Find("dropSlope" + localized_starting_filter.ToString(), true)[0]).SelectedIndex = 1;
                                ((ComboBox)Controls.Find("dropSlope" + localized_starting_filter.ToString(), true)[0]).Invalidate();

                                ((ComboBox)Controls.Find("dropFilter" + localized_starting_filter.ToString(), true)[0]).SelectedIndex = HIGH_PASS;

                            }
                        }
                        else
                        {
                            ((ComboBox)Controls.Find("dropSlope" + localized_starting_filter.ToString(), true)[0]).SelectedIndex = 0;
                            ((ComboBox)Controls.Find("dropSlope" + localized_starting_filter.ToString(), true)[0]).Invalidate();

                            ((ComboBox)Controls.Find("dropFilter" + localized_starting_filter.ToString(), true)[0]).SelectedIndex = (int)PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.FilterType + 1;
                        }

                        filterColors[localized_starting_filter] = filterColors[localized_starting_filter];

                        filterChart.Series[localized_starting_filter].Enabled = true;

                        
                    }

                }

                


                RefreshAllFilters();

                filterChart.ChartAreas[0].RecalculateAxesScale();

                var backImage = new NamedImage("FilterGraph_BG_Blue_Modified", GlobalResources.FilterGraph_BG_Blue_Modified);
                filterChart.Images.Add(backImage);
                filterChart.ChartAreas[0].BackImage = "FilterGraph_BG_Blue_Modified";

                dropFilter0.BackColor = Helpers.Lighten(filterColors[0], 0.20);
                dropFilter1.BackColor = Helpers.Lighten(filterColors[1], 0.30);
                dropFilter2.BackColor = Helpers.Lighten(filterColors[2], 0.40);
                dropFilter3.BackColor = Helpers.Lighten(filterColors[3], 0.10);
                dropFilter4.BackColor = Helpers.Lighten(filterColors[4], 0.10);
                dropFilter5.BackColor = Helpers.Lighten(filterColors[5], 0.10);

                pnlSecondRowFilters.Invalidate();

                lblFilterSelector1.BackColor = Helpers.Darken(filterColors[1], filterSelectorFade);
                lblFilterSelector2.BackColor = Helpers.Darken(filterColors[2], filterSelectorFade);
                lblFilterSelector3.BackColor = Helpers.Darken(filterColors[2], filterSelectorFade);
                lblFilterSelector4.BackColor = Helpers.Darken(filterColors[3], filterSelectorFade);
                lblFilterSelector5.BackColor = Helpers.Darken(filterColors[4], filterSelectorFade);

                dropAction.SelectedIndex = 0;
                dropAction.Invalidate();

                UpdateActiveFilter();

                graph_loaded = true;

            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion


        #region Point Drawing


        private void RefreshAllFilters()
        {
            for (int i = starting_filter; i < starting_filter + total_filters; i++)
            {
                // Do not check for FilterType.None here because we want RefreshPointsInSingleFilter() to clear the points
                if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i] == null)
                {
                    continue;
                }

                RefreshSingleFilter(i);
            }

            RefreshMasterFilter();

        }

        // If the type is FilterType.None then we will have ZERO POINTS when we're done.
        // NOTE - THIS INDEX HAS ALREADY BEEN CORRECTED FOR THE STARTING_FILTER

        private void RefreshSingleFilter(int global_filter_index)
        {
            int local_filter_index = global_filter_index - starting_filter;

            Series filterSeries = filterChart.Series[local_filter_index];

            BiquadFilter singleFilter = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter;

            filterSeries.Points.Clear();

            // Check if there are no points due to no filter
            if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] == null || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Type == FilterType.None)
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
            filterSeries.Points[singleFilter.CenterIndex].MarkerColor = filterColors[local_filter_index];
            filterSeries.Points[singleFilter.CenterIndex].MarkerStyle = MarkerStyle.Circle;
            filterSeries.Points[singleFilter.CenterIndex].MarkerSize = 8;


            if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Bypassed)
            {
                filterSeries.BorderWidth = 2;
                filterSeries.BorderDashStyle = ChartDashStyle.Dot;
                //filterChart.Invalidate();
            }
            else
            {
                filterSeries.BorderWidth = 2;
                filterChart.Series[local_filter_index].BorderDashStyle = ChartDashStyle.Solid;
                //filterChart.Invalidate();
            }



            filterSeries.Color = filterColors[local_filter_index];
            filterSeries.BorderColor = filterColors[local_filter_index];
            filterChart.Refresh();

        }

        private double MasterFilterLogValue(double f)
        {
            double return_value = 0;

            for (int i = starting_filter; i < starting_filter + total_filters; i++)
            {
                if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i] == null || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Type == FilterType.None || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Bypassed == true)
                {
                    continue;
                }

                return_value += PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i].Filter.LogValueAt(f);
            }

            return Math.Max(-24, Math.Min(return_value, 24));

        }

        private void RefreshMasterFilter()
        {

            int active_local_filter_index = active_global_filter_index - starting_filter;
            // First step is to paint just the markers...
            MasterMarkerLine.Points.Clear();

            if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index] == null || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Type == FilterType.None)
            {
                // We should never get here except for if we sloppily code the form loading.. 

                MasterMarkerLine.Points.AddXY(0, 0);
            }
            else
            {
                BiquadFilter activeFilter = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter;

                if (activeFilter is NotchFilter)
                {
                    MasterMarkerLine.Points.AddXY(activeFilter.CenterFrequency, -24);
                }
                else
                {
                    MasterMarkerLine.Points.AddXY(activeFilter.CenterFrequency, activeFilter.LogValueAt(activeFilter.CenterFrequency));
                }


                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[active_local_filter_index];
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 10;
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;

                if ((activeFilter is PeakFilter || activeFilter is LowShelfFilter || activeFilter is HighShelfFilter || activeFilter is NotchFilter || activeFilter is BandPassFilter) && draw_grabber_points)
                {
                    MasterMarkerLine.Points.AddXY(activeFilter.LowerCutoffFrequency, activeFilter.LogValueAt(activeFilter.LowerCutoffFrequency));
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[active_local_filter_index];
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 7;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;

                    MasterMarkerLine.Points.AddXY(activeFilter.UpperCutoffFrequency, activeFilter.LogValueAt(activeFilter.UpperCutoffFrequency));
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[active_local_filter_index];
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

                    if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j] == null || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Type == FilterType.None || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Bypassed == true)
                    {
                        continue;

                    }

                    if (i < PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Filter.CenterFrequency && PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Filter.CenterFrequency < (i * masterFilterStep))
                    {

                        MasterResponseLine.Points.AddXY(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Filter.CenterFrequency,
                                                           MasterFilterLogValue(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Filter.CenterFrequency));

                    }
                }
            }

            filterChart.Refresh();
        }

        /// <summary>
        /// Adds crosshair marker (red x) at given frequency on master response curve
        /// </summary>
        /// <param name="f">Frequency (in Hz) of location for crosshair markers</param>
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

        /// <summary>
        /// Enables grabber handles and highlights filter selector to current active_global_filter_index 
        /// </summary>
        private void UpdateActiveFilter()
        {

            for (int i = starting_filter; i < starting_filter + total_filters; i++)
            {
                Label filterLabel = ((Label)(Controls.Find("lblFilterSelector" + (i-starting_filter).ToString(), true)[0]));

                if (i == active_global_filter_index)
                {
                    if(!show_all_filters)
                    {
                        filterChart.Series[i - starting_filter].Enabled = true; 
                    }

                    filterLabel.BackColor = filterColors[i];
                }
                else
                {
                    if (!show_all_filters)
                    {
                        filterChart.Series[i - starting_filter].Enabled = false;
                    }
                    filterLabel.BackColor = Color.FromArgb(filterColors[i - starting_filter].A,
                                                           (int)(filterColors[i - starting_filter].R * filterSelectorFade),
                                                           (int)(filterColors[i - starting_filter].G * filterSelectorFade),
                                                           (int)(filterColors[i - starting_filter].B * filterSelectorFade));
                }
            }

            RefreshMasterFilter();
        }

        private void dropFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (graph_loaded)
            {
                lblFilterSelector0.Focus(); 
            }
            
            int local_filter_index = int.Parse(((ComboBox) sender).Name.Substring(10));
            int global_filter_index = local_filter_index + starting_filter;
            // Change the handles to be on the one selected

            bool removing_filter = false; // Will be set to true so we can select new active filter if this one is going to FilterType.None

            bool bypass_value = ((PictureCheckbox)Controls.Find("chkBypass" + local_filter_index, true).First()).Checked;
            Label filterSelector = (Label)(Controls.Find("lblFilterSelector" + local_filter_index.ToString(), true)[0]);

            ComboBox dropSlope = (ComboBox)(Controls.Find("dropSlope" + local_filter_index.ToString(), true)[0]);

            TextBox txtFreq = (TextBox)(Controls.Find("txtFreq" + local_filter_index.ToString(), true)[0]);
            TextBox txtGain = (TextBox)(Controls.Find("txtGain" + local_filter_index.ToString(), true)[0]);
            TextBox txtQval = (TextBox)(Controls.Find("txtQval" + local_filter_index.ToString(), true)[0]);

            Label lblFreq = (Label)(Controls.Find("lblFreq" + local_filter_index.ToString(), true)[0]);
            Label lblGain = (Label)(Controls.Find("lblGain" + local_filter_index.ToString(), true)[0]);
            Label lblQ = (Label)(Controls.Find("lblQ" + local_filter_index.ToString(), true)[0]);
            Label lblSlope = (Label)(Controls.Find("lblSlope" + local_filter_index.ToString(), true)[0]);

            Label lblFilterSelector = (Label)(Controls.Find("lblFilterSelector" + local_filter_index.ToString(), true)[0]);


            filterSelector.Visible = true;

            BandPassFilter toolFilter = new BandPassFilter(0, 0, 0);

            switch (((ComboBox)sender).SelectedIndex)
            {
                case LOW_PASS:

                    if (graph_loaded)
                    {
                        if (dropSlope.SelectedIndex == 0)
                        {
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.FirstOrderLowPass, bypass_value);
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new FirstOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                        else
                        {
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.SecondOrderLowPass, bypass_value);
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new SecondOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                    }
                    filterChart.Series[local_filter_index].Enabled = true;

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
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.FirstOrderHighPass, bypass_value);
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new FirstOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                        else
                        {
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.SecondOrderHighPass, bypass_value);
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new SecondOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                        }
                    }
                    
                    filterChart.Series[local_filter_index].Enabled = true;

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
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.LowShelf, bypass_value);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new LowShelfFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[local_filter_index].Enabled = true;

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
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.HighShelf, bypass_value);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new HighShelfFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[local_filter_index].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = true;
                    txtGain.Visible = true;

                    lblQ.Visible = true;
                    txtQval.Visible = true;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    break;

                case BANDPASS:
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.BandPass, bypass_value);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new BandPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[local_filter_index].Enabled = true;

                    lblFreq.Visible = true;
                    txtFreq.Visible = true;

                    lblGain.Visible = false;
                    txtGain.Visible = false;

                    lblQ.Visible = true;
                    txtQval.Visible = true;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    break;

                case PEAK:
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.Peak, bypass_value);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new PeakFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[local_filter_index].Enabled = true;

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
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.Notch, bypass_value);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new NotchFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    filterChart.Series[local_filter_index].Enabled = true;

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
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] = new FilterConfig(FilterType.None, bypass_value);

                    filterChart.Series[local_filter_index].Enabled = false;

                    lblFreq.Visible = false;
                    txtFreq.Visible = false;

                    lblGain.Visible = false;
                    txtGain.Visible = false;

                    lblQ.Visible = false;
                    txtQval.Visible = false;

                    lblSlope.Visible = false;
                    dropSlope.Visible = false;

                    filterSelector.Visible = false;

                    removing_filter = true;
                    break;

            }

            if (graph_loaded)
            {
                if (!removing_filter)
                {
                    active_global_filter_index = global_filter_index;
                }
                else
                {
                    
                    for (int j = starting_filter; j < starting_filter + total_filters; j++)
                    {
                        if ((PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j] != null))
                        {
                            if(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][j].Type != FilterType.None)
                            {
                                active_global_filter_index = j;
                                break;
                            }
                        }
                    }

                }
                UpdateActiveFilter();

                RefreshSingleFilter(global_filter_index);
                RefreshMasterFilter();


                if (graph_loaded)
                {
                    SendFilterToParent(global_filter_index);
                }
            }
        }

        private void lblFilterSelector_Click(object sender, EventArgs e)
        {
            int local_filter_index = int.Parse(((Label)sender).Name.Substring(17));
            int global_filter_index = local_filter_index + starting_filter;

            if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index] == null)
            {
                return;
            }

            active_global_filter_index = global_filter_index;

            UpdateActiveFilter();
        }

        private void lblFilterSelector_Paint(object sender, PaintEventArgs e)
        {
            int local_filter_index = int.Parse(((Label) sender).Name.Substring(17));
            int global_filter_index = local_filter_index + starting_filter;

            if (active_global_filter_index == global_filter_index)
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

            int local_filter_index = int.Parse(((ComboBox)sender).Name.Substring(9));
            int global_filter_index = local_filter_index + starting_filter;


            BandPassFilter toolFilter = new BandPassFilter(0, 0, 0);


            TextBox txtFreq = (TextBox)(Controls.Find("txtFreq" + local_filter_index.ToString(), true)[0]);

            ComboBox dropFilter = (ComboBox)(Controls.Find("dropFilter" + local_filter_index.ToString(), true)[0]);

            if (((ComboBox)sender).SelectedIndex == 0)
            {
                if (dropFilter.SelectedIndex == 1)
                {
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new FirstOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Type = FilterType.FirstOrderLowPass;
                }
                else
                {
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new FirstOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Type = FilterType.FirstOrderHighPass;
                }

            }
            else
            {
                if (dropFilter.SelectedIndex == 1)
                {
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new SecondOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Type = FilterType.SecondOrderLowPass;
                }
                else
                {
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter = new SecondOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), 0.0, 0.0);
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Type = FilterType.SecondOrderHighPass;
                }
            }

            lblFilterSelector0.Focus();

            RefreshAllFilters();

            SendFilterToParent(active_global_filter_index);
        }

        #endregion

        #region Chart Events

        public void UpdateUIToVals(object param)
        {
            try
            {
                MethodInvoker action1, action2, action3;

                int global_filter_index = (int) param;

                int local_filter_index = global_filter_index - starting_filter;

                TextBox freqTextbox = (TextBox)Enumerable.FirstOrDefault(Controls.Find("txtFreq" + local_filter_index.ToString(), true));
                TextBox gainTextbox = (TextBox)Enumerable.FirstOrDefault(Controls.Find("txtGain" + local_filter_index.ToString(), true));
                TextBox qvalTextbox = (TextBox)Enumerable.FirstOrDefault(Controls.Find("txtQval" + local_filter_index.ToString(), true));


                
                while (true)
                {
                    try
                    {
                        if (freqTextbox != null)
                        {
                            action1 = delegate
                                          {
                                              freqTextbox.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.CenterFrequency.ToString("0");
                                              freqTextbox.Update();
                                          };
                            freqTextbox.BeginInvoke(action1);

                            //Debug.WriteLine("Updating Frequency to " + freqTextbox.Text);

                        }

                        if (gainTextbox != null)
                        {
                            action2 = delegate
                                          {
                                              gainTextbox.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.Gain.ToString("0.00");
                                              gainTextbox.Update();
                                          };
                            gainTextbox.BeginInvoke(action2);

                        }

                        if (qvalTextbox != null)
                        {
                            action3 = delegate
                                          {
                                              qvalTextbox.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.QValue.ToString("0.000");
                                              qvalTextbox.Update();
                                          };
                            qvalTextbox.BeginInvoke(action3);

                        }
                        Thread.Sleep(5);


                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception in UpdateUIToVals Level 2: " + ex.Message);
                    }

                    lock (_threadlock)
                    {
                        if (uithread_abort == true)
                        {
                            uithread_abort = false;
                            Debug.WriteLine("Broke UI thread");
                            break;
                        }


                    }
                }

            } catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateUIToVals Level 1: " + ex.Message);

            }

        }


        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {

            if (filterChart.HitTest(e.X, e.Y).Series == MasterMarkerLine)
            {

                filterChart.Cursor = Cursors.Hand;

                int point_index = filterChart.HitTest(e.X, e.Y).PointIndex;

                if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is PeakFilter || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is LowShelfFilter || PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is HighShelfFilter || 
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is NotchFilter ||
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is BandPassFilter)
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
                else if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is FirstOrderLowPassFilter ||
                         PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is SecondOrderLowPassFilter ||
                         PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is FirstOrderHighPassFilter ||
                         PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is SecondOrderHighPassFilter ||
                         PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter is BandPassFilter)
                {
                    dragging_center = true;

                    
                    
                    
                }

                UIThread = new Thread(UpdateUIToVals);
                UIThread.Name = "UIThread";
                UIThread.IsBackground = true;
                UIThread.Start(active_global_filter_index);



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


                    //Debug.WriteLine(yPos);
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


                    yVal = Math.Max(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.GainMin, Math.Min(yVal, PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.GainMax));


                    if (dragging_lowcutoff)
                    {
                        if (xVal > PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency/grabberPrecision)
                        {
                            return;
                        }

                        lock (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1])
                        {
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.QValue = Math.Max(minimumQ,
                                                                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency/
                                                                            ((PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency - xVal)*2));
                        }
                    }
                    else if (dragging_highcutoff)
                    {
                        if (xVal < PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency*grabberPrecision)
                        {
                            return;
                        }


                        lock (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1])
                        {
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.QValue = Math.Max(minimumQ,
                                                                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency/
                                                                            ((xVal - PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency)*2));
                        }
                    }

                    else
                    {
                        lock (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1])
                        {
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.CenterFrequency = xVal;
                            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][active_global_filter_index].Filter.Gain = yVal;
                        }
                    }

                    RefreshSingleFilter(active_global_filter_index);
                    RefreshMasterFilter();


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

            if (dragging_crosshairs)
            {
                int lastPoint = MasterMarkerLine.Points.Count - 1;
                MasterMarkerLine.Points[lastPoint].MarkerSize = 0;
                dragging_crosshairs = false;
            } else
            {
                try
                {
                    lock (_threadlock)
                    {
                        uithread_abort = true;
                    }
                }
                catch (Exception ex)
                {
               

                }

                SendFilterToParent(active_global_filter_index);
            }






            // TODO - FIX NOTIFYME
            
        }


        #endregion

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {

            if (!graph_loaded)
            {
                return;
            }


            int local_filter_index = int.Parse(((PictureCheckbox)sender).Name.Substring(9));
            int global_filter_index = local_filter_index + starting_filter;
            PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Bypassed = ((PictureCheckbox)sender).Checked;

            RefreshAllFilters();

            SendFilterToParent(active_global_filter_index);
        }

        private void Event_Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox active_textbox = (TextBox) sender;

            int local_filter_index = int.Parse(active_textbox.Name.Substring(7));
            int global_filter_index = local_filter_index + starting_filter;

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

            int local_filter_index = int.Parse(active_textbox.Name.Substring(7));
            int global_filter_index = local_filter_index + starting_filter;

            starting_text_value = active_textbox.Text;
            active_textbox.SelectAll();
            editing_textbox = true;
        }

        private void Event_Textbox_Leave(object sender, EventArgs e)
        {
            if (editing_textbox)
            {
                TextBox active_textbox = (TextBox)sender;

                int local_filter_index = int.Parse(active_textbox.Name.Substring(7));
                int global_filter_index = local_filter_index + starting_filter;

                editing_textbox = false;
                active_textbox.Select(0, 0);
                UpdateFilterValuefromTextbox(active_textbox);

                /*if(text_has_changed)
                {
                    SendFilterToParent(active_global_filter_index);
                    text_has_changed = false;
                }*/
                

            }
        }

        private void UpdateFilterValuefromTextbox(TextBox active_textbox)
        {

            int local_filter_index = int.Parse(active_textbox.Name.Substring(7));
            int global_filter_index = local_filter_index + starting_filter;

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
                    active_textbox.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.SuggestedFrequency(parsed_value).ToString();
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.CenterFrequency = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.SuggestedFrequency(parsed_value);

                    RefreshSingleFilter(global_filter_index);
                    RefreshMasterFilter();
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
                    active_textbox.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.SuggestedGain(parsed_value).ToString("##.#");

                    if (active_textbox.Text == "")
                    {
                        active_textbox.Text = "0.0";
                    }
                    
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.Gain = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.SuggestedGain(parsed_value);

                    RefreshSingleFilter(global_filter_index);
                    RefreshMasterFilter();
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
                    active_textbox.Text = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.SuggestedQ(parsed_value).ToString("F3");
                    //if(active_textbox.Text.Substring(0,1) == ".") {
                    //    active_textbox.Text = "0" + active_textbox.Text;
                    //}
                    PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.QValue = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.SuggestedQ(parsed_value);

                    RefreshSingleFilter(global_filter_index);
                    RefreshMasterFilter();
                }

            }

            SendFilterToParent(active_global_filter_index);

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

        private void btnGo_Click(object sender, EventArgs e)
        {
            /*
            CopyFormType copyType = CopyFormType.Filter3;
            if (IS_SIX_CHANNEL)
            {
                copyType = CopyFormType.Filter6;
            }
            using (CopyForm copyForm = new CopyForm(PARENT_FORM, CH_NUMBER, copyType))
            {
                // passing this in ShowDialog will set the .Owner 
                // property of the child form
                copyForm.ShowDialog(this);
            }
             * */
        }

        private void filterControl_Enter(object sender, EventArgs e)
        {
            string controlName = ((Control)sender).Name;
            int control_local_filter_index = 0;

            if (controlName.Contains("dropFilter"))
            {
                control_local_filter_index = int.Parse(controlName.Substring(10, 1));
            } else if(controlName.Contains("chkBypass")) {
                control_local_filter_index = int.Parse(controlName.Substring(9, 1));
            }
            else if (controlName.Contains("txtFreq"))
            {
                control_local_filter_index = int.Parse(controlName.Substring(7, 1));
            }
            else if (controlName.Contains("txtGain"))
            {
                control_local_filter_index = int.Parse(controlName.Substring(7, 1));
            }
            else if (controlName.Contains("dropSlope"))
            {
                control_local_filter_index = int.Parse(controlName.Substring(9, 1));
            }
            else if (controlName.Contains("txtQval"))
            {
                control_local_filter_index = int.Parse(controlName.Substring(7, 1));
            }


            if (control_local_filter_index > total_filters)
            {
                // In case they somehow tab to filters that are hidden on input filters
                return;
            }

            active_global_filter_index = control_local_filter_index + starting_filter;

            UpdateActiveFilter();


        }

        private void FilterForm6_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                uithread_abort = true;
            }
            catch
            {
            }
        }

        private void SendFilterToParent(int global_filter_index)
        {
            UInt32 B0 = 0x20000000;
            UInt32 B1 = 0x00000000;
            UInt32 B2 = 0x00000000;
            UInt32 A1 = 0x00000000;
            UInt32 A2 = 0x00000000;

            UInt32 PACKAGE = 0x00000000;
            UInt32 PACKAGE_GAIN = 0x00000000;
            UInt32 PACKAGE_Q = 0x00000000;
            try
            {

                if(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER-1][global_filter_index] == null)
                {
                    B0 = 0x20000000;
                    B1 = 0x00000000;
                    B2 = 0x00000000;
                    A1 = 0x00000000;
                    A2 = 0x00000000;

                    PACKAGE = 0x00000000;
                    PACKAGE_GAIN = 0x00000000;
                    PACKAGE_Q = 0x00000000;
                } else
                {
                    if(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER-1][global_filter_index].Type == FilterType.None)
                    {
                        B0 = 0x20000000;
                        B1 = 0x00000000;
                        B2 = 0x00000000;
                        A1 = 0x00000000;
                        A2 = 0x00000000;

                        PACKAGE = 0x00000000;
                        PACKAGE_GAIN = 0x00000000;
                        PACKAGE_Q = 0x00000000;

                    } else
                    {
                        if (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Bypassed)
                        {
                            B0 = 0x20000000;
                            B1 = 0x00000000;
                            B2 = 0x00000000;
                            A1 = 0x00000000;
                            A2 = 0x00000000;
                        }
                        else
                        {
                            B0 = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.B0, 3, 29);
                            B1 = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.B1, 3, 29);
                            B2 = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.B2, 3, 29);
                            A1 = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.A1 * -1, 2, 30);
                            A2 = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.A2 * -1, 2, 30);
                        }
                        PACKAGE = DSP_Math.filter_to_package(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index]);
                        PACKAGE_GAIN = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.Gain, 8, 24);
                        PACKAGE_Q = DSP_Math.double_to_MN(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][global_filter_index].Filter.QValue, 8, 24);

                    }

                    
                }

                int starting_index = (40) + ((CH_NUMBER - 1)*45) + (global_filter_index*5);

                // Mute channel output...
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(36 + (CH_NUMBER-1), 0x00000000));

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index, B0));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index+1, B1));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index+2, B2));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index+3, A1));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index+4, A2));

                // Unmute channel output...
                // Recall old value
                UInt32 gain_val = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].gains[CH_NUMBER-1][3].Gain), 3, 29);
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(36 + (CH_NUMBER - 1), gain_val));

                starting_index = (300) + ((CH_NUMBER - 1) * 27) + (global_filter_index * 3);

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index, PACKAGE));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index+1, PACKAGE_GAIN));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(starting_index+2, PACKAGE_Q));

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in FilterForm.SendFilterToParent: " + ex.Message);
            }

        }



    }

    

}
