using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{
    public class DSP_Primitive_Input : DSP_Primitive, ICloneable
    {
        public InputType InputType;
        public string InputName;
        public bool PhantomPower;
        public int PlainOffset;

        public DSP_Primitive_Input(string in_name, int in_channel, int in_positionA, int _plainOffset)
            : base(in_name, in_channel, in_positionA)
        {
            InputName = "Input #" + (in_channel + 1).ToString("N0");

            PhantomPower = false;
            InputType = InputType.Line;

            PlainOffset = _plainOffset;

            this.Type = DSP_Primitive_Types.Input ;

            this.Num_Values = 1;
        }

        public DSP_Primitive_Input(string in_name, int in_channel, int in_positionA,  int _plainOffset, string _name, InputType _inputType, bool _phantomPower)
            : base(in_name, in_channel, in_positionA)
        {
            InputName = _name;
            PhantomPower = _phantomPower;
            InputType = _inputType;

            PlainOffset = _plainOffset;

            this.Type = DSP_Primitive_Types.StandardGain;
            this.Num_Values = 1;
        }

        public List<UInt32> Values
        {
            get
            {
                return new List<UInt32>(new UInt32[] { 0 });
            }
            set { }
        }

        public UInt32 TypeToValue()
        {
            if (this.InputType == InputType.Line)
            {
                return 0;
            }
            else if (this.InputType == InputType.Microphone6)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }

        public override string ToString()
        {
            if (InputType == InputType.Line)
            {
                return "Line Level\nPhantom Power: " + PhantomPower.ToString();
            }
            else if (InputType == InputType.Microphone6)
            {
                return "Microphone +6dB\nPhantom Power: " + PhantomPower.ToString();
            }
            else
            {
                return "Microphone +20dB\nPhantom Power: " + PhantomPower.ToString();

            }
        }

        public override void UpdateFromSettings(List<DSP_Setting> settingsList)
        {
            /*
            this.Gain = DSP_Math.value_to_gain(settingsList[0].Value);
            this.Muted = false;

            if (this.Gain < -90)
            {
                this.Gain = 0;
                this.Muted = true;
            }
             * */
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            // Look up parent program
            /*
            UInt32 new_val =
                    DSP_Math.double_to_MN(
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_NUMBER - 1] +
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].gains[CH_NUMBER - 1][0].Gain, 9, 23);
            
            PARENT_FORM.AddItemToQueue(new LiveQueueItem((0 + CH_NUMBER - 1), new_val));
             *  * */

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PlainOffset, (uint)this.TypeToValue()));
            
        }

        public override void PrintValues()
        {
            for (int i = 0; i < this.Num_Values; i++)
            {
                Console.WriteLine("Value " + this.Values[i] + " at " + (this.Offset + i).ToString());
            }

        }

        public override bool Equals(DSP_Primitive comparePrimitive)
        {
            DSP_Primitive_Input recastPrimitive = (DSP_Primitive_Input)comparePrimitive;

            return (Name == recastPrimitive.Name && InputType == recastPrimitive.InputType && PhantomPower == recastPrimitive.PhantomPower);

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    
}
