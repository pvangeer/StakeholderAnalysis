using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ProjectExplorerViewModel : NotifyPropertyChangedObservable
    {
        private Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.AttitudeImpactDiagrams.CollectionChanged += AttitudeImpactDiagramsCollectionChanged;
            analysis.ForceFieldDiagrams.CollectionChanged += ForceFieldDiagramsCollectionChanged;

            ForceFieldDiagrams = new ObservableCollection<StakeholderForcesDiagramViewModel>();
            AttitudeImpactDiagrams = new ObservableCollection<AttitudeImpactDiagramViewModel>();
        }

        public ObservableCollection<AttitudeImpactDiagramViewModel> AttitudeImpactDiagrams { get; }

        
        public ObservableCollection<StakeholderForcesDiagramViewModel> ForceFieldDiagrams { get; }

        public ProjectExplorerOnionDiagramsViewModel OnionDiagramsViewModel => new ProjectExplorerOnionDiagramsViewModel(analysis, viewManager);

        private void ForceFieldDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AttitudeImpactDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        
    }
}
