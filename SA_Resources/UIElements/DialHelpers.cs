using System;

namespace SA_Resources
{
    public sealed class DialHelpers
    {
        #region General Dial Helpers

        public static string rotations_to_value(int rotations, Func<double, string> formatCallback, double[] tick)
        {
            double dubRotations = rotations;

            if (rotations == 0)
            {
                return formatCallback(tick[0]);
            }
            else if ((dubRotations > 0) && (dubRotations <= 42))
            {
                return formatCallback(((dubRotations / 42.00) * (tick[1] - tick[0])) + tick[0]);
            }
            else if ((dubRotations > 42) && (dubRotations <= 84))
            {
                return formatCallback((((dubRotations - 42.00) / 42.00) * (tick[2] - tick[1])) + tick[1]);
            }
            else if ((dubRotations > 84) && (dubRotations <= 125))
            {
                return formatCallback((((dubRotations - 84.00) / 42.00) * (tick[3] - tick[2])) + tick[2]);
            }
            else if ((dubRotations > 125) && (dubRotations <= 167))
            {
                return formatCallback((((dubRotations - 125.00) / 42.00) * (tick[4] - tick[3])) + tick[3]);
            }
            else if ((dubRotations > 167) && (dubRotations <= 209))
            {
                return formatCallback((((dubRotations - 167.00) / 42.00) * (tick[5] - tick[4])) + tick[4]);
            }
            else if ((dubRotations > 209) && (dubRotations < 250))
            {
                return formatCallback((((dubRotations - 209.00) / 42.00) * (tick[6] - tick[5])) + tick[5]);
            }
            else
            {
                return formatCallback(tick[6]);
            }

        }


        public static int value_to_rotations(double in_value, double[] tick)
        {
            if (in_value == tick[0])
            {
                return 0;
            }
            else if (in_value <= tick[1])
            {
                return (int)((((in_value - tick[0]) / (tick[1] - tick[0])) * (42.0 - 0.0)) + 0.0);
            }
            else if (in_value <= tick[2])
            {
                return (int)((((in_value - tick[1]) / (tick[2] - tick[1])) * (84.0 - 42.0)) + 42.0);
            }
            else if (in_value <= tick[3])
            {
                return (int)((((in_value - tick[2]) / (tick[3] - tick[2])) * (125.0 - 84.0)) + 84.0);
            }
            else if (in_value <= tick[4])
            {
                return (int)((((in_value - tick[3]) / (tick[4] - tick[3])) * (167.0 - 125.0)) + 125.0);
            }
            else if (in_value <= tick[5])
            {
                return (int)((((in_value - tick[4]) / (tick[5] - tick[4])) * (209.0 - 167.0)) + 167.0);
            }
            else if (in_value <= tick[6])
            {
                return (int)((((in_value - tick[5]) / (tick[6] - tick[5])) * (250 - 209)) + 209.0);
            }
            else
            {
                return 250;
            }

        }


        public static double rotations_to_degrees(int rotations)
        {
            return ((((double)rotations / 250.0) * 270.0));

        }


        public static double value_to_degrees(double in_value, double[] ticks)
        {
            return rotations_to_degrees(value_to_rotations(in_value, ticks));
        }

        public static double string_to_value(string in_string)
        {
            if (in_string.Contains("kHz"))
            {
                return (Double.Parse(in_string.Replace("kHz", "")) * 1000.0);
            }
            else if (in_string.Contains("Hz"))
            {
                return Double.Parse(in_string.Replace("Hz", ""));
            }
            else if (in_string.Contains("ms"))
            {
                return (Double.Parse(in_string.Replace("ms", "")) / 1000.0);
            }
            else if (in_string.Contains("s"))
            {
                return Double.Parse(in_string.Replace("s", ""));
            }
            else if (in_string.Contains("dB"))
            {
                return Double.Parse(in_string.Replace("dB", ""));
            }
            else
            {
                return Double.Parse(in_string);
            }
        }


#endregion

        #region Dial Formats

        public static string Format_String_Gain(double g)
        {
            if (g == 0)
            {
                return "0dB";
            }
            else if ((g >= -100) && (g <= -10))
            {
                return g.ToString("F1") + "dB";
            }
            else if ((g > -10) && (g < 10))
            {
                return g.ToString("F2") + "dB";
            }
            else
            {
                return g.ToString("F1") + "dB";
            }
        }
        
        
        public static string Format_String_PEQ_Q(double q)
        {
            if (q < 1)
            {
                return q.ToString("F3");
            }
            else if (q < 10)
            {
                return q.ToString("F2");
            }
            else
            {
                return q.ToString("F1");
            }
        }

        public static string Format_String_PEQ_F(double f)
        {
            if (f == 0)
            {
                return "10.0Hz";
            }
            else if (f < 100)
            {
                return f.ToString("F1") + "Hz";
            }
            else if (f < 1000)
            {
                return f.ToString("F0") + "Hz";
            }
            else if (f < 10000)
            {
                return (f / 1000.0).ToString("F2") + "kHz";
            }
            else
            {
                return (f / 1000.0).ToString("F1") + "kHz";
            }
        }

        public static string Format_String_PEQ_G(double g)
        {
            if (g == 0)
            {
                return "0dB";
            }
            else if ((g >= -24) && (g <= -10))
            {
                return g.ToString("F1") + "dB";
            }
            else if ((g > -10) && (g < 10))
            {
                return g.ToString("F2") + "dB";
            }
            else
            {
                return g.ToString("F1") + "dB";
            }
        }

        public static string Format_String_Duck_Hold(double hold)
        {

            hold = hold * 1000;
            if (hold < 10)
            {
                return hold.ToString("F2") + "ms";
            }
            else if (hold < 100)
            {
                return hold.ToString("F1") + "ms";
            }
            else if (hold < 1000)
            {
                return hold.ToString("F0") + "ms";
            }
            else if (hold < 10000)
            {
                return (hold / 1000.0).ToString("F2") + "s";
            }
            else
            {
                return (hold / 1000.0).ToString("F1") + "s";
            }
        }


        public static string Format_String_Comp_Threshold(double threshold)
        {
            if (threshold == 0)
            {
                return "0dB";
            }
            else if (threshold == -100)
            {
                return "-100dB";
            }
            else if ((threshold > -100) && (threshold <= -10))
            {
                return threshold.ToString("F1") + "dB";
            }
            else if ((threshold > -10) && (threshold < 10))
            {
                return threshold.ToString("F2") + "dB";
            }
            else
            {
                return threshold.ToString("F1") + "dB";
            }
        }

        public static string Format_String_Comp_Ratio(double ratio)
        {
            if (ratio == 100)
            {
                return "100";
            }
            else if (ratio < 10)
            {
                return ratio.ToString("F2");
            }
            else
            {
                return ratio.ToString("F1");
            }
        }

        public static string Format_String_Comp_Attack(double attack)
        {

            attack = attack * 1000;

            if (attack < 10)
            {
                return (attack).ToString("F2") + "ms";
            }
            else if (attack < 100)
            {
                return (attack).ToString("F1") + "ms";
            }
            else if (attack < 1000)
            {
                return (attack).ToString("F0") + "ms";
            }
            else
            {
                return "1s";
            }
        }

        public static string Format_String_Comp_Release(double release)
        {

            release = release * 1000;
            if (release < 10)
            {
                return release.ToString("F2") + "ms";
            }
            else if (release < 100)
            {
                return release.ToString("F1") + "ms";
            }
            else if (release < 1000)
            {
                return release.ToString("F0") + "ms";
            }
            else if (release < 10000)
            {
                return (release / 1000.0).ToString("F2") + "s";
            }
            else
            {
                return (release / 1000.0).ToString("F1") + "s";
            }
        }

        public static string Format_String_Comp_Knee(double knee)
        {
            if (knee == 0)
            {
                return "0dB";
            }
            else if (knee < 10)
            {
                return knee.ToString("F2") + "dB";
            }
            else
            {
                return knee.ToString("F1") + "dB";
            }
        }

        public static string Format_String_Sine_F(double f)
        {
            if (f == 0)
            {
                return "20.0Hz";
            }
            else if (f < 100)
            {
                return f.ToString("F1") + "Hz";
            }
            else if (f < 1000)
            {
                return f.ToString("F0") + "Hz";
            }
            else if (f < 10000)
            {
                return (f / 1000.0).ToString("F2") + "kHz";
            }
            else
            {
                return (f / 1000.0).ToString("F1") + "kHz";
            }
        }

        //
        public static string Format_String_Delay_MS(double delay)
        {

            return (delay*1000).ToString("F1") + "ms";
        }

        public static string Format_String_Delay_FT(double delay)
        {

            return delay.ToString("F1") + "ft";
        }

        public static string Format_String_Delay_M(double delay)
        {

            return delay.ToString("F1") + "m";
        }
        

        #endregion
    }
}
