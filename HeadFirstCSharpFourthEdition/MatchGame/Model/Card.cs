using System.ComponentModel;
using System.Windows;

namespace MatchGame.Model
{
    public class Card : INotifyPropertyChanged
    {
        private string _text;
        private Visibility _visibility;

        public Card(string text)
        {
            Text = text;
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Text;
        }
    }
}