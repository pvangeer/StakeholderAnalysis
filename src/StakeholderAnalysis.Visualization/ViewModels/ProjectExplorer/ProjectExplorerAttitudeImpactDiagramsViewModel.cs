using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerAttitudeImpactDiagramsViewModel : NotifyPropertyChangedObservable, IExpandableContentGroupViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerAttitudeImpactDiagramsViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.AttitudeImpactDiagrams.CollectionChanged += AttitudeImpactDiagramsCollectionChanged;

            Diagrams = new ObservableCollection<IProjectExplorerDiagramViewModel>();
            foreach (var forceFieldDiagram in analysis.AttitudeImpactDiagrams)
            {
                Diagrams.Add(new ProjectExplorerDiagramViewModel(analysis, forceFieldDiagram, viewManager));
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

        public string Name => "Houding - impact";

        public void AddNewDiagram()
        {
            analysis.AttitudeImpactDiagrams.Add(new AttitudeImpactDiagram("Nieuw diagram"));
        }

        private void AttitudeImpactDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var attitudeImpactDiagram in e.NewItems.OfType<AttitudeImpactDiagram>())
                {
                    Diagrams.Add(new ProjectExplorerDiagramViewModel(analysis, attitudeImpactDiagram, viewManager));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var attitudeImpactDiagram in e.OldItems.OfType<AttitudeImpactDiagram>())
                {
                    var diagramToRemove = Diagrams.FirstOrDefault(d => d.IsViewModelFor(attitudeImpactDiagram));
                    if (diagramToRemove != null)
                    {
                        Diagrams.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
