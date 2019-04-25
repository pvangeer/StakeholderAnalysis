using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class OpenAttitudeImpactDiagramCommand : ICommand
    {
        private readonly ProjectExplorerAttitudeImpactDiagramViewModel attitudeImpactDiagramViewModel;

        public OpenAttitudeImpactDiagramCommand(ProjectExplorerAttitudeImpactDiagramViewModel attitudeImpactDiagramViewModel)
        {
            this.attitudeImpactDiagramViewModel = attitudeImpactDiagramViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            attitudeImpactDiagramViewModel.OpenDiagramInDocumentView();
            
        }

        public event EventHandler CanExecuteChanged;
    }
}
