using System;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class CanAlwaysExecuteActionCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public Action<object> ExecuteAction;
    }
}