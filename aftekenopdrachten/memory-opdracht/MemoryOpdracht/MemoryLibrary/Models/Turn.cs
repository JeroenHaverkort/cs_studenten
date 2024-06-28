using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLibrary.Models
{
    public class Turn
    {
        public const int MaxPicks = 2;

        public Guid Id { get; set; }
        public List<Pick> Picks { get; set; }
        public bool IsMatch { get; set;}

        public Turn()
        {
            Id = Guid.NewGuid();
            Picks = new List<Pick>();
        }

        public void AddPick(Card card)
        {
            if (IsComplete())
                throw new InvalidOperationException("Cannot add more than 2 picks to a turn");

            if(Picks.Any(p => p.Card.Id == card.Id))
                throw new InvalidOperationException("Cannot pick the same card twice");

            Picks.Add(new Pick { Id = Guid.NewGuid() , Card = card });
            EvaluatePicks();
        }

        public void EvaluatePicks()
        {
            if (!IsComplete())
                return;

            IsMatch = Picks[0].Card.Value == Picks[1].Card.Value;
        }

        public bool IsComplete()
        {
            return Picks.Count == MaxPicks;
        }
    }
}
