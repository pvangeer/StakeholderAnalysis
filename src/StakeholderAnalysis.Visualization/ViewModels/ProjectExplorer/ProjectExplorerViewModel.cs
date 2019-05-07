using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
        }

        public ProjectExplorerOnionDiagramsViewModel OnionDiagramsViewModel => new ProjectExplorerOnionDiagramsViewModel(analysis, viewManager);


        public ProjectExplorerForceFieldDiagramsViewModel ForceFieldDiagramsViewModel => new ProjectExplorerForceFieldDiagramsViewModel(analysis, viewManager);

        public ProjectExplorerAttitudeImpactDiagramsViewModel AttitudeImpactDiagramsViewModel => new ProjectExplorerAttitudeImpactDiagramsViewModel(analysis, viewManager);

        public ProjectExplorerStakeholderOverviewTableViewModel StakeholderOverviewTableViewModel => new ProjectExplorerStakeholderOverviewTableViewModel(analysis, viewManager);
    }
}
