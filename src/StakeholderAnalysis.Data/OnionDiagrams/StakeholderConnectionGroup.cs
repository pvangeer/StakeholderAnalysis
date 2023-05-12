using System.Windows.Media;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class StakeholderConnectionGroup : NotifyPropertyChangedObservable
    {
        public StakeholderConnectionGroup() : this("", Colors.Black)
        {
        }

        public StakeholderConnectionGroup(string name, Color strokeColor, double strokeThickness = 1.0,
            bool visible = true)
        {
            Name = name;
            StrokeColor = strokeColor;
            StrokeThickness = strokeThickness;
            Visible = visible;
        }

        public string Name { get; set; }

        public Color StrokeColor { get; set; }

        public bool Visible { get; set; }

        public double StrokeThickness { get; set; }
    }
}