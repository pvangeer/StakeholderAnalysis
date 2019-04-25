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
            if (IsAvailableView(viewInfo))
            {
                Views.Remove(viewInfo);
            }
            Views.Add(viewInfo);
        }

        private bool IsAvailableView(ViewInfo viewInfo)
        {
            return Views.Any(v => v == viewInfo || v.ViewModel == viewInfo.ViewModel);
        }

        public void CloseView(ViewInfo viewInfo)
        {
            if (IsAvailableView(viewInfo))
            {
                Views.Remove(viewInfo);
            }
        }

        public void BringToFront(ViewInfo viewInfo)
        {
            if (!IsAvailableView(viewInfo))
            {
                throw new ArgumentException();
            }

            // TODO: Hack since closing a document or anchorable will actually only hide the view and not give us any event to work with
            CloseView(viewInfo);
            OpenView(viewInfo);

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
