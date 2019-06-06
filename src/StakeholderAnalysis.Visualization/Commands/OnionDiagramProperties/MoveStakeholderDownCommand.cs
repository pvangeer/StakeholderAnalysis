using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderDownCommand : ICommand
    {
        private readonly OnionDiagram diagram;
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        public MoveStakeholderDownCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
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
            return diagram != null && onionDiagramStakeholder != null && onionDiagramStakeholder.Rank != 0;
        }

        public void Execute(object parameter)
        {
            var rankLower = diagram.Stakeholders.FirstOrDefault(s => s.Rank == onionDiagramStakeholder.Rank - 1);
            if (rankLower != null)
            {
                rankLower.Rank = rankLower.Rank + 1;
                onionDiagramStakeholder.Rank = onionDiagramStakeholder.Rank - 1;
                onionDiagramStakeholder.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
                rankLower.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
