using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class NewProjectCommand : FileHandlingCommandBase
    {
        public NewProjectCommand(GuiProjectServices guiProjectServices) : base(guiProjectServices)
        {
        }

        public override void Execute(object parameter)
        {
            GuiProjectServices.NewProject();
        }
    }
}