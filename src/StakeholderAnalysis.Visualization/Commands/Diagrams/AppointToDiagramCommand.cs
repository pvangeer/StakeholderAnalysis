using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.Commands.Diagrams
{
    public class AppointToDiagramCommand : ICommand
    {
        private readonly IStakeholderDiagram diagram;
        private readonly StakeholderTableViewModel viewModel;

        public AppointToDiagramCommand(StakeholderTableViewModel viewModel, IStakeholderDiagram diagram)
        {
            this.viewModel = viewModel;
            this.diagram = diagram;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.Stakeholders.Any(s => s.IsSelected);
        }

        public void Execute(object parameter)
        {
            foreach (var tableStakeholderViewModel in viewModel.Stakeholders.Where(s => s.IsSelected))
                AnalysisServices.AddStakeholderToDiagram(diagram, tableStakeholderViewModel.Stakeholder);
        }

        public event EventHandler CanExecuteChanged;
    }
}