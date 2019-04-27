using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class ToggleIsExpandedCommand : ICommand
    {
        private readonly IExpandableContentGroupViewModel expandableGroupViewModel;

        public ToggleIsExpandedCommand(IExpandableContentGroupViewModel expandableGroupViewModel)
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
