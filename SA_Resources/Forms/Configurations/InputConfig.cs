using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    public enum InputType
    {
        Line,
        Microphone
    }
    
    public class InputConfig
    {
        public InputType Type;
        public string Name;
        public bool PhantomPower;

        public InputConfig(int index)
        {
            Name = "Local Input #" + (index + 1).ToString("N0");

            PhantomPower = false;
            Type = InputType.Line;
        }


        public InputConfig(string _name, InputType _inputType, bool _phantomPower)
        {
            Name = _name;
            PhantomPower = _phantomPower;
            Type = _inputType;
        }
    }

    
}