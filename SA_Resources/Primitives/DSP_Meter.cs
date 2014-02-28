using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{
    public class DSP_Meter
    {
        public int Channel;
        public int PositionA;
        public int PositionB;
        public string Name;
        public UInt32 Address;
        public DSP_Primitive_Types PrimitiveType = DSP_Primitive_Types.Unknown;


        public DSP_Meter(UInt32 in_address, DSP_Primitive_Types in_type, int in_channel, int in_positionA = 0, int in_positionB = -1)
        {
            this.Address = in_address;
            this.PrimitiveType = in_type;
            this.Channel = in_channel;
            this.PositionA = in_positionA;
            this.PositionB = in_positionB;
        }
    }
}
