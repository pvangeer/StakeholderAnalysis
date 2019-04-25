using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramsViewModel : NotifyPropertyChangedObservable, IExpandableContentGroup
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerOnionDiagramsViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;

            OnionDiagrams = new ObservableCollection<ProjectExplorerOnionDiagramViewModel>();
            foreach (var analysisOnionDiagram in analysis.OnionDiagrams)
            {
                OnionDiagrams.Add(new ProjectExplorerOnionDiagramViewModel(analysis,analysisOnionDiagram, viewManager));
            }
        }

        public ObservableCollection<ProjectExplorerOnionDiagramViewModel> OnionDiagrams { get; }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleElementsCommand => new ToggleOnionsListCommand(this);

        public ICommand AddElementCommand => new AddOnionDiagramCommand(analysis);

        public string Name => "Ui-diagrammen";

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var onionDiagram in e.NewItems.OfType<OnionDiagram>())
                {
                    OnionDiagrams.Add(new ProjectExplorerOnionDiagramViewModel(analysis,onionDiagram, viewManager));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var onionDiagram in e.OldItems.OfType<OnionDiagram>())
                {
                    var diagramToRemove = OnionDiagrams.FirstOrDefault(d => d.IsViewModelFor(onionDiagram));
                    if (diagramToRemove != null)
                    {
                        OnionDiagrams.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
