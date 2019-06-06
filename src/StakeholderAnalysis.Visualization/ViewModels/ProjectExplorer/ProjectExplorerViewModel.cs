using System.ComponentModel;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : ViewModelBase
    {
        public ProjectExplorerViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            gui.PropertyChanged += GuiPropertyChanged;
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.Analysis):
                    OnPropertyChanged(nameof(OnionDiagramsViewModel));
                    OnPropertyChanged(nameof(ForceFieldDiagramsViewModel));
                    OnPropertyChanged(nameof(AttitudeImpactDiagramsViewModel));
                    OnPropertyChanged(nameof(StakeholderOverviewTableViewModel));
                    OnPropertyChanged(nameof(StakeholderTypesViewModel));
                    break;
            }
        }

        public ProjectExplorerOnionDiagramsViewModel OnionDiagramsViewModel => ViewModelFactory.CreateProjectExplorerOnionDiagramsViewModel();

        public ProjectExplorerForceFieldDiagramsViewModel ForceFieldDiagramsViewModel => ViewModelFactory.CreateProjectExplorerForceFieldDiagramsViewModel();

        public ProjectExplorerAttitudeImpactDiagramsViewModel AttitudeImpactDiagramsViewModel => ViewModelFactory.CreateProjectExplorerAttitudeImpactDiagramsViewModel();

        public ProjectExplorerStakeholderOverviewTableViewModel StakeholderOverviewTableViewModel => ViewModelFactory.CreateProjectExplorerStakeholderOverviewTableViewModel();

        public StakeholderTypesViewModel StakeholderTypesViewModel => ViewModelFactory.CreateStakeholderTypesViewModel();
    }
}
