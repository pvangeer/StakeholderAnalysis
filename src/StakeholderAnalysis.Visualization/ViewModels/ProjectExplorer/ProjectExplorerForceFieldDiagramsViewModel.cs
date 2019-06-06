using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramsViewModel : ViewModelBase, IExpandableDiagramCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;

        public ProjectExplorerForceFieldDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            analysis.ForceFieldDiagrams.CollectionChanged += ForceFieldDiagramsCollectionChanged;

            Diagrams = new ObservableCollection<IProjectExplorerDiagramViewModel>();
            foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
            {
                Diagrams.Add(ViewModelFactory.CreateProjectExplorerForceFieldDiagramViewModel(forceFieldDiagram));
            }
        }

        public ObservableCollection<IProjectExplorerDiagramViewModel> Diagrams { get; }

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

        public ICommand AddNewDiagramCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            analysis.ForceFieldDiagrams.Add(new ForceFieldDiagram("Nieuw diagram"));
        });

        public string DisplayName => "Krachtenveld diagrammen";

        private void ForceFieldDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var forceFieldDiagram in e.NewItems.OfType<ForceFieldDiagram>())
                {
                    Diagrams.Add(ViewModelFactory.CreateProjectExplorerForceFieldDiagramViewModel(forceFieldDiagram));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var forceFieldDiagram in e.OldItems.OfType<ForceFieldDiagram>())
                {
                    var diagramToRemove = Diagrams.FirstOrDefault(d => d.IsViewModelFor(forceFieldDiagram));
                    if (diagramToRemove != null)
                    {
                        Diagrams.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
