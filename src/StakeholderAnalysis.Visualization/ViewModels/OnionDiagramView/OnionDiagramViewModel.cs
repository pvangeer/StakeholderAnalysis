using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramViewModel: NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramViewModel(OnionDiagram onionDiagram)
        {
            diagram = onionDiagram;
        }

        public OnionDiagramRingsCanvasViewModel OnionDiagramRingsCanvasViewModel => new OnionDiagramRingsCanvasViewModel(diagram);

        public OnionDiagramConnectionsPresenterViewModel OnionDiagramConnectionsPresenterViewModel => new OnionDiagramConnectionsPresenterViewModel(diagram);

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel => new OnionDiagramStakeholdersViewModel(diagram);

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
    }
}
