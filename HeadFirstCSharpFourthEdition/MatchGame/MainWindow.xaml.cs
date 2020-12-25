using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = new ();
        private int _tenthsOfSecondsElapsed;
        private int _matchesFound;
        private TextBlock _lastTextBlockClicked;
        private bool _findingMatch = false;
        
        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(.1);
            _timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (_tenthsOfSecondsElapsed / 10f).ToString("0.0s");
            if (_matchesFound != 8)
            {
                return;
            }

            _timer.Stop();
            TimeTextBlock.Text += " - Play again?";
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new()
            {
                "🐷", "🐷",
                "🐯", "🐯",
                "🐴", "🐴",
                "🦊", "🦊",
                "🐢", "🐢",
                "🐟", "🐟",
                "🐓", "🐓",
                "🐪", "🐪"
            };

            Random random = new();

            foreach (var textBlock in MainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "TimeTextBlock")
                {
                    continue;
                }

                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count);
                textBlock.Text = animalEmoji[index];
                animalEmoji.RemoveAt(index);
            }
            
            _timer.Start();
            _tenthsOfSecondsElapsed = 0;
            _matchesFound = 0;

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (!_findingMatch)
            {
                textBlock!.Visibility = Visibility.Hidden;
                _lastTextBlockClicked = textBlock;
                _findingMatch = true;
            }
            else if (textBlock!.Text == _lastTextBlockClicked.Text)
            {
                _matchesFound++;
                textBlock!.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}