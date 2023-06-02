using System.Windows.Media;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class StakeholderConnectionGroup : NotifyPropertyChangedObservable
    {
        public StakeholderConnectionGroup() : this("", Colors.Black)
        {
        }

        public StakeholderConnectionGroup(string name, Color strokeColor = default, double strokeThickness = 1.0,
            LineStyle lineStyle = LineStyle.Solid, bool visible = true)
        {
            Name = name;
            StrokeColor = strokeColor;
            StrokeThickness = strokeThickness;
            LineStyle = lineStyle;
            Visible = visible;
        }

        public string Name { get; set; }

        public bool Visible { get; set; }

        public Color StrokeColor { get; set; }

        public double StrokeThickness { get; set; }

        public LineStyle LineStyle { get; set; }
    }
}