using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class StakeholderForceFieldDiagram : NotifyPropertyChangedObservable
    {
        public StakeholderForceFieldDiagram()
        {
            Stakeholders = new ObservableCollection<Stakeholder>();
        }

        public ObservableCollection<Stakeholder> Stakeholders { get; }

    }
}
