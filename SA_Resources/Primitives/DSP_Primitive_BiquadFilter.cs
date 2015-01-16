using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA_Resources.DSP;
using SA_Resources.DSP.Filters;
using SA_Resources.SAForms;
using SA_Resources.USB;

namespace SA_Resources.DSP.Primitives
{
    public enum FilterType
    {
        None,
        BandPass,
        FirstOrderHighPass,
        SecondOrderHighPass,
        HighShelf,
        FirstOrderLowPass,
        SecondOrderLowPass,
        LowShelf,
        Notch,
        Peak
    }

    public class DSP_Primitive_BiquadFilter : DSP_Primitive, ICloneable
    {

        public FilterType FType;
        public bool Bypassed;
        public BiquadFilter _Filter;
        public UInt32 B0_Value, B1_Value, B2_Value, A1_Value, A2_Value;
        public UInt32 Package_Value, Package_Gain_Value, Package_Q_Value;
        public int Plainfilter_Offset;

        public bool _premix = false;

        public DSP_Primitive_BiquadFilter(string in_name, int in_channel, int in_positionA, int _plainOffset)
            : base(in_name, in_channel, in_positionA)
        {
            this.Type = DSP_Primitive_Types.BiquadFilter;
            this.Num_Values = 5;
            Plainfilter_Offset = _plainOffset;
            Bypassed = false;
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
            set {
                this._Filter = value;
                Recalculate_Values();
            }
        }

        public bool IsPremix
        {
            get { return this._premix; }
            set { this._premix = value; }
        }

        public bool HasNoFilter
        {
            get { return (this.FType == FilterType.None || this.Filter == null); }

            set {}
        }

        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            
            if (PARENT_FORM.LIVE_MODE)
            {
                Debug.WriteLine("BiquadFilter - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X") + " to offset " + (Offset + const_offset));

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {
            // TODO - need plainfilter stuff
            DSP_Primitive_BiquadFilter RecastPrimitive = (DSP_Primitive_BiquadFilter)comparePrimitive;

            for (int i = 0; i < this.Num_Values; i++)
            {
                if (this.Values[i] != RecastPrimitive.Values[i])
                {
                    Debug.WriteLine("Value[" + i + "] " + this.Values[i].ToString("X") + " does not equal " + RecastPrimitive.Values[i].ToString("X"));
                    this.QueueChangeByOffset(PARENT_FORM, i);
                }
            }
        }

        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            Recalculate_Values();
            // MUTE THE CHANNEL OUTPUT GAIN TO REDUCE CRAZY NOISES..

            int Gain_Mute_Offset = 0;
            UInt32 Gain_Mute_Value = 0;

            if (IsPremix)
            {
                Gain_Mute_Offset = PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.StandardGain, this.Channel, 1).Offset + 1;

                Gain_Mute_Value = ((DSP_Primitive_StandardGain)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.StandardGain, this.Channel, 1)).Values[1];
            
            }
            else
            {
                Gain_Mute_Offset = PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.StandardGain, this.Channel, 3).Offset + 1;

                Gain_Mute_Value = ((DSP_Primitive_StandardGain)PARENT_FORM.DSP_PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].LookupPrimitive(DSP_Primitive_Types.StandardGain, this.Channel, 3)).Values[1];
            
            }
            
            // Mute outputs
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Gain_Mute_Offset, 0x00000001));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset, B0_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 1, B1_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 2, B2_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 3, A1_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + 4, A2_Value));

            // Unmute outputs
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(Gain_Mute_Offset, Gain_Mute_Value));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(512 + Plainfilter_Offset, Package_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(576 + Plainfilter_Offset, Package_Gain_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(640 + Plainfilter_Offset, Package_Q_Value));
        }

        public void Recalculate_Values()
        {
            try
            {
                Package_Value = 0x00000000;
                Package_Gain_Value = 0x00000000;
                Package_Q_Value = 0x00000000;

                if (this.Filter != null)
                {
                    Package_Value = this.ToPackage();
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
                    A1_Value = DSP_Math.double_to_MN(this.Filter.A1*-1, 2, 30);
                    A2_Value = DSP_Math.double_to_MN(this.Filter.A2*-1, 2, 30);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in DSP_Primitive_BiquadFilter.Recalculate_Values]: " + ex.Message);
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

        public UInt32 ToPackage()
        {
            UInt32 return_int = 0x00;

            return_int |= Convert.ToUInt32(this.Bypassed);

            return_int <<= 4;

            if (this.Filter != null)
            {
                return_int |= (uint)this.Filter.FilterType;
            }
            else
            {
                return_int |= 0;

            }
            

            return_int <<= 3;

            if (this.FType == FilterType.SecondOrderHighPass || this.FType == FilterType.SecondOrderLowPass)
            {
                return_int |= 0x01;
            }
            else
            {
                return_int |= 0x00;
            }


            return_int <<= 23;

            return_int |= (uint) this.Filter.CenterFrequency;

            return return_int;
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
                Debug.WriteLine("Value " + this.Values[i] + " at " + (this.Offset + i).ToString());
            }

        }

        public override void UpdateFromReadValues(List<UInt32> valuesList)
        {
            try
            {
                uint package_type = 0;

                UInt32 configuredCheck = 0;
                configuredCheck = valuesList[0];
                configuredCheck >>= 26;

                package_type = configuredCheck & 0xF;

                if (package_type == 0)
                {
                    this.Bypassed = true;
                    this.Filter = null;
                    this.FType = FilterType.None;

                }
                else
                {
                    this.Bypassed = ((valuesList[0] & 0x40000000) != 0);



                    this.Filter = DSP_Math.rebuild_filter(valuesList[0], valuesList[1], valuesList[2]);



                    switch ((int) this.Filter.FilterType)
                    {
                        case 0:
                            this.FType = FilterType.None;
                            break;
                        case 1:
                            this.FType = FilterType.FirstOrderLowPass;
                            break;
                        case 2:
                            this.FType = FilterType.FirstOrderHighPass;
                            break;
                        case 3:
                            this.FType = FilterType.LowShelf;
                            break;
                        case 4:
                            this.FType = FilterType.HighShelf;
                            break;
                        case 5:
                            this.FType = FilterType.Peak;
                            break;
                        case 6:
                            this.FType = FilterType.Notch;
                            break;
                        case 7:
                            this.FType = FilterType.SecondOrderLowPass;
                            break;
                        case 8:
                            this.FType = FilterType.SecondOrderHighPass;
                            break;
                        default:
                            this.FType = FilterType.None;
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in BiquadFilter.UpdateFromReadValues: " + ex.Message);
            }


        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
