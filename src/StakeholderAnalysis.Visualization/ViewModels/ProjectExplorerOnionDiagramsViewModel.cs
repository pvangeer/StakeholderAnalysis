using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ProjectExplorerOnionDiagramsViewModel : NotifyPropertyChangedObservable
    {
        private Analysis analysis;
        private bool isOnionDiagramsExpanded = true;
        private readonly ViewManager viewManager;

        public ProjectExplorerOnionDiagramsViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;

            OnionDiagrams = new ObservableCollection<OnionDiagramViewModel>();
            foreach (var analysisOnionDiagram in analysis.OnionDiagrams)
            {
                OnionDiagrams.Add(new OnionDiagramViewModel(analysis,analysisOnionDiagram, viewManager));
            }
        }

        public ObservableCollection<OnionDiagramViewModel> OnionDiagrams { get; }

        public bool IsOnionDiagramsExpanded
        {
            get => isOnionDiagramsExpanded;
            set
            {
                isOnionDiagramsExpanded = value;
                OnPropertyChanged(nameof(IsOnionDiagramsExpanded));
            }
        }

        public ICommand ToggleOnionsListCommand => new ToggleOnionsListCommand(this);

        public ICommand AddOnionDiagramCommand => new AddOnionDiagramCommand(analysis);

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var onionDiagram in e.NewItems.OfType<OnionDiagram>())
                {
                    OnionDiagrams.Add(new OnionDiagramViewModel(analysis,onionDiagram, viewManager));
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
