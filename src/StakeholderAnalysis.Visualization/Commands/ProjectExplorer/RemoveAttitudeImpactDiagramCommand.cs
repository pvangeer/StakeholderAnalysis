using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class RemoveAttitudeImpactDiagramCommand : ICommand
    {
        private readonly ProjectExplorerAttitudeImpactDiagramViewModel viewModel;

        public RemoveAttitudeImpactDiagramCommand(ProjectExplorerAttitudeImpactDiagramViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RemoveAttitudeImpactDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
