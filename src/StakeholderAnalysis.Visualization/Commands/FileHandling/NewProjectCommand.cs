using System;
using StakeholderAnalysis.Visualization.ViewModels;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class NewProjectCommand : FileHandlingCommandBase
    {
        public NewProjectCommand(RibbonViewModel ribbonViewModel) : base(ribbonViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}