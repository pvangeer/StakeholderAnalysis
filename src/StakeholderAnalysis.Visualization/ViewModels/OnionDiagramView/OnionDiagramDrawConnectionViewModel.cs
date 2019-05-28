using System;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramDrawConnectionViewModel : NotifyPropertyChangedObservable, IDrawConnectionHandler
    {
        private readonly OnionDiagram onionDiagram;
        private OnionDiagramStakeholderViewModel newConnectionFromViewModel;

        public OnionDiagramDrawConnectionViewModel(OnionDiagram onionDiagram)
        {
            this.onionDiagram = onionDiagram;
            if (onionDiagram != null)
            {
                onionDiagram.ConnectionGroups.CollectionChanged += RelevantCollectionChanged;
                onionDiagram.Stakeholders.CollectionChanged += RelevantCollectionChanged;
            }

            GetSelectedStakeholderConnectionGroup = diagram => diagram?.ConnectionGroups.FirstOrDefault();
        }

        private void RelevantCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsActive));
        }

        public double NewConnectionToRelativePositionTop { get; set; }

        public double NewConnectionToRelativePositionLeft { get; set; }

        public double NewConnectionFromRelativePositionTop { get; set; }

        public double NewConnectionFromRelativePositionLeft { get; set; }

        public OnionDiagramStakeholderViewModel NewConnectionFromViewModel
        {
            get => newConnectionFromViewModel;
            set
            {
                newConnectionFromViewModel = value;
                NewConnectionFromRelativePositionLeft = newConnectionFromViewModel?.LeftPercentage ?? 0.0;
                OnPropertyChanged(nameof(NewConnectionFromRelativePositionLeft));
                NewConnectionFromRelativePositionTop = newConnectionFromViewModel?.TopPercentage ?? 0.0;
                OnPropertyChanged(nameof(NewConnectionFromRelativePositionTop));
            }
        }

        public OnionDiagramStakeholderViewModel NewConnectionPossibleToViewModel { get; set; }

        public bool IsDrawing { get; set; }

        public void PositionMoved(double relativeLeft, double relativeTop)
        {
            NewConnectionToRelativePositionLeft = relativeLeft;
            NewConnectionToRelativePositionTop = relativeTop;
            OnPropertyChanged(nameof(NewConnectionToRelativePositionLeft));
            OnPropertyChanged(nameof(NewConnectionToRelativePositionTop));
        }

        public void ChangeTarget(OnionDiagramStakeholderViewModel viewModel)
        {
            var oldViewModel = NewConnectionPossibleToViewModel;
            NewConnectionPossibleToViewModel = viewModel == NewConnectionFromViewModel ? null : viewModel;

            oldViewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsConnectionToTarget));
            viewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsConnectionToTarget));
            OnPropertyChanged(nameof(NewConnectionPossibleToViewModel));
        }

        public void InitializeConnection(OnionDiagramStakeholderViewModel stakeholderViewModel)
        {
            NewConnectionFromViewModel = stakeholderViewModel;
            NewConnectionToRelativePositionLeft = stakeholderViewModel.LeftPercentage;
            NewConnectionToRelativePositionTop = stakeholderViewModel.TopPercentage;
            IsDrawing = true;

            OnPropertyChanged(nameof(NewConnectionFromViewModel));
            OnPropertyChanged(nameof(NewConnectionToRelativePositionLeft));
            OnPropertyChanged(nameof(NewConnectionToRelativePositionTop));
            OnPropertyChanged(nameof(IsDrawing));
        }

        public Func<OnionDiagram, StakeholderConnectionGroup> GetSelectedStakeholderConnectionGroup { get; set; }

        public void FinishConnecting()
        {
            OnionDiagramStakeholderViewModel oldTarget = null;
            if (NewConnectionPossibleToViewModel != null)
            {
                oldTarget = NewConnectionPossibleToViewModel;
            }

            if (NewConnectionFromViewModel != null && NewConnectionPossibleToViewModel != null &&
                NewConnectionFromViewModel != NewConnectionPossibleToViewModel &&
                GetSelectedStakeholderConnectionGroup != null)
            {
                var stakeholderConnectionGroup = GetSelectedStakeholderConnectionGroup(onionDiagram);
                onionDiagram.Connections.Add(new StakeholderConnection(stakeholderConnectionGroup, NewConnectionFromViewModel.GetOnionDiagramStakeholder(), NewConnectionPossibleToViewModel.GetOnionDiagramStakeholder()));
            }

            IsDrawing = false;
            NewConnectionFromViewModel = null;
            NewConnectionPossibleToViewModel = null;
            NewConnectionToRelativePositionLeft = double.NaN;
            NewConnectionToRelativePositionTop = double.NaN;

            oldTarget?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsConnectionToTarget));

            OnPropertyChanged(nameof(IsDrawing));
            OnPropertyChanged(nameof(NewConnectionFromViewModel));
            OnPropertyChanged(nameof(NewConnectionPossibleToViewModel));
            OnPropertyChanged(nameof(NewConnectionToRelativePositionLeft));
            OnPropertyChanged(nameof(NewConnectionToRelativePositionTop));
        }

        public bool IsConnectionTarget(Stakeholder stakeholder)
        {
            return NewConnectionPossibleToViewModel?.Stakeholder == stakeholder;
        }

        public bool IsActive => onionDiagram.ConnectionGroups.Any() && onionDiagram.Stakeholders.Count > 1;
    }
}
