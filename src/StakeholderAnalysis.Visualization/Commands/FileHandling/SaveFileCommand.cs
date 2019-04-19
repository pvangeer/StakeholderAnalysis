using System;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public class SaveFileCommand : FileHandlingCommandBase
    {
        public SaveFileCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}