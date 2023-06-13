using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerAttitudeImpactDiagramsViewModel : ProjectExplorerDiagramsViewModelBase<TwoAxisDiagram>
    {
        public ProjectExplorerAttitudeImpactDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory, analysis,
            analysis.AttitudeImpactDiagrams)
        {
        }

        public override ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            Analysis.AttitudeImpactDiagrams.Add(AnalysisFactory.CreateAttitudeImpactDiagram("Nieuw diagram"));
        });

        public override string DisplayName => "Houding-impact diagrammen";
    }
}