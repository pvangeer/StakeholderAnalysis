using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public class Analysis : PropertyChangedElement
    {
        public Analysis(Onion onion = null, ObservableCollection<Stakeholder> stakeholders = null)
        {
            Onion = onion ?? new Onion();
            Stakeholders = stakeholders ?? new ObservableCollection<Stakeholder>();
        }

        public Onion Onion { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }
    }
}
