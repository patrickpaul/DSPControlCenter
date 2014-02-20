using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{
    public class DSP_Primitive_Delay : DSP_Primitive, ICloneable
    {
        public double _Delay;
        public UInt32 Delay_Value, Bypassed_Value;
        public bool _Bypassed;

        public DSP_Primitive_Delay(string in_name, int in_channel, int in_positionA)
            : base(in_name, in_channel, in_positionA)
        {
            Bypassed = true;
            Delay = 0;
            this.Type = DSP_Primitive_Types.Delay; ;
            this.Num_Values = 2;
        }

        public DSP_Primitive_Delay(string in_name, int in_channel, int in_positionA = 0, double in_delay = 0, bool in_bypassed = true)
            : base(in_name, in_channel, in_positionA)
        {
            Bypassed = in_bypassed;
            Delay = in_delay;
            this.Type = DSP_Primitive_Types.Delay;
            this.Num_Values = 2;
        }

        public List<UInt32> Values
        {
            get
            {
                return new List<UInt32>(new UInt32[] { Bypassed_Value, Delay_Value });
            }
            set { }
        }

        public double Delay
        {
            get
            {
                return this._Delay;
            }
            set
            {
                this._Delay = value;

                this.Delay_Value = DSP_Math.double_to_MN(this._Delay, 16, 16);
            }
        }

        public bool Bypassed
        {
            get { return this._Bypassed; }
            set
            {
                this._Bypassed = value;
                this.Bypassed_Value = this._Bypassed ? (UInt32)0x00000001 : 0x00000000;

            }
        }

        public override string ToString()
        {
            if (Bypassed == true)
            {
                return "Bypassed";
            }
            else
            {
                return (_Delay *1000.0).ToString("N1") + "ms";
            }
        }

        public override void UpdateFromSettings(List<DSP_Setting> settingsList)
        {
            this.Bypassed = (settingsList[1].Value == 0x00000001);
            this.Delay = DSP_Math.MN_to_double_signed(settingsList[1].Value, 16, 16);
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset, this.Bypassed_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(this.Offset+1, this.Delay_Value));
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

            DSP_Primitive_Delay recastPrimitive = (DSP_Primitive_Delay)comparePrimitive;


            return (_Delay == recastPrimitive.Delay && _Bypassed == recastPrimitive.Bypassed);
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
