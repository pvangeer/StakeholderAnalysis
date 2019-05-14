using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class ToggleIsExpandedCommand : ICommand
    {
        private readonly IExpandableContentViewModel expandableGroupViewModel;

        public ToggleIsExpandedCommand(IExpandableContentViewModel expandableGroupViewModel)
        {
            this.expandableGroupViewModel = expandableGroupViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            expandableGroupViewModel.IsExpanded =
                !expandableGroupViewModel.IsExpanded;
        }

        public event EventHandler CanExecuteChanged;
    }
}
