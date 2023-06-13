using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramsViewModel : ProjectExplorerDiagramsViewModelBase<TwoAxisDiagram>
    {
        public ProjectExplorerForceFieldDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory, analysis,
            analysis.ForceFieldDiagrams)
        {
        }

        public override ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            Analysis.ForceFieldDiagrams.Add(AnalysisFactory.CreateForceFieldDiagram("Nieuw diagram"));
        });

        public override string DisplayName => "Krachtenveld diagrammen";
    }
}