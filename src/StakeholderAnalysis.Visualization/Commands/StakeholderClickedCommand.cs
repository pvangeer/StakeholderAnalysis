using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class StakeholderClickedCommand : ICommand
    {
        private readonly StakeholderViewModel viewModel;

        public StakeholderClickedCommand(StakeholderViewModel stakeholderViewModel)
        {
            viewModel = stakeholderViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Select();
        }

        public event EventHandler CanExecuteChanged;
    }
}