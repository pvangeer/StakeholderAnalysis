using System.Collections.ObjectModel;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Data
{
    public class Analysis : NotifyPropertyChangedObservable
    {
        public Analysis(ObservableCollection<Stakeholder> stakeholders = null)
        {
            OnionDiagrams = new ObservableCollection<OnionDiagram>();
            ForceFieldDiagrams = new ObservableCollection<ForceFieldDiagram>();
            AttitudeImpactDiagrams = new ObservableCollection<AttitudeImpactDiagram>();
            Stakeholders = stakeholders ?? new ObservableCollection<Stakeholder>();
        }

        public ObservableCollection<OnionDiagram> OnionDiagrams { get; }

        public ObservableCollection<ForceFieldDiagram> ForceFieldDiagrams { get; }

        public ObservableCollection<AttitudeImpactDiagram> AttitudeImpactDiagrams { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }
    }
}