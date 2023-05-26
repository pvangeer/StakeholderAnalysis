using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class PropertyCollectionTreeNodeViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private string iconSourceString;
        private bool isExpanded;

        public PropertyCollectionTreeNodeViewModel(ViewModelFactory factory, string displayName,
            ObservableCollection<ITreeNodeViewModel> items, CollectionType collectionType) : base(factory)
        {
            DisplayName = displayName;
            Items = items;
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
            CollectionType = collectionType;
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

        public CollectionType CollectionType { get; }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public string DisplayName { get; set; }

        public string IconSourceString
        {
            get => iconSourceString;
            set
            {
                iconSourceString = value;
                OnPropertyChanged(IconSourceString);
            }
        }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItem => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
        {
            return false;
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }
    }
}