﻿using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.Diagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderToBottomCommand : ICommand
    {
        private readonly IStakeholderDiagram diagram;
        private readonly PositionedStakeholder stakeholder;

        public MoveStakeholderToBottomCommand(IStakeholderDiagram diagram,
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
            var ranksToIncrease = diagram.Stakeholders.Where(s => s.Rank < stakeholder.Rank).ToList();
            foreach (var diagramStakeholder in ranksToIncrease) diagramStakeholder.Rank = diagramStakeholder.Rank + 1;
            stakeholder.Rank = 0;

            foreach (var diagramStakeholder in ranksToIncrease)
                diagramStakeholder.OnPropertyChanged(nameof(PositionedStakeholder.Rank));
            stakeholder.OnPropertyChanged(nameof(PositionedStakeholder.Rank));
        }

        public event EventHandler CanExecuteChanged;
    }
}