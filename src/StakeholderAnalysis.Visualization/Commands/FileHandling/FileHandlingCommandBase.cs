using System;
using System.Windows.Input;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public abstract class FileHandlingCommandBase : ICommand
    {
        protected GuiProjectServices GuiProjectServices;

        protected FileHandlingCommandBase(GuiProjectServices guiProjectServices)
        {
            GuiProjectServices = guiProjectServices;
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}