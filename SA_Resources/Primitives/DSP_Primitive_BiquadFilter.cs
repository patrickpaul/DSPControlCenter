using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Filters;
using SA_Resources.Forms;

namespace SA_Resources
{
    public class DSP_Primitive_BiquadFilter : DSP_Primitive, ICloneable
    {

        public FilterType FType;
        public bool Bypassed;
        public BiquadFilter _Filter;
        public UInt32 B0_Value, B1_Value, B2_Value, A1_Value, A2_Value;
        public UInt32 Package_Value, Package_Gain_Value, Package_Q_Value;
        public int Plainfilter_Offset;

        public DSP_Primitive_BiquadFilter(string in_name, int in_channel, int in_positionA, int _plainOffset)
            : base(in_name, in_channel, in_positionA)
        {
            this.Type = DSP_Primitive_Types.BiquadFilter; ;
            this.Num_Values = 5;
            Plainfilter_Offset = _plainOffset;
        }


        public DSP_Primitive_BiquadFilter(string in_name, int in_channel, int in_positionA, int _plainOffset, FilterType type, bool bypassed)
            : base(in_name, in_channel, in_positionA)
        {
            this.Type = DSP_Primitive_Types.BiquadFilter; ;
            this.Num_Values = 5;
            Plainfilter_Offset = _plainOffset;

            FType = type;
            Bypassed = bypassed;
            Filter = null;
        }


        public DSP_Primitive_BiquadFilter(string in_name, int in_channel, int in_positionA, int _plainOffset,FilterType type, bool bypassed, BiquadFilter in_filter)
            : base(in_name, in_channel, in_positionA)
        {
            this.Type = DSP_Primitive_Types.BiquadFilter; ;
            this.Num_Values = 5;
            Plainfilter_Offset = _plainOffset;

            FType = type;
            Bypassed = bypassed;
            Filter = in_filter;
        }

        public BiquadFilter Filter
        {
            get { return this._Filter; }
            set { this._Filter = value;}
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            Recalculate_Values();

            
            // MUTE THE CHANNEL OUTPUT GAIN TO REDUCE CRAZY NOISES..

            // We will use a primitive lookup to find output gain

            //PARENT_FORM.AddItemToQueue(new LiveQueueItem(36 + (CH_NUMBER - 1), 0x00000000));

            // TODO - Change this to use current program
            int OutputGain_Offset = PARENT_FORM.PRIMITIVE_PROGRAMS[0].LookupPrimitive(DSP_Primitive_Types.StandardGain, this.Channel, 4).Offset;

            UInt32 OutputGain_Value = ((DSP_Primitive_StandardGain)PARENT_FORM.PRIMITIVE_PROGRAMS[0].LookupPrimitive(DSP_Primitive_Types.StandardGain, this.Channel, 4)).Values[0];
            
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(OutputGain_Offset, 0x00000000));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset, B0_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 1, B1_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 2, B2_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 3, A1_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 4, A2_Value));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(OutputGain_Offset, OutputGain_Value));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Plainfilter_Offset, Package_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Plainfilter_Offset + 1, Package_Gain_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Plainfilter_Offset + 2, Package_Q_Value));
        }

        public void Recalculate_Values()
        {
            Package_Value = 0x00000000;
            Package_Gain_Value = 0x00000000;
            Package_Q_Value = 0x00000000;

            if (!this.Bypassed)
            {
                Package_Value = DSP_Math.filter_primitive_to_package(this);
                Package_Gain_Value = DSP_Math.double_to_MN(this.Filter.Gain, 8, 24);
                Package_Q_Value = DSP_Math.double_to_MN(this.Filter.QValue, 8, 24);
            }

            if (this.FType == FilterType.None || this.Bypassed)
            {
                B0_Value = 0x20000000;
                B1_Value = 0x00000000;
                B2_Value = 0x00000000;
                A1_Value = 0x00000000;
                A2_Value = 0x00000000;
            }
            else
            {
                B0_Value = DSP_Math.double_to_MN(this.Filter.B0, 3, 29);
                B1_Value = DSP_Math.double_to_MN(this.Filter.B1, 3, 29);
                B2_Value = DSP_Math.double_to_MN(this.Filter.B2, 3, 29);
                A1_Value = DSP_Math.double_to_MN(this.Filter.A1 * -1, 2, 30);
                A2_Value = DSP_Math.double_to_MN(this.Filter.A2 * -1, 2, 30);

            }
        }

        public override bool Equals(DSP_Primitive comparePrimitive)
        {
            if (comparePrimitive == null)
            {
                return false;
            }

            DSP_Primitive_BiquadFilter recastPrimitive = (DSP_Primitive_BiquadFilter)comparePrimitive;

            if (FType != recastPrimitive.FType)
            {
                return false;
            }

            if (Bypassed != recastPrimitive.Bypassed)
            {
                return false;
            }

            if (!Filter.IsEqual(recastPrimitive.Filter))
            {
                return false;
            }

            return true;
        }

        public List<UInt32> Values
        {
            get
            {
                Recalculate_Values();
                return new List<UInt32>(new UInt32[] { B0_Value, B1_Value, B2_Value, A1_Value, A2_Value });
            }
            set { }
        }

        public override void PrintValues()
        {
            for (int i = 0; i < this.Num_Values; i++)
            {
                Console.WriteLine("Value " + this.Values[i] + " at " + (this.Offset + i).ToString());
            }

        }

        public override void UpdateFromSettings(List<DSP_Setting> settingsList)
        {
            
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
