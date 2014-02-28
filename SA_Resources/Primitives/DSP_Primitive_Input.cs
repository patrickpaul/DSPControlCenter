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
        public bool PhantomAvailable;
        public bool PhantomPower;
        public int PhantomOffset;
        public int NameOffset;

        public DSP_Primitive_Input(string in_name, int in_channel, int in_positionA, int in_nameOffset, int in_phantomOffset = -1)
            : base(in_name, in_channel, in_positionA)
        {
            InputName = "Input #" + (in_channel + 1).ToString("N0");

            if(in_phantomOffset < 0)
            {
                PhantomAvailable = false;
                PhantomPower = false;
            } else
            {
                PhantomOffset = in_phantomOffset;
            }

            NameOffset = in_nameOffset;

            InputType = InputType.Line;

            this.Type = DSP_Primitive_Types.Input;

            this.Num_Values = 1;
        }

        public DSP_Primitive_Input(string in_name, int in_channel, int in_positionA, int in_nameOffset, int in_phantomOffset, string _name, InputType _inputType, bool _phantomPower)
            : base(in_name, in_channel, in_positionA)
        {
            InputName = _name;

            if(in_phantomOffset < 0)
            {
                PhantomAvailable = false;
                PhantomPower = false;
            } else
            {
                PhantomAvailable = true;
                PhantomOffset = in_phantomOffset;
                PhantomPower = _phantomPower;
            }

            NameOffset = in_nameOffset;

            InputType = _inputType;

            this.Type = DSP_Primitive_Types.Input;

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

        public double Pregain
        {
            set { }
            get
            {
                if (this.InputType == InputType.Line)
                {
                    return 0;
                }
                else if (this.InputType == InputType.Microphone6)
                {
                    return 6;
                }
                else
                {
                    return 20;
                }
            }
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

        public override void UpdateFromReadValues(List<UInt32> valuesList)
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

            //PARENT_FORM.AddItemToQueue(new LiveQueueItem(PlainOffset, (uint)this.TypeToValue()));
            
        }

        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            Console.WriteLine("Input - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

            if (PARENT_FORM.LIVE_MODE)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {

            DSP_Primitive_Input RecastPrimitive = (DSP_Primitive_Input)comparePrimitive;

            for (int i = 0; i < this.Num_Values; i++)
            {
                if (this.Values[i] != RecastPrimitive.Values[i])
                {
                    Console.WriteLine("Value[" + i + "] " + this.Values[i].ToString("X") + " does not equal " + RecastPrimitive.Values[i].ToString("X"));
                    this.QueueChangeByOffset(PARENT_FORM, i);
                }
            }
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
