using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public class Analysis : PropertyChangedElement
    {
        public Analysis(Onion onion = null, ObservableCollection<Stakeholder> stakeholders = null, ObservableCollection<StakeholderConnection> connectors = null)
        {
            Onion = onion ?? new Onion();
            Stakeholders = stakeholders ?? new ObservableCollection<Stakeholder>();
            Connections = connectors ?? new ObservableCollection<StakeholderConnection>();
        }

        public Onion Onion { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }

        public ObservableCollection<StakeholderConnection> Connections { get; }
    }
}
