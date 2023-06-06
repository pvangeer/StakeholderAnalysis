using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.Commands.Diagrams
{
    public class ChangeStakeholderTypeCommand : ICommand
    {
        private readonly StakeholderTableViewModel tableViewModel;

        public ChangeStakeholderTypeCommand(StakeholderTableViewModel tableViewModel)
        {
            this.tableViewModel = tableViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var type = parameter as StakeholderType;
            foreach (var selectedStakeholder in tableViewModel.Stakeholders.Where(st => st.IsSelected && st.Type != type))
            {
                selectedStakeholder.Type = type;
                selectedStakeholder.OnPropertyChanged(nameof(selectedStakeholder.Type));
            }

            tableViewModel.StakeholderViewSource.View.Refresh();
        }

        public event EventHandler CanExecuteChanged;
    }
}