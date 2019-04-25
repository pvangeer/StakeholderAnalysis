using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class ToggleAttitudeImpactDiagramsListCommand : ICommand
    {
        private readonly ProjectExplorerAttitudeImpactDiagramsViewModel projectExplorerAttitudeImpactDiagramsViewModel;

        public ToggleAttitudeImpactDiagramsListCommand(ProjectExplorerAttitudeImpactDiagramsViewModel projectExplorerAttitudeImpactDiagramsViewModel)
        {
            this.projectExplorerAttitudeImpactDiagramsViewModel = projectExplorerAttitudeImpactDiagramsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            projectExplorerAttitudeImpactDiagramsViewModel.IsExpanded =
                !projectExplorerAttitudeImpactDiagramsViewModel.IsExpanded;
        }

        public event EventHandler CanExecuteChanged;
    }
}
