using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramConnectionsPresenterViewModel : ViewModelBase
    {
        private readonly OnionDiagram diagram;
        private readonly ISelectionRegister selectionRegister;

        public OnionDiagramConnectionsPresenterViewModel(ViewModelFactory factory, OnionDiagram onionDiagram, ISelectionRegister selectionRegister) : base(factory)
        {
            this.diagram = onionDiagram;
            this.selectionRegister = selectionRegister;

            if (diagram != null)
            {
                StakeholderConnections = new ObservableCollection<StakeholderConnectionViewModel>(diagram.Connections.Select(c => ViewModelFactory.CreateStakeholderConnectionViewModel(c, selectionRegister)));
                diagram.Connections.CollectionChanged += ConnectorsCollectionChanged;
            }
        }

        public ObservableCollection<StakeholderConnectionViewModel> StakeholderConnections { get; }

        private void ConnectorsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var connection in e.NewItems.OfType<StakeholderConnection>())
                    StakeholderConnections.Add(ViewModelFactory.CreateStakeholderConnectionViewModel(connection, selectionRegister));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<StakeholderConnection>())
                    StakeholderConnections.Remove(
                        StakeholderConnections.FirstOrDefault(vm => vm.StakeholderConnection == stakeholder));
        }
    }
}
