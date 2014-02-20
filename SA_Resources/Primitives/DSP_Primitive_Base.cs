using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
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

        public abstract void UpdateFromSettings(List<DSP_Setting> settingsList);

        public abstract void PrintValues();

        public abstract bool Equals(DSP_Primitive comparePrimitive);

        public abstract void QueueChange(MainForm_Template PARENT_FORM);

        public abstract object Clone();
    }
}
