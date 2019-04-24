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
        private readonly Analysis analysis;
        private readonly OnionDiagram onionDiagramViewModel;

        public RemoveOnionDiagramCommand(Analysis analysis, OnionDiagram onionDiagramViewModel)
        {
            this.analysis = analysis;
            this.onionDiagramViewModel = onionDiagramViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            analysis.OnionDiagrams.Remove(onionDiagramViewModel);
        }

        public event EventHandler CanExecuteChanged;
    }
}
