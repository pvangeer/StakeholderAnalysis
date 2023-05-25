using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class StakeholderClickedCommand : ICommand
    {
        private readonly StakeholderViewModel stakeholderViewModel;

        public StakeholderClickedCommand(StakeholderViewModel stakeholderViewModel)
        {
            this.stakeholderViewModel = stakeholderViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return stakeholderViewModel != null;
        }

        public void Execute(object parameter)
        {
            stakeholderViewModel.SelectStakeholder();
        }

        public event EventHandler CanExecuteChanged;
    }
}