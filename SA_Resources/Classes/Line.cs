/*
 * File     : Line.cs
 * Created  : 28 July 2013
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : A class that performs line calculations (slope and values)
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */
namespace SA_Resources.Utilities
{
    public class Line
    {
        private double Intercept;
        private double Slope;

        public Line(double _slope, double _intercept = 0)
        {
            Slope = _slope;
            Intercept = _intercept;
        }
        
        public Line(double x1, double y1, double x2, double y2)
        {
            Slope = (y2 - y1)/(x2 - x1);
            Intercept = y1 - x1*Slope;
        }

        public double ValueAt(double x)
        {
            return Slope*x + Intercept;
        }
    }
}
