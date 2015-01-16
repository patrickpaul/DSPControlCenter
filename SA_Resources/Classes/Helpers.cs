/*
 * File     : Helpers.cs
 * Created  : 28 July 2013
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : Helper class that performs some color modifications and form resizing.
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources.Utilities
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

        // The dimensions are based off of a SystemInformation.FrameBorderSize of 3
        // Normalize dimensions to this
        public static int NormalizeFormDimension(int newWidth)
        {
            return newWidth - ((SystemInformation.FrameBorderSize.Width - 3)*2);
        }
        
    }
}
