using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public class Onion
    {
        public Onion()
        {
            Rings = new ObservableCollection<OnionRing>();
        }

        public ObservableCollection<OnionRing> Rings { get; }
    }
}
