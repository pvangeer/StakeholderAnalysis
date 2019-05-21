using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramViewModel: NotifyPropertyChangedObservable, ISelectionRegister
    {
        private readonly OnionDiagram diagram;
        private object selectedObject = null;

        public OnionDiagramViewModel(OnionDiagram onionDiagram)
        {
            diagram = onionDiagram;
            OnionDiagramDrawConnectionViewModel = new OnionDiagramDrawConnectionViewModel(onionDiagram);
            OnionDiagramStakeholdersViewModel = new OnionDiagramStakeholdersViewModel(onionDiagram, this, OnionDiagramDrawConnectionViewModel);
        }

        public OnionDiagramRingsCanvasViewModel OnionDiagramRingsCanvasViewModel => new OnionDiagramRingsCanvasViewModel(diagram);

        public OnionDiagramConnectionsPresenterViewModel OnionDiagramConnectionsPresenterViewModel => new OnionDiagramConnectionsPresenterViewModel(diagram);

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel { get; }

        public ICommand GridClickedCommand => new ClearSelectionCommand(this);

        public OnionDiagramDrawConnectionViewModel OnionDiagramDrawConnectionViewModel { get; }

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
    }
}
