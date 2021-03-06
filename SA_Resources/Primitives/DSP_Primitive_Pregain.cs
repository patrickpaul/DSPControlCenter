﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA_Resources.DSP;
using SA_Resources.SAForms;
using SA_Resources.USB;

namespace SA_Resources.DSP.Primitives
{

    public class DSP_Primitive_Pregain : DSP_Primitive, ICloneable
    {
        public double _Gain;

        public UInt32 Gain_Value, Muted_Value;
        public bool _Muted;
        public UInt32 _Meter;
        public int _Pregain;

        public DSP_Primitive_Pregain(string in_name, int in_channel, int in_positionA)
            : base(in_name,in_channel,in_positionA)
        {
            Muted = false;
            Gain = 0;
            this.Type = DSP_Primitive_Type.Pregain;

            this.Pregain = 0;

            this.Num_Values = 2;
        }

        public DSP_Primitive_Pregain(string in_name, int in_channel, int in_positionA, UInt32 in_meter)
            : base(in_name, in_channel, in_positionA)
        {
            Muted = false;
            Gain = 0;
            this.Type = DSP_Primitive_Type.Pregain;
            this.Num_Values = 2;
            this.Meter = in_meter;
            this.Pregain = 0;
        }

        public DSP_Primitive_Pregain(string in_name, int in_channel, int in_positionA, UInt32 in_meter, double in_gain = 0, bool in_muted = false)
            : base(in_name, in_channel, in_positionA)
        {
            Muted = in_muted;
            Gain = in_gain;
            this.Type = DSP_Primitive_Type.Pregain;
            this.Num_Values = 2;
            this.Meter = in_meter;
        }

        public List<UInt32> Values
        {
            get
            {
                return new List<UInt32>(new UInt32[] { Gain_Value, Muted_Value });
            }
            set {}
        }

        public UInt32 Meter
        {
            get { return this._Meter; }
            set { this._Meter = value; }
        }

        public int Pregain
        {
            get { return this._Pregain; }
            set
            {
                this._Pregain = value;
                this.Gain_Value = DSP_Math.double_to_MN(this._Gain + this.Pregain, 9, 23);
            }
        }
        public double Gain
        {
            get
            {
                return this._Gain;
            }
            set {
                this._Gain = value;

                this.Gain_Value = DSP_Math.double_to_MN(this._Gain + this.Pregain, 9,23);
            }
        }

        public bool Muted
        {
            get
            {
                return this._Muted;
            }
            set
            {
                this._Muted = value;
                if (this._Muted)
                {
                    this.Muted_Value = 0x00000001;
                }
                else
                {
                    this.Muted_Value = 0x00000000;
                }
            }
        }

        public override string ToString()
        {
            if (Muted == true)
            {
                return "Muted (" + Gain.ToString("N1") + "dB)";
            }
            else
            {
                return Gain.ToString("N1") + "dB";
            }
        }

        public override void UpdateFromReadValues(List<UInt32> valuesList)
        {
            this.Gain = DSP_Math.Value_To_Pregain(valuesList[0]);
            this.Muted = (valuesList[1] == 0x0000001);
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            if (PARENT_FORM.LIVE_MODE)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset, this.Gain_Value));
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset+1, this.Muted_Value));
            }
        }


        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            
            if (PARENT_FORM.LIVE_MODE)
            {
                Debug.WriteLine("StandardGain - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {

            DSP_Primitive_Pregain RecastPrimitive = (DSP_Primitive_Pregain)comparePrimitive;

            for (int i = 0; i < this.Num_Values; i++)
            {
                if (this.Values[i] != RecastPrimitive.Values[i])
                {
                    Debug.WriteLine("Value[" + i + "] " + this.Values[i].ToString("X") + " does not equal " + RecastPrimitive.Values[i].ToString("X"));
                    this.QueueChangeByOffset(PARENT_FORM, i);
                }
            }
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
            if (comparePrimitive == null)
            {
                return false;
            }

            DSP_Primitive_Pregain recastPrimitive = (DSP_Primitive_Pregain)comparePrimitive;

            // TODO - Do we lose precision when we test?
            return ((Muted == recastPrimitive.Muted) && (Gain == recastPrimitive.Gain));

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
