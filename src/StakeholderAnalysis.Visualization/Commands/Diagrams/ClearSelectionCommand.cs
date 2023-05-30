using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.Commands.Diagrams
{
    public class ClearSelectionCommand : ICommand
    {
        private readonly ISelectionRegister selectionRegister;

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
            selectionRegister.SelectObject(null);
        }

        public event EventHandler CanExecuteChanged;
    }
}