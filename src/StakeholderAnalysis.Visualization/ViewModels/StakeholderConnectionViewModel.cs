using System.ComponentModel;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderConnectionViewModel : PropertyChangedElement
    {
        public StakeholderConnectionViewModel(StakeholderConnection connection)
        {
            StakeholderConnection = connection;
            StakeholderConnection.ConnectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
            StakeholderConnection.ConnectFrom.PropertyChanged += ConnectFromPropertyChanged;
            StakeholderConnection.ConnectTo.PropertyChanged += ConnectToPropertyChanged;
        }


        public StakeholderConnection StakeholderConnection { get; }

        public Brush StrokeColor => new SolidColorBrush(StakeholderConnection.ConnectionGroup.Color);

        public bool IsVisible => StakeholderConnection.ConnectionGroup.Visible;

        public double ConnectFromLeft => StakeholderConnection.ConnectFrom.LeftPercentage;

        public double ConnectFromTop => StakeholderConnection.ConnectFrom.TopPercentage;

        public double ConnectToLeft => StakeholderConnection.ConnectTo.LeftPercentage;

        public double ConnectToTop => StakeholderConnection.ConnectTo.TopPercentage;

        private void ConnectToPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.LeftPercentage):
                    OnPropertyChanged(nameof(ConnectToLeft));
                    break;
                case nameof(Stakeholder.TopPercentage):
                    OnPropertyChanged(nameof(ConnectToTop));
                    break;
            }
        }

        private void ConnectFromPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.LeftPercentage):
                    OnPropertyChanged(nameof(ConnectFromLeft));
                    break;
                case nameof(Stakeholder.TopPercentage):
                    OnPropertyChanged(nameof(ConnectFromTop));
                    break;
            }
        }

        private void ConnectionGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ConnectionGroup.Color):
                    OnPropertyChanged(nameof(StrokeColor));
                    break;
                case nameof(ConnectionGroup.Visible):
                    OnPropertyChanged(nameof(IsVisible));
                    break;
            }
        }
    }
}