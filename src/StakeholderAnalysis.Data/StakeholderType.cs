using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public class StakeholderType : NotifyPropertyChangedObservable
    {
        public StakeholderType()
        {
            Color = Color.FromRgb(0, 204, 150);
            Name = "Nieuw type";
            IconType = StakeholderIconType.Other;
        }

        public string Name { get; set; }

        public StakeholderIconType IconType { get; set; }

        public Color Color { get; set; }
    }
}