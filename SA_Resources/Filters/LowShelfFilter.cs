using System;
using System.Drawing;

namespace SA_Resources.Filters
{
    public class LowShelfFilter : BiquadFilter
    {
        public LowShelfFilter(double in_cf, double in_gain, double in_q) : base(in_cf, in_gain, in_q)
        {
            filter_type = 2;
        }

        public override void Recalculate()
        {
            gain_abs = Math.Pow(10, gainDB / 40);
            double omega = 2 * Math.PI * center_freq / sample_rate;
            double sn = Math.Sin(omega);
            double cs = Math.Cos(omega);
            double alpha = sn / (2 * Q);
            double beta = Math.Sqrt(gain_abs + gain_abs);

            b0 = gain_abs*((gain_abs + 1) - (gain_abs - 1)*cs + 2*Math.Sqrt(gain_abs)*alpha);
            b1 = 2*gain_abs*((gain_abs - 1) - (gain_abs + 1)*cs);
            b2 = gain_abs*((gain_abs + 1) - (gain_abs - 1)*cs - 2*Math.Sqrt(gain_abs)*alpha);
            a0 = (gain_abs + 1) + (gain_abs - 1)*cs + 2*Math.Sqrt(gain_abs)*alpha;
            a1 = -2*((gain_abs - 1) + (gain_abs + 1)*cs);
            a2 = (gain_abs + 1) + (gain_abs - 1) * cs - 2 * Math.Sqrt(gain_abs) * alpha;
            

            b0 /= a0;
            b1 /= a0;
            b2 /= a0;
            a1 /= a0;
            a2 /= a0;
        }

    }
}
