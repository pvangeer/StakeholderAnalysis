using System;
using System.Windows.Input;

namespace StakeholderAnalysis.Gui
{
    public class ViewInfo
    {
        public ViewInfo(string title, object viewModel, string iconReference)
        {
            IconReference = iconReference;
            Title = title;
            ViewModel = viewModel;
        }

        public string Title { get; }

        public string IconReference { get; }

        public object ViewModel { get; }

        public ICommand CloseViewCommand => new CloseViewCommand(this);

        public event EventHandler CloseView;

        public void RaiseCloseViewEvent()
        {
            CloseView?.Invoke(this, null);
        }
    }

    public class CloseViewCommand : ICommand
    {
        private readonly ViewInfo viewInfo;

        public CloseViewCommand(ViewInfo viewInfo)
        {
            this.viewInfo = viewInfo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewInfo.RaiseCloseViewEvent();
        }

        public event EventHandler CanExecuteChanged;
    }
}