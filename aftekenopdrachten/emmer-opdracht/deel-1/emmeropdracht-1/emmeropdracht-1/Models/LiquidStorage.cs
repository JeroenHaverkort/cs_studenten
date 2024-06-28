using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using emmeropdracht_1.Events;

namespace emmeropdracht_1.Models
{
    public abstract class LiquidStorage
    {
        public delegate void FullEventHandler(object sender, EventArgs e);
        public delegate void OverflowEventHandler(object sender, OverflowEventArgs e);

        public event FullEventHandler Full;
        public event OverflowEventHandler Overflowing;

        public int Capacity { get;  }

        private int _content;
        public int Content 
        {
            get => _content;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Content), "Content cannot be negative.");
                }

                if (value == Capacity)
                {
                    OnFull(EventArgs.Empty);
                }
                else if (value > Capacity)
                {
                    var args = new OverflowEventArgs(value - Capacity);
                    OnOverflowing(args);

                    if (args.Cancel)
                    {
                        value = Content;
                    }
                    else
                    {
                        value = Capacity; 
                    }
                }

                _content = value;
            }
        }

        public LiquidStorage(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative");
            }

            if (capacity > GetMaxCapacity())
            {
                throw new ArgumentException("Capacity cannot exceed maximum capacity");
            }

            Capacity = capacity;

        }

        public abstract int GetMaxCapacity();

        public void Fill(int volume)
        {
            if (volume < 0)
            {
                throw new ArgumentException("Volume cannot be negative");
            } 

            Content += volume;
        }

        public void Empty(int volume)
        {
            if (Content - volume < 0)
            {
                throw new ArgumentException("Volume exceeds content");
            }

            if (volume < 0)
            {
                throw new ArgumentException("Volume cannot be negative");
            }

            Content -= volume;
        }

        public void Empty()
        {
            Content = 0;
        }

        public void OnFull(EventArgs e)
        {
            Full?.Invoke(this, e);
        }

        public void OnOverflowing(OverflowEventArgs e)
        {
            Overflowing?.Invoke(this, e);
        }
    }
}
