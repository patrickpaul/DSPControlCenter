using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{

    public class DelayConfig
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
    }
}
