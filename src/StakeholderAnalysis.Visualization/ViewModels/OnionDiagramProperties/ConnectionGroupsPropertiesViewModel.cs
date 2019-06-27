using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class ConnectionGroupsPropertiesViewModel : ViewModelBase, IExpandableContentViewModel
    {
        private readonly ViewManager viewManager;
        private bool isExpanded;

        public ConnectionGroupsPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
            SetActiveOnionDiagram();
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    SetActiveOnionDiagram();
                    break;
            }
        }

        public string DisplayName => "Connectiegroepen";

        private OnionDiagram SelectedOnionDiagram { get; set; }

        public ObservableCollection<ConnectionGroupPropertiesViewModel> ConnectionGroups { get; private set; }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand AddNewConnectionGroupCommand => CommandFactory.CreateAddConnectionGroupCommand(SelectedOnionDiagram);

        public bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        private void SetActiveOnionDiagram()
        {
            var activeOnionDiagram = (viewManager?.ActiveDocument?.ViewModel as OnionDiagramViewModel)?.GetDiagram();
            if (SelectedOnionDiagram != activeOnionDiagram)
            {
                if (SelectedOnionDiagram != null)
                {
                    SelectedOnionDiagram.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;
                }

                SelectedOnionDiagram = activeOnionDiagram;
                ConnectionGroups = new ObservableCollection<ConnectionGroupPropertiesViewModel>(
                    SelectedOnionDiagram?.ConnectionGroups.Select(connectionGroup => ViewModelFactory.CreateConnectionGroupPropertiesViewModel(connectionGroup, SelectedOnionDiagram)) ??
                    new List<ConnectionGroupPropertiesViewModel>());

                if (SelectedOnionDiagram != null)
                {
                    SelectedOnionDiagram.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;
                }

                OnPropertyChanged(nameof(ConnectionGroups));
            }
        }

        private void ConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var connectionGroup in e.NewItems.OfType<StakeholderConnectionGroup>())
                    {
                        ConnectionGroups.Add(ViewModelFactory.CreateConnectionGroupPropertiesViewModel(connectionGroup, SelectedOnionDiagram));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var connectionGroup in e.OldItems.OfType<StakeholderConnectionGroup>())
                    {
                        var viewModel = ConnectionGroups.FirstOrDefault(vm => vm.IsViewModelFor(connectionGroup));
                        if (viewModel != null)
                        {
                            ConnectionGroups.Remove(viewModel);
                        }
                    }
                    break;
            }
        }
    }
}
