using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    public enum InputType
    {
        Line,
        Microphone20,
        Microphone40
    }
    
    public class InputConfig
    {
        public InputType Type;
        public string Name;
        public bool PhantomPower;

        public InputConfig(int index)
        {
            Name = "Input #" + (index + 1).ToString("N0");

            PhantomPower = false;
            Type = InputType.Line;
        }


        public InputConfig(string _name, InputType _inputType, bool _phantomPower)
        {
            Name = _name;
            PhantomPower = _phantomPower;
            Type = _inputType;
        }

        public UInt32 TypeToValue()
        {
            if(this.Type == InputType.Line)
            {
                return 0;
            }
            else if (this.Type == InputType.Microphone20)
            {
                return 1;
            } else
            {
                return 2;
            }

        }

        public override string ToString()
        {
            if (Type == InputType.Line)
            {
                return "Line Level\nPhantom Power: " + PhantomPower.ToString();
            }
            else if (Type == InputType.Microphone20)
            {
                return "Microphone +20dB\nPhantom Power: " + PhantomPower.ToString();
            }
            else
            {
                return "Microphone +40dB\nPhantom Power: " + PhantomPower.ToString();

            }
        }
    }

    
}