using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramConnectionsPresenterViewModel : ViewModelBase
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramConnectionsPresenterViewModel(ViewModelFactory factory, OnionDiagram onionDiagram) : base(factory)
        {
            this.diagram = onionDiagram;

            if (diagram != null)
            {
                StakeholderConnections = new ObservableCollection<StakeholderConnectionViewModel>(diagram.Connections.Select(c => ViewModelFactory.CreateStakeholderConnectionViewModel(c)));
                diagram.Connections.CollectionChanged += ConnectorsCollectionChanged;
            }
        }

        public ObservableCollection<StakeholderConnectionViewModel> StakeholderConnections { get; }

        private void ConnectorsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var connection in e.NewItems.OfType<StakeholderConnection>())
                    StakeholderConnections.Add(ViewModelFactory.CreateStakeholderConnectionViewModel(connection));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<StakeholderConnection>())
                    StakeholderConnections.Remove(
                        StakeholderConnections.FirstOrDefault(vm => vm.StakeholderConnection == stakeholder));
        }
    }
}
