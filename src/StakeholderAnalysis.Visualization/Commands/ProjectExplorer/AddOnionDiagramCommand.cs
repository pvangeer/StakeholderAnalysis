using System;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class AddOnionDiagramCommand : ICommand
    {
        private Analysis analysis;

        public AddOnionDiagramCommand(Analysis analysis)
        {
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            analysis.OnionDiagrams.Add(new OnionDiagram("Nieuw diagram"));
        }

        public event EventHandler CanExecuteChanged;
    }
}
