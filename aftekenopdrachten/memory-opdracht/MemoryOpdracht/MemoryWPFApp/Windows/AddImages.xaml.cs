using MemoryWPFApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
namespace MemoryWPFApp.Windows
{
    /// <summary>
    /// Interaction logic for AddImages.xaml
    /// </summary>
    public partial class AddImages : Window
    {
        private readonly string[] STANDARD_URLS = { "https://picsum.photos/id/21/3008/2008",
            "https://picsum.photos/id/22/4434/3729", "https://picsum.photos/id/23/3887/4899",
            "https://picsum.photos/id/24/4855/1803", "https://picsum.photos/id/28/4928/3264"
        };
        private readonly int MIN_IMAGES = 5;

        public ObservableCollection<CardImage> Images { get; set; }

        private string _name;
        public AddImages(string name)
        {
            Images = new ObservableCollection<CardImage>();
            AddStandardImages();
            _name = name;
            InitializeComponent();
            ImageListBox.ItemsSource = Images;
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            Images.Add(new CardImage { Index = Images.Count + 1, Url="" });

        }

        private void RemoveImage(object sender, RoutedEventArgs e)
        {
            if (Images.Count > 0)
                Images.RemoveAt(Images.Count - 1);

        }

        private void UseStandardImages(object sender, RoutedEventArgs e)
        {
            AddStandardImages();
        }

        private void SubmitImages(object sender, RoutedEventArgs e) 
        {
            if (Images.Count < MIN_IMAGES) 
            { 
                MessageBox.Show($"Please add at least {MIN_IMAGES} images"); 
                return; 
            }

            if (!TryLoadImages()) 
            {
                return;
            }

            MemoryGameWindow memoryGame = new(_name, Images);
            this.Visibility = Visibility.Hidden;
            memoryGame.Show();
        }

        private void AddStandardImages()
        {
            Images.Clear();
            for (int i = 0; i < STANDARD_URLS.Length; i++)
            {
                Images.Add(new CardImage { Index = i + 1, Url = STANDARD_URLS[i] });
            }
        }

        private bool TryLoadImages()
        {
            foreach (CardImage image in Images)
            {
                if (image.Url == null || image.Url == "")
                {
                    MessageBox.Show("Please enter a URL for all images");
                    return false;
                }
                try
                {
                    image.Bitmap = new BitmapImage(new Uri(image.Url));
                }
                catch
                {
                    MessageBox.Show("Invalid URL: " + image.Url);
                    return false;
                }
            }
            return true;
        }

    }
}
