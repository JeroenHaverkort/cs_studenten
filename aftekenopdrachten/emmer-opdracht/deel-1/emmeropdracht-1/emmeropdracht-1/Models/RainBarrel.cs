using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmeropdracht_1.Models
{
    internal class RainBarrel: LiquidStorage
    {
        private static readonly int[] CAPACITIES = { 80, 100, 120 };
        public RainBarrel(int capacity) : base(capacity)
        {
            if (!CAPACITIES.Contains(capacity))
            {
                throw new ArgumentException("Capacity must be 80, 100 or 120");
            }
        }

        public override int GetMaxCapacity()
        {
            return Int32.MaxValue;
        }
    }
}
