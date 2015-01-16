/*
 * File     : DSP_Setting.cs
 * Created  : 28 July 2013
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : Enum that holds all the information for a DSP_Setting stored on the device.
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */

using System;

namespace SA_Resources.DSP
{
    public class DSP_Setting
    {
        public DSP_Setting(uint new_index, string new_name, UInt32 default_value)
        {
            Index = new_index;
            Name = new_name;
            Value = default_value;
        }

        public uint Index;

        public string Name;

        public UInt32 Value;
    }
}
