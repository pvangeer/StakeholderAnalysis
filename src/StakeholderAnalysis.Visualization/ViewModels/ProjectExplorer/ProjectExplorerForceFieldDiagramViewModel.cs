using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramViewModel : NotifyPropertyChangedObservable, IProjectExplorerDiagramViewModel
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

        public ICommand RemoveDiagramCommand => new RemoveDiagramCommand(this);

        public ICommand OpenViewForDiagramCommand => new OpenDiagramCommand(this);

        public string IconSourceString => "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/forces.png";

        public bool IsViewModelFor(object otherObject)
        {
            return otherObject as ForceFieldDiagram == diagram;
        }

        public void OpenDiagramInDocumentView()
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is ForceFieldDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, new ForceFieldDiagramViewModel(diagram),IconSourceString);
                viewManager.OpenView(viewInfo);
            }
            viewManager.BringToFront(viewInfo);
        }

        public void RemoveDiagram()
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi => vi.ViewModel is ForceFieldDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null)
            {
                viewManager.CloseView(viewInfo);
            }
            analysis.ForceFieldDiagrams.Remove(diagram);
        }
    }
}
