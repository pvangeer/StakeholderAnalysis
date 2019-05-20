using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : NotifyPropertyChangedObservable
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly ViewManager viewManager;

        public ProjectExplorerViewModel(StakeholderAnalysisGui gui, ViewManager viewManager)
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
                    break;
            }
        }

        public ProjectExplorerOnionDiagramsViewModel OnionDiagramsViewModel => new ProjectExplorerOnionDiagramsViewModel(gui.Analysis, viewManager);

        public ProjectExplorerForceFieldDiagramsViewModel ForceFieldDiagramsViewModel => new ProjectExplorerForceFieldDiagramsViewModel(gui.Analysis, viewManager);

        public ProjectExplorerAttitudeImpactDiagramsViewModel AttitudeImpactDiagramsViewModel => new ProjectExplorerAttitudeImpactDiagramsViewModel(gui.Analysis, viewManager);

        public ProjectExplorerStakeholderOverviewTableViewModel StakeholderOverviewTableViewModel => new ProjectExplorerStakeholderOverviewTableViewModel(gui.Analysis, viewManager);
    }
}
