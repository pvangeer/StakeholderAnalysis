using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class ShowProjectExplorerCommand : ICommand
    {
        private readonly ViewManager viewManager;
        private readonly Analysis analysis;

        public ShowProjectExplorerCommand(Analysis analysis, ViewManager viewManager)
        {
            this.analysis = analysis;
            this.viewManager = viewManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var projectExplorerInfo = viewManager.ToolWindows.FirstOrDefault(i => i.ViewModel is ProjectExplorerViewModel);
            if (projectExplorerInfo != null)
            {
                viewManager.CloseToolWindow(projectExplorerInfo);
            }
            viewManager.OpenToolWindow(new ViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis, viewManager), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/document.png", false));
        }

        public event EventHandler CanExecuteChanged;
    }
}
