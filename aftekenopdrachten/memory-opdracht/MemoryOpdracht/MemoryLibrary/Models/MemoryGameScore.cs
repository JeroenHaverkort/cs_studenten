using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLibrary.Models
{
    public class MemoryGameScore
    {
        public Guid GameId { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int AmountOfTurns { get; set; }
    }
}
