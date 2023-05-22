using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public abstract class PropertyTreeNodeViewModelBase : NotifyPropertyChangedObservable, ITreeNodeViewModel
    {
        public PropertyTreeNodeViewModelBase(string displayName)
        {
            DisplayName = displayName;
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public string DisplayName { get; }

        public bool IsExpandable => false;

        public bool IsExpanded
        {
            get => false;
            set { }
        }

        public ICommand ToggleIsExpandedCommand => null;

        public string IconSourceString => null;

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;
        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public abstract bool IsViewModelFor(object o);
    }
}