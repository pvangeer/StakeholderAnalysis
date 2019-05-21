using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class RemoveOnionRingCommand : ICommand
    {
        private readonly OnionRingPropertiesViewModel onionRingPropertiesViewModel;

        public RemoveOnionRingCommand(OnionRingPropertiesViewModel onionRingPropertiesViewModel)
        {
            this.onionRingPropertiesViewModel = onionRingPropertiesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return onionRingPropertiesViewModel != null;
        }

        public void Execute(object parameter)
        {
            onionRingPropertiesViewModel.RemoveOnionRing();
        }

        public event EventHandler CanExecuteChanged;
    }
}
