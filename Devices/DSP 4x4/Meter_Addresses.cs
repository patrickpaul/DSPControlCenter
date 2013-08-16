using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP_4x4
{
    public static class Meter_Addresses
    {
        public static UInt32[,] _gain_meters = {
        { 1, 2, 3 },
        { 1, 2, 3 },
        { 1, 2, 3 }
        };

        /// <summary>
        /// Get array of dog breeds publicly.
        /// </summary>
        public static UInt32[,] Gains
        {
            get
            {
                return _gain_meters;
            }
        }
    }
}
