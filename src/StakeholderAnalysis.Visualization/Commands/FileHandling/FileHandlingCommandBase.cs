using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands.FileHandling
{
    public abstract class FileHandlingCommandBase : ICommand
    {
        private MainWindowViewModel mainWindowViewModel;

        protected FileHandlingCommandBase(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public virtual bool CanExecute(object parameter)
        {
            return false;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}