using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{
    public enum InputType
    {
        Line,
        Microphone20,
        Microphone40
    }
    
    public class InputConfig : ICloneable
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

        public uint PhantomAsInt()
        {
            return PhantomPower == true ? (uint)1 : (uint)0;
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

        public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX, double CH_NUMBER)
        {
            /*
            UInt32 new_val =
                    DSP_Math.double_to_MN(
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_NUMBER - 1] +
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].gains[CH_NUMBER - 1][0].Gain, 9, 23);

            PARENT_FORM.AddItemToQueue(new LiveQueueItem((0 + CH_NUMBER - 1), new_val));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem((412 + CH_NUMBER - 1), (uint)dropInputType.SelectedIndex));
             * */
        }

        public bool Equals(InputConfig compareInput)
        {
            return (Name == compareInput.Name && Type == compareInput.Type && PhantomPower == compareInput.PhantomPower);

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    
}