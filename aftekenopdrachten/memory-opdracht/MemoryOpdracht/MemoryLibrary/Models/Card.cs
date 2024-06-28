using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLibrary.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public object? Image { get; set; }
        public string Value { get; set; }
    }
}
