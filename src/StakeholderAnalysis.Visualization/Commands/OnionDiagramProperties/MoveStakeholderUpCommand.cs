using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.Diagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderUpCommand : ICommand
    {
        private readonly IStakeholderDiagram diagram;
        private readonly PositionedStakeholder stakeholder;

        public MoveStakeholderUpCommand(IStakeholderDiagram diagram, PositionedStakeholder stakeholder)
        {
            this.diagram = diagram;
            this.stakeholder = stakeholder;
            if (diagram != null)
                diagram.Stakeholders.CollectionChanged += (o, e) => CanExecuteChanged?.Invoke(this, null);

            stakeholder.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(PositionedStakeholder.Rank)) CanExecuteChanged?.Invoke(this, null);
            };
        }

        public bool CanExecute(object parameter)
        {
            return diagram != null && stakeholder != null && diagram.Stakeholders.Any() &&
                   stakeholder.Rank != diagram.Stakeholders.Max(s => s.Rank);
        }

        public void Execute(object parameter)
        {
            var rankHigher = diagram.Stakeholders.FirstOrDefault(s => s.Rank == stakeholder.Rank + 1);

            if (rankHigher != null)
            {
                stakeholder.Rank = stakeholder.Rank + 1;
                rankHigher.Rank = rankHigher.Rank - 1;
                stakeholder.OnPropertyChanged(nameof(PositionedStakeholder.Rank));
                rankHigher.OnPropertyChanged(nameof(PositionedStakeholder.Rank));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}