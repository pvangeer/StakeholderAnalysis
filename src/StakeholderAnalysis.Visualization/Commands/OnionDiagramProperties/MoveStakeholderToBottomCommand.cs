using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderToBottomCommand<TStakeholder> : ICommand where TStakeholder : class , IRankedStakeholder
    {
        private readonly IRankedStakeholderDiagram<TStakeholder> diagram;
        private readonly IRankedStakeholder stakeholder;

        public MoveStakeholderToBottomCommand(IRankedStakeholderDiagram<TStakeholder> diagram, IRankedStakeholder stakeholder)
        {
            this.diagram = diagram;
            this.stakeholder = stakeholder;
            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += (o, e) => CanExecuteChanged?.Invoke(this, null);
            }

            stakeholder.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(IRankedStakeholder.Rank))
                {
                    CanExecuteChanged?.Invoke(this, null);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            return diagram != null && stakeholder != null && stakeholder.Rank != 0;
        }

        public void Execute(object parameter)
        {
            var ranksToIncrease = diagram.Stakeholders.Where(s => s.Rank < stakeholder.Rank).ToList();
            foreach (var diagramStakeholder in ranksToIncrease)
            {
                diagramStakeholder.Rank = diagramStakeholder.Rank + 1;
            }
            stakeholder.Rank = 0;

            foreach (var diagramStakeholder in ranksToIncrease)
            {
                diagramStakeholder.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
            }
            stakeholder.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
        }

        public event EventHandler CanExecuteChanged;
    }
}
