using System;
using System.Collections.Generic;
using SA_Resources.DSP;
using SA_Resources.SAForms;

namespace SA_Resources.DSP.Primitives
{
    public enum CompressorType
    {
        Compressor,
        Limiter
    }

    public class DSP_Primitive_Compressor : DSP_Primitive, ICloneable
    {

        public const int THRESHOLD_OFFSET = 0;
        public const int SOFTKNEE_OFFSET = 1;
        public const int RATIO_OFFSET = 2;
        public const int ATTACK_OFFSET = 3;
        public const int RELEASE_OFFSET = 4;
        public const int BYPASSED_OFFSET = 5;
        
        public double _Threshold, _Ratio, _Attack, _Release;
        public bool _SoftKnee, _Bypassed;
        public CompressorType _Type;
        public int NUM_DSP_SETTINGS = 6;
        public UInt32 Threshold_Value, Ratio_Value, Attack_Value, Release_Value, SoftKnee_Value, Bypassed_Value, Type_Value;

        public UInt16 METER1, METER2;


        public DSP_Primitive_Compressor(string in_name, int in_channel, int in_positionA, DSP_Primitive_Types in_type = DSP_Primitive_Types.Compressor)
            : base(in_name,in_channel,in_positionA)
        {
            Threshold = -20;
            Ratio = 100;
            Attack = 0.01; // 10ms
            Release = 0.01; // 10ms
            SoftKnee = false;
            Bypassed = true;
            this.Type = in_type;
            this.Num_Values = 6;
        }



        public DSP_Primitive_Compressor(string in_name, int in_channel, int in_positionA, double th, double rat, double a, double rel, bool sk, bool bypassed)
            : base(in_name, in_channel, in_positionA)
        {
            Threshold = th;
            Ratio = rat;
            Attack = a;
            Release = rel;
            SoftKnee = sk;
            Bypassed = bypassed;
            this.Type = DSP_Primitive_Types.Compressor;
            this.Num_Values = 6;
        }

        public override void UpdateFromReadValues(List<UInt32> valuesList)
        {
            this.Threshold = DSP_Math.MN_to_double_signed(valuesList[0], 9, 23);
            this.SoftKnee = (valuesList[1] == 0x03000000);
            this.Ratio = DSP_Math.value_to_comp_ratio(valuesList[2]);
            this.Attack = DSP_Math.value_to_comp_attack(valuesList[3]);
            this.Release = DSP_Math.value_to_comp_release(valuesList[4]);
            this.Bypassed = (valuesList[5] == 0x00000001);

        }

        

        public List<UInt32> Values
        {
            get
            {
                return new List<UInt32>(new UInt32[] { Threshold_Value, SoftKnee_Value, Ratio_Value,Attack_Value,Release_Value,Bypassed_Value });
            }
            set { }
        } 

        public double Threshold
        {
            get { return this._Threshold; }
            set
            {
                this._Threshold = value;
                this.Threshold_Value = DSP_Math.double_to_MN(value, 9, 23);
            }

        }

        public double Ratio
        {
            get { return this._Ratio; }
            set
            {
                this._Ratio = value;
                this.Ratio_Value = DSP_Math.comp_ratio_to_value(value);
            }

        }

        public double Attack
        {
            get { return this._Attack; }
            set
            {
                this._Attack = value;
                this.Attack_Value = DSP_Math.comp_attack_to_value(value);
            }

        }

        public double Release
        {
            get { return this._Release; }
            set
            {
                this._Release = value;
                this.Release_Value = DSP_Math.comp_release_to_value(value);
            }

        }

        public bool SoftKnee
        {
            get { return this._SoftKnee; }
            set
            {
                this._SoftKnee = (bool)value;
                this.SoftKnee_Value = (_SoftKnee) ? (UInt32)0x03000000 : (UInt32)0x00000000;
            }
        }

        public bool Bypassed
        {
            get { return this._Bypassed; }
            set
            {
                this._Bypassed = (bool)value;
                this.Bypassed_Value = (_Bypassed) ? (UInt32)0x00000001 : (UInt32)0x00000000;
            }

        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            for (int i = 0; i < this.Num_Values; i++)
            {
                Console.WriteLine("Compressor - QueueChange - Sending " + this.Values[i].ToString("X") + " to offset " + (Offset + i));
            }

            if (PARENT_FORM.LIVE_MODE)
            {
                for (int i = 0; i < this.Num_Values; i++)
                {
                    PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + i, this.Values[i]));
                }
            }
        }


        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM,int const_offset)
        {
            Console.WriteLine("Compressor - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

            if (PARENT_FORM.LIVE_MODE)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {

            DSP_Primitive_Compressor RecastPrimitive = (DSP_Primitive_Compressor)comparePrimitive;

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
            if (comparePrimitive == null)
            {
                return false;
            }

            DSP_Primitive_Compressor recastPrimitive = (DSP_Primitive_Compressor)comparePrimitive;

            if (_Threshold != recastPrimitive.Threshold || _Ratio != recastPrimitive.Ratio || _Attack != recastPrimitive.Attack)
            {
                return false;
            }

            if (_Release != recastPrimitive.Release || _SoftKnee != recastPrimitive.SoftKnee || _Bypassed != recastPrimitive.Bypassed)
            {
                return false;
            }

            if (_Type != recastPrimitive._Type)
            {
                return false;
            }

            return true;

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
