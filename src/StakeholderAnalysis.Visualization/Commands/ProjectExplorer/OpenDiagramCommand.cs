using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class OpenDiagramCommand : ICommand
    {
        private readonly IProjectExplorerDiagramViewModel diagramViewModel;

        public OpenDiagramCommand(IProjectExplorerDiagramViewModel diagramViewModel)
        {
            this.diagramViewModel = diagramViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            diagramViewModel.OpenDiagramInDocumentView();
        }

        public event EventHandler CanExecuteChanged;
    }
}
