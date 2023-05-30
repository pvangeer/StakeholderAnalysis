using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties
{
    public abstract class PropertiesCollectionViewModelBase : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private bool isExpanded = true;

        protected PropertiesCollectionViewModelBase(ViewModelFactory factory) : base(factory)
        {
        }

        public virtual bool IsExpandable => false;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }

        public virtual ICommand ToggleIsExpandedCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return null;
        }

        public virtual string DisplayName => "";

        // TODO: Remove from properties control and viewmodels?
        public string IconSourceString => "";

        public virtual bool CanRemove => false;

        public virtual ICommand RemoveItemCommand => null;

        public virtual bool CanAdd => false;

        public virtual ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public virtual ObservableCollection<ContextMenuItemViewModel> ContextMenuItems =>
            new ObservableCollection<ContextMenuItemViewModel>();

        public abstract override bool IsViewModelFor(object o);

        public virtual ObservableCollection<ITreeNodeViewModel> Items => new ObservableCollection<ITreeNodeViewModel>();

        public abstract CollectionType CollectionType { get; }
    }
}