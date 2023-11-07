using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmeropdracht_1.Models
{
    internal class OilBarrel: LiquidStorage
    {
        private const int CAPACITY = 159;
        public OilBarrel() : base(CAPACITY)
        {
        }

        public override int GetMaxCapacity()
        {
            return Int32.MaxValue;
        }
    }
}
