using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class RemoveForceFieldDiagramCommand : ICommand
    {
        private readonly ProjectExplorerForceFieldDiagramViewModel viewModel;

        public RemoveForceFieldDiagramCommand(ProjectExplorerForceFieldDiagramViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RemoveForceFieldDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
