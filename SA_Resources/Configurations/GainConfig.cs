using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{
        public class GainConfig : ICloneable
        {
            public double Gain;
            public bool Muted; 
            
            public string[] _filterNames;

            public GainConfig()
            {
                Muted = false;
                Gain = 0;
            }


            public GainConfig(double in_gain, bool in_muted)
            {
                Gain = in_gain;
                Muted = in_muted;
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
            
            public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX, bool is_first_gain = false, int CH_INDEX = 0)
            {
                if(this.Muted)
                {
                    PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(-100), 3, 29)));
                } else
                {
                    if(is_first_gain)
                    {
                        UInt32 new_val = DSP_Math.double_to_MN(this.Gain + PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].pregains[CH_INDEX], 9, 23);

                        // Notice that this is a 
                        PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, new_val));

                        // Save this for the first node so we can remember
                        PARENT_FORM.AddItemToQueue(new LiveQueueItem((416 + CH_INDEX), DSP_Math.double_to_MN(this.Gain, 6, 26)));

                    } else
                    {
                        PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, DSP_Math.double_to_MN(DSP_Math.decibels_to_voltage_gain(this.Gain), 3, 29)));
                    }
                    
                }
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

        }
    }