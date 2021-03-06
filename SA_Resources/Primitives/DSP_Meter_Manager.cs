﻿using System.Collections.Generic;

namespace SA_Resources.DSP.Primitives
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

        public void PrintIndexUsage()
        {
            

        }

        public int LookupIndex(DSP_Primitive_Type inType, int in_ch, int in_positiona, int in_positionb = -1)
        {
            return METERS.FindIndex(
                prim => 
                    (prim.PrimitiveType == inType) && 
                    (prim.Channel == in_ch) &&
                    (prim.PositionA == in_positiona) &&
                    (prim.PositionB == in_positionb)
                );

        }

        public DSP_Meter LookupMeter(DSP_Primitive_Type inType, int in_ch, int in_positiona, int in_positionb = -1)
        {
            return METERS.Find(
                prim =>
                    (prim.PrimitiveType == inType) &&
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
