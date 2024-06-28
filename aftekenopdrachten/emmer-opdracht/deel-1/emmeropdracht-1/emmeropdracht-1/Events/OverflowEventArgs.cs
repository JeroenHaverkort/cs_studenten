using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmeropdracht_1.Events
{
    public class OverflowEventArgs
    {
        public int OverflowAmount { get; set; }
        public bool Cancel { get; set; }

        public OverflowEventArgs(int overflowAmount)
        {
            OverflowAmount = overflowAmount;
        }
    }
}
