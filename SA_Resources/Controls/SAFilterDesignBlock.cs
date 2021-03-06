﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using SA_Resources.DSP.Filters;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAControls
{
    public delegate void FilterDesignerEventHandler(object sender, FilterEventArgs e);

    public partial class SAFilterDesignBlock : UserControl
    {
        public DSP_Primitive_BiquadFilter FilterPrimitive;
        public bool InitialLoadComplete = false;

        public int Index = 0;
        private bool SelectingNewFilter_BlockSlope;
        private bool SelectingNewFilter_BlockBypass;

        private const int INDEX_NONE = 0;
        private const int INDEX_LOWPASS = 1;
        private const int INDEX_HIGHPASS = 2;
        private const int INDEX_LOWSHELF = 3;
        private const int INDEX_HIGHSHELF = 4;
        private const int INDEX_PEAK = 5;
        private const int INDEX_NOTCH = 6;
        private const int INDEX_BANDPASS = 7;

        public event FilterDesignerEventHandler OnChange;

        public event FilterDesignerEventHandler OnFocus;

        private bool editing_textbox;
        private string starting_text_value;

        private bool textbox_selected_to_clear = false;
        public bool PlaySounds = false;

        public SAFilterDesignBlock()
        {
            InitializeComponent();

            this.Visible = false;

        }

        public void SetBackgroundColor(Color newBackColor)
        {
            this.dropFilter.BackColor = newBackColor;
        }

        public SAFilterDesignBlock(int _index, DSP_Primitive_BiquadFilter inputPrimitive)
        {
            InitializeComponent();

            dropSlope.SelectedIndex = 0;

            this.Visible = false;

            this.RegisterPrimitive(_index, inputPrimitive);
            
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
                    Debug.WriteLine("Exception in SAFilterDesignBlock.OnChangeEvent: " + ex.Message);
                }
            }
        }

        protected void OnFocusEvent(FilterEventArgs e)
        {
            if (this.OnFocus != null)
            {
                try
                {
                    OnFocus(this, e);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception in SAFilterDesignBlock.OnFocusEvent: " + ex.Message);
                }
            }
        } 

        public void RegisterPrimitive(int _index,DSP_Primitive_BiquadFilter inputPrimitive)
        {
            this.Index = _index;
            FilterPrimitive = inputPrimitive;

            LoadInitialValues();

            this.Visible = true;
            InitialLoadComplete = true;
        }

        public double CenterFrequency
        {
            get
            {
                if (FilterPrimitive != null)
                {
                    return FilterPrimitive.Filter.CenterFrequency;
                } else
                {
                    return 0;
                }
            }
            set
            {
                if(FilterPrimitive != null)
                {
                    FilterPrimitive.Filter.CenterFrequency = value;
                    txtFreq.Text = value.ToString("0");
                    txtFreq.Update();
                } 
                
            }
        }

        public double QValue
        {
            get
            {
                if (FilterPrimitive != null)
                {
                    return FilterPrimitive.Filter.QValue;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (FilterPrimitive != null)
                {
                    FilterPrimitive.Filter.QValue = value;
                    txtQval.Text = value.ToString("0.000");
                    txtQval.Update();
                }

            }
        }

        public double Gain
        {
            get
            {
                if (FilterPrimitive != null)
                {
                    return FilterPrimitive.Filter.Gain;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (FilterPrimitive != null)
                {
                    FilterPrimitive.Filter.Gain = value;
                    txtGain.Text = value.ToString("0.00");
                    txtGain.Update();
                }

            }
        }

        private bool loadingValues = false;

        private void LoadInitialValues()
        {
            loadingValues = true;
            switch(FilterPrimitive.FType)
            {
                case FilterType.FirstOrderLowPass:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_LOWPASS;
                    dropFilter.Invalidate();

                    dropSlope.SelectedIndex = 0;
                    dropSlope.Invalidate();

                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.SecondOrderLowPass:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_LOWPASS;
                    dropFilter.Invalidate();

                    dropSlope.SelectedIndex = 1;
                    dropSlope.Invalidate();

                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");

                    chkBypass.Checked = FilterPrimitive.Bypassed;


                    break;

                case FilterType.FirstOrderHighPass:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_HIGHPASS;
                    dropFilter.Invalidate();

                    dropSlope.SelectedIndex = 0;
                    dropSlope.Invalidate();

                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.SecondOrderHighPass:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_HIGHPASS;
                    dropFilter.Invalidate();

                    dropSlope.SelectedIndex = 1;
                    dropSlope.Invalidate();

                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.LowShelf:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_LOWSHELF;
                    dropFilter.Invalidate();

                    txtQval.Text = FilterPrimitive.Filter.QValue.ToString("F3");
                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");
                    txtGain.Text = FilterPrimitive.Filter.Gain.ToString("F1");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.HighShelf:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_HIGHSHELF;
                    dropFilter.Invalidate();

                    txtQval.Text = FilterPrimitive.Filter.QValue.ToString("F3");
                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");
                    txtGain.Text = FilterPrimitive.Filter.Gain.ToString("F1");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.Peak:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_PEAK;
                    dropFilter.Invalidate();

                    txtQval.Text = FilterPrimitive.Filter.QValue.ToString("F3");
                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");
                    txtGain.Text = FilterPrimitive.Filter.Gain.ToString("F1");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.Notch:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_NOTCH;
                    dropFilter.Invalidate();

                    txtQval.Text = FilterPrimitive.Filter.QValue.ToString("F3");
                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");
                    txtGain.Text = FilterPrimitive.Filter.Gain.ToString("F1");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                case FilterType.BandPass:

                    // This will show/hide the necessary controls
                    dropFilter.SelectedIndex = INDEX_BANDPASS;
                    dropFilter.Invalidate();

                    txtQval.Text = FilterPrimitive.Filter.QValue.ToString("F3");
                    txtFreq.Text = FilterPrimitive.Filter.CenterFrequency.ToString("#.");
                    txtGain.Text = FilterPrimitive.Filter.Gain.ToString("F1");

                    chkBypass.Checked = FilterPrimitive.Bypassed;

                    break;

                default :
                    dropFilter.SelectedIndex = INDEX_NONE;
                    dropFilter.Invalidate();

                    chkBypass.Checked = true;

                    break;
            }

            loadingValues = false;
        }


        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {

            if (!InitialLoadComplete || FilterPrimitive == null)
            {
                return;
            }

            FilterPrimitive.Bypassed = chkBypass.Checked;

            if(SelectingNewFilter_BlockBypass)
            {
                SelectingNewFilter_BlockBypass = false;
                return;
            }

            if (!loadingValues)
            {
                Console.WriteLine("Onchange Event called by chkBypass_CheckedChanged");
                OnChangeEvent(new FilterEventArgs(Index, FilterPrimitive));
            }
        }

        private void SetFrequencyVisibility(bool shown)
        {
            lblFreq.Visible = shown;
            txtFreq.Visible = shown;
        }

        private void SetGainVisibility(bool shown)
        {
            lblGain.Visible = shown;
            txtGain.Visible = shown;
        }

        private void SetQVisibility(bool shown)
        {
            lblQ.Visible = shown;
            txtQval.Visible = shown;
        }

        private void SetSlopeVisibility(bool shown)
        {
            lblSlope.Visible = shown;
            dropSlope.Visible = shown;
        }



        private void dropFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO - Call event that we've switched focus

            BandPassFilter toolFilter = new BandPassFilter(0, 0, 0);

            SelectingNewFilter_BlockSlope = true;
            SelectingNewFilter_BlockBypass = true;

            chkBypass.Checked = false;
            switch(dropFilter.SelectedIndex)
            {

                case INDEX_LOWPASS:

                    // EVENT to enable filter series

                    SetFrequencyVisibility(true);
                    SetGainVisibility(false);
                    SetQVisibility(false);
                    SetSlopeVisibility(true);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.FirstOrderLowPass;
                        FilterPrimitive.Filter = new FirstOrderLowPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                    }

                    // Check if InitialLoadComplete so we don't unnecessarily fire a SlopeChanged event
                    if (InitialLoadComplete)
                    {
                        dropSlope.SelectedIndex = 0;
                        dropSlope.Invalidate();
                    }



                    break;

                case INDEX_HIGHPASS:

                    SetFrequencyVisibility(true);
                    SetGainVisibility(false);
                    SetQVisibility(false);
                    SetSlopeVisibility(true);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.FirstOrderHighPass;
                        FilterPrimitive.Filter = new FirstOrderHighPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                    
                    }

                    // Check if InitialLoadComplete so we don't unnecessarily fire a SlopeChanged event
                    if (InitialLoadComplete)
                    {
                        dropSlope.SelectedIndex = 0;
                        dropSlope.Invalidate();
                    }

                    break;

                case INDEX_LOWSHELF:

                    SetFrequencyVisibility(true);
                    SetGainVisibility(true);
                    SetQVisibility(true);
                    SetSlopeVisibility(false);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.LowShelf;
                        FilterPrimitive.Filter = new LowShelfFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                    
                    }

                    break;

                case INDEX_HIGHSHELF:

                    SetFrequencyVisibility(true);
                    SetGainVisibility(true);
                    SetQVisibility(true);
                    SetSlopeVisibility(false);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.HighShelf;
                        FilterPrimitive.Filter = new HighShelfFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                    
                    }

                    break;

                case INDEX_PEAK:

                    SetFrequencyVisibility(true);
                    SetGainVisibility(true);
                    SetQVisibility(true);
                    SetSlopeVisibility(false);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.Peak;
                        FilterPrimitive.Filter = new PeakFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                    
                    }

                    break;

                case INDEX_NOTCH:

                    SetFrequencyVisibility(true);
                    SetGainVisibility(false);
                    SetQVisibility(true);
                    SetSlopeVisibility(false);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.Notch;
                        FilterPrimitive.Filter = new NotchFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));
                    
                    }

                    break;

                case INDEX_BANDPASS:

                    SetFrequencyVisibility(true);
                    SetGainVisibility(false);
                    SetQVisibility(true);
                    SetSlopeVisibility(false);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.BandPass;
                        FilterPrimitive.Filter = new BandPassFilter(toolFilter.SuggestedFrequency(txtFreq.Text), toolFilter.SuggestedGain(txtGain.Text), toolFilter.SuggestedQ(txtQval.Text));

                    }

                    break;

                default :
                    // NOT USED

                    SetFrequencyVisibility(false);
                    SetGainVisibility(false);
                    SetQVisibility(false);
                    SetSlopeVisibility(false);

                    if (FilterPrimitive != null && InitialLoadComplete)
                    {
                        FilterPrimitive.FType = FilterType.None;
                        FilterPrimitive.Filter = null;
                        FilterPrimitive.Recalculate_Values();
                    }
                    break;




            }

            if (!InitialLoadComplete || FilterPrimitive == null)
            {
                SelectingNewFilter_BlockSlope = false;
                SelectingNewFilter_BlockBypass = false;
                return;
            }

            Console.WriteLine("Onchange Event called by dropFilter_SelectedIndexChanged"); 
            
            OnChangeEvent(new FilterEventArgs(Index, FilterPrimitive));

            label1.Focus();
        }

        private void dropSlope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!InitialLoadComplete || FilterPrimitive == null || SelectingNewFilter_BlockSlope)
            {
                SelectingNewFilter_BlockSlope = false;
                return;
            }

            if(dropSlope.SelectedIndex == 0)
            {
                // Selected first order

                if(FilterPrimitive.FType == FilterType.SecondOrderLowPass)
                {
                    FilterPrimitive.FType = FilterType.FirstOrderLowPass;
                    FilterPrimitive.Filter = new FirstOrderLowPassFilter(CenterFrequency); // TODO - Suggested frequency?

                } else
                {
                    // Currently second order high pass
                    FilterPrimitive.FType = FilterType.FirstOrderHighPass;
                    FilterPrimitive.Filter = new FirstOrderHighPassFilter(CenterFrequency);
                }
            } else
            {
                // Selected second order

                if (FilterPrimitive.FType == FilterType.FirstOrderLowPass)
                {
                    FilterPrimitive.FType = FilterType.SecondOrderLowPass;
                    FilterPrimitive.Filter = new SecondOrderLowPassFilter(CenterFrequency);

                }
                else
                {
                    // Currently second order high pass
                    FilterPrimitive.FType = FilterType.SecondOrderHighPass;
                    FilterPrimitive.Filter = new SecondOrderHighPassFilter(CenterFrequency);
                }
            }

            Console.WriteLine("Onchange Event called by dropSlope_SelectedIndexChanged"); 
            OnChangeEvent(new FilterEventArgs(Index, FilterPrimitive));

        }

        private void Event_Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox active_textbox = (TextBox)sender;

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

                return;
            }


            string allowedCharacterSet = "0123456789.-\b\n";

            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {
                if (is_freq && (e.KeyChar.ToString() == "." || e.KeyChar.ToString() == "-"))
                {
                    // Frequency doesn't allow negative or decimal
                    if (PlaySounds)
                    {
                        SystemSounds.Beep.Play();
                    }
                    e.Handled = true;

                }
                else if (is_gain && (e.KeyChar.ToString() == "." && active_textbox.Text.Contains(".")) || (e.KeyChar.ToString() == "-" && active_textbox.Text != "" && active_textbox.SelectedText == ""))
                {
                    // Gain allows one decimal and a negative at the beginning
                    if (textbox_selected_to_clear && e.KeyChar.ToString() == ".")
                    {
                        //okay!
                        e.Handled = true;
                        active_textbox.Text = "0.";
                        active_textbox.Select(active_textbox.Text.Length, 0);
                    }
                    {
                        // QVal allows no negative but one decimal
                        if (PlaySounds)
                        {
                            SystemSounds.Beep.Play();
                        }
                        e.Handled = true;
                    }
                }
                else if (is_q && (e.KeyChar.ToString() == "-" || (e.KeyChar.ToString() == "." && active_textbox.Text.Contains("."))))
                {
                    if (textbox_selected_to_clear && e.KeyChar.ToString() == ".")
                    {
                        //okay!
                        e.Handled = true;
                        active_textbox.Text = "0.";
                        active_textbox.Select(active_textbox.Text.Length, 0);
                    }
                    {
                        // QVal allows no negative but one decimal
                        if (PlaySounds)
                        {
                            SystemSounds.Beep.Play();
                        }
                        e.Handled = true;
                    }

                }
            }
            else
            {

                if (e.KeyChar != 0x03 && e.KeyChar != 0x16)
                {

                    if (PlaySounds)
                    {
                        SystemSounds.Beep.Play();
                    }


                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar == 0x16)
                    {
                        editing_textbox = false;

                        active_textbox.Text = Clipboard.GetText();
                        active_textbox.Select(0, 0);

                        UpdateFilterValuefromTextbox(active_textbox);

                        e.Handled = true;
                    }

                    return;

                }
            }

            textbox_selected_to_clear = false;
        }

        private void Event_Textbox_Enter(object sender, EventArgs e)
        {
            TextBox active_textbox = (TextBox)sender;

            starting_text_value = active_textbox.Text;
            active_textbox.SelectAll();
            editing_textbox = true;
            textbox_selected_to_clear = true;

            OnFocusEvent(new FilterEventArgs(Index, FilterPrimitive));
        }

        private void Event_Textbox_MouseUp(object sender, MouseEventArgs e)
        {
            // Removed here, code moved to Event_Textbox_Enter so that we capture Tab enters
        }

        private void Event_Textbox_Leave(object sender, EventArgs e)
        {
            if (editing_textbox)
            {
                TextBox active_textbox = (TextBox)sender;
                editing_textbox = false;
                active_textbox.Select(0, 0);

                if (active_textbox.Text != starting_text_value)
                {
                    UpdateFilterValuefromTextbox(active_textbox);
                }
            }
        }

        private void UpdateFilterValuefromTextbox(TextBox active_textbox)
        {

            double parsed_value;

            if (active_textbox.Name.Contains("Freq"))
            {
                if (!double.TryParse(active_textbox.Text, out parsed_value))
                {
                    // Can't parse. Switch to the original text
                    active_textbox.Text = starting_text_value;
                    return;
                }
                else
                {
                    active_textbox.Text = FilterPrimitive.Filter.SuggestedFrequency(parsed_value).ToString();
                    FilterPrimitive.Filter.CenterFrequency = FilterPrimitive.Filter.SuggestedFrequency(parsed_value);

                    Console.WriteLine("Onchange Event called by UpdateFilterValuefromTextbox A"); 
                    OnChangeEvent(new FilterEventArgs(Index, FilterPrimitive, FilterChangeType.CenterFrequency));
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
                    active_textbox.Text = FilterPrimitive.Filter.SuggestedGain(parsed_value).ToString("##.#");

                    if (active_textbox.Text == "")
                    {
                        active_textbox.Text = "0.0";
                    }

                    FilterPrimitive.Filter.Gain = FilterPrimitive.Filter.SuggestedGain(parsed_value);
                    Console.WriteLine("Onchange Event called by UpdateFilterValuefromTextbox B"); 

                    OnChangeEvent(new FilterEventArgs(Index, FilterPrimitive, FilterChangeType.Gain));
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
                    active_textbox.Text = FilterPrimitive.Filter.SuggestedQ(parsed_value).ToString("F3");
                    //if(active_textbox.Text.Substring(0,1) == ".") {
                    //    active_textbox.Text = "0" + active_textbox.Text;
                    //}
                    FilterPrimitive.Filter.QValue = FilterPrimitive.Filter.SuggestedQ(parsed_value);

                    Console.WriteLine("Onchange Event called by UpdateFilterValuefromTextbox C"); 
                    OnChangeEvent(new FilterEventArgs(Index, FilterPrimitive, FilterChangeType.QVal));
                }

            }

        }

        private void dropFilter_Enter(object sender, EventArgs e)
        {
            if (dropFilter.SelectedIndex != 0)
            {
                OnFocusEvent(new FilterEventArgs(Index, FilterPrimitive));
            }
        }

        
    }

    public class FilterEventArgs : EventArgs
    {
        private readonly int _index;
        private readonly DSP_Primitive_BiquadFilter _primitive;
        private readonly FilterChangeType _changeType;

        
        
        // Constructor.
        public FilterEventArgs(int in_index, DSP_Primitive_BiquadFilter in_primitive, FilterChangeType in_changetype = FilterChangeType.Full)
        {
            _index = in_index;
            _primitive = in_primitive;
            _changeType = in_changetype;
        }

        public int Index
        {
            get { return _index; }
        }

        public DSP_Primitive_BiquadFilter Primitive
        {
            get { return _primitive; }
        }

        public FilterChangeType ChangeType
        {
            get { return _changeType; }
        }
        // Properties.



    }

    public enum FilterChangeType
    {
        Full,
        Type,
        CenterFrequency,
        Gain,
        CenterFrequencyGain,
        QVal,
        Slope
    }
}
