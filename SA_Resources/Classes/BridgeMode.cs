/*
 * File     : BridgeMode.cs
 * Created  : 15 August 2014
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : Enum class for bridge-mode's for low-impedance amplifiers
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */

namespace SA_Resources.DeviceManagement
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
