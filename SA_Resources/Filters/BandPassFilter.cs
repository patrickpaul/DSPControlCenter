using System;
using System.Drawing;

namespace SA_Resources.DSP.Filters
{
    public class BandPassFilter : BiquadFilter
    {
        public BandPassFilter(double in_cf, double in_gain, double in_q) : base(in_cf, in_gain, in_q)
        {
            filter_type = 7;
        }

        public override void Recalculate()
        {
            double omega = 2 * Math.PI * center_freq / sample_rate;
            double sn = Math.Sin(omega);
            double cs = Math.Cos(omega);
            double alpha = sn / (2 * Q);

            b0 = alpha;
            b1 = 0;
            b2 = -alpha;
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
