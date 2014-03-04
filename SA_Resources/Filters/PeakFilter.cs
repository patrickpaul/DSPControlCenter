using System;
using System.Drawing;

namespace SA_Resources.DSP.Filters
{
    public class PeakFilter : BiquadFilter
    {
        public PeakFilter(double in_cf, double in_gain, double in_q) : base(in_cf, in_gain, in_q)
        {
            filter_type = 4;
        }

        public override void Recalculate()
        {
            gain_abs = Math.Pow(10, gainDB / 40);
            double omega = 2 * Math.PI * center_freq / sample_rate;
            double sn = Math.Sin(omega);
            double cs = Math.Cos(omega);
            double alpha = sn / (2 * Q);
            double beta = Math.Sqrt(gain_abs + gain_abs);

            b0 = 1 + (alpha * gain_abs);
            b1 = -2 * cs;
            b2 = 1 - (alpha * gain_abs);
            a0 = 1 + (alpha / gain_abs);
            a1 = -2 * cs;
            a2 = 1 - (alpha / gain_abs);

            b0 /= a0;
            b1 /= a0;
            b2 /= a0;
            a1 /= a0;
            a2 /= a0;
        }

    }
}
