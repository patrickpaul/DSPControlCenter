using System;

namespace SA_Resources.Configurations
{
    public class OutputConfig : ICloneable
    {
        public string Name;

        public OutputConfig(int index)
        {
            Name = "Local Output #" + (index + 1).ToString("N0");

        }


        public OutputConfig(string _name)
        {
            Name = _name;
        }

        public bool Equals(OutputConfig compareConfig)
        {
            return (Name == compareConfig.Name);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}