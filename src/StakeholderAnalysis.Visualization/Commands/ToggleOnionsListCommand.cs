using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class ToggleOnionsListCommand : ICommand
    {
        private ProjectExplorerOnionDiagramsViewModel projectExplorerOnionDiagramsViewModel;

        public ToggleOnionsListCommand(ProjectExplorerOnionDiagramsViewModel projectExplorerOnionDiagramsViewModel)
        {
            this.projectExplorerOnionDiagramsViewModel = projectExplorerOnionDiagramsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            projectExplorerOnionDiagramsViewModel.IsOnionDiagramsExpanded =
                !projectExplorerOnionDiagramsViewModel.IsOnionDiagramsExpanded;
        }

        public event EventHandler CanExecuteChanged;
    }
}
