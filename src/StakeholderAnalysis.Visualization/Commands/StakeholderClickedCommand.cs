using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class StakeholderClickedCommand : ICommand
    {
        private StakeholderViewModel stakeholderViewModel;

        public StakeholderClickedCommand(StakeholderViewModel stakeholderViewModel)
        {
            this.stakeholderViewModel = stakeholderViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return stakeholderViewModel != null;
        }

        public void Execute(object parameter)
        {
            stakeholderViewModel.SelectStakeholder();
        }

        public event EventHandler CanExecuteChanged;
    }
}
