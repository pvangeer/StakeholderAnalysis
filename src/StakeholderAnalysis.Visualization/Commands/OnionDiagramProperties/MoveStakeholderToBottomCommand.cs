using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderToBottomCommand : ICommand
    {
        private readonly OnionDiagram diagram;
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        public MoveStakeholderToBottomCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
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
            var ranksToIncrease = diagram.Stakeholders.Where(s => s.Rank < onionDiagramStakeholder.Rank).ToList();
            foreach (var diagramStakeholder in ranksToIncrease)
            {
                diagramStakeholder.Rank = diagramStakeholder.Rank + 1;
            }
            onionDiagramStakeholder.Rank = 0;

            foreach (var diagramStakeholder in ranksToIncrease)
            {
                diagramStakeholder.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
            }
            onionDiagramStakeholder.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
        }

        public event EventHandler CanExecuteChanged;
    }
}
