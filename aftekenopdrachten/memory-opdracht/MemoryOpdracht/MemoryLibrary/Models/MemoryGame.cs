using MemoryLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLibrary.Models
{
    public class MemoryGame
    {
        public delegate void GameOverEventHandler(object sender, GameOverEventArgs e);
        public event GameOverEventHandler GameWon;

        public IHighscoreRepository HighscoreRepository { get; set; }
        public Guid Id { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int NumberOfPairs { get; set; }
        public bool IsComplete { get; set; }
        public List<Turn> Turns { get; set; }
        public List<Card> GameCards { get; set; }
        public List<Card> MatchedCards { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxHighscoreCount { get; set; }

        public MemoryGame(string playerName, string[] cardvalues, IHighscoreRepository highscoreRepository, int maxHighscoreCount)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));
            if (cardvalues.Count() < 5)
                throw new ArgumentException("Number of pairs must be greater than 5", nameof(cardvalues));

            Id = Guid.NewGuid();
            PlayerName = playerName;
            NumberOfPairs = cardvalues.Count();
            Turns = new List<Turn>();
            GameCards = new List<Card>();
            MatchedCards = new List<Card>();
            CreateGameCards(GameCards, cardvalues);
            ShuffleGameCards(GameCards);
            StartTime = DateTime.Now;
            MaxHighscoreCount = maxHighscoreCount;

            HighscoreRepository = highscoreRepository;
        }

        public void AddPick(int cardNumber)
        {
            if (IsComplete)
                throw new InvalidOperationException("Game is already complete");

            if (cardNumber < 0 || cardNumber >= GameCards.Count)
                throw new ArgumentOutOfRangeException(nameof(cardNumber), "Card number is out of range");

            if (Turns.Count == 0 || Turns.Last().IsComplete())
                AddTurn();

            Card card = GameCards[cardNumber];

            if(MatchedCards.Contains(card))
                throw new InvalidOperationException("Card has already been matched");

            Turns.Last().AddPick(card);
            if (Turns.Last().IsComplete())
            {
                CheckIsMatch();
                CheckEndGame();
            }
        }

        public int CalculateScore(int amountOfCards, DateTime start, DateTime End, List<Turn> turns)
        {
            int timeInSeconds = CalculateSeconds();
            if(timeInSeconds == 0)
            {
                // Prevent divide by zero
                return -1;
            }

            double timeDividedByTurns = timeInSeconds / turns.Count;

            double scoreCalculation = ((amountOfCards * 2) / timeDividedByTurns) * 1000;

            int score = (int) scoreCalculation;
            return score;
        }

        private void CheckIsMatch()
        {
            if (Turns.Last().IsMatch)
                MatchedCards.AddRange(Turns.Last().Picks.Select(p => p.Card));
        }

        private void CheckEndGame()
        {
            if (MatchedCards.Count == GameCards.Count)
                EndGame();
        }

        private void EndGame()
        {
            IsComplete = true;
            EndTime = DateTime.Now;
            Score = CalculateScore(GameCards.Count, StartTime, EndTime, Turns);
            HighscoreRepository.AddHighscore(new MemoryGameScore { 
                GameId = Id, 
                Score = Score, 
                AmountOfTurns = Turns.Count, 
                PlayerName = PlayerName 
            }, MaxHighscoreCount);
            GameWon?.Invoke(this, new GameOverEventArgs() { 
                    Score = Score, 
                    Time = CalculateSeconds()
                });
        }

        private int CalculateSeconds()
        {
            int seconds = (int)(EndTime - StartTime).TotalSeconds;
            return seconds;
        }

        private void AddTurn()
        {
            Turns.Add(new Turn());
        }

        private void CreateGameCards(List<Card> gameCards, string[] cardValues)
        {
            for (int i = 0; i < NumberOfPairs; i++)
            {
                gameCards.Add(new Card { Id = Guid.NewGuid(), Value = cardValues[i] });
                gameCards.Add(new Card { Id = Guid.NewGuid(), Value = cardValues[i] });
            }
        }

        private void ShuffleGameCards(List<Card> gameCards)
        {
            Random random = new Random();
            for (int i = 0; i < gameCards.Count; i++)
            {
                int j = random.Next(i, gameCards.Count);
                Card temp = gameCards[i];
                gameCards[i] = gameCards[j];
                gameCards[j] = temp;
            }
        }
    }

    public class GameOverEventArgs : EventArgs
    {
        public int Score { get; set; }
        public int Time { get; set; }
    }
}
