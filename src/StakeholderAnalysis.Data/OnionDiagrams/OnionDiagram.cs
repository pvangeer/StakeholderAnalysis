using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class OnionDiagram : NotifyPropertyChangedObservable
    {
        public OnionDiagram(string name, ObservableCollection<OnionRing> onionRings = null, 
            ObservableCollection<StakeholderConnection> connections = null,
            ObservableCollection<StakeholderConnectionGroup> connectionGroups = null)
        {
            Name = name;
            OnionRings = onionRings ?? new ObservableCollection<OnionRing>();
            Stakeholders = new ObservableCollection<OnionDiagramStakeholder>();
            Connections = connections ?? new ObservableCollection<StakeholderConnection>();
            ConnectionGroups = connectionGroups ?? new ObservableCollection<StakeholderConnectionGroup>();
            Asymmetry = 0.7;
        }

        public OnionDiagram() : this("") { }

        public string Name { get; set; }

        public double Asymmetry { get; set; }

        public ObservableCollection<OnionDiagramStakeholder> Stakeholders { get; }

        public ObservableCollection<OnionRing> OnionRings { get; }

        public ObservableCollection<StakeholderConnection> Connections { get; }

        public ObservableCollection<StakeholderConnectionGroup> ConnectionGroups { get; }
    }
}
