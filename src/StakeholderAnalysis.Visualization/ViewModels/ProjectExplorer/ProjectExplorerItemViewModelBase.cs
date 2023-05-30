using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public abstract class ProjectExplorerItemViewModelBase : ViewModelBase, ITreeNodeCollectionViewModel
    {
        protected ProjectExplorerItemViewModelBase(ViewModelFactory factory) : base(factory)
        {
            Items = new ObservableCollection<ITreeNodeViewModel>();
            SelectItemCommand = CommandFactory.CreateSelectItemCommand(this);
        }

        public abstract string DisplayName { get; }

        public bool CanRemove => true;

        public abstract ICommand RemoveItemCommand { get; }

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public virtual bool CanOpen => OpenViewCommand != null;

        public virtual ICommand OpenViewCommand => null;

        public bool CanSelect => true;
        
        public bool IsSelected { get; set; }
        
        public ICommand SelectItemCommand { get; }
        
        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; protected set; }
        
        public virtual bool IsViewModelFor(object o)
        {
            return false;
        }

        public abstract string IconSourceString { get; }

        public bool IsExpandable => false;

        public bool IsExpanded { get; set; }

        public ICommand ToggleIsExpandedCommand => null;

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;
    }
}