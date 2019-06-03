using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class CommandFactory
    {
        private readonly Analysis analysis;

        public CommandFactory(Analysis analysis)
        {
            this.analysis = analysis;
        }

        public ICommand CreateRemoveStakeholderTypeCommand(StakeholderType stakeholderType)
        {
            return new RemoveStakeholderTypeCommand(stakeholderType, analysis);
        }
    }
}
