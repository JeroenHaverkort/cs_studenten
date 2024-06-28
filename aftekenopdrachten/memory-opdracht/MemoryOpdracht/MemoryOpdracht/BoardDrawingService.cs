using MemoryDataAccess;
using MemoryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryConsoleApp
{
    public class BoardDrawingService
    {
        public MemoryGame Game { get; set; }

        public HighscoreRepository HighscoreRepository { get; set; }

        public BoardDrawingService(MemoryGame game, HighscoreRepository highscoreRepository) {
            Game = game;
            HighscoreRepository = highscoreRepository;
        }

        public void DrawHighscores()
        {
            Console.Clear();
            Console.WriteLine("Memory Game Highscores");
            Console.WriteLine("-----------");
            var highscores = HighscoreRepository.GetHighscores();
            foreach (var score in highscores)
            {
                Console.WriteLine($"Score: {score.Score}, Player: {score.PlayerName}, Turns: {score.AmountOfTurns}");
            }
            Console.WriteLine();
        }

        public void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine($"Memory Game (id {Game.Id})");
            Console.WriteLine("-----------");
            Console.WriteLine($"Player Name: {Game.PlayerName}");
            Console.WriteLine();

            // Write all the cards as (?) with the numbers next to it
            for (int i = 0; i < Game.GameCards.Count; i++)
            {
                Console.Write($"{i} (");
                var card = Game.GameCards[i];
                if (Game.Turns.Count > 0 && (Game.Turns.Last().Picks.Any(p => p.Card == card) || Game.MatchedCards.Any(c => c == card)))
                {
                    Console.Write($"{card.Value}");
                }
                else
                {
                    Console.Write($"?");
                }
                Console.Write(") ");
            }
            Console.WriteLine();
        }
    }
}
