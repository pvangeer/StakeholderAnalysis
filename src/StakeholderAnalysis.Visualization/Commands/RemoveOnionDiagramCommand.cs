using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class RemoveOnionDiagramCommand : ICommand
    {
        private readonly OnionDiagramViewModel viewModel;

        public RemoveOnionDiagramCommand(OnionDiagramViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RemoveOnionDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
