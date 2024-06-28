
using MemoryLibrary.Models;
using MemoryLibrary.Interfaces;
using Moq;

namespace MemoryLibraryTests
{
    public class MemoryGameTests
    {
        [Fact]
        public void MemoryGame_Constructor_ShouldThrowArgumentException_WhenPlayerNameIsNull()
        {
            // Arrange
            string playerName = null;
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            // Act
            var game = () => new MemoryGame(playerName, cardValues, highscoreRepo.Object);

            // Assert
            Assert.Throws<ArgumentException>(game);
        }

        [Fact]
        public void MemoryGame_Constructor_ShouldThrowArgumentException_WhenPlayerNameIsEmpty()
        {
            // Arrange
            string playerName = string.Empty; 
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();

            // Act
            var game = () => new MemoryGame(playerName, cardValues, highscoreRepo.Object);

            // Assert
            Assert.Throws<ArgumentException>(game);
        }

        [Fact]
        public void MemoryGame_Constructor_ShouldThrowArgumentException_WhenNumberOfPairsIsLessThan5()
        {
            // Arrange
            string playerName = "Player";
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var cardValues = new string[] { "A", "B", "C", "D" };

            // Act
            var game = () => new MemoryGame(playerName, cardValues, highscoreRepo.Object);

            // Assert
            Assert.Throws<ArgumentException>(game);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldThrowInvalidOperationException_WhenGameIsComplete()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);
            game.IsComplete = true;

            // Act
            var addPick = () => game.AddPick(0);

            // Assert
            Assert.Throws<InvalidOperationException>(addPick);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldThrowArgumentOutOfRangeException_WhenCardNumberIsLessThan0()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);

            // Act
            var addPick = () => game.AddPick(-1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(addPick);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldThrowArgumentOutOfRangeException_WhenCardNumberIsGreaterThanEqualGameCardsCount()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);

            // Act
            var addPick = () => game.AddPick(game.GameCards.Count);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(addPick);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldAddTurn_WhenNoTurnsExist()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);

            // Act
            game.AddPick(0);

            // Assert
            Assert.Single(game.Turns);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldAddTurn_WhenLastTurnIsComplete()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);
            game.AddPick(0);
            game.AddPick(1);

            // Act
            game.AddPick(2);

            // Assert
            Assert.Equal(2, game.Turns.Count);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldAddPickToLastTurn()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);

            // Act
            game.AddPick(0);

            // Assert
            Assert.Single(game.Turns.Last().Picks);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldThrowInvalidOperationException_WhenCardHasAlreadyBeenMatched()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);
            game.MatchedCards.Add(game.GameCards[0]);

            // Act
            var addPick = () => game.AddPick(0);

            // Assert
            Assert.Throws<InvalidOperationException>(addPick);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldCheckIsMatchAndAddMatchedCard_WhenTurnIsComplete()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);
            
            // set up a match so we can test the match logic
            game.GameCards[0].Value = "A";
            game.GameCards[1].Value = "A";

            // Act
            game.AddPick(0);
            game.AddPick(1);

            // Assert
            Assert.Contains(game.GameCards[0], game.MatchedCards);
            Assert.Contains(game.GameCards[1], game.MatchedCards);
        }

        [Fact]
        public void MemoryGame_AddPick_ShouldCheckEndGame_WhenTurnIsComplete()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);

            game.GameCards[0].Value = "A";
            game.GameCards[1].Value = "A";
            game.GameCards[2].Value = "B";
            game.GameCards[3].Value = "B";
            game.GameCards[4].Value = "C";
            game.GameCards[5].Value = "C";
            game.GameCards[6].Value = "D";
            game.GameCards[7].Value = "D";
            game.GameCards[8].Value = "E";
            game.GameCards[9].Value = "E";

            // Act
            game.AddPick(0);
            game.AddPick(1);
            game.AddPick(2);
            game.AddPick(3);
            game.AddPick(4);
            game.AddPick(5);
            game.AddPick(6);
            game.AddPick(7);
            game.AddPick(8);
            game.AddPick(9);

            // Assert
            Assert.True(game.IsComplete);
        }

        [Fact]
        public void MemoryGame_CalculateScore_ShouldReturnNegativeOne_WhenTimeInSecondsIsZero()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);
            game.StartTime = DateTime.Now;
            game.EndTime = DateTime.Now;

            // Act
            var score = game.CalculateScore(10, game.StartTime, game.EndTime, game.Turns);

            // Assert
            Assert.Equal(-1, score);
        }

        [Fact]
        public void MemoryGame_CalculateScore_ShouldReturnScore_WhenTimeInSecondsIsGreaterThanZero()
        {
            // Arrange
            var cardValues = new string[] { "A", "B", "C", "D", "E" };
            var highscoreRepo = new Mock<IHighscoreRepository>();
            var game = new MemoryGame("Player", cardValues, highscoreRepo.Object);
            game.StartTime = DateTime.Now;
            game.EndTime = DateTime.Now.AddSeconds(10);
            game.Turns.Add(new Turn());
            game.Turns.Add(new Turn());

            double calculation = ((10 * 2) / (10 / game.Turns.Count)) * 1000;
            var expected = (int) calculation;

            // Act
            var score = game.CalculateScore(10, game.StartTime, game.EndTime, game.Turns);

            // Assert
            Assert.Equal(expected, score);
        }
    }
}
