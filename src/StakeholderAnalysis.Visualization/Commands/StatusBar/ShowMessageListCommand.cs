using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.Commands.StatusBar
{
    public class ShowMessageListCommand : ICommand
    {
        private readonly StatusBarViewModel viewModel;

        public ShowMessageListCommand(StatusBarViewModel statusBarViewModel)
        {
            viewModel = statusBarViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.ShowMessages = viewModel.MessagesViewModel.MessageList.Count != 0;
            viewModel.OnPropertyChanged(nameof(StatusBarViewModel.ShowMessages));
        }

        public event EventHandler CanExecuteChanged;
    }
}