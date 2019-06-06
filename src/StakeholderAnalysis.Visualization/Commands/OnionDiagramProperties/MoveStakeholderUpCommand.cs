using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderUpCommand : ICommand
    {
        private readonly OnionDiagram diagram;
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        public MoveStakeholderUpCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
        {
            this.diagram = diagram;
            this.onionDiagramStakeholder = onionDiagramStakeholder;
            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += (o, e) => CanExecuteChanged?.Invoke(this, null);
            }

            onionDiagramStakeholder.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(OnionDiagramStakeholder.Rank))
                {
                    CanExecuteChanged?.Invoke(this, null);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            return diagram != null && onionDiagramStakeholder != null && onionDiagramStakeholder.Rank != diagram.Stakeholders.Max(s => s.Rank);
        }

        public void Execute(object parameter)
        {
            var rankHigher = diagram.Stakeholders.FirstOrDefault(s => s.Rank == onionDiagramStakeholder.Rank + 1);

            if (rankHigher != null)
            {
                onionDiagramStakeholder.Rank = onionDiagramStakeholder.Rank + 1;
                rankHigher.Rank = rankHigher.Rank - 1;
                onionDiagramStakeholder.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
                rankHigher.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
