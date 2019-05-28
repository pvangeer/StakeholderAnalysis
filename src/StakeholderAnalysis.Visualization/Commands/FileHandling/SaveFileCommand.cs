using System;
using StakeholderAnalysis.Visualization.ViewModels;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class SaveFileCommand : FileHandlingCommandBase
    {
        public SaveFileCommand(RibbonViewModel ribbonViewModel) : base(ribbonViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            ribbonViewModel.GuiProjectServices.SaveProject();
        }
    }
}