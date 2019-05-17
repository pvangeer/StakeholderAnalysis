using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class ForceFieldDiagram : NotifyPropertyChangedObservable
    {
        public ForceFieldDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<ForceFieldDiagramStakeholder>();
        }

        public ForceFieldDiagram() : this("") { }

        public string Name { get; set; }

        public ObservableCollection<ForceFieldDiagramStakeholder> Stakeholders { get; }

    }
}
