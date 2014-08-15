using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SA_Resources.DSP.Filters;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.DSP
{
    public class DSP_Math
    {

        #region Filter Packaging

        public double Q_to_Octaves(double q)
        {
            double Q2bw1st = ((2.0 * Math.Pow(q, 2.0)) + 1) / (2.0 * Math.Pow(q, 2.0));
            double Q2bw2nd = Math.Pow(2.0 * Q2bw1st, 2.0) / 4.0;
            double Q2bw3rd = Math.Sqrt(Q2bw2nd - 1);
            double Q2bw4th = Q2bw1st + Q2bw3rd;
            return Math.Log10(Q2bw4th)/Math.Log10(2);

        }

        public double Octaves_to_Q(double octaves)
        {
            return Math.Sqrt(Math.Pow(2.0, octaves))/(Math.Pow(2.0, octaves) - 1.0);

        }

        public static UInt32 filter_to_package(DSP_Primitive_BiquadFilter in_filter)
        {
            UInt32 return_int = 0x00;

            return_int |= Convert.ToUInt32(in_filter.Bypassed);

            return_int <<= 4;

            return_int |= (uint)in_filter.Filter.FilterType;

            return_int <<= 3;

            if (in_filter.FType == FilterType.SecondOrderHighPass || in_filter.FType == FilterType.SecondOrderLowPass)
            {
                return_int |= 0x01;
            }
            else
            {
                return_int |= 0x00;
            }
            

            return_int <<= 23;

            return_int |= (uint)in_filter.Filter.CenterFrequency;

            return return_int;

        }

        public static UInt32 filter_primitive_to_package(DSP_Primitive_BiquadFilter in_primitive)
        {

            // center = 23:0
            // slope = 26:24
            // type = 30:27
            // 
            UInt32 return_int = 0x00;
            return_int |= Convert.ToUInt32(in_primitive.Bypassed);

            return_int <<= 4;

            return_int |= (uint)in_primitive.Filter.FilterType;

            return_int <<= 3;

            if (in_primitive.FType == FilterType.SecondOrderHighPass || in_primitive.FType == FilterType.SecondOrderLowPass)
            {
                return_int |= 0x01;
            }
            else
            {
                return_int |= 0x00;
            }


            return_int <<= 23;
            return_int |= (uint)in_primitive.Filter.CenterFrequency;

            return return_int;

        }


        public static BiquadFilter rebuild_filter(UInt32 package, UInt32 Gain, UInt32 QVal)
        {
            uint center_freq = package & 0x7FFFFF; //23 bits
            package >>= 23;

            uint slope = package & 0x7; // 3 bits
            package >>= 3;

            uint type = package & 0xF; // 4 bits
            package >>= 4;

            uint bypassed = package & 0x01; // This does not go into BiquadFilter constructor.. we must process it when we rebuild primitive
            double gain = DSP_Math.MN_to_double_signed(Gain, 8, 24);
            double q_val = DSP_Math.MN_to_double_unsigned(QVal, 8, 24);


            switch (type)
            {
                default:
                    return null;
                case 0 :
                    return null;
                case 1:
                    return new FirstOrderLowPassFilter(center_freq,gain,q_val);
                case 2:
                    return new FirstOrderHighPassFilter(center_freq, gain, q_val);
                case 3:
                    return new LowShelfFilter(center_freq, gain, q_val);
                case 4:
                    return new HighShelfFilter(center_freq, gain, q_val);
                case 5:
                    return new PeakFilter(center_freq, gain, q_val);
                case 6:
                    return new NotchFilter(center_freq, gain, q_val);
                case 7:
                    return new SecondOrderLowPassFilter(center_freq, gain, q_val);
                case 8:
                    return new SecondOrderHighPassFilter(center_freq, gain, q_val);
            }
        }
         
        
        #endregion

        #region Decibel and Voltage Gain Conversions

        public static double decibels_to_voltage_gain(double decibels)
        {
            return Math.Pow(10.0, decibels / 20.0);
        }

        public static double voltage_gain_to_decibels(double voltage_gain)
        {
            return 20.0 * Math.Log10(voltage_gain);
        }

        #endregion

        #region M.N fractional format

        public static UInt32 double_to_MN(double input, int M, int N)
        {

            uint whole = 0;
            double fraction = 0;
            UInt32 output = 0x00000000;
            UInt32 fractional = 0x00000000;
            bool inverse = false;

            if (input < 0)
            {
                inverse = true;
                input = (-1 * input);
            }
            whole = (uint)Math.Floor(input);
            fraction = input - whole;

            output |= whole;
            output = output << N;
            fractional = (UInt32)(fraction * Math.Pow(2.0, (double)N));
            output |= fractional;

            if (!inverse)
            {
                return output;
            }

            return (output ^ 0xFFFFFFFF);
        }


        public static double MN_to_double_unsigned(UInt32 input, int M, int N)
        {
            UInt32 whole_value = 0x00, fractional_value = 0x00;
            UInt32 whole_mask = (UInt32)(Math.Pow(2.0, 32.0) - Math.Pow(2.0, (double)N));
            whole_value = (input & whole_mask) >> N;
            fractional_value = (input & (whole_mask ^ 0xFFFFFFFF));
            double fraction = (double)(fractional_value / Math.Pow(2.0, (double)N));
            double final_value = whole_value + fraction;

            return Math.Round(final_value, 3);

        }

        public static double MN_to_double_signed(UInt32 input, int M, int N)
        {
            UInt32 wholeValue, fractionalValue, fractionalMask = (UInt32)(Math.Pow(2.0, N) - 1.0);
            double finalValue;

            wholeValue = (input & (UInt32)((Math.Pow(2.0, 32.0) - Math.Pow(2.0, (double)N)))) >> N;

            if (wholeValue > Math.Pow(2.0, M - 1))
            {
                // negative value...
                wholeValue = (wholeValue ^ (uint)(Math.Pow(2.0, M) - 1.0));
                fractionalValue = (input ^ fractionalMask) & fractionalMask;
                double fraction = (fractionalValue / Math.Pow(2.0, N));

                finalValue = (-1 * wholeValue) - fraction;
            }
            else
            {
                fractionalValue = (input & fractionalMask);
                double fraction = (fractionalValue / Math.Pow(2.0, N));

                finalValue = wholeValue + fraction;
            }

            return finalValue;

        }

        #endregion

        #region Standard Gain 3.29

        public static double Value_To_Pregain(UInt32 value)
        {
            return MN_to_double_signed(value, 9, 23);
        }

        public static double Value_To_StandardGain(UInt32 value)
        {
            return voltage_gain_to_decibels(MN_to_double_signed(value, 3, 29));
        }

        #endregion

        #region Sine Frequency

        public static UInt32 sine_freq_to_value(double frequency)
        {
            return (UInt32)((frequency / 48000.0) * Math.Pow(2.0, 32.0));
        }

        public static double sine_value_to_freq(UInt32 value)
        {
            return (value / Math.Pow(2.0, 32.0)) * 48000.0;
        }

        public static UInt32 sine_gain_to_value(double gain)
        {
            return double_to_MN(-(Math.Pow(10.0, (gain - 20.0)/20.0)/16.0), 1, 31);
        }

        public static UInt32 pink_gain_to_value(double gain)
        {
            return double_to_MN(-(Math.Pow(10.0, (gain - 23.0103) / 20.0) / 16.0), 1, 31);
        }

        #endregion

        #region Dynamics

        public static UInt32 dynamic_hold_to_value(double hold)
        {
            return (UInt32) (hold*(48000.0/16.0));
        }

        public static double value_to_dynamic_hold(UInt32 value)
        {
            return (double)(value / (48000.0 / 16.0));
        }

        #endregion


        #region Compressor Attack - Needs verification of 3000 vs 48000

        /// <summary>
        /// Generates 32-bit DSP value for Compressor Attack
        /// </summary>
        /// <param name="attack">Attack time in seconds</param>
        /// <returns>32-bit DSP value</returns>
        public static UInt32 comp_attack_to_value(double attack)
        {
            return (UInt32)((1.0 - Math.Exp(-1.0 / (attack * (48000.0/16.0)))) * Math.Pow(2.0, 31.0));
        }

        public static double value_to_comp_attack(UInt32 value)
        {
            return -1.0 / ((48000.0 / 16.0) * Math.Log(1 - (value / Math.Pow(2.0, 31.0))));
        }

        #endregion

        #region Compressor

        public static UInt32 comp_release_to_value(double release)
        {
            return DSP_Math.double_to_MN(1.0 - Math.Pow(10.0, (-1.0 / (release * (48000.0 / 16.0)))), 1, 31);
        }

        public static double comp_value_to_release(UInt32 value)
        {
            double converted = DSP_Math.MN_to_double_signed(value, 1, 31);

            return (-1 / (Math.Log10(1 - converted) * (48000.0 / 16.0)));

        }

        #endregion

        #region Ducker

        public static UInt32 ducker_release_to_value(double release)
        {
            return DSP_Math.double_to_MN(1.0 - Math.Exp(-1.0 / (0.1 * (48000.0 / 16.0))), 1, 31);
        }

        public static double ducker_value_to_release(UInt32 value)
        {
            double converted = DSP_Math.MN_to_double_signed(value, 1, 31);

            return (-1 / (Math.Log(1 - converted) * (48000.0 / 16.0)));
        }

        #endregion
        /*
 * 
 *      Because.. why not?
 *      
        NNNNNNNNNNNNNN8IIMNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNNN$IIIIIIIIIINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNNIIIIIIIIIIIIIONNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNIIIIIIIIIIIIIIIINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNZIIIIIIIIIIIIIIIII$NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNZIIIIIIIINIIIIIIIIIDNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNIIIIIIIIIII$I$ID88DNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNMIIIIIIIZN. M7.....NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNN$IIII7.....II7.. MNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNNNIIII$....$?IIIIIINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
        NNNNNNNNNNN?IIIIIIMIIIIIIIIINNNNNNNNNNNNNNNNNNNNNNNNNNINNNNN
        NNNNNNNNNNNNIIIIIIIIIIIIIIIIIINNNNNNNNNNNNNNNNNNNNNNNMIINNNN
        NNNNNNNNNNNN8IIIIIIIIIIIINIIIIDNNNNNNNNNNNNINNNNNNNNNIIIDNNN
        NNNNNNNNNNNNNIIIII$MIII?II?IOIINNNNNNNNNNNIINNNNNNNNIIIIINNN
        NNNNNNNNNNNNNIIIIIMI?II$IIZI?I?NNNNNNNNNNIIINNNNNNIIIIIIINNN
        NNNNNNNNNNNNZ?IIIIIIIIIIIIOINNNNNNNNNNNNNIIIZNNNIIIIIIIIINNN
        NNNNNNNNNNND,.IIIIIIIMI7D8I,NNNNNNNNNNNNNIIIINNDIIIIIIII7NNN
        NNNNNNNNNNN,,,.~MIIIIIIII7=,,,NNNNNNNNNNNIIIIIIIIIIIIIIINNNN
        NNNNNNNNN~,,,,,.~~~~~~~N~~~,,,,,MNNNNNNNNIIIIIIIIIIIIIINNNNN
        NNNNNNNN,,,,,,,,,ZN~~~~~I~~,,,,,,.,NNNNNNNIIIIIIIIIIIINNNNNN
        NNNNNNN..,,,,,,,,,,~~~~~~~~,,,,,,,,,,NNNN.IIIIIIIIIINNNNNNNN
        NNNNNN,,,I,,,,,,,,,ZII=~~~=,,,,,,,,,,,.NN,,IIIIIIIMNNNNNNNNN
        NNNNM.,,,,:,,,,,,NIII,,~=~$,,,,,,,M,,,,,,,,,,,,NI,NNNNNNNNNN
        NNNN,,,,,,,O,,,,IIIIM,,,,,D,,,,,,,.,,,,,,,,,,,,,,,NNNNNNNNNN
        NNN,,,,,,,,,,,,OIIII,,,,,,,,,=,,,,,,,,,,,,,,,,,,,MNNNNNNNNNN
        NN.,,,,,,,,,,,,IIIII,,,,,,,,,,,,,,,,,,,,,,,,,,,,,NNNNNNNNNNN
        N,,,,,,,,,,,.,?IIIII,,,,,,,,,,,,.,,~,,,,,,,,,,,,:NNNNNNNNNNN
        N,,,,,,,,,,,,~8IIIIII~,,,,,,,M,,:,,D,,,,,,,,,,,,NNNNNNNNNNNN
        ,,,,,,,,,,M,,,IIIIIIIIII,,,,,,,,?,,,,,,,,,,,,,,,NNNNNNNNNNNN
        ,,,,,,,,,,,,,,IIIIIIIIIIIIIIIMNMM,,,:,,,,,,,,,,NNNNNNNNNNNNN
*/

        #region Compressor Ratio

        public static double value_to_comp_ratio(UInt32 value)
        {
            return (1/(DSP_Math.MN_to_double_signed(value, 1, 31) - 1));
        }

        public static UInt32 comp_ratio_to_value(double value)
        {
            return DSP_Math.double_to_MN((1 / value) - 1, 1, 31);
        }
        

        #endregion

    }
}
