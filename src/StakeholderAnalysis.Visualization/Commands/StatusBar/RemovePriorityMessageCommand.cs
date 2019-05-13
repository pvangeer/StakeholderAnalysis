using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.Commands.StatusBar
{
    public class RemovePriorityMessageCommand : ICommand
    {
        public RemovePriorityMessageCommand(StatusBarViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        private StatusBarViewModel ViewModel { get; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.PriorityMessage = null;
            ViewModel.OnPropertyChanged(nameof(StatusBarViewModel.PriorityMessage));
        }

        public event EventHandler CanExecuteChanged;
    }
}