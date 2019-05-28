using System;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class SaveFileAsCommand : FileHandlingCommandBase
    {
        public SaveFileAsCommand(RibbonViewModel ribbonViewModel) : base(ribbonViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            ribbonViewModel.GuiProjectServices.SaveProjectAs();
        }
    }
}