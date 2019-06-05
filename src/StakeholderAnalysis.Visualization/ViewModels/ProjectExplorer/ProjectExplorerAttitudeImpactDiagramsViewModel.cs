using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerAttitudeImpactDiagramsViewModel : ViewModelBase, IExpandableDiagramCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;

        public ProjectExplorerAttitudeImpactDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            analysis.AttitudeImpactDiagrams.CollectionChanged += AttitudeImpactDiagramsCollectionChanged;

            Diagrams = new ObservableCollection<IProjectExplorerDiagramViewModel>();
            foreach (var forceFieldDiagram in analysis.AttitudeImpactDiagrams)
            {
                Diagrams.Add(ViewModelFactory.CreateProjectExplorerDiagramViewModel(analysis, forceFieldDiagram));
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
            analysis.AttitudeImpactDiagrams.Add(new AttitudeImpactDiagram("Nieuw diagram"));
        });

        public string DisplayName => "Houding - impact";

        private void AttitudeImpactDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var attitudeImpactDiagram in e.NewItems.OfType<AttitudeImpactDiagram>())
                {
                    Diagrams.Add(ViewModelFactory.CreateProjectExplorerDiagramViewModel(analysis, attitudeImpactDiagram));
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
