using System;
using System.Collections.Generic;
using SA_Resources.SAForms;

namespace SA_Resources.DSP.Primitives
{
    public abstract class DSP_Primitive
    {
        public int Offset;
        public int Num_Values;
        public int Channel;
        public int PositionA;
        public int PositionB;
        public string Name;
        public DSP_Primitive_Types Type = DSP_Primitive_Types.Unknown;

        public DSP_Primitive(string in_name, int in_channel, int in_positionA = 0, int in_positionB = -1)
        {
            this.Name = in_name;
            this.Channel = in_channel;
            this.PositionA = in_positionA;
            this.PositionB = in_positionB;
            this.Num_Values = 1;
        }

        public void SetOffset(int new_offset)
        {
            this.Offset = new_offset;
        }

        public abstract void UpdateFromReadValues(List<UInt32> valuesList);

        

        public abstract void PrintValues();

        public abstract void QueueChange(MainForm_Template PARENT_FORM);
        public abstract void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset);
        public abstract void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive);

        public abstract bool Equals(DSP_Primitive comparePrimitive);
        public abstract object Clone();
    }
}
