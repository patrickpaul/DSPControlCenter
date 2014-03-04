using System;
using System.Drawing;

namespace SA_Resources.DSP.Filters
{

    public abstract class BiquadFilter
    {
        protected double a0,a1,a2,b0,b1,b2;
        protected double center_freq, lower_cutoff_freq, upper_cutoff_freq, gainDB, gain_abs, Q;
        protected double sample_rate = 48000;

        protected int center_index, lower_cutoff_index,upper_cutoff_index;

        protected double section1, section2, section3, section4;

        private double frequency_min = 10;
        private double frequency_max = 20000;
        private double frequency_default = 1000;

        private double gain_max = 24.0;
        private double gain_min = -24.0;
        private double gain_default = 0;

        private double q_max = 20;
        private double q_min = 0.707;
        private double q_default= 0.707;

        protected double filter_type = -1;

        public bool IsEqual(BiquadFilter compareFilter)
        {
            if(center_freq != compareFilter.center_freq)
            {
                return false;
            }

            if(gainDB != compareFilter.gainDB)
            {
                return false;
            }

            if(Q != compareFilter.Q)
            {
                return false;
            }

            if(this.FilterType != compareFilter.FilterType)
            {
                return false;
            }

            return true;
        }

        public BiquadFilter(double in_cf, double in_gain = 0, double in_q = 0)
        {
            center_freq = in_cf;
            gainDB = in_gain;
            Q = in_q;
            Recalculate();
        }

        public double FilterType
        {
            get { return filter_type; }
            set {}
        }
        public double CenterFrequency
        {
            get
            {
                return center_freq;
            }
            set
            {
                center_freq = value;
                Recalculate();
            }
        }

        public double LowerCutoffFrequency
        {
            get {
                return center_freq*(Math.Sqrt(1+(1/(4*Math.Pow(Q,2.0)))) - (1/(2*Q)));
            }
            set
            {
            }
        }

        public double UpperCutoffFrequency
        {
            get
            {
                return center_freq*(Math.Sqrt(1 + (1/(4*Math.Pow(Q, 2.0)))) + (1/(2*Q)));
            }
            set
            {
            }
        }

        public double Gain
        {
            get
            {
                return gainDB;
            }
            set
            {
                gainDB = value;
                Recalculate();
            }
        }

        public double QValue
        {
            get
            {
                return Q;
            }
            set
            {
                Q = value;
                Recalculate();
            }
        }

        public double GainMax
        {
            get
            {
                return gain_max;
            }
            set
            {
            }
        }

        public double GainMin
        {
            get
            {
                return gain_min;
            }
            set
            {
            }
        }

        public double SampleRate
        {
            get
            {
                return sample_rate;
            }
            set
            {
                sample_rate = value;
                Recalculate();
            }
        }

     
        public int CenterIndex
        {
            get
            {
                return center_index;
            }
            set
            {
            }
        }

        public int LowerCutoffIndex
        {
            get
            {
                return lower_cutoff_index;
            }
            set
            {
            }
        }

        public int UpperCutoffIndex
        {
            get
            {
                return upper_cutoff_index;
            }
            set
            {
            }
        }

        public double B0
        {
            get { return b0; }
            set { }
        }

        public double B1
        {
            get { return b1; }
            set { }
        }

        public double B2
        {
            get { return b2; }
            set { }
        }

        public double A1
        {
            get { return a1; }
            set { }
        }

        public double A2
        {
            get { return a2; }
            set { }
        }

        public double ValueAt(double f)
        {
            double phi = Math.Pow((Math.Sin(2.0 * Math.PI * f / (2.0 * sample_rate))), 2.0);
            return (Math.Pow(b0 + b1 + b2, 2.0) - 4.0 * (b0 * b1 + 4.0 * b0 * b2 + b1 * b2) * phi + 16.0 * b0 * b2 * phi * phi) / (Math.Pow(1.0 + a1 + a2, 2.0) - 4.0 * (a1 + 4.0 * a2 + a1 * a2) * phi + 16.0 * a2 * phi * phi);
        }

        public double LogValueAt(double f)
        {
            double r;
            try
            {
                r = 10 * Math.Log10(ValueAt(f));
            }
            catch
            {
                r = -100;
            }
            if (Double.IsInfinity(r) || Double.IsNaN(r))
            {
                r = -100;
            }
            return r;
        }

        public double SuggestedFrequency(double f)
        {
            if(f < frequency_min)
            {
                return frequency_min;
            } 

            if(f > frequency_max)
            {
                return frequency_max;
            }

            return f;

        }

        public double SuggestedFrequency(string fstring)
        {
            double parsed_f;

            if (Double.TryParse(fstring, out parsed_f))
            {
                if (parsed_f < frequency_min)
                {
                    return frequency_min;
                }

                if (parsed_f > frequency_max)
                {
                    return frequency_max;
                }

                return parsed_f;
            } else
            {
                return frequency_default;
            }
        }

        public double SuggestedGain(double g)
        {
            if (g < gain_min)
            {
                return gain_min;
            }

            if (g > gain_max)
            {
                return gain_max;
            }

            return g;

        }


        public double SuggestedGain(string gstring_lol)
        {
            double parsed_g;

            if (Double.TryParse(gstring_lol, out parsed_g))
            {
                if (parsed_g < gain_min)
                {
                    return gain_min;
                }

                if (parsed_g > gain_max)
                {
                    return gain_max;
                }
                return parsed_g;
            }
            else
            {
                return gain_default;
            }

        }

        public double SuggestedQ(double q)
        {
            if (q < q_min)
            {
                return q_min;
            }

            if (q > q_max)
            {
                return q_max;
            }

            return q;

        }

        public double SuggestedQ(string qstring)
        {
            double parsed_q;

            if (Double.TryParse(qstring, out parsed_q))
            {
                if (parsed_q < q_min)
                {
                    return q_min;
                }

                if (parsed_q > q_max)
                {
                    return q_max;
                }

                return parsed_q;
            }
            else
            {
                return q_default;
            }

        }

        public abstract void Recalculate();
    }
}
