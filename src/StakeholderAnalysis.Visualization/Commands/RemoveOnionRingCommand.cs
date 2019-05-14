using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.Commands
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
            onionRingPropertiesViewModel.RemoveRing();
        }

        public event EventHandler CanExecuteChanged;
    }
}
