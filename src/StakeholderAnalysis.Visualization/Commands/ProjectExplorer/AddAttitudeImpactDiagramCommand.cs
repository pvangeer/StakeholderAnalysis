using System;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class AddAttitudeImpactDiagramCommand : ICommand
    {
        private Analysis analysis;

        public AddAttitudeImpactDiagramCommand(Analysis analysis)
        {
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            analysis.AttitudeImpactDiagrams.Add(new AttitudeImpactDiagram("Nieuw diagram"));
        }

        public event EventHandler CanExecuteChanged;
    }
}
