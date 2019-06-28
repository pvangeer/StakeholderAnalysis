using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class ConnectionGroupsPropertiesViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
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

        public string IconSourceString { get; }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => true;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool IsViewModelFor(object o)
        {
            return false;
        }

        private OnionDiagram SelectedOnionDiagram { get; set; }

        public ObservableCollection<ITreeNodeViewModel> Items { get; private set; }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand AddItemCommand => CommandFactory.CreateAddConnectionGroupCommand(SelectedOnionDiagram);

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
                Items = new ObservableCollection<ITreeNodeViewModel>(
                    SelectedOnionDiagram?.ConnectionGroups.Select(connectionGroup => ViewModelFactory.CreateConnectionGroupPropertiesViewModel(connectionGroup, SelectedOnionDiagram)) ??
                    new List<ConnectionGroupPropertiesViewModel>());

                if (SelectedOnionDiagram != null)
                {
                    SelectedOnionDiagram.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;
                }

                OnPropertyChanged(nameof(Items));
            }
        }

        private void ConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var connectionGroup in e.NewItems.OfType<StakeholderConnectionGroup>())
                    {
                        Items.Add(ViewModelFactory.CreateConnectionGroupPropertiesViewModel(connectionGroup, SelectedOnionDiagram));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var connectionGroup in e.OldItems.OfType<StakeholderConnectionGroup>())
                    {
                        var viewModel = Items.FirstOrDefault(vm => vm.IsViewModelFor(connectionGroup));
                        if (viewModel != null)
                        {
                            Items.Remove(viewModel);
                        }
                    }
                    break;
            }
        }

        public CollectionType CollectionType => CollectionType.CollectionList;
    }
}
