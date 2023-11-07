using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmeropdracht_1.Models
{
    internal abstract class LiquidStorage
    {
        public int Capacity { get;  }
        public int Content { get; protected set; }

        public LiquidStorage(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative");
            }

            if (capacity > GetMaxCapacity())
            {
                throw new ArgumentException("Capacity cannot exceed maximum capacity");
            }

            Capacity = capacity;
        }

        public abstract int GetMaxCapacity();

        public void Fill(int volume)
        {
            if (Content + volume > Capacity)
            {
                throw new ArgumentException("Volume exceeds capacity");
            }

            if (volume < 0)
            {
                throw new ArgumentException("Volume cannot be negative");
            }

            Content += volume;
        }

        public void Empty(int volume)
        {
            if (Content - volume < 0)
            {
                throw new ArgumentException("Volume exceeds content");
            }

            if (volume < 0)
            {
                throw new ArgumentException("Volume cannot be negative");
            }

            Content -= volume;
        }

        public void Empty()
        {
            Content = 0;
        }
    }
}
