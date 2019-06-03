using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly ViewManager viewManager;

        public ProjectExplorerViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
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

        public ProjectExplorerOnionDiagramsViewModel OnionDiagramsViewModel => new ProjectExplorerOnionDiagramsViewModel(ViewModelFactory, gui.Analysis, viewManager);

        public ProjectExplorerForceFieldDiagramsViewModel ForceFieldDiagramsViewModel => new ProjectExplorerForceFieldDiagramsViewModel(gui.Analysis, viewManager);

        public ProjectExplorerAttitudeImpactDiagramsViewModel AttitudeImpactDiagramsViewModel => new ProjectExplorerAttitudeImpactDiagramsViewModel(gui.Analysis, viewManager);

        public ProjectExplorerStakeholderOverviewTableViewModel StakeholderOverviewTableViewModel => new ProjectExplorerStakeholderOverviewTableViewModel(ViewModelFactory, gui.Analysis, viewManager);

        public StakeholderTypesViewModel StakeholderTypesViewModel => ViewModelFactory.CreateStakeholderTypesViewModel();
    }
}
