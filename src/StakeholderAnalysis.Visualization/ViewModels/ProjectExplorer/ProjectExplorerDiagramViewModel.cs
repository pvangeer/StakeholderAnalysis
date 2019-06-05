using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerDiagramViewModel : ViewModelBase, IProjectExplorerDiagramViewModel
    {
        private readonly AttitudeImpactDiagram diagram;
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public ProjectExplorerDiagramViewModel(ViewModelFactory factory, Analysis analysis, AttitudeImpactDiagram attitudeImpactDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = attitudeImpactDiagram;
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
            var viewInfo = viewManager?.Views.FirstOrDefault(vi => vi.ViewModel is AttitudeImpactDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null)
            {
                viewManager.CloseView(viewInfo);
            }
            analysis.AttitudeImpactDiagrams.Remove(diagram);
        });

        public ICommand OpenViewForDiagramCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is AttitudeImpactDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, new AttitudeImpactDiagramViewModel(diagram),IconSourceString, true);
                viewManager.OpenView(viewInfo);
            }
            viewManager.BringToFront(viewInfo);
        });

        public string IconSourceString => "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/involvement.png";

        public bool IsViewModelFor(object otherObject)
        {
            return otherObject as AttitudeImpactDiagram == diagram;
        }
    }
}
