using System;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.Commands.Diagrams
{
    public class RemoveSelectedStakeholderFromDiagramCommand : ICommand
    {
        private readonly IRemoveStakeholderViewModel stakeholderViewModel;

        public RemoveSelectedStakeholderFromDiagramCommand(IRemoveStakeholderViewModel stakeholderViewModel)
        {
            this.stakeholderViewModel = stakeholderViewModel;
            stakeholderViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return stakeholderViewModel.IsSelectedStakeholder;
        }

        public void Execute(object parameter)
        {
            stakeholderViewModel.RemoveFromDiagram();
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