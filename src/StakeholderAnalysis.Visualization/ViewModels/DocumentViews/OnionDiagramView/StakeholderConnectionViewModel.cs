using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class StakeholderConnectionViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisLog
            log = new StakeholderAnalysisLog(typeof(StakeholderConnectionViewModel));

        private readonly Action<StakeholderConnection> removeStakeholderConnectionAction;

        public StakeholderConnectionViewModel(ViewModelFactory factory, StakeholderConnection connection,
            Action<StakeholderConnection> removeStakeholderConnectionAction) : base(factory)
        {
            this.removeStakeholderConnectionAction = removeStakeholderConnectionAction;
            StakeholderConnection = connection;
            StakeholderConnection.StakeholderConnectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
            StakeholderConnection.ConnectFrom.PropertyChanged += ConnectFromPropertyChanged;
            StakeholderConnection.ConnectTo.PropertyChanged += ConnectToPropertyChanged;
        }

        public StakeholderConnection StakeholderConnection { get; }

        public Brush StrokeColor => new SolidColorBrush(StakeholderConnection.StakeholderConnectionGroup.StrokeColor);

        public double StrokeThickness => StakeholderConnection.StakeholderConnectionGroup.StrokeThickness;

        public LineStyle LineStyle => StakeholderConnection.StakeholderConnectionGroup.LineStyle;

        public bool IsVisible => StakeholderConnection.StakeholderConnectionGroup.Visible;

        public double ConnectFromLeft => StakeholderConnection.ConnectFrom.Left;

        public double ConnectFromTop => StakeholderConnection.ConnectFrom.Top;

        public double ConnectToLeft => StakeholderConnection.ConnectTo.Left;

        public double ConnectToTop => StakeholderConnection.ConnectTo.Top;

        public ICommand RemoveConnectionCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            removeStakeholderConnectionAction?.Invoke(StakeholderConnection);
        });

        private void ConnectToPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PositionedStakeholder.Left):
                    OnPropertyChanged(nameof(ConnectToLeft));
                    break;
                case nameof(PositionedStakeholder.Top):
                    OnPropertyChanged(nameof(ConnectToTop));
                    break;
            }
        }

        private void ConnectFromPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PositionedStakeholder.Left):
                    OnPropertyChanged(nameof(ConnectFromLeft));
                    break;
                case nameof(PositionedStakeholder.Top):
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
                case nameof(StakeholderConnectionGroup.LineStyle):
                    OnPropertyChanged(nameof(LineStyle));
                    break;
            }
        }

        public bool IsViewModelFor(StakeholderConnection connection)
        {
            return connection == StakeholderConnection;
        }

        public override bool IsViewModelFor(object o)
        {
            return IsViewModelFor(o as StakeholderConnection);
        }
    }
}