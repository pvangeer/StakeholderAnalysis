using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Controls;

namespace StakeholderAnalysis.Visualization.Commands.Ribbon
{
    public class AddStakeholdersToDiagramCommand : ICommand
    {
        private readonly OnionDiagram diagram;
        private readonly Analysis analysis;

        public AddStakeholdersToDiagramCommand(OnionDiagram onionDiagram, Analysis analysis)
        {
            this.diagram = onionDiagram;
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // Show dialog and select stakeholders
            var dialog = new SelectStakeholdersDialog(analysis)
            {
                Owner = Application.Current.MainWindow
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            // Add stakeholders to diagram
            var selectedStakeholders = dialog.SelectedStakeholders;
            var currentStakeholders = diagram.Stakeholders.Select(s => s.Stakeholder);
            foreach (var selectedStakeholder in selectedStakeholders.Except(currentStakeholders))
            {
                diagram.Stakeholders.Add(new OnionDiagramStakeholder(selectedStakeholder, 0.5, 0.5));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
