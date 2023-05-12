using System;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class AddConnectionGroupCommand : ICommand
    {
        private readonly OnionDiagram selectedOnionDiagram;

        public AddConnectionGroupCommand(OnionDiagram selectedOnionDiagram)
        {
            this.selectedOnionDiagram = selectedOnionDiagram;
        }

        public bool CanExecute(object parameter)
        {
            return selectedOnionDiagram != null;
        }

        public void Execute(object parameter)
        {
            selectedOnionDiagram.ConnectionGroups.Add(new StakeholderConnectionGroup("Nieuwe groep", Colors.Black));
        }

        public event EventHandler CanExecuteChanged;
    }
}