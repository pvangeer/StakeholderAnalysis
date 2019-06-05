using System;
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
    public class ProjectExplorerOnionDiagramsViewModel : ViewModelBase, IExpandableDiagramCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerOnionDiagramsViewModel(ViewModelFactory factory, Analysis analysis, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;

            Diagrams = new ObservableCollection<IProjectExplorerDiagramViewModel>();
            foreach (var analysisOnionDiagram in analysis.OnionDiagrams)
            {
                Diagrams.Add(new ProjectExplorerOnionDiagramViewModel(ViewModelFactory, analysis,analysisOnionDiagram, viewManager));
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
            analysis.OnionDiagrams.Add(new OnionDiagram("Nieuw diagram"));
        });

        public string DisplayName => "Ui-diagrammen";

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var onionDiagram in e.NewItems.OfType<OnionDiagram>())
                {
                    Diagrams.Add(new ProjectExplorerOnionDiagramViewModel(ViewModelFactory, analysis,onionDiagram, viewManager));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var onionDiagram in e.OldItems.OfType<OnionDiagram>())
                {
                    var diagramToRemove = Diagrams.FirstOrDefault(d => d.IsViewModelFor(onionDiagram));
                    if (diagramToRemove != null)
                    {
                        Diagrams.Remove(diagramToRemove);
                    }
                }
            }
        }
    }
}
