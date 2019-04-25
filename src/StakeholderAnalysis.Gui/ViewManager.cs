using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StakeholderAnalysis.Gui.Annotations;

namespace StakeholderAnalysis.Gui
{
    public class ViewManager : INotifyPropertyChanged
    {
        private ViewInfo currentViewInfo;

        public ViewManager()
        {
            Views = new ObservableCollection<ViewInfo>();
            Views.CollectionChanged += ViewsCollectionChanged;
            ToolWindows = new ObservableCollection<ViewInfo>();
            ActiveDocument = null;
        }

        private void ViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        public ObservableCollection<ViewInfo> Views { get; }

        public ViewInfo CurrentViewInfo
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

        public ViewInfo ActiveDocument { get; set; }

        public ObservableCollection<ViewInfo> ToolWindows { get; }

        public void OpenView(ViewInfo viewInfo)
        {
            if (!Views.Any(v => v == viewInfo || v.ViewModel == viewInfo.ViewModel))
            {
                Views.Add(viewInfo);
            }
        }

        public void CloseView(ViewInfo viewInfo)
        {
            if (Views.Contains(viewInfo))
            {
                Views.Remove(viewInfo);
            }
        }

        public void BringToFront(ViewInfo viewInfo)
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
