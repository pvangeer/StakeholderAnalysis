using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramsViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;

        public ProjectExplorerForceFieldDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            analysis.ForceFieldDiagrams.CollectionChanged += ForceFieldDiagramsCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>();
            foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
                Items.Add(ViewModelFactory.CreateProjectExplorerTwoAxisDiagramViewModel(forceFieldDiagram));

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

        public ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            analysis.ForceFieldDiagrams.Add(AnalysisFactory.CreateForceFieldDiagram("Nieuw diagram"));
        });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return null;
        }

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }

        public string DisplayName => "Krachtenveld diagrammen";

        public string IconSourceString { get; }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        private void ForceFieldDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var forceFieldDiagram in e.NewItems.OfType<TwoAxisDiagram>())
                {
                    var projectExplorerForceFieldDiagramViewModel =
                        ViewModelFactory.CreateProjectExplorerTwoAxisDiagramViewModel(forceFieldDiagram);
                    Items.Add(projectExplorerForceFieldDiagramViewModel);
                    if (IsExpanded)
                        projectExplorerForceFieldDiagramViewModel.SelectItemCommand?.Execute(null);
                }

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var forceFieldDiagram in e.OldItems.OfType<TwoAxisDiagram>())
                {
                    var diagramToRemove = Items.FirstOrDefault(d => d.IsViewModelFor(forceFieldDiagram));
                    if (diagramToRemove != null) Items.Remove(diagramToRemove);
                }
        }
    }
}