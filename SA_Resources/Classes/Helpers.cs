using System;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources
{
    public sealed class Helpers
    {
        public static Color Lighten(Color inColor, double inAmount)
        {
            return Color.FromArgb(
              inColor.A,
              (int)Math.Min(255, inColor.R + 255 * inAmount),
              (int)Math.Min(255, inColor.G + 255 * inAmount),
              (int)Math.Min(255, inColor.B + 255 * inAmount));
        }

        public static Color Darken(Color inColor, double inAmount)
        {
            return Color.FromArgb(
                inColor.A,
                (int)(inColor.R * inAmount),
                (int)(inColor.G * inAmount),
                (int)(inColor.B * inAmount)
                );

        }
        
    }
}
