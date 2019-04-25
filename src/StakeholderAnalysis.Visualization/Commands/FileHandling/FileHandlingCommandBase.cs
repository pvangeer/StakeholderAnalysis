using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public abstract class FileHandlingCommandBase : ICommand
    {
        protected RibbonViewModel ribbonViewModel;

        protected FileHandlingCommandBase(RibbonViewModel ribbonViewModel)
        {
            this.ribbonViewModel = ribbonViewModel;
        }

        public virtual bool CanExecute(object parameter)
        {
            return false;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}