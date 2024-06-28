using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmeropdracht_1.Exceptions
{
    public class FalseCapacityException: Exception
    {
        public FalseCapacityException(int capacity, string requiredCapacity) : base("False capacity: "+ capacity + ". required: " + requiredCapacity)
        {
        }
    }
}
