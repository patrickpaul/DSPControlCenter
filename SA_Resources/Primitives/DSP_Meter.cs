using System;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Meter
    {
        public int Channel;
        public int PositionA;
        public int PositionB;
        public string Name;
        public UInt32 Address;
        public DSP_Primitive_Type PrimitiveType = DSP_Primitive_Type.Unknown;


        public DSP_Meter(UInt32 in_address, DSP_Primitive_Type inType, int in_channel, int in_positionA = 0, int in_positionB = -1)
        {
            this.Address = in_address;
            this.PrimitiveType = inType;
            this.Channel = in_channel;
            this.PositionA = in_positionA;
            this.PositionB = in_positionB;
        }
    }
}
