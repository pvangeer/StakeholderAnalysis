using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.AttitudeImpactDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramStakeholderViewModel : DiagramStakeholderViewModel, IPositionedStakeholderViewModel
    {
        public AttitudeImpactDiagramStakeholderViewModel(ViewModelFactory factory, AttitudeImpactDiagram diagram,
            PositionedStakeholder stakeholder, ISelectionRegister selectionRegister)
            : base(factory, stakeholder, diagram, selectionRegister)
        {
        }
    }
}