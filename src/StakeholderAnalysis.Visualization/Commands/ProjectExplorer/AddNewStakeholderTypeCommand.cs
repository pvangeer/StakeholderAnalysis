using System;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class AddNewStakeholderTypeCommand : ICommand
    {
        private readonly Analysis analysis;

        public AddNewStakeholderTypeCommand(Analysis analysis)
        {
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return analysis != null;
        }

        public void Execute(object parameter)
        {
            analysis.StakeholderTypes.Add(new StakeholderType());
        }

        public event EventHandler CanExecuteChanged;
    }
}