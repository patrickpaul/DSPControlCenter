using System;
using SA_Resources.Filters;
using SA_Resources.Forms;

namespace SA_Resources
{
    public enum FilterType
    {
        None,
        BandPass,
        FirstOrderHighPass,
        SecondOrderHighPass,
        HighShelf,
        FirstOrderLowPass,
        SecondOrderLowPass,
        LowShelf,
        Notch,
        Peak
    }

    public class FilterConfig : ICloneable
    {
        public string[] _filterNames;
        public FilterType Type;
        public bool Bypassed;
        public BiquadFilter Filter;

        public FilterConfig()
        {
            Type = FilterType.None;
            Bypassed = true;
            Filter = null;
            _filterNames = new string[10];
        }
        
        
        public FilterConfig(FilterType type, bool bypassed)
        {
            Type = type;
            Bypassed = bypassed;
            Filter = null;
        }

        public FilterConfig(FilterType type, bool bypassed, BiquadFilter in_filter)
        {
            Type = type;
            Bypassed = bypassed;
            Filter = in_filter;
        }

        public bool IsEqual(FilterConfig compareFilter)
        {
            if(Type != compareFilter.Type)
            {
                return false;
            }    

            if(Bypassed != compareFilter.Bypassed)
            {
                return false;
            }

            if(!Filter.IsEqual(compareFilter.Filter))
            {
                return false;
            }

            return true;
        }

        public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX, int PLAINFILTER_INDEX, int CH_NUMBER)
        {
            UInt32 B0 = 0x20000000;
            UInt32 B1 = 0x00000000;
            UInt32 B2 = 0x00000000;
            UInt32 A1 = 0x00000000;
            UInt32 A2 = 0x00000000;

            UInt32 PACKAGE = 0x00000000;
            UInt32 PACKAGE_GAIN = 0x00000000;
            UInt32 PACKAGE_Q = 0x00000000;

            if(this.Type == FilterType.None)
            {
                B0 = 0x20000000;
                B1 = 0x00000000;
                B2 = 0x00000000;
                A1 = 0x00000000;
                A2 = 0x00000000;

                PACKAGE = 0x00000000;
                PACKAGE_GAIN = 0x00000000;
                PACKAGE_Q = 0x00000000;
            } else if(this.Bypassed)
            {
                B0 = 0x20000000;
                B1 = 0x00000000;
                B2 = 0x00000000;
                A1 = 0x00000000;
                A2 = 0x00000000;

                PACKAGE = DSP_Math.filter_to_package(this);
                PACKAGE_GAIN = DSP_Math.double_to_MN(this.Filter.Gain, 8, 24);
                PACKAGE_Q = DSP_Math.double_to_MN(this.Filter.QValue, 8, 24);
            } else
            {
                B0 = DSP_Math.double_to_MN(this.Filter.B0, 3, 29);
                B1 = DSP_Math.double_to_MN(this.Filter.B1, 3, 29);
                B2 = DSP_Math.double_to_MN(this.Filter.B2, 3, 29);
                A1 = DSP_Math.double_to_MN(this.Filter.A1 * -1, 2, 30);
                A2 = DSP_Math.double_to_MN(this.Filter.A2 * -1, 2, 30);

                PACKAGE = DSP_Math.filter_to_package(this);
                PACKAGE_GAIN = DSP_Math.double_to_MN(this.Filter.Gain, 8, 24);
                PACKAGE_Q = DSP_Math.double_to_MN(this.Filter.QValue, 8, 24);
            }

            
            // MUTE THE CHANNEL OUTPUT GAIN TO REDUCE CRAZY NOISES..

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(36 + (CH_NUMBER - 1), 0x00000000));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, B0));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 1, B1));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 2, B2));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 3, A1));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 4, A2));

            UInt32 gain_val = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].gains[CH_NUMBER - 1][3].Gain), 3, 29);
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(36 + (CH_NUMBER - 1), gain_val));

            


            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PLAINFILTER_INDEX, PACKAGE));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PLAINFILTER_INDEX + 1, PACKAGE_GAIN));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PLAINFILTER_INDEX + 2, PACKAGE_Q));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
