using System;
using System.Drawing;

namespace SA_Resources.Filters
{

    public class FirstOrderHighPassFilter : BiquadFilter
    {
        public FirstOrderHighPassFilter(double in_cf, double in_gain = 0, double in_q = 0): base(in_cf, in_gain, in_q)
        {
            filter_type = 1;
        }

        public override void Recalculate()
        {
            gain_abs = Math.Pow(10, gainDB / 40);

            double omega = 2 * Math.PI * center_freq / sample_rate;
            double K = Math.Tan(omega/2);
            double alpha = 1 + K;

            b0 = 1/alpha;
            b1 = -1/alpha;
            b2 = 0;

            a0 = 1;
            a1 = -((1-K)/alpha);
            a2 = 0;

            b0 /= a0;
            b1 /= a0;
            b2 /= a0;
            a1 /= a0;
            a2 /= a0;
        }

    }

    public class SecondOrderHighPassFilter : BiquadFilter
    {
        public SecondOrderHighPassFilter(double in_cf, double in_gain = 0, double in_q = 0) : base(in_cf, in_gain, in_q)
        {
            filter_type = 7;
        }

        public override void Recalculate()
        {
            gain_abs = Math.Pow(10, gainDB / 40);
            double omega = 2 * Math.PI * center_freq / sample_rate;
            double sn = Math.Sin(omega);
            double cs = Math.Cos(omega);
            double alpha = sn / (2 * 0.707);
            double beta = Math.Sqrt(gain_abs)/Q;


            b0 = (1 + cs) / 2;
            b1 = -(1 + cs);
            b2 = (1 + cs) / 2;

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
