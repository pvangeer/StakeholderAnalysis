using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public class Analysis : NotifyPropertyChangedObservable
    {
        public Analysis(ObservableCollection<OnionRing> onionRings = null, ObservableCollection<Stakeholder> stakeholders = null,
            ObservableCollection<StakeholderConnection> connections = null,
            ObservableCollection<StakeholderConnectionGroup> connectionGroups = null)
        {
            OnionRings = onionRings ?? new ObservableCollection<OnionRing>();
            Stakeholders = stakeholders ?? new ObservableCollection<Stakeholder>();
            Connections = connections ?? new ObservableCollection<StakeholderConnection>();
            ConnectionGroups = connectionGroups ?? new ObservableCollection<StakeholderConnectionGroup>();
        }

        public ObservableCollection<OnionRing> OnionRings { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }

        public ObservableCollection<StakeholderConnection> Connections { get; }

        public ObservableCollection<StakeholderConnectionGroup> ConnectionGroups { get; set; }
    }
}