using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public class Analysis : PropertyChangedElement
    {
        public Analysis(Onion onion = null, ObservableCollection<Stakeholder> stakeholders = null, ObservableCollection<StakeholderConnection> connections = null, ObservableCollection<ConnectionGroup> connectionGroups = null)
        {
            Onion = onion ?? new Onion();
            Stakeholders = stakeholders ?? new ObservableCollection<Stakeholder>();
            Connections = connections ?? new ObservableCollection<StakeholderConnection>();
            ConnectionGroups = connectionGroups ?? new ObservableCollection<ConnectionGroup>();
        }

        public Onion Onion { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }

        public ObservableCollection<StakeholderConnection> Connections { get; }

        public ObservableCollection<ConnectionGroup> ConnectionGroups { get; set; }
    }
}
