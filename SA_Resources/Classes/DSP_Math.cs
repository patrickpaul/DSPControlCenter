using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SA_Resources.Filters;

namespace SA_Resources
{
    public class DSP_Math
    {

        #region Filter Packaging

        public static UInt32 filter_to_package(FilterConfig in_filter)
        {
            UInt32 return_int = 0x00;

            return_int |= Convert.ToUInt32(in_filter.Bypassed);

            return_int <<= 4;

            return_int |= (uint)in_filter.Filter.FilterType;

            return_int <<= 3;

            if (in_filter.Type == FilterType.SecondOrderHighPass || in_filter.Type == FilterType.SecondOrderLowPass)
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


        public static FilterConfig rebuild_filter(UInt32 package, UInt32 Gain, UInt32 QVal)
        {
            uint center_freq = package & 0x7FFFFF; //23 bits
            package >>= 23;

            uint slope = package & 0x7; // 3 bits
            package >>= 3;

            uint type = package & 0xF; // 4 bits
            package >>= 4;

            uint enabled = package & 0x01;
            double gain = DSP_Math.MN_to_double_signed(Gain, 8, 24);
            double q_val = DSP_Math.MN_to_double_unsigned(QVal, 8, 24);


            switch (type)
            {
                default:
                case 0:
                    return new FilterConfig(FilterType.FirstOrderLowPass, false, new FirstOrderLowPassFilter(center_freq, gain, q_val));
                case 1:
                    return new FilterConfig(FilterType.FirstOrderHighPass, false, new FirstOrderHighPassFilter(center_freq, gain, q_val));
                case 2:
                    return new FilterConfig(FilterType.LowShelf, false, new LowShelfFilter(center_freq, gain, q_val));
                case 3:
                    return new FilterConfig(FilterType.HighShelf, false, new HighShelfFilter(center_freq, gain, q_val));
                case 4:
                    return new FilterConfig(FilterType.Peak,false,new PeakFilter(center_freq, gain, q_val));
                case 5:
                    return new FilterConfig(FilterType.Notch, false, new NotchFilter(center_freq, gain, q_val));
                case 6:
                    return new FilterConfig(FilterType.SecondOrderLowPass, false, new SecondOrderLowPassFilter(center_freq, gain, q_val));
                case 7:
                    return new FilterConfig(FilterType.SecondOrderHighPass, false, new SecondOrderHighPassFilter(center_freq, gain, q_val));
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

        public static double value_to_gain(UInt32 value)
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

        #region Compressor Release - Needs verification of 1302 vs 48000

        // TODO - REVERSE ME!
        public static UInt32 comp_release_to_value(double release)
        {
            //3000 = BLOCK_RATE
            UInt32 method1 = (UInt32)((1.0 - Math.Exp(-1.0 / (release * 1302.88343))) * Math.Pow(2.0, 31.0));
            //dsp_release = 1 - pow(10, (-1 / ( release * block_rate )) )
            UInt32 method2 = (UInt32)((1.0 - Math.Pow(10.0,(-1 / (release * 3000)))) * Math.Pow(2.0, 31.0));

            return (UInt32)((1.0 - Math.Exp(-1.0 / (release * 1302.88343))) * Math.Pow(2.0, 31.0));
        }

        
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
        public static double value_to_comp_release(UInt32 value)
        {
            return -1.0 / (1302.88343 * Math.Log(1 - (value / Math.Pow(2.0, 31.0))));
        }

        #endregion

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
