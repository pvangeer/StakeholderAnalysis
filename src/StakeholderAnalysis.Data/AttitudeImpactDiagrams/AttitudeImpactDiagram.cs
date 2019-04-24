using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
