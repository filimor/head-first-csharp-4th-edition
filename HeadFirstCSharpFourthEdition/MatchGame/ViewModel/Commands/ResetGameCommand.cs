using System;
using System.Windows.Input;

namespace MatchGame.ViewModel.Commands
{
    public class ResetGameCommand : ICommand
    {
        public ResetGameCommand(GameViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public GameViewModel ViewModel { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.ResetGame();
        }

        public event EventHandler CanExecuteChanged;
    }
}