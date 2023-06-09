using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public class ForceFieldDiagramStakeholderViewModel : DiagramStakeholderViewModel, IPositionedStakeholderViewModel
    {
        public ForceFieldDiagramStakeholderViewModel(ViewModelFactory factory, ForceFieldDiagram diagram,
            PositionedStakeholder stakeholder, ISelectionRegister selectionRegister) :
            base(factory, stakeholder, diagram, selectionRegister)
        {
        }
    }
}