using System;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class RemoveSelectedStakeholderFromDiagramCommand : ICommand
    {
        private readonly IRemoveStakeholderViewModel onionDiagramViewModel;

        public RemoveSelectedStakeholderFromDiagramCommand(IRemoveStakeholderViewModel onionDiagramViewModel)
        {
            this.onionDiagramViewModel = onionDiagramViewModel;
            onionDiagramViewModel.PropertyChanged += ViewModelPropertyChanged;
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

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder):
                    CanExecuteChanged?.Invoke(this, null);
                    break;
            }
        }
    }
}