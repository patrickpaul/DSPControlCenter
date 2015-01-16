/*
 * File     : LiveQueueItem.cs
 * Created  : 16 August 2013
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : Class that stores data to be queued to be sent to the device.
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */

using System;

namespace SA_Resources.USB
{
    public class LiveQueueItem
    {
        public int Index;
        public UInt32 Value;
        public string ValueString;
        
        
        public LiveQueueItem(int new_index, UInt32 new_value)
        {
            Index = new_index;
            Value = new_value;
            ValueString = "";
        }

        public LiveQueueItem(int new_index, String _valueString)
        {
            Index = new_index;
            Value = 0xFFFFFFFF;
            ValueString = _valueString;
        }

        
    }
}
