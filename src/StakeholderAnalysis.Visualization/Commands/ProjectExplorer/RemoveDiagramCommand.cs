using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class RemoveDiagramCommand : ICommand
    {
        private readonly IProjectExplorerDiagramViewModel viewModel;

        public RemoveDiagramCommand(IProjectExplorerDiagramViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RemoveDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
