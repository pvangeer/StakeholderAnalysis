using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class OnionRingsPropertiesViewModel : PropertiesCollectionViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly OnionDiagram diagram;

        public OnionRingsPropertiesViewModel(ViewModelFactory factory, OnionDiagram diagram) : base(factory)
        {
            this.diagram = diagram;
            if (this.diagram != null)
                this.diagram.OnionRings.CollectionChanged += OnionRingsCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>(
                this.diagram?.OnionRings.Select(r =>
                    ViewModelFactory.CreateOnionRingPropertiesViewModel(r, this.diagram)) ??
                new List<OnionRingPropertiesViewModel>());
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public override string DisplayName => "Ringen";

        public override bool CanAdd => true;

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public override ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public override ICommand AddItemCommand => CommandFactory.CreateAddOnionRingCommand(diagram);

        public override ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public override bool IsViewModelFor(object o)
        {
            return o as OnionDiagram == diagram;
        }

        public override bool IsExpandable => true;

        private void OnionRingsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var onionRing in e.NewItems.OfType<OnionRing>())
                        Items.Add(ViewModelFactory.CreateOnionRingPropertiesViewModel(onionRing, diagram));
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