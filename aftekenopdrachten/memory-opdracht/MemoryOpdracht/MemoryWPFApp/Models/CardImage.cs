using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MemoryWPFApp.Models
{
    public class CardImage : INotifyPropertyChanged
    {
        private string url;
        private BitmapImage bitmap;

        public int Index { get; set; }

        public string Url
        {
            get => url;
            set
            {
                if (url != value)
                {
                    url = value;
                    OnPropertyChanged(nameof(Url));
                    LoadBitmap();
                }
            }
        }

        public BitmapImage Bitmap
        {
            get => bitmap;
            set
            {
                if (bitmap != value)
                {
                    bitmap = value;
                    OnPropertyChanged(nameof(Bitmap));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadBitmap()
        {
            try
            {
                Bitmap = new BitmapImage(new Uri(Url));
            }
            catch
            {
                Bitmap = null;
            }
        }
    }
}
