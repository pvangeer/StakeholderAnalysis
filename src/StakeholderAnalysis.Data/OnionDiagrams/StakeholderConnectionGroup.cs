using System.Windows.Media;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class StakeholderConnectionGroup : NotifyPropertyChangedObservable
    {
        public StakeholderConnectionGroup(string name, Color color, double strokeThickness = 1.0, bool visible = true)
        {
            Name = name;
            Color = color;
            StrokeThickness = strokeThickness;
            Visible = visible;
        }

        public string Name { get; set; }

        public Color Color { get; set; }

        public bool Visible { get; set; }

        public double StrokeThickness { get; set; }
    }
}