using System;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class AddForceFieldDiagramCommand : ICommand
    {
        private Analysis analysis;

        public AddForceFieldDiagramCommand(Analysis analysis)
        {
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            analysis.ForceFieldDiagrams.Add(new ForceFieldDiagram("Nieuw diagram"));
        }

        public event EventHandler CanExecuteChanged;
    }
}
