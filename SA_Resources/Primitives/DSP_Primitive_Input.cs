using System;
using System.Collections.Generic;
using System.Text;
using SA_Resources.SAForms;

namespace SA_Resources.DSP.Primitives
{
    public enum InputType
    {
        Line,
        Microphone6,
        Microphone20
    }

    public class DSP_Primitive_Input : DSP_Primitive, ICloneable
    {
        public InputType InputType;
        public string InputName;
        public bool PhantomAvailable;
        public bool PhantomPower;
        public int PregainOffset;
        public int NameOffset;
        public List<UInt32> NameValues;

        public DSP_Primitive_Input(string in_name, int in_channel, int in_positionA, int in_nameOffset, int in_pregainOffset = -1, bool in_phantomAvailable = false)
            : base(in_name, in_channel, in_positionA)
        {
            InputName = "Input #" + (in_channel + 1).ToString("N0");

            PhantomAvailable = in_phantomAvailable;
            PhantomPower = false;
            PregainOffset = in_pregainOffset;


            NameValues = new List<UInt32>();
            NameOffset = in_nameOffset;

            InputType = InputType.Line;

            this.Type = DSP_Primitive_Types.Input;

            this.Num_Values = 1;
        }

        public DSP_Primitive_Input(string in_name, int in_channel, int in_positionA, int in_nameOffset, int in_pregainOffset, string _name, InputType _inputType, bool in_phantomAvailable = false, bool _phantomPower = false)
            : base(in_name, in_channel, in_positionA)
        {
            InputName = _name;

            PhantomAvailable = in_phantomAvailable;
            PhantomPower = _phantomPower;

            PregainOffset = in_pregainOffset;

            NameValues = new List<UInt32>();

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

        public uint Pregain
        {
            set
            {
                switch (value)
                {
                    
                    case 6 :
                        this.InputType = InputType.Microphone6;
                        break;
                    case 20 :
                        this.InputType = InputType.Microphone20;
                        break;
                    default:
                        this.InputType = InputType.Line;
                    break;
                }
            }
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

        public void NameToValues()
        {
            List<UInt32> NameToValues = new List<UInt32>();

            string padded_name = this.InputName.PadRight(20, (char)0x00);

            this.NameValues.Clear();

            NameValues.Add(this.NameBlockToValue(padded_name.Substring(0, 4)));  //A
            NameValues.Add(this.NameBlockToValue(padded_name.Substring(4, 4)));  //B
            NameValues.Add(this.NameBlockToValue(padded_name.Substring(8, 4)));  //C
            NameValues.Add(this.NameBlockToValue(padded_name.Substring(12, 4))); //D
            NameValues.Add(this.NameBlockToValue(padded_name.Substring(16, 4))); //E
        }

        public UInt32 NameBlockToValue(string in_block)
        {

            UInt32 return_value = 0;

            // in_block MUST BE 4 CHARACTERS LONG OR SHORTER

            // abcd => (d)(c)(b)(a)

                return_value |= in_block[0];
                return_value <<= 8;

                return_value |= in_block[1];
                return_value <<= 8;
   
                return_value |= in_block[2];
                return_value <<= 8;

                return_value |= in_block[3];

            return return_value;
        }

        public void LoadNameFromValues(List<UInt32> valuesList)
        {
            StringBuilder sb = new StringBuilder();

            char char1, char2, char3, char4;

            for (int i = 0; i < 5; i++)
            {
                char4 = (char)(valuesList[i] & 0xFF);
                valuesList[i] >>= 8;

                char3 = (char)(valuesList[i] & 0xFF);
                valuesList[i] >>= 8;

                char2 = (char)(valuesList[i] & 0xFF);
                valuesList[i] >>= 8;

                char1 = (char)(valuesList[i] & 0xFF);

                if (char1 != 0x00)
                {
                    sb.Append(char1);
                }

                if (char2 != 0x00)
                {
                    sb.Append(char2);
                }

                if (char3 != 0x00)
                {
                    sb.Append(char3);
                }

                if (char4 != 0x00)
                {
                    sb.Append(char4);
                }

            }

            this.InputName = sb.ToString();
            //this.Name.Insert(0, (char) (valuesList[0] & 0xFF));
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

        public void LoadPregainFromValue(UInt32 Pregain_Value)
        {
            this.Pregain = (uint) Pregain_Value;

        }

        public void QueuePregain(MainForm_Template PARENT_FORM)
        {
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PregainOffset, this.Pregain));
        }


        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            this.NameToValues();

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset, NameValues[0]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset+1, NameValues[1]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset+2, NameValues[2]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset+3, NameValues[3]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset+4, NameValues[4]));

            if (PhantomAvailable)
            {
                this.QueuePhantom(PARENT_FORM);
            }

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PregainOffset, this.Pregain));

        }

        public void QueuePhantom(MainForm_Template PARENT_FORM)
        {
            UInt32 PhantomMask = 0;
            DSP_Primitive_Input InputPrimitive = null;

            for (int i = PARENT_FORM.GetNumInputChannels(); i > 0 ; i--)
            {
                PhantomMask <<= 1;

                InputPrimitive = (DSP_Primitive_Input)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.Input, i-1, 0);

                if (InputPrimitive.PhantomAvailable)
                {
                    if (InputPrimitive.PhantomPower)
                    {
                        PhantomMask |= 1;
                    }
                }
            }

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(569, PhantomMask));
            
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
