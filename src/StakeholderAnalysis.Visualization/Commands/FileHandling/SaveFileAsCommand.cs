using System;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class SaveFileAsCommand : FileHandlingCommandBase
    {
        public SaveFileAsCommand(GuiProjectServices guiProjectServices) : base(guiProjectServices)
        {
        }

        public override void Execute(object parameter)
        {
            GuiProjectServices.SaveProjectAs();
        }
    }
}