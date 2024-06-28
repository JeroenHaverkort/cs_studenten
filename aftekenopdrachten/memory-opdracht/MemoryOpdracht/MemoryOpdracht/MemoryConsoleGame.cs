using MemoryDataAccess;
using MemoryLibrary.Models;

namespace MemoryConsoleApp
{
    public class MemoryConsoleGame
    {
        public static readonly string[] CARD_VALUES = { "A", "B", "C", "D", "E" };
        public static readonly string DATABASE_CONNECTIONSTRING =
            "Server=(localdb)\\mssqllocaldb;Database=MemoryOpdracht;";

        public BoardDrawingService BoardDrawingService { get; set; }
        public MemoryGame MemoryGame { get; set; }
        public HighscoreRepository HighscoreRepository { get; set; }
        public bool GameOver { get; set; }

        public MemoryConsoleGame()
        {
            HighscoreRepository = new HighscoreRepository(DATABASE_CONNECTIONSTRING);
            CreateGame();
        }

        public void CreateGame()
        {
            Console.WriteLine("Hello, Player!");
            string playerName = AskForName();

            try
            {
                MemoryGame = new MemoryGame(playerName, CARD_VALUES, HighscoreRepository);
                MemoryGame.GameWon += OnGameWon;
                BoardDrawingService = new BoardDrawingService(MemoryGame, HighscoreRepository);
                BoardDrawingService.DrawHighscores();
                Console.WriteLine("Press any key to start the game");
                Console.ReadLine();
                BoardDrawingService.DrawBoard();
                while (!GameOver)
                {
                    AskForPick();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CreateGame();
            }
        }

        public void AskForPick()
        {
            Console.WriteLine("Enter the number of the card you want to pick: ");
            string input = Console.ReadLine();
            int cardNumber;
            try
            {
                cardNumber = int.Parse(input);
                MemoryGame.AddPick(cardNumber);
                if (!GameOver)
                {
                    BoardDrawingService.DrawBoard();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please enter a valid number");
                AskForPick();
            }
        }

        public string AskForName()
        {
           Console.WriteLine("Enter your name: ");
           string playerName = Console.ReadLine();
           return playerName;
        }

        public void OnGameWon(object sender, GameOverEventArgs e)
        {
            BoardDrawingService.DrawBoard();
            Console.WriteLine("Congratulations! You won the game!");
            Console.WriteLine($"Your score is {MemoryGame.Score}");
            GameOver = true;
        }
    }
}
