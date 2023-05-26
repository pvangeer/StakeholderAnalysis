using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class ConnectionGroupsPropertiesViewModel : PropertiesCollectionViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly OnionDiagram diagram;

        public ConnectionGroupsPropertiesViewModel(ViewModelFactory factory, OnionDiagram diagram) : base(factory)
        {
            this.diagram = diagram;
            if (this.diagram != null)
                this.diagram.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>(
                this.diagram?.ConnectionGroups.Select(connectionGroup =>
                    ViewModelFactory.CreateConnectionGroupPropertiesViewModel(connectionGroup, this.diagram)) ??
                new List<ConnectionGroupPropertiesViewModel>());
        }

        public override string DisplayName => "Connectiegroepen";

        public override bool CanRemove => false;

        public override bool CanAdd => true;

        public override bool IsViewModelFor(object o)
        {
            return false;
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public override ICommand AddItemCommand => CommandFactory.CreateAddConnectionGroupCommand(diagram);

        public override bool IsExpandable => true;

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        private void ConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var connectionGroup in e.NewItems.OfType<StakeholderConnectionGroup>())
                        Items.Add(ViewModelFactory.CreateConnectionGroupPropertiesViewModel(connectionGroup,
                            diagram));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var connectionGroup in e.OldItems.OfType<StakeholderConnectionGroup>())
                    {
                        var viewModel = Items.FirstOrDefault(vm => vm.IsViewModelFor(connectionGroup));
                        if (viewModel != null) Items.Remove(viewModel);
                    }

                    break;
            }
        }
    }
}