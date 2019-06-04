using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class OpenFileCommand : FileHandlingCommandBase
    {
        public OpenFileCommand(GuiProjectServices guiProjectServices) : base(guiProjectServices)
        {
        }

        public override void Execute(object parameter)
        {
            GuiProjectServices.OpenProject();
        }
    }
}