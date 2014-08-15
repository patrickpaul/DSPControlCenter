using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    /// <summary>The amplifiers current BridgeMode (Applicable for FLX units only at this time)</summary>
    public enum BridgeMode
    {
        /// <summary>Amplifier is in standard 4 Channel Mode</summary>
        FourChannel = 0,
        /// <summary>Amplifier is in 2 channel bridged mode</summary>
        TwoChannel = 1,
        /// <summary>Amplifier is in 2.1 channel bridged mode</summary>
        TwoOneChannel = 2
    }
}
