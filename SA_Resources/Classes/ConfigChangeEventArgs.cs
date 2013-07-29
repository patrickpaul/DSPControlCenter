using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    public class ConfigChangeEventArgs
    {
        public ConfigType Type;
        public double Value;
        public int ChIndex;
        public FilterConfig FilterConfig;

        public ConfigChangeEventArgs(ConfigType setting_type, double setting_value, int setting_ch_index = 1)
        {
            this.Type = setting_type;
            this.Value = setting_value;
            this.ChIndex = setting_ch_index;
        }

        public ConfigChangeEventArgs(ConfigType setting_type, FilterConfig fVals, int setting_ch_index = 1)
        {
            this.Type = setting_type;
            this.FilterConfig = fVals;
            this.ChIndex = setting_ch_index;
        }

    }

    public delegate void ConfigChangeEventHandler(object sender, ConfigChangeEventArgs e);
}
