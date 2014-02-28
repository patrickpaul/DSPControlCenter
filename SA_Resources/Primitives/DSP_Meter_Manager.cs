using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    public class DSP_Meter_Manager
    {

        public List<DSP_Meter> METERS;

        public DSP_Meter_Manager()
        {
            METERS = new List<DSP_Meter>();
        }

        public void RegisterNewMeter(DSP_Meter in_meter)
        {
            METERS.Add(in_meter);
        }

        public int LookupIndex(DSP_Primitive_Types in_type, int in_ch, int in_positiona, int in_positionb = -1)
        {
            return METERS.FindIndex(
                prim => 
                    (prim.PrimitiveType == in_type) && 
                    (prim.Channel == in_ch) &&
                    (prim.PositionA == in_positiona) &&
                    (prim.PositionB == in_positionb)
                );

        }

        public DSP_Meter LookupMeter(DSP_Primitive_Types in_type, int in_ch, int in_positiona, int in_positionb = -1)
        {
            return METERS.Find(
                prim =>
                    (prim.PrimitiveType == in_type) &&
                    (prim.Channel == in_ch) &&
                    (prim.PositionA == in_positiona) &&
                    (prim.PositionB == in_positionb)
                );

        }


        public DSP_Meter LookupMeter(DSP_Primitive MatchingPrimitive)
        {
            return METERS.Find(
                prim =>
                    (prim.PrimitiveType == MatchingPrimitive.Type) &&
                    (prim.Channel == MatchingPrimitive.Channel) &&
                    (prim.PositionA == MatchingPrimitive.PositionA) &&
                    (prim.PositionB == MatchingPrimitive.PositionB)
                );

        }
    }
}
