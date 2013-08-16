using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{
    public class LiveQueueItem
    {
        public LiveQueueItem(int new_index, UInt32 new_value)
        {
            Index = new_index;
            Value = new_value;
        }

        public int Index;


        public UInt32 Value;
    }
}
