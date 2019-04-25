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
    public class ProjectExplorerAttitudeImpactDiagramsViewModel : NotifyPropertyChangedObservable, IExpandableContentGroup
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerAttitudeImpactDiagramsViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.AttitudeImpactDiagrams.CollectionChanged += AttitudeImpactDiagramsCollectionChanged;

            Elements = new ObservableCollection<IProjectExplorerDiagramViewModel>();
            foreach (var forceFieldDiagram in analysis.AttitudeImpactDiagrams)
            {
                Elements.Add(new ProjectExplorerAttitudeImpactDiagramViewModel(analysis, forceFieldDiagram, viewManager));
            }
        }

        public ObservableCollection<IProjectExplorerDiagramViewModel> Elements { get; }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleElementsCommand => new ToggleAttitudeImpactDiagramsListCommand(this);

        public ICommand AddElementCommand => new AddAttitudeImpactDiagramCommand(analysis);

        public string Name => "Omgeving";

        private void AttitudeImpactDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var attitudeImpactDiagram in e.NewItems.OfType<AttitudeImpactDiagram>())
                {
                    Elements.Add(new ProjectExplorerAttitudeImpactDiagramViewModel(analysis, attitudeImpactDiagram, viewManager));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var attitudeImpactDiagram in e.OldItems.OfType<AttitudeImpactDiagram>())
                {
                    var diagramToRemove = Elements.FirstOrDefault(d => d.IsViewModelFor(attitudeImpactDiagram));
                    if (diagramToRemove != null)
                    {
                        Elements.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
