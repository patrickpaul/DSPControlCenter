using System;
using System.Drawing;

namespace SA_Resources.DSP.Filters
{
    public class NotchFilter : BiquadFilter
    {
        public NotchFilter(double in_cf, double in_gain, double in_q) : base(in_cf, in_gain, in_q)
        {
            filter_type = 5;
        }

        public override void Recalculate()
        {
            double omega = 2 * Math.PI * center_freq / sample_rate;
            double sn = Math.Sin(omega);
            double cs = Math.Cos(omega);
            double alpha = sn / (2 * Q);

            b0 = 1;
            b1 = -2 * cs;
            b2 = 1;
            a0 = 1 + alpha;
            a1 = -2 * cs;
            a2 = 1 - alpha;

            b0 /= a0;
            b1 /= a0;
            b2 /= a0;
            a1 /= a0;
            a2 /= a0;
        }

    }
}
