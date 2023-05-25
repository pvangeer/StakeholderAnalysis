using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands.BackStage
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