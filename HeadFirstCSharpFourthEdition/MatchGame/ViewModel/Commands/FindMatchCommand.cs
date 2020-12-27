using System;
using System.Windows.Input;

namespace MatchGame.ViewModel.Commands
{
    public class FindMatchCommand : ICommand
    {
        public FindMatchCommand(GameViewModel viewModel)
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
            ViewModel.FindMatch(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}