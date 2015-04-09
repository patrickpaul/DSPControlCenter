using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SA_GFXLib;
using SA_Resources.DSP.Filters;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAControls
{
    public partial class FilterDesigner : UserControl
    {
        public List<DSP_Primitive_BiquadFilter> PrimitiveCache = new List<DSP_Primitive_BiquadFilter>();

        private int NumberPrimitives = 0;
        private int MaximumPrimitives = 6;

        private bool ShowAllFilters = true; // Whether or not to show just active filter and master or all filters and master

        /* Colors and Display */
        private Color[] filterColors = new Color[10];
        private double filterSelectorFade = 0.6;

        /* Dragging */
        public bool dragging_lowcutoff, dragging_center, dragging_highcutoff, dragging_crosshairs;
        private double grabberPrecision = 1.01;
        private double last_dragging_x = 0;
        private double last_dragging_y = 0;

        /* Chart components and configuration */
        private Series MasterResponseLine;
        private Series MasterMarkerLine;

        private int chartarea_min_x = 73;
        private int chartarea_max_x = 860;
        private int chartarea_min_y = 9;
        private int chartarea_max_y = 260; 
        
        private double MinimumFreq = 10;
        private double MaximumFreq = 20000;

        private double MinimumGain = -24;
        private double MaximumGain = 24;

        private double singleFilterStep = 1.04;
        private double masterFilterStep = 1.02;

        public int FocusedFilterID = 0;
        public int PreviouslyFocusedFilterID = 0;

        public event FilterDesignerEventHandler OnChange;

        public event EventHandler OnDragBegin;
        public event EventHandler OnDragEnd;

        public FilterDesigner()
        {
            InitializeComponent();

            /* Initialize filter colors */
            filterColors[0] = Color.Chocolate;
            filterColors[1] = Color.Chartreuse;
            filterColors[2] = Color.HotPink;
            filterColors[3] = Color.SandyBrown;
            filterColors[4] = Color.PaleGreen;
            filterColors[5] = Color.Thistle;

            MasterResponseLine = filterChart.Series[6];
            MasterMarkerLine = filterChart.Series[7];

            filterChart.ChartAreas[0].RecalculateAxesScale();

            var backImage = new NamedImage("FilterGraph_BG_Blue_Modified", SA_GFXLib_Resources.FilterGraph_BG_Blue);
            filterChart.Images.Add(backImage);
            filterChart.ChartAreas[0].BackImage = "FilterGraph_BG_Blue_Modified";

            // A key point to remember is that upon initialization, there are NO filters assigned to it
        }

        public void RegisterFilterPrimitive(DSP_Primitive_BiquadFilter InputPrimitive)
        {

            if(NumberPrimitives == MaximumPrimitives)
            {
                throw new Exception("Filter limit of " + MaximumPrimitives + " exceed during RegisterFilterPrimitive()");
            }

            PrimitiveCache.Add(InputPrimitive);

            filterChart.Series[NumberPrimitives].Enabled = true;

            Label filterLabel = ((Label)(Controls.Find("lblFilterSelector" + (NumberPrimitives).ToString(), true).FirstOrDefault()));

            if (filterLabel != null)
            {
                filterLabel.Visible = true;
            }

            NumberPrimitives++;

            RefreshAllFilters();


        }

        protected void OnChangeEvent(FilterEventArgs e)
        {
            if (this.OnChange != null)
            {
                try
                {
                    OnChange(this, e);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in FilterDesigner.OnChangeEvent: " + ex.Message);
                }
            }
        }

        protected void OnDragBeginEvent(EventArgs e)
        {
            if (this.OnDragBegin != null)
            {
                try
                {
                    OnDragBegin(this, e);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in FilterDesigner.OnDragBeginEvent: " + ex.Message);
                }
            }
        }

        protected void OnDragEndEvent(EventArgs e)
        {
            if (this.OnDragEnd != null)
            {
                try
                {
                    OnDragEnd(this, e);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in FilterDesigner.OnDragEndEvent: " + ex.Message);
                }
            }
        } 

        public void SetActiveFilter(int FilterIndex)
        {
            PreviouslyFocusedFilterID = FocusedFilterID;
            FocusedFilterID = FilterIndex;
            UpdateFocusedFilter();

        }
        public void RefreshAllFilters()
        {

            bool NeedsNewFocus = false;
            Label filterLabel;

            // A refresh was called, so it's possibly that this occured because a filter was deleted (set to None)
            // Let's see if the filter that was deleted has focus
            if(PrimitiveCache[FocusedFilterID].HasNoFilter)
            {
                // See if our previous filter has focus
                // This is beause when the FilterDesignBlock changes filter type (even to none) it'll focus on that DesignBlock's filter
                if (PrimitiveCache[PreviouslyFocusedFilterID].HasNoFilter)
                {
                    // Because the old filter no longer exists, we will set a flag to focus on the first available filter we see
                    NeedsNewFocus = true;
                } else
                {
                    // Previously focused filter still exists, revert to it
                    FocusedFilterID = PreviouslyFocusedFilterID;
                    UpdateFocusedFilter();
                }
            }
             
            // As we loop through we're going to show/high filter select labels based off of their current (ie new) status
            for (int i = 0; i < NumberPrimitives; i++)
            {
                filterLabel = ((Label)(Controls.Find("lblFilterSelector" + (i).ToString(), true).FirstOrDefault()));

                if (PrimitiveCache[i].HasNoFilter)
                {
                    if (filterLabel != null)
                    {
                        filterLabel.Visible = false;
                    }
                } else
                {
                    if (filterLabel != null)
                    {
                        filterLabel.Visible = true;
                    }
                    // This filter exists, if the flag is set that we need the first available filter, focus on it
                    if (NeedsNewFocus)
                    {
                        PreviouslyFocusedFilterID = FocusedFilterID;
                        FocusedFilterID = i;

                        UpdateFocusedFilter();

                        NeedsNewFocus = false;
                    }
                }
                // Do not check for FilterType.None here because we want RefreshPointsInSingleFilter() to clear the points
                if (PrimitiveCache[i].Filter == null)
                {
                    //continue;
                }

                RefreshSingleFilter(i);
            }

            RefreshMasterFilter();

        }

        private void RefreshSingleFilter(int FilterIndex)
        {
            if(FilterIndex > NumberPrimitives || PrimitiveCache[FilterIndex] == null)
            {
                throw new Exception("There was no Filter Primitive at index " + FilterIndex + " in RefreshSingleFilter()");
            }

            Series FilterSeries = filterChart.Series[FilterIndex];

            FilterSeries.Points.Clear();

            // Check if there are no points due to no filter
            if (PrimitiveCache[FilterIndex].HasNoFilter)
            {
                filterChart.Refresh();
                return;
            }

            double CenterFrequency = PrimitiveCache[FilterIndex].Filter.CenterFrequency;

            double ValueAtFrequency = 0.0;

            for (double CurrentFrequency = MinimumFreq; CurrentFrequency < MaximumFreq; CurrentFrequency *= singleFilterStep)
            {

                ValueAtFrequency = PrimitiveCache[FilterIndex].Filter.LogValueAt(CurrentFrequency);
                FilterSeries.Points.AddXY(CurrentFrequency, ValueAtFrequency);

                // Force it to add a point at the center frequency if it is between steps
                if (CurrentFrequency < CenterFrequency && CenterFrequency < (CurrentFrequency * singleFilterStep))
                {
                    PrimitiveCache[FilterIndex].Filter.CenterIndex = FilterSeries.Points.Count;
                    ValueAtFrequency = PrimitiveCache[FilterIndex].Filter.LogValueAt(CenterFrequency);
                    FilterSeries.Points.AddXY(CenterFrequency, ValueAtFrequency);
                }

            }

            // Add a point to the center index which was saved when we force added it
            FilterSeries.Points[PrimitiveCache[FilterIndex].Filter.CenterIndex].MarkerColor = filterColors[FilterIndex];
            FilterSeries.Points[PrimitiveCache[FilterIndex].Filter.CenterIndex].MarkerStyle = MarkerStyle.Circle;
            FilterSeries.Points[PrimitiveCache[FilterIndex].Filter.CenterIndex].MarkerSize = 8;


            if (PrimitiveCache[FilterIndex].Bypassed)
            {
                FilterSeries.BorderWidth = 2;
                FilterSeries.BorderDashStyle = ChartDashStyle.Dot;
            }
            else
            {
                FilterSeries.BorderWidth = 2;
                FilterSeries.BorderDashStyle = ChartDashStyle.Solid;
            }



            FilterSeries.Color = filterColors[FilterIndex];
            FilterSeries.BorderColor = filterColors[FilterIndex];
            filterChart.Refresh();

        }

        private double MasterFilterLogValue(double f)
        {
            double return_value = 0;

            for (int i = 0; i < NumberPrimitives; i++)
            {
                if (PrimitiveCache[i].Bypassed || PrimitiveCache[i].HasNoFilter)
                {
                    continue;
                }

                return_value += PrimitiveCache[i].Filter.LogValueAt(f);
            }

            return Math.Max(MinimumGain, Math.Min(return_value, MaximumGain));

        }

        private void RefreshMasterFilter()
        {

            // STEP 1 First we see what the active filter ID is (the one that currently has grabbers associated) and draw those points

            // Clear master market line
            MasterMarkerLine.Points.Clear();

            if (PrimitiveCache[FocusedFilterID].HasNoFilter)
            {
                // We should never get here except for if we sloppily code the form loading.. 

                MasterMarkerLine.Points.AddXY(0, 0);
            }
            else
            {
                DSP_Primitive_BiquadFilter FocusedPrimitive = PrimitiveCache[FocusedFilterID];
                BiquadFilter FocusedFilter = FocusedPrimitive.Filter;


                // All filters get a market at the center frequency, the notch filter however gets one at the MinimumGain sinece it's theoretically -Infinity
                if (FocusedPrimitive.FType == FilterType.Notch)
                {
                    MasterMarkerLine.Points.AddXY(FocusedFilter.CenterFrequency, MinimumGain);
                }
                else
                {
                    MasterMarkerLine.Points.AddXY(FocusedFilter.CenterFrequency, FocusedFilter.LogValueAt(FocusedFilter.CenterFrequency));
                }

                // Set the most recent point (Center frequency) properties

                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[FocusedFilterID];
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 10;
                MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;

                if (FocusedFilter is PeakFilter || FocusedFilter is LowShelfFilter || FocusedFilter is HighShelfFilter || FocusedFilter is NotchFilter || FocusedFilter is BandPassFilter)
                {
                    // A filter with a variable Q so we need to have upper and lower cutoff markers
                    MasterMarkerLine.Points.AddXY(FocusedFilter.LowerCutoffFrequency, FocusedFilter.LogValueAt(FocusedFilter.LowerCutoffFrequency));
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[FocusedFilterID];
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerStyle = MarkerStyle.Circle;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerSize = 7;
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerBorderColor = Color.White;

                    MasterMarkerLine.Points.AddXY(FocusedFilter.UpperCutoffFrequency, FocusedFilter.LogValueAt(FocusedFilter.UpperCutoffFrequency));
                    MasterMarkerLine.Points[MasterMarkerLine.Points.Count() - 1].MarkerColor = filterColors[FocusedFilterID];
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

            // Outer loop is frequencies, inner loop is filters
            for (double CurrentFrequency = MinimumFreq; CurrentFrequency < MaximumFreq; CurrentFrequency *= masterFilterStep)
            {
                // Draw the master value at the CurrentFrequency
                MasterResponseLine.Points.AddXY(CurrentFrequency, MasterFilterLogValue(CurrentFrequency));

                // This next portion ensures that each filter's center frequency has a point (To avoid display issues)
                // We must check to see if we are less than the center frequency but that the next increment would go past it
                // We cannot do this after the fact because the points must stay in order

                for (int j = 0; j < NumberPrimitives; j++)
                {
                    // Skip if there is no value here anyways
                    if (PrimitiveCache[j].HasNoFilter || PrimitiveCache[j].Bypassed)
                    {
                        continue;
                    }

                    // Check if the conditions are met to add an extra point
                    if (CurrentFrequency < PrimitiveCache[j].Filter.CenterFrequency && PrimitiveCache[j].Filter.CenterFrequency < (CurrentFrequency * masterFilterStep))
                    {
                        MasterResponseLine.Points.AddXY(PrimitiveCache[j].Filter.CenterFrequency,MasterFilterLogValue(PrimitiveCache[j].Filter.CenterFrequency));
                    }
                }
            }

            filterChart.Refresh();
        }

        private void AddCrosshairMarker(double f)
        {
            int lastPoint = MasterMarkerLine.Points.Count - 1; // The last point is the one used for the crosshairs
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


        private void UpdateFocusedFilter()
        {

            for (int i = 0; i < NumberPrimitives; i++)
            {
                Label filterLabel = ((Label)(Controls.Find("lblFilterSelector" + (i).ToString(), true)[0]));

                if (i == FocusedFilterID)
                {
                    if (!ShowAllFilters)
                    {
                        filterChart.Series[i].Enabled = true;
                    }

                    filterLabel.BackColor = filterColors[i];
                }
                else
                {
                    if (!ShowAllFilters)
                    {
                        filterChart.Series[i].Enabled = false;
                    }

                    filterLabel.BackColor = Color.FromArgb(filterColors[i].A,
                                                           (int)(filterColors[i].R * filterSelectorFade),
                                                           (int)(filterColors[i].G * filterSelectorFade),
                                                           (int)(filterColors[i].B * filterSelectorFade));
                }
            }

            RefreshMasterFilter();
        }

        private void lblFilterSelector_Click(object sender, EventArgs e)
        {
            int SelectedID = int.Parse(((Label)sender).Name.Substring(17));

            if (PrimitiveCache[SelectedID].HasNoFilter)
            {
                return;
            }
            PreviouslyFocusedFilterID = FocusedFilterID;
            FocusedFilterID = SelectedID;

            UpdateFocusedFilter();
        }

        private void lblFilterSelector_Paint(object sender, PaintEventArgs e)
        {
            int FilterIndex = int.Parse(((Label)sender).Name.Substring(17));

            if (FocusedFilterID == FilterIndex)
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.White, ButtonBorderStyle.Solid);
            }
        }


        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (PrimitiveCache.Count < 1)
            {
                return;
            }

            // First see if we hit a grabber point (if not then we'll create crosshairs)
            if (filterChart.HitTest(e.X, e.Y).Series == MasterMarkerLine)
            {

                OnDragBeginEvent(new EventArgs());


                filterChart.Cursor = Cursors.Hand;

                // There are up to 3 points on the MasterMarkerLine
                // LowerCutoff = 1
                // CenterFreq = 0
                // UpperCutoff = 2

                int point_index = filterChart.HitTest(e.X, e.Y).PointIndex;

                BiquadFilter FocusedFilter = PrimitiveCache[FocusedFilterID].Filter;
                
                // Check to see if it's a filter with a variable Q
                if (FocusedFilter is PeakFilter || FocusedFilter is LowShelfFilter || FocusedFilter is HighShelfFilter || 
                    FocusedFilter is NotchFilter || FocusedFilter is BandPassFilter)
                {
                    dragging_lowcutoff = false;
                    dragging_center = false;
                    dragging_highcutoff = false;

                    if (point_index == 1)
                    {
                        dragging_lowcutoff = true;
                    }
                    else if (point_index == 0)
                    {
                        dragging_center = true;
                    }
                    else
                    {
                        dragging_highcutoff = true;
                    }


                }
                else if (FocusedFilter is FirstOrderLowPassFilter || FocusedFilter is SecondOrderLowPassFilter || 
                        FocusedFilter is FirstOrderHighPassFilter || FocusedFilter is SecondOrderHighPassFilter || 
                        FocusedFilter is BandPassFilter)
                {
                    // Basically checking to make sure it's not null
                    dragging_center = true;
                }
                
                // Based on this a UIThread is now created in the FilterDesignerForm

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

            // TODO - Check that moving off the chart is handled cleanly
            try
            {
                if(PrimitiveCache.Count < 1)
                {
                    return;
                }

                var xVal = 0.0;

                var yVal = 0.0;

                var xPos = e.X;
                var yPos = e.Y;

                BiquadFilter FocusedFilter = PrimitiveCache[FocusedFilterID].Filter;

                // Dragging everything but the crosshairs
                if (dragging_lowcutoff || dragging_center || dragging_highcutoff)
                {

                    // If we've gone off the chart set to the extrema
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

                    Axis x_axis = filterChart.ChartAreas[0].AxisX;

                    // Get the frequency (xVal) using the logarithm conversion
                    xVal = Math.Min(filterChart.ChartAreas[0].AxisX.Maximum, Math.Max(filterChart.ChartAreas[0].AxisX.Minimum, Math.Pow(x_axis.LogarithmBase, x_axis.PixelPositionToValue(xPos))));

                    // Get the yVal, however it could be outside the range of the allowable gain.
                    yVal = filterChart.ChartAreas[0].AxisY.PixelPositionToValue(yPos);

                    // Now is where we will put it back inside the proper region
                    yVal = Math.Max(FocusedFilter.GainMin, Math.Min(yVal, FocusedFilter.GainMax));

                    if (dragging_lowcutoff)
                    {
                        // We can't have a lower cutoff that is greater than the center frequency
                        // TODO - Figure out what the / grabberPrecision does (it works though so don't remove it)
                        if (xVal > FocusedFilter.CenterFrequency / grabberPrecision)
                        {
                            return;
                        }

                        lock (PrimitiveCache[FocusedFilterID].Filter)
                        {
                            // Calculate the new Q value
                            // The new bandwidth is twice the distance between lower cutoff and center frequency (to account for the distance between center frequency and upper cutoff)
                            // Q = CenterFrequency/Bandwidth
                            PrimitiveCache[FocusedFilterID].Filter.QValue = FocusedFilter.SuggestedQ(FocusedFilter.CenterFrequency / ((FocusedFilter.CenterFrequency - xVal) * 2));
                            OnChangeEvent(new FilterEventArgs(FocusedFilterID, PrimitiveCache[FocusedFilterID], FilterChangeType.QVal));
                        }
                    }
                    else if (dragging_highcutoff)
                    {
                        // Similar to dragging_lowcutoff, see comments there
                        if (xVal < FocusedFilter.CenterFrequency * grabberPrecision)
                        {
                            return;
                        }


                        lock (PrimitiveCache[FocusedFilterID].Filter)
                        {
                            PrimitiveCache[FocusedFilterID].Filter.QValue = FocusedFilter.SuggestedQ(FocusedFilter.CenterFrequency/((xVal - FocusedFilter.CenterFrequency)*2));
                            OnChangeEvent(new FilterEventArgs(FocusedFilterID, PrimitiveCache[FocusedFilterID], FilterChangeType.QVal));
                        }
                    }

                    else
                    {
                        // Dragging center marker (center frequency and gain), just move it to the current xVal (Frequency)
                        lock (PrimitiveCache[FocusedFilterID].Filter)
                        {
                            FocusedFilter.CenterFrequency = xVal;
                            FocusedFilter.Gain = yVal; // yVal has already been set to between GainMin and GainMax
                            OnChangeEvent(new FilterEventArgs(FocusedFilterID, PrimitiveCache[FocusedFilterID], FilterChangeType.CenterFrequencyGain));
                        }
                    }

                    

                    RefreshSingleFilter(FocusedFilterID);
                    RefreshMasterFilter();


                }
                else if (dragging_crosshairs)
                {
                    // Check to make sure we're still on the chart
                    if (filterChart.HitTest(xPos, yPos).ChartArea != filterChart.ChartAreas[0])
                    {
                        // HitTest will still trigger even if we are 6 pixels above the visible portion of the chart
                        // Make sure we are within allowable limits
                        if (last_dragging_y > (filterChart.ChartAreas[0].AxisY.Maximum - 6) || last_dragging_y < filterChart.ChartAreas[0].AxisY.Minimum)
                        {
                            return;
                        }

                        // HitTest will trigger if we are less than 1000
                        // TODO - Test why last_dragging isn't okay < 1000
                        if (last_dragging_x < 1000)
                        {
                            // Assume we went off the left-hand side
                            AddCrosshairMarker(filterChart.ChartAreas[0].AxisX.Minimum + 0.4);
                            return;
                        }
                        else
                        {
                            AddCrosshairMarker(filterChart.ChartAreas[0].AxisX.Maximum - 50);
                            return;
                        }
                    }

                    xVal = Math.Pow(filterChart.ChartAreas[0].AxisX.LogarithmBase,filterChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X));

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
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (PrimitiveCache.Count < 1)
            {
                return;
            }

            dragging_lowcutoff = false;
            dragging_center = false;
            dragging_highcutoff = false;

            if (dragging_crosshairs)
            {
                int lastPoint = MasterMarkerLine.Points.Count - 1;
                MasterMarkerLine.Points[lastPoint].MarkerSize = 0;
                dragging_crosshairs = false;
            }
            else
            {
                OnDragEndEvent(new EventArgs());

                // TODO - Trigger FilterDesigner OnChange
                //SendFilterToParent(active_global_filter_index);
            }
        }


    }
}
