using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
