using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderToTopCommand : ICommand
    {
        private readonly OnionDiagram diagram;
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        public MoveStakeholderToTopCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
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
            var ranksToLower = diagram.Stakeholders.Where(s => s.Rank > onionDiagramStakeholder.Rank).ToList();
            foreach (var diagramStakeholder in ranksToLower)
            {
                diagramStakeholder.Rank = diagramStakeholder.Rank - 1;
            }
            onionDiagramStakeholder.Rank = diagram.Stakeholders.Count - 1;

            foreach (var diagramStakeholder in ranksToLower)
            {
                diagramStakeholder.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
            }
            onionDiagramStakeholder.OnPropertyChanged(nameof(OnionDiagramStakeholder.Rank));
        }

        public event EventHandler CanExecuteChanged;
    }
}
