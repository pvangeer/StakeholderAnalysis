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

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is StakeholderViewType)) return;

            var viewType = (StakeholderViewType) parameter;
            var openView = mainWindowViewModel.ViewList.FirstOrDefault(vi => vi.Type == viewType);
            if (openView == null)
            {
                openView = new StakeholderViewInfo(viewType, mainWindowViewModel);
                mainWindowViewModel.ViewList.Add(openView);
            }

            mainWindowViewModel.SelectedViewInfo = openView;
        }
    }
}