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
        private ViewInfo currentViewInfo;

        public ViewManager()
        {
            Views = new ObservableCollection<ViewInfo>();
            ToolWindows = new ObservableCollection<ViewInfo>();
            ActiveDocument = null;
        }

        public ObservableCollection<ViewInfo> Views { get; }

        public ObservableCollection<ViewInfo> ToolWindows { get; }

        public ViewInfo CurrentViewInfo
        {
            get => currentViewInfo;
            set
            {
                // TODO: This should be in a separate viewmodel since this logic is needed because it is being used as a binding property. In fact, this property shouldn't even be in the viewManager.
                currentViewInfo = value;
                OnPropertyChanged(nameof(CurrentViewInfo));
                if (currentViewInfo == null)
                {
                    if (ActiveDocument != null)
                    {
                        ActiveDocument = null;
                        OnPropertyChanged(nameof(ActiveDocument));
                    }
                }
                else if (currentViewInfo.IsDocumentView)
                {
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
        }

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

            CurrentViewInfo = viewInfo;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsAvailableView(ViewInfo viewInfo)
        {
            return Views.Any(v => v == viewInfo || v.ViewModel == viewInfo.ViewModel);
        }
    }
}
