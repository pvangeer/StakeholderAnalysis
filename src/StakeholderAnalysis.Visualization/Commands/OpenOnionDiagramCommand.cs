using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class OpenOnionDiagramCommand : ICommand
    {
        private readonly ProjectExplorerOnionDiagramViewModel onionDiagramViewModel;

        public OpenOnionDiagramCommand(ProjectExplorerOnionDiagramViewModel onionDiagramViewModel)
        {
            this.onionDiagramViewModel = onionDiagramViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            onionDiagramViewModel.OpenDiagramInDocumentView();
            
        }

        public event EventHandler CanExecuteChanged;
    }
}
