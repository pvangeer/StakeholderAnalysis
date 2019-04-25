using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class ToggleForceFieldsListCommand : ICommand
    {
        private readonly ProjectExplorerForceFieldDiagramsViewModel projectExplorerForceFieldDiagramsViewModel;

        public ToggleForceFieldsListCommand(ProjectExplorerForceFieldDiagramsViewModel projectExplorerForceFieldDiagramsViewModel)
        {
            this.projectExplorerForceFieldDiagramsViewModel = projectExplorerForceFieldDiagramsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            projectExplorerForceFieldDiagramsViewModel.IsExpanded =
                !projectExplorerForceFieldDiagramsViewModel.IsExpanded;
        }

        public event EventHandler CanExecuteChanged;
    }
}
