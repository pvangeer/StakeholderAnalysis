using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class AddNewDiagramCommand : ICommand
    {
        private readonly IExpandableContentGroupViewModel expandableContentGroupViewModel;

        public AddNewDiagramCommand(IExpandableContentGroupViewModel expandableContentGroupViewModel)
        {
            this.expandableContentGroupViewModel = expandableContentGroupViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            expandableContentGroupViewModel.AddNewDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
