using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class AddConnectionGroupCommand : ICommand
    {
        private readonly OnionDiagram selectedOnionDiagram;
        private readonly StakeholderAnalysisGui gui;

        public AddConnectionGroupCommand(OnionDiagram selectedOnionDiagram, StakeholderAnalysisGui gui)
        {
            this.gui = gui;
            this.selectedOnionDiagram = selectedOnionDiagram;
        }

        public bool CanExecute(object parameter)
        {
            return selectedOnionDiagram != null;
        }

        public void Execute(object parameter)
        {
            var newStakeholderConnectionGroup = new StakeholderConnectionGroup("Nieuwe groep", Colors.Black);
            selectedOnionDiagram.ConnectionGroups.Add(newStakeholderConnectionGroup);
            var stakeholderConnectionGroupSelection = gui.SelectedStakeholderGroupRegister.SelectedStakeholderConnectionGroups.FirstOrDefault(g => g.OnionDiagram == selectedOnionDiagram);
            if (stakeholderConnectionGroupSelection != null &&
                stakeholderConnectionGroupSelection.StakeholderConnectionGroup == null)
            {
                stakeholderConnectionGroupSelection.StakeholderConnectionGroup = newStakeholderConnectionGroup;
                stakeholderConnectionGroupSelection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection.StakeholderConnectionGroup));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}