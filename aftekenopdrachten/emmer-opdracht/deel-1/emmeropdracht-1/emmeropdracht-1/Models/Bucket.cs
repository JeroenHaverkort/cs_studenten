using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emmeropdracht_1.Events;
using emmeropdracht_1.Exceptions;

namespace emmeropdracht_1.Models
{
    public class Bucket: LiquidStorage
    {
        private const int BASE_CAPACITY = 12;
        private const int MIN_CAPACITY = 10;

        public Bucket(int capacity) : base(capacity)
        {
            if (capacity < MIN_CAPACITY)
            {
                throw new FalseCapacityException(capacity, MIN_CAPACITY.ToString());
            }
        }

        public Bucket() : this(BASE_CAPACITY)
        {
        }

        public void Fill(Bucket bucket)
        {
            this.Fill(bucket.Content);

            bucket.Empty();
        }

        public override int GetMaxCapacity()
        {
            return 2500;
        }
    }
}
