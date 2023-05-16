using System;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class CloseApplicationCommand : ICommand
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly GuiProjectServices projectServices;

        public CloseApplicationCommand(StakeholderAnalysisGui gui, GuiProjectServices guiProjectServices)
        {
            this.gui = gui;
            projectServices = guiProjectServices;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (projectServices != null && gui != null)
                projectServices.HandleUnsavedChanges(() => Application.Current.Shutdown());
            else
                Application.Current.Shutdown();
        }

        public event EventHandler CanExecuteChanged;
    }
}