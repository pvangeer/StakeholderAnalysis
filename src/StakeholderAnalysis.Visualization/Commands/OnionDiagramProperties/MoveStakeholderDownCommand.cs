﻿using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class MoveStakeholderDownCommand<TStakeholder> : ICommand where TStakeholder : class, IRankedStakeholder
    {
        private readonly IRankedStakeholderDiagram<TStakeholder> diagram;
        private readonly IRankedStakeholder stakeholder;

        public MoveStakeholderDownCommand(IRankedStakeholderDiagram<TStakeholder> diagram, IRankedStakeholder stakeholder)
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
            var rankLower = diagram.Stakeholders.FirstOrDefault(s => s.Rank == stakeholder.Rank - 1);
            if (rankLower != null)
            {
                rankLower.Rank = rankLower.Rank + 1;
                stakeholder.Rank = stakeholder.Rank - 1;
                stakeholder.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
                rankLower.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
