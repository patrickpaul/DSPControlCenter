using System;

namespace SA_Resources
{
    public class DSP_Setting
    {
        public DSP_Setting(uint new_index, string new_name, UInt32 default_value)
        {
            Index = new_index;
            Name = new_name;
            Value = default_value;
        }

        public uint Index;

        public string Name;

        public UInt32 Value;
    }
}
