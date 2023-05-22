using System;
using System.Windows.Input;

namespace StakeholderAnalysis.Gui
{
    public class CloseViewCommand : ICommand
    {
        private readonly ViewInfo viewInfo;
        private readonly ViewManager viewManager;

        public CloseViewCommand(ViewManager viewManager, ViewInfo viewInfo)
        {
            this.viewManager = viewManager;
            this.viewInfo = viewInfo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (viewManager != null && viewInfo != null)
            {
                 viewManager.CloseView(viewInfo);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}