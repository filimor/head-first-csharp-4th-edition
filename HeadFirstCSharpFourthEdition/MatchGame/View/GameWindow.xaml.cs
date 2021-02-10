using System;
using System.Windows;
using System.Windows.Controls;
using MatchGame.ViewModel;
using MatchGame.ViewModel.Commands;

namespace MatchGame.View
{
    public partial class GameWindow
    {
        public GameWindow()
        {
            InitializeComponent();

            const int size = 4;

            var findMatchCommand = new FindMatchCommand(new GameViewModel());

            for (int i = 0; i < 4; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.RowDefinitions.Add(new RowDefinition());
            }
            Grid.RowDefinitions.Add(new RowDefinition());

            int card = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var button = new Button
                    {
                        Command = findMatchCommand,
                        CommandParameter = card,
                        Content = findMatchCommand.ViewModel.Cards[card].Text,
                        Style = (Style)MainWindow.FindResource("ButtonStyle"),
                        Visibility = findMatchCommand.ViewModel.Cards[card].Visibility
                    };

                    Grid.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    card++;
                }
            }
        }
    }
}
//< Button Grid.Row = "0"
//        Grid.Column = "0"
//        Command = "{Binding FindMatchCommand}"
//        CommandParameter = "0"
//        Content = "{Binding Cards[0].Text}"
//        Style = "{StaticResource ButtonStyle}"
//        Visibility = "{Binding Cards[0].Visibility}" />