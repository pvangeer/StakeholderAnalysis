using System.Collections.ObjectModel;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Data
{
    public class Analysis : NotifyPropertyChangedObservable
    {
        public Analysis()
        {
            OnionDiagrams = new ObservableCollection<OnionDiagram>();
            ForceFieldDiagrams = new ObservableCollection<TwoAxisDiagram>();
            AttitudeImpactDiagrams = new ObservableCollection<TwoAxisDiagram>();
            StakeholderTypes = new ObservableCollection<StakeholderType>();
            Stakeholders = new ObservableCollection<Stakeholder>();
        }

        public ObservableCollection<OnionDiagram> OnionDiagrams { get; }

        public ObservableCollection<TwoAxisDiagram> ForceFieldDiagrams { get; }

        public ObservableCollection<TwoAxisDiagram> AttitudeImpactDiagrams { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }

        public ObservableCollection<StakeholderType> StakeholderTypes { get; }
    }
}