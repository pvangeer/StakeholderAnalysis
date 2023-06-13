using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public abstract class ProjectExplorerDiagramsViewModelBase<TDiagram> : ViewModelBase, ITreeNodeCollectionViewModel
        where TDiagram : IStakeholderDiagram
    {
        protected readonly Analysis Analysis;
        private bool isExpanded = true;

        protected ProjectExplorerDiagramsViewModelBase(ViewModelFactory factory, Analysis analysis,
            ObservableCollection<TDiagram> diagramsCollection) : base(factory)
        {
            Analysis = analysis;

            Items = new ObservableCollection<ITreeNodeViewModel>();

            if (diagramsCollection != null)
            {
                diagramsCollection.CollectionChanged += DiagramsCollectionChanged;
                foreach (var diagram in diagramsCollection)
                    Items.Add(ViewModelFactory.CreateProjectExplorerDiagramViewModel(diagram));
            }

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyItemsCollection;

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

        public bool CanAdd => true;

        public abstract ICommand AddItemCommand { get; }

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public abstract string DisplayName { get; }

        public string IconSourceString => null;

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public object GetSelectableObject()
        {
            return null;
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }

        private void DiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var diagram in e.NewItems.OfType<IStakeholderDiagram>())
                {
                    var viewModel = ViewModelFactory.CreateProjectExplorerDiagramViewModel(diagram);
                    Items.Add(viewModel);
                    if (IsExpanded)
                        viewModel.SelectItemCommand?.Execute(null);
                }

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var diagram in e.OldItems.OfType<IStakeholderDiagram>())
                {
                    var diagramToRemove = Items.FirstOrDefault(d => d.IsViewModelFor(diagram));
                    if (diagramToRemove != null) Items.Remove(diagramToRemove);
                }
        }
    }
}