using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramsViewModel : NotifyPropertyChangedObservable, IExpandableContentGroup
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerForceFieldDiagramsViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.ForceFieldDiagrams.CollectionChanged += ForceFieldDiagramsCollectionChanged;

            ForceFieldDiagrams = new ObservableCollection<ProjectExplorerForceFieldDiagramViewModel>();
            foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
            {
                ForceFieldDiagrams.Add(new ProjectExplorerForceFieldDiagramViewModel(analysis, forceFieldDiagram, viewManager));
            }
        }

        public ObservableCollection<ProjectExplorerForceFieldDiagramViewModel> ForceFieldDiagrams { get; }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleElementsCommand => new ToggleForceFieldsListCommand(this);

        public ICommand AddElementCommand => new AddForceFieldDiagramCommand(analysis);

        public string Name => "Krachtenveld diagrammen";

        private void ForceFieldDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var forceFieldDiagram in e.NewItems.OfType<ForceFieldDiagram>())
                {
                    ForceFieldDiagrams.Add(new ProjectExplorerForceFieldDiagramViewModel(analysis, forceFieldDiagram, viewManager));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var forceFieldDiagram in e.OldItems.OfType<ForceFieldDiagram>())
                {
                    var diagramToRemove = ForceFieldDiagrams.FirstOrDefault(d => d.IsViewModelFor(forceFieldDiagram));
                    if (diagramToRemove != null)
                    {
                        ForceFieldDiagrams.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
