using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{

    public class DuckerConfig : ICloneable
    {
        public bool Bypassed;

        public int _priorityChannel;
        public double Threshold;
        public double Depth;
        public double Holdtime;
        public double Attack;
        public double Release;
        public int[] RouterInputs;
        public int[] RouterOutputs;

        public int PriorityChannel
        {
            get
            {
                return _priorityChannel;
            }

            set
            {
                _priorityChannel = value;
                RecalculateRouter();
            }
        }
        public DuckerConfig()
        {
            RouterInputs = new int[4];
            RouterOutputs = new int[4];
            
            Bypassed = true;
            PriorityChannel = 0;
            Threshold = 0;
            Depth = -15;
            Holdtime = 0.010;
            Attack = 0.1;
            Release = 0.1;

            
        }


        public DuckerConfig(bool _bypassed, int _priorityChannel, double _threshold, double _depth, double _holdTime, double _attack, double _release)
        {
            RouterInputs = new int[4];
            RouterOutputs = new int[4];
            
            Bypassed = _bypassed;
            PriorityChannel = _priorityChannel;
            Threshold = _threshold;
            Depth = _depth;
            Holdtime = _holdTime;
            Attack = _attack;
            Release = _release;

        }


        public bool Equals(DuckerConfig compareConfig)
        {

            if(Bypassed != compareConfig.Bypassed || PriorityChannel != compareConfig.PriorityChannel || Threshold != compareConfig.Threshold)
            {
                return false;
            }

            if(Depth != compareConfig.Depth || Holdtime != compareConfig.Holdtime || Attack != compareConfig.Attack)
            {
                return false;
            }

            if(Release != compareConfig.Release)
            {
                return false;
            }

            return true;
        }

        private void RecalculateRouter()
        {
            int input_router_counter = 0;
            int output_router_counter = 0;
            for(int i = 0; i < 4; i++)
            {
                if (i == _priorityChannel)
                {
                    RouterInputs[3] = _priorityChannel+1;
                    RouterOutputs[_priorityChannel] = 4;
                }
                else
                {
                    RouterInputs[input_router_counter++] = i+1;

                    if (output_router_counter < _priorityChannel)
                    {
                        RouterOutputs[i] = (i)+1;
                        output_router_counter++;
                    } else
                    {
                        RouterOutputs[i] = (output_router_counter++)+1;
                    }

                    
                }
            }
        }

        public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX)
        {

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, DSP_Math.double_to_MN(this.Threshold, 9, 23)));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 1, DSP_Math.dynamic_hold_to_value(this.Holdtime)));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 2, DSP_Math.double_to_MN(this.Depth, 9, 23)));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 3, DSP_Math.comp_attack_to_value(this.Attack)));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 4, DSP_Math.comp_release_to_value(this.Release))); 
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 5, Convert.ToUInt32(this.Bypassed))); 

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 6, (uint)this.RouterInputs[0]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 7, (uint)this.RouterInputs[1]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 8, (uint)this.RouterInputs[2]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 9, (uint)this.RouterInputs[3]));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 10, (uint)this.RouterOutputs[0]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 11, (uint)this.RouterOutputs[1]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 12, (uint)this.RouterOutputs[2]));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX + 13, (uint)this.RouterOutputs[3]));
        }


        public void QueueChannelChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX)
        {
            // On DSP 4x4, SETTINGS_INDEX will be 278

            //UInt32 in_priority_address = SETTINGS_INDEX+
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
