using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderToTopCommand : ICommand
    {
        private readonly IStakeholderDiagram diagram;
        private readonly IRankedStakeholder stakeholder;

        public MoveStakeholderToTopCommand(IStakeholderDiagram diagram,
            IRankedStakeholder stakeholder)
        {
            this.diagram = diagram;
            this.stakeholder = stakeholder;
            if (diagram != null)
                diagram.Stakeholders.CollectionChanged += (o, e) => CanExecuteChanged?.Invoke(this, null);

            stakeholder.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(IRankedStakeholder.Rank)) CanExecuteChanged?.Invoke(this, null);
            };
        }

        public bool CanExecute(object parameter)
        {
            return diagram != null && stakeholder != null && diagram.Stakeholders.Any() &&
                   stakeholder.Rank != diagram.Stakeholders.Max(s => s.Rank);
        }

        public void Execute(object parameter)
        {
            var ranksToLower = diagram.Stakeholders.Where(s => s.Rank > stakeholder.Rank).ToList();
            foreach (var diagramStakeholder in ranksToLower) diagramStakeholder.Rank = diagramStakeholder.Rank - 1;
            stakeholder.Rank = diagram.Stakeholders.Count - 1;

            foreach (var diagramStakeholder in ranksToLower)
                diagramStakeholder.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
            stakeholder.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
        }

        public event EventHandler CanExecuteChanged;
    }
}