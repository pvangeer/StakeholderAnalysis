using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class RemoveStakeholderTypeCommand : ICommand
    {
        private readonly Analysis analysis;
        private readonly StakeholderType stakeholderType;

        public RemoveStakeholderTypeCommand(StakeholderType stakeholderType, Analysis analysis)
        {
            this.stakeholderType = stakeholderType;
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return analysis != null && stakeholderType != null;
        }

        public void Execute(object parameter)
        {
            foreach (var stakeholder in analysis.Stakeholders.Where(s => s.Type == stakeholderType).ToList())
                AnalysisServices.RemoveStakeholderFromAnalysis(analysis, stakeholder);

            analysis.StakeholderTypes.Remove(stakeholderType);
        }

        public event EventHandler CanExecuteChanged;
    }
}