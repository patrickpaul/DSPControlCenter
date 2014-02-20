using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{
    public class DSP_Primitive_StandardGain : DSP_Primitive, ICloneable
    {
        public double _Gain;
        public UInt32 Gain_Value, Muted_Value;
        public bool _Muted; 

        public DSP_Primitive_StandardGain(string in_name, int in_channel, int in_positionA)
            : base(in_name,in_channel,in_positionA)
        {
            Muted = false;
            Gain = 0;
            this.Type = DSP_Primitive_Types.StandardGain;;
            this.Num_Values = 1;
        }

        public DSP_Primitive_StandardGain(string in_name, int in_channel, int in_positionA, double in_gain = 0, bool in_muted = false)
            : base(in_name, in_channel, in_positionA)
        {
            Muted = in_muted;
            Gain = in_gain;
            this.Type = DSP_Primitive_Types.StandardGain;
            this.Num_Values = 1;
        }

        public List<UInt32> Values
        {
            get
            {
                return new List<UInt32>(new UInt32[] { Gain_Value });
            }
            set {}
        } 

        public double Gain
        {
            get
            {
                return this._Gain;
            }
            set {
                this._Gain = value;

                if (this._Muted)
                {
                    this.Gain_Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(-100), 3, 29);
                }
                else
                {
                    this.Gain_Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(this._Gain), 9, 23);
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
                    this.Gain_Value = DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(this._Gain), 9, 23);
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

        public override void UpdateFromSettings(List<DSP_Setting> settingsList)
        {
            this.Gain = DSP_Math.value_to_gain(settingsList[0].Value);
            this.Muted = false;

            if(this.Gain < -90)
            {
                this.Gain = 0;
                this.Muted = true;
            }
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset, this.Gain_Value));
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

            DSP_Primitive_StandardGain recastPrimitive = (DSP_Primitive_StandardGain)comparePrimitive;

            // TODO - Do we lose precision when we test?
            return ((Muted == recastPrimitive.Muted) && (Gain == recastPrimitive.Gain));

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
