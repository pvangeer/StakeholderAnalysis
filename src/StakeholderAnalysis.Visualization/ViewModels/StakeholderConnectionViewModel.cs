using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderConnectionViewModel : PropertyChangedElement
    {
        public StakeholderConnectionViewModel(StakeholderConnection connection)
        {
            this.StakeholderConnection = connection;
        }

        public StakeholderConnection StakeholderConnection { get; }

        public Brush StrokeColor => new SolidColorBrush(StakeholderConnection.ConnectionGroup.Color);

        public double ConnectFromLeft => StakeholderConnection.ConnectFrom.LeftPercentage;

        public double ConnectFromTop => StakeholderConnection.ConnectFrom.TopPercentage;

        public double ConnectToLeft => StakeholderConnection.ConnectTo.LeftPercentage;

        public double ConnectToTop => StakeholderConnection.ConnectTo.TopPercentage;
    }
}
