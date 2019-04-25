using System;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class OpenFileCommand : FileHandlingCommandBase
    {
        public OpenFileCommand(RibbonViewModel ribbonViewModel) : base(ribbonViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}