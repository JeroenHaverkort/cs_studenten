using MemoryDataAccess;
using MemoryLibrary.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryWPFApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly string DATABASE_CONNECTIONSTRING =
            "Server=(localdb)\\mssqllocaldb;Database=MemoryOpdracht;";
        public static HighscoreRepository HighscoreRepository = new HighscoreRepository(DATABASE_CONNECTIONSTRING);

        public List<MemoryGameScore> GameScores { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var highscores = HighscoreRepository.GetHighscores();
            GameScores = highscores;
            HighscoreListView.DataContext = GameScores;

            Name.Text = "ExampleName";
        }

        private void OnPlay(object sender, RoutedEventArgs e)
        {
            var name = Name.Text;
            if(string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a name");
                return;
            }

            var imageWindow = new AddImages(name);
            this.Visibility = Visibility.Hidden;
            imageWindow.Show();
        }

    }
}