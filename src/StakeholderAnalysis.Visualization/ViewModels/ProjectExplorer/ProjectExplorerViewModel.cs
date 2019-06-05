using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;

        public ProjectExplorerViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            this.gui = gui;
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

        public ProjectExplorerOnionDiagramsViewModel OnionDiagramsViewModel => ViewModelFactory.CreateProjectExplorerOnionDiagramsViewModel(gui.Analysis);

        public ProjectExplorerForceFieldDiagramsViewModel ForceFieldDiagramsViewModel => ViewModelFactory.CreateProjectExplorerForceFieldDiagramsViewModel(gui.Analysis);

        public ProjectExplorerAttitudeImpactDiagramsViewModel AttitudeImpactDiagramsViewModel => ViewModelFactory.CreateProjectExplorerAttitudeImpactDiagramsViewModel(gui.Analysis);

        public ProjectExplorerStakeholderOverviewTableViewModel StakeholderOverviewTableViewModel => ViewModelFactory.CreateProjectExplorerStakeholderOverviewTableViewModel(gui.Analysis);

        public StakeholderTypesViewModel StakeholderTypesViewModel => ViewModelFactory.CreateStakeholderTypesViewModel();
    }
}
