using System;
using System.Collections.Generic;
using SA_Resources.DSP;
using SA_Resources.SAForms;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Primitive_MixerCrosspoint : DSP_Primitive, ICloneable
    {
        public double _Gain;
        public UInt32 Gain_Value, Muted_Value;
        public bool _Muted;
        public double _Pregain;

        public DSP_Primitive_MixerCrosspoint(string in_name, int in_channel, int in_positionA)
            : base(in_name, in_channel, in_positionA)
        {
            Muted = false;
            Gain = 0;
            _Pregain = 0;

            this.Type = DSP_Primitive_Types.MixerCrosspoint; ;
            this.Num_Values = 1;
        }

        public DSP_Primitive_MixerCrosspoint(string in_name, int in_channel, int in_positionA, double in_gain = 0, bool in_muted = false)
            : base(in_name, in_channel, in_positionA)
        {
            Muted = (in_gain == -100) ? true : in_muted;
            Gain = in_gain;
            _Pregain = 0;
            this.Type = DSP_Primitive_Types.MixerCrosspoint;
            this.Num_Values = 1;
        }

        public List<UInt32> Values
        {
            get
            {
                return new List<UInt32>(new UInt32[] { Gain_Value });
            }
            set { }
        }

        public double Gain
        {
            get
            {
                return this._Gain + _Pregain;
            }
            set
            {
                this._Gain = value;

                if (this._Muted)
                {
                    this.Gain_Value = 0x00000000; // To prevent any possible cross-channel issues we do zero instead of -100dB (0x000014F8)
                }
                else
                {
                    this.Gain_Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(this._Gain), 3, 29);
                }
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
                    this.Gain_Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(-100), 3, 29);
                }
                else
                {
                    this.Gain_Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(this._Gain), 3, 29);
                }
            }
        }

        public override string ToString()
        {
            if (Muted == true)
            {
                return "Muted";
            }
            else
            {
                return Gain.ToString("N1") + "dB";
            }
        }

        public override void UpdateFromReadValues(List<UInt32> valuesList)
        {
            if(valuesList.Count > this.Num_Values)
            {
                
            }
            this.Gain = DSP_Math.Value_To_StandardGain(valuesList[0]);
            this.Muted = false;

            if (this.Gain < -90)
            {
                this.Gain = 0;
                this.Muted = true;
            }
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            if (PARENT_FORM.LIVE_MODE)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset, this.Gain_Value));
            }
        }


        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            Console.WriteLine("MixerCrosspoint - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

            if (PARENT_FORM.LIVE_MODE)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {

            DSP_Primitive_MixerCrosspoint RecastPrimitive = (DSP_Primitive_MixerCrosspoint)comparePrimitive;

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

            DSP_Primitive_MixerCrosspoint recastPrimitive = (DSP_Primitive_MixerCrosspoint)comparePrimitive;

            // TODO - Do we lose precision when we test?
            return ((Muted == recastPrimitive.Muted) && (Gain == recastPrimitive.Gain));

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
