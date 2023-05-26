using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public abstract class PropertiesCollectionViewModelBase : ViewModelBase, IPropertyCollectionTreeNodeViewModel
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

        public ICommand SelectItem => null;

        public virtual string DisplayName { get; }

        public string IconSourceString { get; }

        public virtual bool CanRemove { get; }

        public virtual ICommand RemoveItemCommand => null;

        public virtual bool CanAdd => false;

        public virtual ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public virtual ObservableCollection<ContextMenuItemViewModel> ContextMenuItems =>
            new ObservableCollection<ContextMenuItemViewModel>();

        public abstract bool IsViewModelFor(object o);

        public virtual ObservableCollection<ITreeNodeViewModel> Items => new ObservableCollection<ITreeNodeViewModel>();

        public abstract CollectionType CollectionType { get; }
    }

    public class OnionDiagramPropertiesViewModel : PropertiesCollectionViewModelBase
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramPropertiesViewModel(ViewModelFactory factory, OnionDiagram diagram) : base(factory)
        {
            this.diagram = diagram;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                ViewModelFactory.CreateOnionRingsPropertiesViewModel(diagram),
                ViewModelFactory.CreateConnectionGroupsPropertiesViewModel(diagram)
            };
        }

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public override bool IsViewModelFor(object otherObject)
        {
            return otherObject as OnionDiagram == diagram;
        }
    }
}