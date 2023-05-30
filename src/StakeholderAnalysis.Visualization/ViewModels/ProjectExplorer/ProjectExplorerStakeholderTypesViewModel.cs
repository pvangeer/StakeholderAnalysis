using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerStakeholderTypesViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;

        public ProjectExplorerStakeholderTypesViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;

            analysis.StakeholderTypes.CollectionChanged += StakeholderTypesCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>();
            foreach (var stakeholderType in analysis.StakeholderTypes)
                Items.Add(ViewModelFactory.CreateProjectExploreStakeholderTypeViewModel(stakeholderType));

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public string DisplayName => "Stakeholder types";

        public string IconSourceString => null;

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => true;

        public ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            analysis.StakeholderTypes.Add(new StakeholderType());
        });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

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

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }


        public bool IsViewModelFor(object o)
        {
            return false;
        }

        private void StakeholderTypesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var stakeholderType in e.NewItems.OfType<StakeholderType>())
                    {
                        var projectExplorerStakeholderTypeViewModel = ViewModelFactory.CreateProjectExploreStakeholderTypeViewModel(stakeholderType);
                        Items.Add(projectExplorerStakeholderTypeViewModel);
                        if (IsExpanded)
                            projectExplorerStakeholderTypeViewModel.SelectItemCommand?.Execute(null);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderType in e.OldItems.OfType<StakeholderType>())
                    {
                        var viewModelToRemove = Items.FirstOrDefault(viewModel => viewModel.IsViewModelFor(stakeholderType));
                        if (viewModelToRemove != null) Items.Remove(viewModelToRemove);
                    }
                    break;
            }
        }
    }
}