using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emmeropdracht_1.Exceptions;

namespace emmeropdracht_1.Models
{
    public class RainBarrel: LiquidStorage
    {
        private static readonly int[] CAPACITIES = { 80, 100, 120 };
        public RainBarrel(int capacity) : base(capacity)
        {
            if (!CAPACITIES.Contains(capacity))
            {
                string capacities = CAPACITIES.Aggregate("", (current, cap) => current + (cap + ", "));
                throw new FalseCapacityException(capacity, capacities);
            }
        }

        public override int GetMaxCapacity()
        {
            return Int32.MaxValue;
        }
    }
}
