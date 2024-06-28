using MemoryWPFApp.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MemoryWPFApp.Windows
{
    public partial class MemoryGameWindow : Window
    {
        private ObservableCollection<CardImage> _images;
        private string _name;
        private MemoryLibrary.Models.MemoryGame _memoryGame;
        private ObservableCollection<GameCardViewModel> _gameCardViewModels;

        public MemoryGameWindow(string name, ObservableCollection<CardImage> images)
        {
            _name = name;
            _images = images;
            InitializeComponent();
            Title.Text = $"Memory Card Game - {name}";
            CreateGame();
            CreateViewModels();
        }

        private void MemoryGame_GameWon(object sender, MemoryLibrary.Models.GameOverEventArgs e)
        {
            MessageBox.Show($"Congratulations {_name}, you won the game with a score of {e.Score}");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void OnCardTurnClick(object sender, RoutedEventArgs e)
        {
            //get the card that was clicked
            var card = (sender as Button).DataContext as GameCardViewModel;
            if(card == null)
                return;
            //get the index of the card
            var index = _gameCardViewModels.IndexOf(card);
            //turn the card
            try
            {
                _memoryGame.AddPick(index);
                //update the UI
                UpdateCardUI();
            } catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateGame()
        {
            var images = _images.Select(i => i.Index.ToString()).ToArray();
            _memoryGame = new MemoryLibrary.Models.MemoryGame(_name, images, MainWindow.HighscoreRepository);
            _memoryGame.GameWon += MemoryGame_GameWon;
        }

        private void CreateViewModels()
        {
            _gameCardViewModels = new ObservableCollection<GameCardViewModel>();
            foreach (var card in _memoryGame.GameCards)
            {
                var cardImage = _images.FirstOrDefault(i => i.Index.ToString() == card.Value);
                _gameCardViewModels.Add(new GameCardViewModel
                {
                    Card = card,
                    CardImage = cardImage,
                });
            }
            CardListBox.ItemsSource = _gameCardViewModels;
        }

        private void UpdateCardUI()
        {
            foreach (var card in _gameCardViewModels)
            {
                card.IsVisible = _memoryGame.Turns.Count > 0 && (_memoryGame.Turns.Last().Picks.Any(p => p.Card == card.Card) || _memoryGame.MatchedCards.Contains(card.Card));
            }
        }
    }
}
