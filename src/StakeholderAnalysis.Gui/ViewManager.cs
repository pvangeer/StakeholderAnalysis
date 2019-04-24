using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Gui.Annotations;

namespace StakeholderAnalysis.Gui
{
    public class ViewManager : IViewManager, INotifyPropertyChanged
    {
        private IViewInfo currentViewInfo;

        public ViewManager()
        {
            Views = new ObservableCollection<IViewInfo>();
            ToolWindows = new ObservableCollection<IViewInfo>();
            ActiveDocument = null;
        }

        public ObservableCollection<IViewInfo> Views { get; }

        public IViewInfo CurrentViewInfo
        {
            get => currentViewInfo;
            set
            {
                currentViewInfo = value;
                OnPropertyChanged(nameof(CurrentViewInfo));
                if (!Views.Any())
                {
                    if (ActiveDocument != null)
                    {
                        ActiveDocument = null;
                        OnPropertyChanged(nameof(ActiveDocument));
                    }
                }
                else if (Views.Contains(CurrentViewInfo))
                {
                    ActiveDocument = CurrentViewInfo;
                    OnPropertyChanged(nameof(ActiveDocument));
                }
            }
        }

        public IViewInfo ActiveDocument { get; set; }

        public ObservableCollection<IViewInfo> ToolWindows { get; }

        public void OpenView(IViewInfo viewInfo)
        {
            if (!Views.Contains(viewInfo))
            {
                Views.Add(viewInfo);
            }
        }

        public void CloseView(IViewInfo viewInfo)
        {
            if (Views.Contains(viewInfo))
            {
                Views.Remove(viewInfo);
            }
        }

        public void BringToFront(IViewInfo viewInfo)
        {
            if (!Views.Contains(viewInfo))
            {
                throw new ArgumentException();
            }

            CurrentViewInfo = viewInfo;
            OnPropertyChanged(nameof(CurrentViewInfo));
        }

        public void OpenToolWindow(ToolWindowViewInfo viewInfo)
        {
            if (!ToolWindows.Contains(viewInfo))
            {
                ToolWindows.Add(viewInfo);
            }
        }

        public void CloseToolWindow(ToolWindowViewInfo viewInfo)
        {
            if (ToolWindows.Contains(viewInfo))
            {
                ToolWindows.Remove(viewInfo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
