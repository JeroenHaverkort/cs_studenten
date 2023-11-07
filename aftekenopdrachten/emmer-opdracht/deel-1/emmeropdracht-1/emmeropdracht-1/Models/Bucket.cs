using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmeropdracht_1.Models
{
    internal class Bucket: LiquidStorage
    {
        private const int BASE_CAPACITY = 12;
        private const int MIN_CAPACITY = 10;
        public Bucket(int capacity) : base(capacity)
        {
            if (capacity < MIN_CAPACITY)
            {
                throw new ArgumentException("Capacity cannot be less than 10");
            }
        }

        public Bucket() : base(BASE_CAPACITY)
        {
        }

        public void Fill(Bucket bucket)
        {
            int newContent = Content + bucket.Content;
            if (newContent > Capacity)
            {
                throw new ArgumentException("Bucket content cannot exceed bucket capacity");
            }

            Content += bucket.Content;
            bucket.Empty(bucket.Content);
        }

        public override int GetMaxCapacity()
        {
            return 2500;
        }
    }
}
