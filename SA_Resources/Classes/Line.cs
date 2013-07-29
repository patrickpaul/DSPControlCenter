using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    class Line
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
