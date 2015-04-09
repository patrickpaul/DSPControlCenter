using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA_Resources.SAForms;
using System.Text;
using SA_Resources.USB;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Primitive_Output : DSP_Primitive, ICloneable
    {
        public string OutputName;
        public List<UInt32> NameValues;
        public int NameOffset;
        public DSP_Primitive_Output(string in_name, int in_channel, int in_positionA, int in_nameOffset)
            : base(in_name, in_channel, in_positionA)
        {
            OutputName = "Output #" + (in_channel + 1).ToString("N0");

            this.Type = DSP_Primitive_Type.Output;

            NameValues = new List<UInt32>();
            NameOffset = in_nameOffset; 
            
            this.Num_Values = 1;
        }

        public DSP_Primitive_Output(string in_name, int in_channel, int in_positionA, int in_nameOffset, string _name)
            : base(in_name, in_channel, in_positionA)
        {
            OutputName = _name;

            this.Type = DSP_Primitive_Type.Output;

            NameValues = new List<UInt32>();
            NameOffset = in_nameOffset;

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

        public void NameToValues()
        {
            List<UInt32> NameToValues = new List<UInt32>();

            string padded_name = this.OutputName.PadRight(20, (char)0x00);

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

            this.OutputName = sb.ToString();
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

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            if (PARENT_FORM.LIVE_MODE)
            {
                // All we have is the name
                //PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset, this.Gain_Value));
            }

            this.NameToValues();
            
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset, NameValues[0]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset + 1, NameValues[1]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset + 2, NameValues[2]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset + 3, NameValues[3]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(NameOffset + 4, NameValues[4]));
        }


        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            
            if (PARENT_FORM.LIVE_MODE)
            {
                Debug.WriteLine("Output - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

                //PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {

            DSP_Primitive_StandardGain RecastPrimitive = (DSP_Primitive_StandardGain)comparePrimitive;

            /*
            for (int i = 0; i < this.Num_Values; i++)
            {
                if (this.Values[i] != RecastPrimitive.Values[i])
                {
                    Debug.WriteLine("Value[" + i + "] " + this.Values[i].ToString("X") + " does not equal " + RecastPrimitive.Values[i].ToString("X"));
                    this.QueueChangeByOffset(PARENT_FORM, i);
                }
            }
             * */
        }

        public override void PrintValues()
        {
            for (int i = 0; i < this.Num_Values; i++)
            {
                Debug.WriteLine("Value " + this.Values[i] + " at " + (this.Offset + i).ToString());
            }

        }

        public override bool Equals(DSP_Primitive comparePrimitive)
        {
            DSP_Primitive_Output recastPrimitive = (DSP_Primitive_Output)comparePrimitive;

            return (Name == recastPrimitive.Name);

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    
}
