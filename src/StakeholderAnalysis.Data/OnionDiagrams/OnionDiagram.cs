using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class OnionDiagram : NotifyPropertyChangedObservable
    {
        public OnionDiagram(ObservableCollection<OnionRing> onionRings = null, 
            ObservableCollection<StakeholderConnection> connections = null,
            ObservableCollection<StakeholderConnectionGroup> connectionGroups = null)
        {
            OnionRings = onionRings ?? new ObservableCollection<OnionRing>();
            Stakeholders = new ObservableCollection<OnionDiagramStakeholder>();
            Connections = connections ?? new ObservableCollection<StakeholderConnection>();
            ConnectionGroups = connectionGroups ?? new ObservableCollection<StakeholderConnectionGroup>();
        }

        public double Asymmetry { get; set; }

        public ObservableCollection<OnionDiagramStakeholder> Stakeholders { get; }

        public ObservableCollection<OnionRing> OnionRings { get; }

        public ObservableCollection<StakeholderConnection> Connections { get; }

        public ObservableCollection<StakeholderConnectionGroup> ConnectionGroups { get; set; }
    }
}
