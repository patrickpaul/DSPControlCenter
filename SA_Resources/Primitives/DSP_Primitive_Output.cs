using System;
using System.Collections.Generic;
using SA_Resources.SAForms;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Primitive_Output : DSP_Primitive, ICloneable
    {
        public string OutputName;

        public DSP_Primitive_Output(string in_name, int in_channel, int in_positionA, int in_nameOffset)
            : base(in_name, in_channel, in_positionA)
        {
            OutputName = "Output #" + (in_channel + 1).ToString("N0");

            this.Type = DSP_Primitive_Types.Output;

            this.Num_Values = 1;
        }

        public DSP_Primitive_Output(string in_name, int in_channel, int in_positionA, string _name)
            : base(in_name, in_channel, in_positionA)
        {
            OutputName = _name;

            this.Type = DSP_Primitive_Types.Output;

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
        }


        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            Console.WriteLine("Output - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

            if (PARENT_FORM.LIVE_MODE)
            {
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
                    Console.WriteLine("Value[" + i + "] " + this.Values[i].ToString("X") + " does not equal " + RecastPrimitive.Values[i].ToString("X"));
                    this.QueueChangeByOffset(PARENT_FORM, i);
                }
            }
             * */
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
            DSP_Primitive_Output recastPrimitive = (DSP_Primitive_Output)comparePrimitive;

            return (Name == recastPrimitive.Name);

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    
}
