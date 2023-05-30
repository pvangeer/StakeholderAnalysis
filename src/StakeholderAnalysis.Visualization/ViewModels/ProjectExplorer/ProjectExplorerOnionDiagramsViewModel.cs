using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramsViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;

        public ProjectExplorerOnionDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;

            analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>();
            foreach (var analysisOnionDiagram in analysis.OnionDiagrams)
                Items.Add(ViewModelFactory.CreateProjectExplorerOnionDiagramViewModel(analysisOnionDiagram));
            
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public string DisplayName => "Ui-diagrammen";

        public string IconSourceString => null;

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => true;

        public ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            analysis.OnionDiagrams.Add(new OnionDiagram("Nieuw diagram"));
        });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItem => null;

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

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var onionDiagram in e.NewItems.OfType<OnionDiagram>())
                    {
                        var projectExplorerOnionDiagramViewModel =
                            ViewModelFactory.CreateProjectExplorerOnionDiagramViewModel(onionDiagram);
                        Items.Add(projectExplorerOnionDiagramViewModel);
                        if (IsExpanded)
                            projectExplorerOnionDiagramViewModel.SelectItem?.Execute(null);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var onionDiagram in e.OldItems.OfType<OnionDiagram>())
                    {
                        var diagramToRemove = Items.FirstOrDefault(d => d.IsViewModelFor(onionDiagram));
                        if (diagramToRemove != null) Items.Remove(diagramToRemove);
                    }
                    break;
            }
        }
    }
}