using MemoryLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryWPFApp.Models
{
    public class GameCardViewModel: INotifyPropertyChanged
    {
        private bool _isVisible;
        public Card Card { get; set; }
        public CardImage CardImage { get; set; }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
