using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
        public class GainConfig
        {
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
            public double Gain;
            public bool Muted;

        }
    }