using System.Windows.Media;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class OnionRing : NotifyPropertyChangedObservable
    {
        public OnionRing(double percentage = 1.0)
        {
            Percentage = percentage;
            BackgroundColor = Colors.Azure;
            StrokeColor = Colors.Gray;
            StrokeThickness = 1.0;
        }

        public double Percentage { get; set; }

        public Color BackgroundColor { get; set; }

        public Color StrokeColor { get; set; }

        public double StrokeThickness { get; set; }

        public LineStyle LineStyle { get; set; }
    }
}