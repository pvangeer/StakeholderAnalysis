using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramsViewModel : ProjectExplorerDiagramsViewModelBase<OnionDiagram>
    {
        public ProjectExplorerOnionDiagramsViewModel(ViewModelFactory factory, Analysis analysis) : base(factory, analysis,
            analysis.OnionDiagrams)
        {
        }

        public override string DisplayName => "Ui-diagrammen";

        public override ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            Analysis.OnionDiagrams.Add(AnalysisFactory.CreateOnionDiagram("Nieuw diagram"));
        });
    }
}