using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramsViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        
        public ProjectExplorerOnionDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>();
            foreach (var analysisOnionDiagram in analysis.OnionDiagrams)
            {
                Items.Add(ViewModelFactory.CreateProjectExplorerOnionDiagramViewModel(analysisOnionDiagram));
            }
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
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public bool CanAdd => true;

        public ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            analysis.OnionDiagrams.Add(new OnionDiagram("Nieuw diagram"));
        });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool IsViewModelFor(object o)
        {
            return false;
        }

        public string DisplayName => "Ui-diagrammen";

        public string IconSourceString { get; }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var onionDiagram in e.NewItems.OfType<OnionDiagram>())
                {
                    Items.Add(ViewModelFactory.CreateProjectExplorerOnionDiagramViewModel(onionDiagram));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var onionDiagram in e.OldItems.OfType<OnionDiagram>())
                {
                    var diagramToRemove = Items.FirstOrDefault(d => d.IsViewModelFor(onionDiagram));
                    if (diagramToRemove != null)
                    {
                        Items.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
