using System;
using System.Windows.Input;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class SelectItemCommand : ICommand
    {
        private readonly ISelectable selectable;
        private readonly SelectionManager selectionManager;

        public SelectItemCommand(SelectionManager selectionManager, ISelectable selectable)
        {
            this.selectionManager = selectionManager;
            this.selectable = selectable;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object o)
        {
            selectionManager.Select(selectable);
        }

        public event EventHandler CanExecuteChanged;
    }
}