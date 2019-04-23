using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class OnionConnectionsPresenterViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;

        public OnionConnectionsPresenterViewModel(OnionDiagram onionDiagram)
        {
            this.diagram = onionDiagram;

            if (diagram != null)
            {
                StakeholderConnections = new ObservableCollection<StakeholderConnectionViewModel>(diagram.Connections.Select(c => new StakeholderConnectionViewModel(c)));
                diagram.Connections.CollectionChanged += ConnectorsCollectionChanged;
            }
        }

        public ObservableCollection<StakeholderConnectionViewModel> StakeholderConnections { get; }

        private void ConnectorsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<StakeholderConnection>())
                    StakeholderConnections.Add(new StakeholderConnectionViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<StakeholderConnection>())
                    StakeholderConnections.Remove(
                        StakeholderConnections.FirstOrDefault(vm => vm.StakeholderConnection == stakeholder));
        }
    }
}
