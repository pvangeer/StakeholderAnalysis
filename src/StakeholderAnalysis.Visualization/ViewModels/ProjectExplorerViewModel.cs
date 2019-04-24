using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ProjectExplorerViewModel : NotifyPropertyChangedObservable
    {
        private Analysis analysis;

        public ProjectExplorerViewModel(Analysis analysis)
        {
            this.analysis = analysis;
            analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;
            analysis.AttitudeImpactDiagrams.CollectionChanged += AttitudeImpactDiagramsCollectionChanged;
            analysis.ForceFieldDiagrams.CollectionChanged += ForceFieldDiagramsCollectionChanged;

            OnionDiagrams = new ObservableCollection<OnionDiagramViewModel>();
            ForceFieldDiagrams = new ObservableCollection<StakeholderForcesDiagramViewModel>();
            AttitudeImpactDiagrams = new ObservableCollection<StakeholderAttitudeImpactDiagramViewModel>();
        }

        public ObservableCollection<StakeholderAttitudeImpactDiagramViewModel> AttitudeImpactDiagrams { get; }

        public ObservableCollection<OnionDiagramViewModel> OnionDiagrams { get; }

        public ObservableCollection<StakeholderForcesDiagramViewModel> ForceFieldDiagrams { get; }

        private void ForceFieldDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AttitudeImpactDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
