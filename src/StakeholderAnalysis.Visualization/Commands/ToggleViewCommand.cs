using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class ToggleViewCommand : ICommand
    {
        private readonly StakeholderViewType viewType;
        private readonly MainWindowViewModel mainWindowViewModel;

        public ToggleViewCommand(MainWindowViewModel mainWindowViewModel, StakeholderViewType viewType)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.viewType = viewType;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var openView = mainWindowViewModel.ViewsList.FirstOrDefault(vi => vi.Type == viewType);
            if (openView == null)
            {
                // TODO: Get correct name by viewtype
                mainWindowViewModel.ViewsList.Add(new StakeholderViewInfo("UI-diagram", viewType, mainWindowViewModel));
            }
            else
            {
                mainWindowViewModel.ViewsList.Remove(openView);
            }

            mainWindowViewModel.OnPropertyChanged(nameof(MainWindowViewModel.IsOnionViewOpened));
        }
    }
}
