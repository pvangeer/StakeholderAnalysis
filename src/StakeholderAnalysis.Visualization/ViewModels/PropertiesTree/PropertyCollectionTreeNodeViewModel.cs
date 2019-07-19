using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class PropertyCollectionTreeNodeViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private bool isExpanded;
        private string iconSourceString;

        public PropertyCollectionTreeNodeViewModel(ViewModelFactory factory, string displayName, ObservableCollection<ITreeNodeViewModel> items, CollectionType collectionType) : base(factory)
        {
            DisplayName = displayName;
            Items = items;
            CollectionType = collectionType;
        }

        public virtual bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public virtual CollectionType CollectionType { get; }

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

        public bool IsViewModelFor(object o)
        {
            return false;
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }
    }
}
