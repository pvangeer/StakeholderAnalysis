using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderDownCommand : ICommand
    {
        private readonly IStakeholderDiagram diagram;
        private readonly PositionedStakeholder stakeholder;

        public MoveStakeholderDownCommand(IStakeholderDiagram diagram,
            PositionedStakeholder stakeholder)
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
            return diagram != null && stakeholder != null && stakeholder.Rank != 0;
        }

        public void Execute(object parameter)
        {
            var rankLower = diagram.Stakeholders.FirstOrDefault(s => s.Rank == stakeholder.Rank - 1);
            if (rankLower != null)
            {
                rankLower.Rank = rankLower.Rank + 1;
                stakeholder.Rank = stakeholder.Rank - 1;
                stakeholder.OnPropertyChanged(nameof(PositionedStakeholder.Rank));
                rankLower.OnPropertyChanged(nameof(PositionedStakeholder.Rank));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}