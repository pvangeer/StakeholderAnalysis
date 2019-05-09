using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Gui.Annotations;

namespace StakeholderAnalysis.Gui
{
    public class ViewManager : INotifyPropertyChanged
    {
        public ViewManager()
        {
            Views = new ObservableCollection<ViewInfo>();
            ToolWindows = new ObservableCollection<ViewInfo>();
            ActiveDocument = null;
        }

        public ObservableCollection<ViewInfo> Views { get; }

        public ObservableCollection<ViewInfo> ToolWindows { get; }

        public ViewInfo ActiveDocument { get; set; }

        public void OpenView(ViewInfo viewInfo)
        {
            if (IsAvailableView(viewInfo))
            {
                BringToFront(viewInfo);
                return;
            }

            viewInfo.CloseViewCommand = new CloseViewCommand(this, viewInfo);
            Views.Add(viewInfo);
        }


        public void CloseView(ViewInfo viewInfo)
        {
            if (IsAvailableView(viewInfo))
            {
                Views.Remove(viewInfo);
                OnPropertyChanged(nameof(ActiveDocument));
            }
        }

        public void BringToFront(ViewInfo viewInfo)
        {
            if (!IsAvailableView(viewInfo))
            {
                OpenView(viewInfo);
            }

            ActiveDocument = viewInfo;
            OnPropertyChanged(nameof(ActiveDocument));
        }

        public void OpenToolWindow(ViewInfo viewInfo)
        {
            if (viewInfo.IsDocumentView)
            {
                throw new ArgumentException();
            }

            if (!ToolWindows.Contains(viewInfo))
            {
                viewInfo.CloseViewCommand = new CloseViewCommand(this, viewInfo);
                ToolWindows.Add(viewInfo);
            }
        }

        public void CloseToolWindow(ViewInfo viewInfo)
        {
            if (ToolWindows.Contains(viewInfo))
            {
                ToolWindows.Remove(viewInfo);
                OnPropertyChanged(nameof(ActiveDocument));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsAvailableView(ViewInfo viewInfo)
        {
            return Views.Any(v => v == viewInfo || v.ViewModel == viewInfo.ViewModel);
        }
    }
}
