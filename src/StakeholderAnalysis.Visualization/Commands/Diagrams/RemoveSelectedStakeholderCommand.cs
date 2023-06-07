using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.Commands.Diagrams
{
    public class RemoveSelectedStakeholderCommand : ICommand
    {
        private readonly Analysis analysis;
        private readonly StakeholderTableViewModel viewModel;

        public RemoveSelectedStakeholderCommand(StakeholderTableViewModel stakeholderTableViewModel, Analysis analysis)
        {
            viewModel = stakeholderTableViewModel;
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return analysis != null && viewModel.Stakeholders.Any(st => st.IsSelected);
        }

        public void Execute(object parameter)
        {
            // TODO: Ask the user for confirmation?
            foreach (var stakeholderViewModel in viewModel.Stakeholders.Where(s => s.IsSelected).ToList())
                AnalysisServices.RemoveStakeholderFromAnalysis(analysis, stakeholderViewModel.Stakeholder);
        }

        public event EventHandler CanExecuteChanged;
    }
}