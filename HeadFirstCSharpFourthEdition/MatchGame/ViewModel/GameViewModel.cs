using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using MatchGame.Model;
using MatchGame.ViewModel.Commands;

namespace MatchGame.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer = new();
        private TrulyObservableCollection<Card> _cards;
        private bool _findingMatch;
        private int _lastCard;
        private int _timeElapsed;
        private string _timeElapsedLabel;

        public GameViewModel()
        {
            FindMatchCommand = new FindMatchCommand(this);
            ResetGameCommand = new ResetGameCommand(this);
            Cards = new TrulyObservableCollection<Card>();
            Cards.CollectionChanged += OnCollectionChanged;
            SetUpGame();
        }

        public string TimeElapsedLabel
        {
            get => _timeElapsedLabel;
            set
            {
                _timeElapsedLabel = value;
                OnPropertyChanged(nameof(TimeElapsedLabel));
            }
        }

        public FindMatchCommand FindMatchCommand { get; set; }

        public ResetGameCommand ResetGameCommand { get; set; }

        public int BoardSize { get; } = 16; // TODO: Custom Boardsize

        public int MatchesFound { get; private set; }

        public TrulyObservableCollection<Card> Cards
        {
            get => _cards;
            set
            {
                _cards = value;
                OnPropertyChanged(nameof(Cards));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                TimeElapsedLabel = "0.0s";
                return;
            }

            _timeElapsed++;
            TimeElapsedLabel = (_timeElapsed / 10f).ToString("0.0s");
            if (MatchesFound != 8)
            {
                return;
            }

            _timer.Stop();
            TimeElapsedLabel += " - Play again?";
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        public void FindMatch(object parameter)
        {
            var index = Convert.ToInt32(parameter);
            if (!_findingMatch)
            {
                Cards[index].Visibility = Visibility.Hidden;
                _lastCard = index;
                _findingMatch = true;
            }
            else if (Cards[index].Text == Cards[_lastCard].Text)
            {
                MatchesFound++;
                Cards[index].Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                Cards[_lastCard].Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

        public void SetUpGame()
        {
            List<string> animalEmoji = new()
            {
                "🐷",
                "🐷",
                "🐯",
                "🐯",
                "🐴",
                "🐴",
                "🦊",
                "🦊",
                "🐢",
                "🐢",
                "🐟",
                "🐟",
                "🐓",
                "🐓",
                "🐪",
                "🐪"
            };

            Random random = new();
            Cards.Clear();

            for (var i = 0; i < BoardSize; i++)
            {
                int index = random.Next(animalEmoji.Count);
                Cards.Add(new Card(animalEmoji[index]));
                animalEmoji.RemoveAt(index);
            }

            _timer.Interval = TimeSpan.FromSeconds(.1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _timeElapsed = 0;
            MatchesFound = 0;
        }

        public void ResetGame()
        {
            if (MatchesFound == BoardSize / 2)
            {
                SetUpGame();
            }
        }
    }
}