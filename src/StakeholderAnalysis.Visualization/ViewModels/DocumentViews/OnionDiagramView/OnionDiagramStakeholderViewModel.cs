using System.Linq;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class OnionDiagramStakeholderViewModel : DiagramStakeholderViewModel
    {
        private readonly OnionDiagram onionDiagram;

        public OnionDiagramStakeholderViewModel(ViewModelFactory factory, OnionDiagram diagram,
            PositionedStakeholder stakeholder,
            ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory,
            stakeholder, diagram, selectionRegister)
        {
            onionDiagram = diagram;
            DrawConnectionHandler = drawConnectionHandler;
        }

        public IDrawConnectionHandler DrawConnectionHandler { get; }

        public override bool IsConnectionToTarget =>
            DrawConnectionHandler != null && DrawConnectionHandler.IsConnectionTarget(Stakeholder);

        public override void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder)
            {
                Diagram.Stakeholders.Remove(PositionedStakeholder);
                var connectionsToRemove = onionDiagram.Connections.Where(c =>
                    c.ConnectFrom == PositionedStakeholder || c.ConnectTo == PositionedStakeholder).ToList();
                foreach (var connection in connectionsToRemove) onionDiagram.Connections.Remove(connection);
            }
        }
    }
}