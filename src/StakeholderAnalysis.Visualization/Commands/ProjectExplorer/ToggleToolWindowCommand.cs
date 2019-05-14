using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class ToggleToolWindowCommand : ICommand
    {
        private readonly ViewManager viewManager;
        private readonly Analysis analysis;

        public ToggleToolWindowCommand(Analysis analysis, ViewManager viewManager)
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
            var type = parameter as Type;
            var viewInfo = viewManager.ToolWindows.FirstOrDefault(i => i.ViewModel.GetType() == type);
            if (viewInfo != null)
            {
                viewManager.CloseToolWindow(viewInfo);
            }
            else
            {
                var viewInfoByType = GetViewInfoByType(type);
                if (viewInfoByType != null)
                {
                    viewManager.OpenToolWindow(viewInfoByType);
                }
            }
        }

        private ViewInfo GetViewInfoByType(Type type)
        {
            if (type == typeof(ProjectExplorerViewModel))
            {
                return new ViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis, viewManager), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/document.png", false);
            }

            return null;
        }

        public event EventHandler CanExecuteChanged;
    }
}
