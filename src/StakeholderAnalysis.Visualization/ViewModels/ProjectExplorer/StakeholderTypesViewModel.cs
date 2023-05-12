using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class StakeholderTypesViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded;

        public StakeholderTypesViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            if (analysis != null)
            {
                Items = new ObservableCollection<ITreeNodeViewModel>(
                    analysis?.StakeholderTypes.Select(st => ViewModelFactory.CreateStakeholderTypeViewModel(st)));
                analysis.StakeholderTypes.CollectionChanged += StakeholderTypesCollectionChanged;
            }
            else
            {
                Items = new ObservableCollection<ITreeNodeViewModel>();
            }

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public string DisplayName => "Stakeholder types";

        public string IconSourceString => null;

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => true;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
        {
            return false;
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

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand AddItemCommand => CommandFactory.CreateAddNewStakeholderTypeCommand(analysis);

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        private void StakeholderTypesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var stakeholderType in e.NewItems.OfType<StakeholderType>())
                        Items.Add(ViewModelFactory.CreateStakeholderTypeViewModel(stakeholderType));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderType in e.OldItems.OfType<StakeholderType>())
                    {
                        var viewModelToRemove =
                            Items.FirstOrDefault(viewModel => viewModel.IsViewModelFor(stakeholderType));
                        if (viewModelToRemove != null) Items.Remove(viewModelToRemove);
                    }

                    break;
            }
        }
    }
}