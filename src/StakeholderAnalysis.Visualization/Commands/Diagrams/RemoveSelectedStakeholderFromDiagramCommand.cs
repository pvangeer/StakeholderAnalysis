using System;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.Commands.Diagrams
{
    public class RemoveSelectedStakeholderFromDiagramCommand : ICommand
    {
        private readonly IRemoveStakeholderViewModel diagramViewModel;

        public RemoveSelectedStakeholderFromDiagramCommand(IRemoveStakeholderViewModel diagramViewModel)
        {
            this.diagramViewModel = diagramViewModel;
            diagramViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return diagramViewModel.IsSelectedStakeholder;
        }

        public void Execute(object parameter)
        {
            diagramViewModel.RemoveFromDiagram();
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