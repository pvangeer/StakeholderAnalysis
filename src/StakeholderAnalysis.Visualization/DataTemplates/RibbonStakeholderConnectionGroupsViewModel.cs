using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.DataTemplates
{
    public class RibbonStakeholderConnectionGroupsViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram onionDiagram;

        public RibbonStakeholderConnectionGroupsViewModel(OnionDiagram onionDiagram)
        {
            this.onionDiagram = onionDiagram;
            if (onionDiagram != null)
            {
                onionDiagram.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;
                StakeholderConnectionGroups = new ObservableCollection<ConnectionGroupViewModel>(onionDiagram.ConnectionGroups.Select(g => new ConnectionGroupViewModel(g)));
            }
        }

        public ObservableCollection<ConnectionGroupViewModel> StakeholderConnectionGroups { get; }

        private void ConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<StakeholderConnectionGroup>())
                    StakeholderConnectionGroups.Add(new ConnectionGroupViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var connectionGroup in e.OldItems.OfType<StakeholderConnectionGroup>())
                    StakeholderConnectionGroups.Remove(
                        StakeholderConnectionGroups.FirstOrDefault(vm => vm.StakeholderConnectionGroup == connectionGroup));
        }

    }
}
