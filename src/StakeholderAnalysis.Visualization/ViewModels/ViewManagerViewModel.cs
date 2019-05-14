using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewManagerViewModel : NotifyPropertyChangedObservable
    {
        private readonly ViewManager viewManager;
        private ViewInfo currentViewInfo;

        public ViewManagerViewModel(ViewManager viewManager)
        {
            this.viewManager = viewManager;

            viewManager.PropertyChanged += ViewManagerPropertyChanged;
        }

        public ViewInfo CurrentViewInfo
        {
            get => currentViewInfo;
            set
            {
                currentViewInfo = value;
                OnPropertyChanged(nameof(CurrentViewInfo));

                if (currentViewInfo == null)
                {
                    if (viewManager.ActiveDocument != null)
                    {
                        viewManager.ActiveDocument = null;
                        viewManager.OnPropertyChanged(nameof(ActiveDocument));
                    }
                }
                else if (currentViewInfo.IsDocumentView)
                {
                    if (!viewManager.Views.Any())
                    {
                        if (ActiveDocument != null)
                        {
                            viewManager.ActiveDocument = null;
                            viewManager.OnPropertyChanged(nameof(ActiveDocument));
                        }
                    }
                    else if (viewManager.Views.Contains(CurrentViewInfo))
                    {
                        viewManager.ActiveDocument = CurrentViewInfo;
                        viewManager.OnPropertyChanged(nameof(ActiveDocument));
                    }
                }
            }
        }

        public ViewInfo ActiveDocument
        {
            get => viewManager?.ActiveDocument;
            set
            {
                viewManager.ActiveDocument = value;
                viewManager.OnPropertyChanged(nameof(ViewManager.ActiveDocument));
            }
        }

        public ObservableCollection<ViewInfo> Views => viewManager.Views;

        public ObservableCollection<ViewInfo> ToolWindows => viewManager.ToolWindows;

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    currentViewInfo = viewManager.ActiveDocument;
                    OnPropertyChanged(nameof(ActiveDocument));
                    OnPropertyChanged(nameof(CurrentViewInfo));
                    break;
            }
        }
    }
}
