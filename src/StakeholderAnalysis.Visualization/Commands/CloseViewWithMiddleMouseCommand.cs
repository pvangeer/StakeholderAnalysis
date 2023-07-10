using System;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class CloseViewWithMiddleMouseCommand : DependencyObject, ICommand
    {
        public bool CanExecute(object parameter)
        {
            return parameter is CloseViewWithMiddleMouseCommandParameters p
                   && p.ViewManager != null
                   && p.ClickedViewInfo != null;
        }

        public void Execute(object parameter)
        {
            if (parameter is CloseViewWithMiddleMouseCommandParameters parameters)
                parameters.ViewManager.GetViewManager().CloseView(parameters.ClickedViewInfo);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class CloseViewWithMiddleMouseCommandParameters
    {
        public ViewManagerViewModel ViewManager { get; set; }

        public ViewInfo ClickedViewInfo { get; set; }
    }
}