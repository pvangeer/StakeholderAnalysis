using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class OpenForceFieldDiagramCommand : ICommand
    {
        private readonly ProjectExplorerForceFieldDiagramViewModel forceFieldDiagramViewModel;

        public OpenForceFieldDiagramCommand(ProjectExplorerForceFieldDiagramViewModel forceFieldDiagramViewModel)
        {
            this.forceFieldDiagramViewModel = forceFieldDiagramViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            forceFieldDiagramViewModel.OpenDiagramInDocumentView();
            
        }

        public event EventHandler CanExecuteChanged;
    }
}
