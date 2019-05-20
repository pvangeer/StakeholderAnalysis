using System;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class ClearSelectionCommand : ICommand
    {
        private ISelectionRegister selectionRegister;

        public ClearSelectionCommand(ISelectionRegister selectionRegister)
        {
            this.selectionRegister = selectionRegister;
        }

        public bool CanExecute(object parameter)
        {
            return selectionRegister != null;
        }

        public void Execute(object parameter)
        {
            selectionRegister.Select(null);
        }

        public event EventHandler CanExecuteChanged;
    }
}
