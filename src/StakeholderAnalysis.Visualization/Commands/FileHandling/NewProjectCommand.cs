using System;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class NewProjectCommand : FileHandlingCommandBase
    {
        public NewProjectCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}