using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagram : NotifyPropertyChangedObservable
    {
        public AttitudeImpactDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<Stakeholder>();
        }

        public string Name { get; set; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }
    }
}
