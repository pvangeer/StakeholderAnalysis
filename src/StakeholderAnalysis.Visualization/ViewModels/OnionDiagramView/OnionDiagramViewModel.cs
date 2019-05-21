using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramViewModel: NotifyPropertyChangedObservable, ISelectionRegister, IDrawConnectionHandler
    {
        private readonly OnionDiagram diagram;
        private object selectedObject = null;
        private OnionDiagramStakeholderViewModel newConnectionFromViewModel;
        private OnionDiagramStakeholderViewModel newConnectionPossibleToViewModel;
        private Point newConnectionToRelativePosition;

        public OnionDiagramViewModel(OnionDiagram onionDiagram)
        {
            diagram = onionDiagram;
            OnionDiagramStakeholdersViewModel = new OnionDiagramStakeholdersViewModel(onionDiagram, this, this);
        }

        public OnionDiagramRingsCanvasViewModel OnionDiagramRingsCanvasViewModel => new OnionDiagramRingsCanvasViewModel(diagram);

        public OnionDiagramConnectionsPresenterViewModel OnionDiagramConnectionsPresenterViewModel => new OnionDiagramConnectionsPresenterViewModel(diagram);

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel { get; }

        public ICommand GridClickedCommand => new ClearSelectionCommand(this);

        // TODO: Move below code to separate viewmodel for Ribbon (or toolwindow in future?)
        public void RegisterConnectionGroupsCollectionChanged(NotifyCollectionChangedEventHandler handler)
        {
            if (diagram != null)
            {
                diagram.ConnectionGroups.CollectionChanged += handler;
            }
        }

        public void UnRegisterConnectionGroupsCollectionChanged(NotifyCollectionChangedEventHandler handler)
        {
            if (diagram != null)
            {
                diagram.ConnectionGroups.CollectionChanged -= handler;
            }
        }

        public ObservableCollection<ConnectionGroupViewModel> GetConnectionGroupsViewModels()
        {
            return new ObservableCollection<ConnectionGroupViewModel>(diagram.ConnectionGroups.Select(g => new ConnectionGroupViewModel(g)));
        }

        public bool IsViewModelFor(OnionDiagram otherDiagram)
        {
            return diagram == otherDiagram;
        }

        public OnionDiagram GetDiagram()
        {
            return diagram;
        }


        public bool IsSelected(object o)
        {
            return selectedObject == o;
        }

        public void Select(object o)
        {
            selectedObject = o;
            foreach (var onionDiagramStakeholderViewModel in OnionDiagramStakeholdersViewModel.OnionDiagramStakeholders)
            {
                onionDiagramStakeholderViewModel.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder));
            }
        }

        public void PositionMoved(double relativeLeft, double relativeTop)
        {
            newConnectionToRelativePosition = new Point(relativeLeft, relativeTop);
        }

        public void ChangeTarget(OnionDiagramStakeholderViewModel viewModel)
        {
            var oldViewModel = newConnectionPossibleToViewModel;
            newConnectionPossibleToViewModel = viewModel == newConnectionFromViewModel ? null : viewModel;
            oldViewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsConnectionToTarget));
            viewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsConnectionToTarget));
        }

        public void InitializeConnection(OnionDiagramStakeholderViewModel stakeholderViewModel)
        {
            newConnectionFromViewModel = stakeholderViewModel;
            newConnectionToRelativePosition = new Point(stakeholderViewModel.LeftPercentage, stakeholderViewModel.TopPercentage);
        }

        public void FinishConnecting()
        {
            if (newConnectionFromViewModel != null && newConnectionPossibleToViewModel != null &&
                newConnectionFromViewModel != newConnectionPossibleToViewModel)
            {
                diagram.Connections.Add(new StakeholderConnection(diagram.ConnectionGroups.FirstOrDefault(), newConnectionFromViewModel.GetOnionDiagramStakeholder(), newConnectionPossibleToViewModel.GetOnionDiagramStakeholder()));
            }

            newConnectionFromViewModel = null;
            newConnectionPossibleToViewModel = null;
            newConnectionToRelativePosition = new Point(0.0,0.0);
        }

        public bool IsConnectionTarget(Stakeholder stakeholder)
        {
            return newConnectionPossibleToViewModel?.Stakeholder == stakeholder;
        }
    }
}
