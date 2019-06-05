using System.ComponentModel;
using System.Windows.Media;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class StakeholderConnectionViewModel : ViewModelBase
    {
        public StakeholderConnectionViewModel(ViewModelFactory factory, StakeholderConnection connection) : base(factory)
        {
            StakeholderConnection = connection;
            StakeholderConnection.StakeholderConnectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
            StakeholderConnection.ConnectFrom.PropertyChanged += ConnectFromPropertyChanged;
            StakeholderConnection.ConnectTo.PropertyChanged += ConnectToPropertyChanged;
        }


        public StakeholderConnection StakeholderConnection { get; }

        public Brush StrokeColor => new SolidColorBrush(StakeholderConnection.StakeholderConnectionGroup.Color);

        public double StrokeThickness => StakeholderConnection.StakeholderConnectionGroup.StrokeThickness;

        public bool IsVisible => StakeholderConnection.StakeholderConnectionGroup.Visible;

        public double ConnectFromLeft => StakeholderConnection.ConnectFrom.Left;

        public double ConnectFromTop => StakeholderConnection.ConnectFrom.Top;

        public double ConnectToLeft => StakeholderConnection.ConnectTo.Left;

        public double ConnectToTop => StakeholderConnection.ConnectTo.Top;

        private void ConnectToPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagramStakeholder.Left):
                    OnPropertyChanged(nameof(ConnectToLeft));
                    break;
                case nameof(OnionDiagramStakeholder.Top):
                    OnPropertyChanged(nameof(ConnectToTop));
                    break;
            }
        }

        private void ConnectFromPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagramStakeholder.Left):
                    OnPropertyChanged(nameof(ConnectFromLeft));
                    break;
                case nameof(OnionDiagramStakeholder.Top):
                    OnPropertyChanged(nameof(ConnectFromTop));
                    break;
            }
        }

        private void ConnectionGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderConnectionGroup.Color):
                    OnPropertyChanged(nameof(StrokeColor));
                    break;
                case nameof(StakeholderConnectionGroup.Visible):
                    OnPropertyChanged(nameof(IsVisible));
                    break;
                case nameof(StakeholderConnectionGroup.StrokeThickness):
                    OnPropertyChanged(nameof(StrokeThickness));
                    break;
            }
        }
    }
}