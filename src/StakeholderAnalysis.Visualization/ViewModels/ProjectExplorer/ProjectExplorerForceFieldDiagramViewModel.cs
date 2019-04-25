using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramViewModel : NotifyPropertyChangedObservable
    {
        private readonly ForceFieldDiagram diagram;
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerForceFieldDiagramViewModel(Analysis analysis, ForceFieldDiagram forceFieldDiagram, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = forceFieldDiagram;
            if (diagram != null)
            {
                diagram.PropertyChanged += DiagramPropertyChanged;
            }
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ForceFieldDiagram.Name):
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

        public ICommand RemoveForceFieldDiagramCommand => new RemoveForceFieldDiagramCommand(this);

        public ICommand OpenForceFieldDiagramCommand => new OpenForceFieldDiagramCommand(this);

        public bool IsViewModelFor(ForceFieldDiagram otherDiagram)
        {
            return otherDiagram == this.diagram;
        }

        public void OpenDiagramInDocumentView()
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is ForceFieldDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, new ForceFieldDiagramViewModel(diagram),
                    "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png");
                viewManager.OpenView(viewInfo);
            }
            viewManager.BringToFront(viewInfo);
        }

        public void RemoveForceFieldDiagram()
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi => vi.ViewModel is ForceFieldDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null)
            {
                viewManager.CloseView(viewInfo);
                analysis.ForceFieldDiagrams.Remove(diagram);
            }
        }
    }
}
