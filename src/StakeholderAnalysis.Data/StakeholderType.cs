using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public class StakeholderType : NotifyPropertyChangedObservable, INameProperty
    {
        public string Name { get; set; }

        public StakeholderIconType IconType { get; set; }

        public Color Color { get; set; }
    }
}
