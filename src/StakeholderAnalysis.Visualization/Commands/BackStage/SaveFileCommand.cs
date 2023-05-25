using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands.BackStage
{
    public class SaveFileCommand : FileHandlingCommandBase
    {
        public SaveFileCommand(GuiProjectServices guiProjectServices) : base(guiProjectServices)
        {
        }

        public override void Execute(object parameter)
        {
            GuiProjectServices.SaveProject();
        }
    }
}