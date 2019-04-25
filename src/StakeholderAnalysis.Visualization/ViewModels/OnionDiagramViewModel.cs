using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class OnionDiagramViewModel: NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramViewModel(OnionDiagram onionDiagram)
        {
            diagram = onionDiagram;
        }

        public OnionRingsCanvasViewModel OnionRingsCanvasViewModel => new OnionRingsCanvasViewModel(diagram);

        public OnionConnectionsPresenterViewModel OnionConnectionsPresenterViewModel => new OnionConnectionsPresenterViewModel(diagram);

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
    }
}
