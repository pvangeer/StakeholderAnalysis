using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class RemoveSelectedStakeholderFromDiagramCommand : ICommand
    {
        private IRemoveStakeholderViewModel onionDiagramViewModel;

        public RemoveSelectedStakeholderFromDiagramCommand(IRemoveStakeholderViewModel onionDiagramViewModel)
        {
            this.onionDiagramViewModel = onionDiagramViewModel;
            onionDiagramViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder):
                    CanExecuteChanged?.Invoke(this,null);
                    break;
            }
        }

        public bool CanExecute(object parameter)
        {
            return onionDiagramViewModel.IsSelectedStakeholder;
        }

        public void Execute(object parameter)
        {
            onionDiagramViewModel.RemoveFromDiagram();
        }

        public event EventHandler CanExecuteChanged;
    }
}
