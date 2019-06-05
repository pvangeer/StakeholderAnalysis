using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramViewModel : ViewModelBase, IProjectExplorerDiagramViewModel
    {
        private readonly ForceFieldDiagram diagram;
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerForceFieldDiagramViewModel(ViewModelFactory factory, Analysis analysis, ForceFieldDiagram forceFieldDiagram, ViewManager viewManager) : base(factory)
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

        public ICommand RemoveDiagramCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi => vi.ViewModel is ForceFieldDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null)
            {
                viewManager.CloseView(viewInfo);
            }
            analysis.ForceFieldDiagrams.Remove(diagram);
        });

        public ICommand OpenViewForDiagramCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is ForceFieldDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, ViewModelFactory.CreateForceFieldDiagramViewModel(diagram),IconSourceString, true);
                viewManager.OpenView(viewInfo);
            }
            viewManager.BringToFront(viewInfo);
        });

        public string IconSourceString => "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/forces.png";

        public bool IsViewModelFor(object otherObject)
        {
            return otherObject as ForceFieldDiagram == diagram;
        }
    }
}
