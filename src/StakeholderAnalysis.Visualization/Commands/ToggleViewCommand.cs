using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class ToggleViewCommand : ICommand
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public ToggleViewCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (!(parameter is StakeholderViewType))
            {
                return;
            }

            var viewType = (StakeholderViewType)parameter;
            var openView = mainWindowViewModel.ViewList.FirstOrDefault(vi => vi.Type == viewType);
            if (openView == null)
            {
                // TODO: Get correct name by viewtype
                openView = new StakeholderViewInfo("UI-diagram", viewType, mainWindowViewModel);
                mainWindowViewModel.ViewList.Add(openView);
            }

            mainWindowViewModel.SelectedViewInfo = openView;
            mainWindowViewModel.OnPropertyChanged(nameof(MainWindowViewModel.IsOnionViewOpened));
            mainWindowViewModel.OnPropertyChanged(nameof(MainWindowViewModel.IsCommunicationStrategyViewOpened));
            mainWindowViewModel.OnPropertyChanged(nameof(MainWindowViewModel.IsForcesViewOpened));
            mainWindowViewModel.OnPropertyChanged(nameof(MainWindowViewModel.IsTableViewOpened));
        }
    }
}
