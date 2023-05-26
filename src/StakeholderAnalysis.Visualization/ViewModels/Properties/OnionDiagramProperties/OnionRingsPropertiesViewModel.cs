using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class OnionRingsPropertiesViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly ViewManager viewManager;
        private bool isExpanded = true;

        public OnionRingsPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
            SetActiveOnionDiagram();
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        private OnionDiagram SelectedOnionDiagram { get; set; }

        public string DisplayName => "Ringen";

        public string IconSourceString { get; }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => true;

        public ObservableCollection<ITreeNodeViewModel> Items { get; private set; }

        public CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand AddItemCommand => CommandFactory.CreateAddOnionRingCommand(SelectedOnionDiagram);

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItem => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
        {
            return o as OnionDiagram == SelectedOnionDiagram;
        }

        public bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
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

        private void SetActiveOnionDiagram()
        {
            var activeOnionDiagram = (viewManager?.ActiveDocument?.ViewModel as OnionDiagramViewModel)?.GetDiagram();
            if (SelectedOnionDiagram != activeOnionDiagram)
            {
                if (SelectedOnionDiagram != null)
                    SelectedOnionDiagram.OnionRings.CollectionChanged += OnionRingsCollectionChanged;

                SelectedOnionDiagram = activeOnionDiagram;
                Items = new ObservableCollection<ITreeNodeViewModel>(
                    SelectedOnionDiagram?.OnionRings.Select(r =>
                        ViewModelFactory.CreateOnionRingPropertiesViewModel(r, SelectedOnionDiagram)) ??
                    new List<OnionRingPropertiesViewModel>());

                if (SelectedOnionDiagram != null)
                    SelectedOnionDiagram.OnionRings.CollectionChanged += OnionRingsCollectionChanged;

                OnPropertyChanged(nameof(Items));
            }
        }

        private void OnionRingsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var onionRing in e.NewItems.OfType<OnionRing>())
                        Items.Add(ViewModelFactory.CreateOnionRingPropertiesViewModel(onionRing, SelectedOnionDiagram));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var ringViewModel in e.OldItems.OfType<OnionRing>())
                    {
                        var viewModel = Items.FirstOrDefault(vm => vm.IsViewModelFor(ringViewModel));
                        if (viewModel != null) Items.Remove(viewModel);
                    }

                    break;
            }
        }
    }
}