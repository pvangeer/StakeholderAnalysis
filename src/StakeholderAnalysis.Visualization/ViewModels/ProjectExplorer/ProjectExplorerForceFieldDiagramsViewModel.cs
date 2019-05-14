using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramsViewModel : NotifyPropertyChangedObservable, IExpandableContentGroupViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerForceFieldDiagramsViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.ForceFieldDiagrams.CollectionChanged += ForceFieldDiagramsCollectionChanged;

            Diagrams = new ObservableCollection<IProjectExplorerDiagramViewModel>();
            foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
            {
                Diagrams.Add(new ProjectExplorerForceFieldDiagramViewModel(analysis, forceFieldDiagram, viewManager));
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

        public ICommand ToggleIsExpandedCommand => new ToggleIsExpandedCommand(this);

        public ICommand AddNewDiagramCommand => new AddNewDiagramCommand(this);

        public string Name => "Krachtenveld diagrammen";

        public void AddNewDiagram()
        {
            analysis.ForceFieldDiagrams.Add(new ForceFieldDiagram("Nieuw diagram"));
        }

        private void ForceFieldDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var forceFieldDiagram in e.NewItems.OfType<ForceFieldDiagram>())
                {
                    Diagrams.Add(new ProjectExplorerForceFieldDiagramViewModel(analysis, forceFieldDiagram, viewManager));
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
