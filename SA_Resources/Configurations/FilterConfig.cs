using System;
using SA_Resources.Filters;

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

    public class FilterConfig
    {
        public string[] _filterNames;
        
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

        public FilterType Type;
        public bool Bypassed;
        public BiquadFilter Filter;

    }
}
