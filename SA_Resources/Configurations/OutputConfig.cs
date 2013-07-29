using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    public class OutputConfig
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
    }
}