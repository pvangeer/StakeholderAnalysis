using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramViewModel : ViewModelBase, IProjectExplorerDiagramViewModel
    {
        private readonly OnionDiagram diagram;
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerOnionDiagramViewModel(ViewModelFactory factory, Analysis analysis, OnionDiagram onionDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = onionDiagram;
            if (diagram != null)
            {
                diagram.PropertyChanged += DiagramPropertyChanged;
            }
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
            }
        }

        public string Name
        {
            get => diagram.Name;
            set
            {
                diagram.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand RemoveDiagramCommand => new RemoveDiagramCommand(this);

        public ICommand OpenViewForDiagramCommand => new OpenDiagramCommand(this);

        public bool IsViewModelFor(object otherObject)
        {
            return otherObject as OnionDiagram == this.diagram;
        }

        public string IconSourceString => "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png";

        public void OpenDiagramInDocumentView()
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is OnionDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                var onionDiagramViewModel = new OnionDiagramViewModel(ViewModelFactory, diagram);
                viewInfo = new ViewInfo(diagram.Name, onionDiagramViewModel, IconSourceString, true);
                viewManager.OpenView(viewInfo);
            }
            viewManager.BringToFront(viewInfo);
        }

        public void RemoveDiagram()
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi => vi.ViewModel is OnionDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null)
            {
                viewManager.CloseView(viewInfo);
            }
            analysis.OnionDiagrams.Remove(diagram);
        }
    }
}
