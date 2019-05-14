using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties
{
    public class RemoveConnectionGroupCommand : ICommand
    {
        private ConnectionGroupPropertiesViewModel connectionGroupPropertiesViewModel;

        public RemoveConnectionGroupCommand(ConnectionGroupPropertiesViewModel connectionGroupPropertiesViewModel)
        {
            this.connectionGroupPropertiesViewModel = connectionGroupPropertiesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return connectionGroupPropertiesViewModel != null;
        }

        public void Execute(object parameter)
        {
            connectionGroupPropertiesViewModel.RemoveConnectionGroup();
        }

        public event EventHandler CanExecuteChanged;
    }
}
