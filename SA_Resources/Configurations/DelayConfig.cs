using System;
using SA_Resources.DSP;
using SA_Resources.SAForms;

namespace SA_Resources.Configurations
{

    public class DelayConfig : ICloneable
    {
        public double Delay;
        public bool Bypassed;


        public DelayConfig()
        {
            Delay = 0;
            Bypassed = true;
        }


        public DelayConfig(double in_delay, bool bypassed)
        {
            Bypassed = bypassed;
            Delay = in_delay;
        }

        public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX)
        {
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(SETTINGS_INDEX, DSP_Math.double_to_MN(this.Delay, 16, 16)));
        }

        public bool Equals(DelayConfig compareConfig)
        {
            return (Delay == compareConfig.Delay && Bypassed == compareConfig.Bypassed);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
