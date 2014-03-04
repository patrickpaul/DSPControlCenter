using System;
using System.Drawing;

namespace SA_Resources.DSP.Filters
{
    public class FirstOrderLowPassFilter : BiquadFilter
    {
        public FirstOrderLowPassFilter(double in_cf, double in_gain = 0, double in_q = 0) : base(in_cf, in_gain, in_q)
        {
            filter_type = 0;
        }

        public override void Recalculate()
        {
            gain_abs = Math.Pow(10, gainDB / 40);

            double omega = 2 * Math.PI * center_freq / sample_rate;
            double K = Math.Tan(omega / 2);
            double alpha = 1 + K;

            b0 = K / alpha;
            b1 = K / alpha;
            b2 = 0;

            a0 = 1;
            a1 = -((1 - K) / alpha);
            a2 = 0;

            b0 /= a0;
            b1 /= a0;
            b2 /= a0;
            a1 /= a0;
            a2 /= a0;
        }

    }

    public class SecondOrderLowPassFilter : BiquadFilter
    {
        public SecondOrderLowPassFilter(double in_cf, double in_gain = 0, double in_q = 0) : base(in_cf, in_gain, in_q)
        {
            filter_type = 6;
        }

        public override void Recalculate()
        {
            gain_abs = Math.Pow(10, gainDB / 40);
            double omega = 2 * Math.PI * center_freq / sample_rate;
            double sn = Math.Sin(omega);
            double cs = Math.Cos(omega);
            double alpha = sn / (2 * 0.707);


            b0 = (1 - cs) / 2;
            b1 = 1 - cs;
            b2 = (1 - cs) / 2;

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
