using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonStakeholderConnectionGroupsViewModel : NotifyPropertyChangedObservable
    {
        private OnionDiagramViewModel onionDiagram;
        private readonly ViewManager viewManager;

        public RibbonStakeholderConnectionGroupsViewModel(ViewManager viewManager)
        {
            this.viewManager = viewManager;

            viewManager.PropertyChanged += ViewManagerPropertyChanged;

            if (viewManager?.ActiveDocument != null)
            {
                RegisterGroupsCollectionChanged(viewManager.ActiveDocument);
            }
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewManager.ActiveDocument))
            {
                if (onionDiagram != null)
                {
                    UnRegisterGroupsCollectionChanged();
                }

                if (viewManager?.ActiveDocument != null)
                {
                    RegisterGroupsCollectionChanged(viewManager.ActiveDocument);
                }
            }
        }

        private void RegisterGroupsCollectionChanged(ViewInfo viewInfo)
        {
            if (viewInfo.ViewModel is OnionDiagramViewModel diagramViewModel)
            {
                diagramViewModel.RegisterConnectionGroupsCollectionChanged(ConnectionGroupsCollectionChanged);
                StakeholderConnectionGroups = diagramViewModel.GetConnectionGroupsViewModels();
                OnPropertyChanged(nameof(StakeholderConnectionGroups));
                onionDiagram = diagramViewModel;
            }
        }

        private void UnRegisterGroupsCollectionChanged()
        {
            onionDiagram.UnRegisterConnectionGroupsCollectionChanged(ConnectionGroupsCollectionChanged);
            onionDiagram = null;
        }

        public ObservableCollection<ConnectionGroupViewModel> StakeholderConnectionGroups { get; set; }

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
