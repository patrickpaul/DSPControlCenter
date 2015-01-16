using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using SA_Resources.DSP;
using SA_Resources.SAForms;
using SA_Resources.USB;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Primitive_Ducker4x4 : DSP_Primitive, ICloneable
    {

        #region OFFSETS

        public const int THRESHOLD_OFFSET = 16;
        public const int HOLDTIME_OFFSET = 17;
        public const int DEPTH_OFFSET = 18;
        public const int ATTACK_OFFSET = 19;
        public const int RELEASE_OFFSET = 20;
        public const int BYPASSED_OFFSET = 21;

        public const int INDUCK_SELECT_0_OFFSET = 0;
        public const int INDUCK_SELECT_1_OFFSET = 1;
        public const int INDUCK_SELECT_2_OFFSET = 2;
        public const int INDUCK_SELECT_3_OFFSET = 3;
        public const int INDUCK_SELECT_4_OFFSET = 4;
        public const int INDUCK_SELECT_5_OFFSET = 5;
        public const int INDUCK_SELECT_6_OFFSET = 6;
        public const int INDUCK_SELECT_7_OFFSET = 7;

        public const int OUTDUCK_SELECT_0_OFFSET = 8;
        public const int OUTDUCK_SELECT_1_OFFSET = 9;
        public const int OUTDUCK_SELECT_2_OFFSET = 10;
        public const int OUTDUCK_SELECT_3_OFFSET = 11;
        public const int OUTDUCK_SELECT_4_OFFSET = 12;
        public const int OUTDUCK_SELECT_5_OFFSET = 13;
        public const int OUTDUCK_SELECT_6_OFFSET = 14;
        public const int OUTDUCK_SELECT_7_OFFSET = 15;

        #endregion

        #region VARIABLES

        public int NUM_CHANNELS = 8;


        public double _Threshold, _HoldTime, _Depth, _Attack, _Release;
        public bool _Bypassed;

        public int _PriorityChannel;

        public bool _Bypass_CH0, _Bypass_CH1, _Bypass_CH2, _Bypass_CH3, _Bypass_CH4, _Bypass_CH5;
        
        public List<bool> CH_Bypasses = new List<bool>(); 
        #endregion

        #region VALUES

        public UInt32 Threshold_Value, Holdtime_Value, Depth_Value, Attack_Value, Release_Value, Bypassed_Value;

        public List<UInt32> INDUCK_Values, OUTDUCK_Values;
 
        #endregion

        #region METERS

        public UInt16 METER1, METER2;

        #endregion

        public int PlainValue_Offset;
        public UInt32 Ducker_Package;



        public DSP_Primitive_Ducker4x4(string in_name, int in_channel, int in_positionA, int in_plainvalue_offset)
            : base(in_name, in_channel, in_positionA)
        {

            INDUCK_Values = new List<UInt32>();
            OUTDUCK_Values = new List<UInt32>();
            Threshold = 0;
            Depth = -15.0;
            Attack = 0.01; // 10ms
            Release = 0.1; // 100ms
            HoldTime = 0.1; // 100ms
            Bypassed = true;

            PlainValue_Offset = in_plainvalue_offset;

            this.Type = DSP_Primitive_Types.Ducker4x4;
            this.Num_Values = 22;

            for (int i = 0; i < NUM_CHANNELS; i ++)
            {
                CH_Bypasses.Add(false);
            }

            // Do this last so it will automatically call RecalculateRouters();
            PriorityChannel = 0;

        }


        public DSP_Primitive_Ducker4x4(string in_name, int in_channel, int in_positionA, int in_plainvalue_offset, double in_thresh, double in_depth, double in_attack, double in_release, double in_holdtime, bool in_bypassed, int in_priority = 0)
            : base(in_name, in_channel, in_positionA)
        {
            INDUCK_Values = new List<UInt32>();
            OUTDUCK_Values = new List<UInt32>(); 
            
            Threshold = in_thresh;
            Depth = in_depth;
            Attack = in_attack; // 10ms
            Release = in_release; // 100ms
            HoldTime = in_holdtime; // 100ms
            Bypassed = in_bypassed;

            PlainValue_Offset = in_plainvalue_offset;
            
            this.Type = DSP_Primitive_Types.Ducker4x4;
            this.Num_Values = 22;

            for (int i = 0; i < NUM_CHANNELS; i++)
            {
                CH_Bypasses.Add(false);
            }

            // Do this last so it will automatically call RecalculateRouters();
            PriorityChannel = 0;
        }

        private bool SuspendRecalculations = false;

        public override void UpdateFromReadValues(List<UInt32> valuesList)
        {
            this.Threshold = DSP_Math.MN_to_double_signed(valuesList[16], 9, 23) - 16.2;
            this.HoldTime = DSP_Math.value_to_dynamic_hold(valuesList[17]);
            this.Depth = DSP_Math.MN_to_double_signed(valuesList[18], 9, 23);
            this.Attack = DSP_Math.value_to_comp_attack(valuesList[19]);
            this.Release = DSP_Math.ducker_value_to_release(valuesList[20]);
            this.Bypassed = valuesList[21] == 0x00000001;

        }

        public void UpdateFromPlainValues(UInt32 plainValue)
        {
            SuspendRecalculations = true;
            for (int i = 0; i < NUM_CHANNELS; i++)
            {
                SetChannelBypass(i, (plainValue & 0x1) == 0x01);
                plainValue >>= 1;

            }
            SuspendRecalculations = false;
            this.PriorityChannel = (int)(plainValue & 0xFF);

        }

        

        public void SetChannelBypass(int ch_index, bool in_bypass)
        {
            CH_Bypasses[ch_index] = in_bypass;

            RecalculateRouters();
        }

        public List<UInt32> Values
        {
            get
            {
                List<UInt32> ReturnValues = new List<UInt32>();

                ReturnValues.AddRange(INDUCK_Values);
                ReturnValues.AddRange(OUTDUCK_Values);
                ReturnValues.AddRange(new UInt32[] { Threshold_Value, Holdtime_Value, Depth_Value, Attack_Value, Release_Value, Bypassed_Value });
                
                return ReturnValues;
            }
            set { }
        }

        public int PriorityChannel
        {
            get { return this._PriorityChannel; }
            set { this._PriorityChannel = value; RecalculateRouters(); }
        }
        public double Threshold
        {
            get { return this._Threshold; }
            set
            {
                this._Threshold = value;
                this.Threshold_Value = DSP_Math.double_to_MN(this._Threshold + 16.2, 9, 23);
            }

        }

        public double HoldTime
        {
            get { return this._HoldTime; }
            set
            {
                this._HoldTime = value;
                this.Holdtime_Value = DSP_Math.dynamic_hold_to_value(this._HoldTime);
            }
        }

        public double Depth
        {
            get { return this._Depth; }
            set
            {
                this._Depth = value;
                this.Depth_Value = DSP_Math.double_to_MN(this._Depth, 9, 23);
            }

        }

        public double Attack
        {
            get { return this._Attack; }
            set
            {
                this._Attack = value;
                this.Attack_Value = DSP_Math.comp_attack_to_value(this._Attack);
            }

        }

        public double Release
        {
            get { return this._Release; }
            set
            {
                this._Release = value;
                this.Release_Value = DSP_Math.ducker_release_to_value(this._Release);
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

        /// <summary>
        /// Generates new router configurations once the priority channel or channel bypasses have been changed
        /// </summary>
        public void RecalculateRouters()
        {

            if (SuspendRecalculations)
            {
                return;
            }

            int induck_index_counter = 1;
            try
            {
                Ducker_Package = 0x00;

                Ducker_Package |= (uint) PriorityChannel;


                for (int i = NUM_CHANNELS; i > 0; i--)
                {
                    Ducker_Package <<= 1;
                    Ducker_Package |= CH_Bypasses[i - 1] ? (uint) 0x01 : (uint) 0x00;

                }

                INDUCK_Values.Clear();
                OUTDUCK_Values.Clear();

                for (int x = 0; x < NUM_CHANNELS; x++)
                {
                    INDUCK_Values.Add(0);
                    OUTDUCK_Values.Add(0);
                }
                // *REMEMBER* - WE STORE THEM IN INDUCK_VALUES AND OUTDUCK_VALUES AT ZERO-BASED ADDRESSES
                // HOWEVER - THE DSP IS LOOKING FOR ONE-BASED VALUES

                int[] channel_cache = new int[8]; // Key = zereo-based Channel Number, Value = The zero-based index coming out of INDUCK_ROUTER

                induck_index_counter = 1;

                for (int i = 0; i < NUM_CHANNELS; i++)
                {
                    if (i == PriorityChannel)
                    {
                        channel_cache[i] = 0; // Good
                        INDUCK_Values[0] = (UInt32) (i + 1); // Good
                        OUTDUCK_Values[i] = (UInt32) 1; // Good
                    }
                    else
                    {
                        channel_cache[i] = induck_index_counter; // Good

                        INDUCK_Values[induck_index_counter] = (UInt32) (i + 1); // Good

                        if (CH_Bypasses[i])
                        {
                            OUTDUCK_Values[i] = (UInt32) (induck_index_counter + NUM_CHANNELS + 1);
                        }
                        else
                        {
                            OUTDUCK_Values[i] = (UInt32) (induck_index_counter + 1);
                        }

                        induck_index_counter++;
                    }


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in Ducker.RecalculateRouters] at index_counter_value " + induck_index_counter + ": " + ex.Message);
            }
            /*
            for(int x = 0; x < INDUCK_Values.Count; x++)
            {
                Debug.WriteLine("INDUCK " + x + " = " + INDUCK_Values[x]);
                
            }

            for (int y = 0; y < INDUCK_Values.Count; y++)
            {
                Debug.WriteLine("OUTDUCK " + y + " = " + OUTDUCK_Values[y]);

            }
             * */
        }

        public void QueuePackageChange(MainForm_Template PARENT_FORM)
        {
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(PlainValue_Offset, this.Ducker_Package));
        }


        public override void QueueChange(MainForm_Template PARENT_FORM)
        {
            for (int i = 0; i < this.Num_Values; i++)
            {
                Debug.WriteLine("Ducker4x4 - QueueChange - Sending " + this.Values[i].ToString("X") + " to offset " + (Offset + i));
            }

            if (PARENT_FORM.LIVE_MODE)
            {
                for (int i = 0; i < this.Num_Values; i++)
                {
                    PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + i, this.Values[i]));
                }
            }
        }


        public override void QueueChangeByOffset(MainForm_Template PARENT_FORM, int const_offset)
        {
            
            if (PARENT_FORM.LIVE_MODE)
            {
                Debug.WriteLine("Ducker4x4 - QueueChangeByOffset - Sending " + this.Values[const_offset].ToString("X8") + " to offset " + (Offset + const_offset));

                PARENT_FORM.AddItemToQueue(new LiveQueueItem(Offset + const_offset, this.Values[const_offset]));
            }
        }


        public override void QueueDeltas(MainForm_Template PARENT_FORM, DSP_Primitive comparePrimitive)
        {

            DSP_Primitive_Ducker4x4 RecastPrimitive = (DSP_Primitive_Ducker4x4)comparePrimitive;

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

        /// <summary>
        /// Checks if another primitive's values are equal to this one
        /// </summary>
        /// <param name="comparePrimitive">The primitive to be compared.</param>
        /// <returns><c>true</c> if all values of the compare primitive are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(DSP_Primitive comparePrimitive)
        {
            if (comparePrimitive == null)
            {
                return false;
            }

            DSP_Primitive_Ducker4x4 recastPrimitive = (DSP_Primitive_Ducker4x4)comparePrimitive;

            if (_Threshold != recastPrimitive.Threshold || _HoldTime != recastPrimitive.HoldTime || _Depth != recastPrimitive.Depth)
            {
                return false;
            }

            if (_Attack != recastPrimitive.Attack || _Release != recastPrimitive.Release || _Bypassed != recastPrimitive.Bypassed)
            {
                return false;
            }

            if(_PriorityChannel != recastPrimitive.PriorityChannel)
            {
                return false;
            }

            for (int i = 0; i < NUM_CHANNELS; i++)
            {
                if(this.CH_Bypasses[i] != recastPrimitive.CH_Bypasses[i])
                {
                    return false;
                }
            }
            
            return true;

        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
