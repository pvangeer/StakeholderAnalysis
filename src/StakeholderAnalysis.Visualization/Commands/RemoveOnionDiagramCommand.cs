using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class RemoveOnionDiagramCommand : ICommand
    {
        private readonly ProjectExplorerOnionDiagramViewModel viewModel;

        public RemoveOnionDiagramCommand(ProjectExplorerOnionDiagramViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RemoveOnionDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
