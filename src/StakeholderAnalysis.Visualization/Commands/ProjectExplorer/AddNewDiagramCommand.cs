using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class AddNewDiagramCommand : ICommand
    {
        private readonly IExpandableDiagramCollectionViewModel expandableDiagramCollectionViewModel;

        public AddNewDiagramCommand(IExpandableDiagramCollectionViewModel expandableDiagramCollectionViewModel)
        {
            this.expandableDiagramCollectionViewModel = expandableDiagramCollectionViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            expandableDiagramCollectionViewModel.AddNewDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
