using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class StakeholderConnectionViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisLog log = new StakeholderAnalysisLog(typeof(StakeholderConnectionViewModel));
        private readonly ISelectionRegister selectionRegister;
        
        public StakeholderConnectionViewModel(ViewModelFactory factory, StakeholderConnection connection, ISelectionRegister selectionRegister) : base(factory)
        {
            this.selectionRegister = selectionRegister;
            StakeholderConnection = connection;
            StakeholderConnection.StakeholderConnectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
            StakeholderConnection.ConnectFrom.PropertyChanged += ConnectFromPropertyChanged;
            StakeholderConnection.ConnectTo.PropertyChanged += ConnectToPropertyChanged;
        }


        public StakeholderConnection StakeholderConnection { get; }

        public bool IsSelected => selectionRegister != null && selectionRegister.IsSelected(StakeholderConnection);

        public Brush StrokeColor => new SolidColorBrush(StakeholderConnection.StakeholderConnectionGroup.StrokeColor);

        public double StrokeThickness => StakeholderConnection.StakeholderConnectionGroup.StrokeThickness;

        public bool IsVisible => StakeholderConnection.StakeholderConnectionGroup.Visible;

        public double ConnectFromLeft => StakeholderConnection.ConnectFrom.Left;

        public double ConnectFromTop => StakeholderConnection.ConnectFrom.Top;

        public double ConnectToLeft => StakeholderConnection.ConnectTo.Left;

        public double ConnectToTop => StakeholderConnection.ConnectTo.Top;

        public ICommand StakeholderConnectionClickedCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            selectionRegister?.Select(StakeholderConnection);
        });

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
                case nameof(StakeholderConnectionGroup.StrokeColor):
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

        public bool IsViewModelFor(StakeholderConnection connection)
        {
            return connection == StakeholderConnection;
        }
    }
}