using MemoryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLibrary.Interfaces
{
    public interface IHighscoreRepository
    {
        void AddHighscore(MemoryGameScore highscore, int maxHighscoreCount);
        List<MemoryGameScore> GetHighscores();
    }
}
