using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerOnionDiagramViewModel(Analysis analysis, OnionDiagram onionDiagram, ViewManager viewManager)
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

        public ICommand RemoveOnionDiagramCommand => new RemoveOnionDiagramCommand(this);

        public ICommand OpenOnionDiagramCommand => new OpenOnionDiagramCommand(this);

        public bool IsViewModelFor(OnionDiagram onionDiagram)
        {
            return onionDiagram == this.diagram;
        }

        public void OpenDiagramInDocumentView()
        {
            var viewInfo = new ViewInfo(diagram.Name, new OnionDiagramViewModel(diagram),
                "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png");
            viewManager.OpenView(viewInfo);
            viewManager.BringToFront(viewInfo);
        }

        public void RemoveOnionDiagram()
        {
            if (viewManager != null)
            {
                viewManager.CloseView(viewManager.Views.FirstOrDefault(vi => vi.ViewModel is ProjectExplorerOnionDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram)));
                analysis.OnionDiagrams.Remove(diagram);
            }
        }
    }
}
