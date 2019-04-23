using System.Windows.Media;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class StakeholderConnectionGroup : NotifyPropertyChangedObservable
    {
        public StakeholderConnectionGroup(string name, Color color, bool visible = true)
        {
            Name = name;
            Color = color;
            Visible = visible;
        }

        public string Name { get; set; }

        public Color Color { get; set; }

        public bool Visible { get; set; }
    }
}