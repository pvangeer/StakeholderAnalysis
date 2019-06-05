using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewManagerViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;

        public ViewManagerViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;

            viewManager.PropertyChanged += ViewManagerPropertyChanged;
            viewManager.ToolWindows.CollectionChanged += ToolWindowsCollectionChanged;
            SelectedToolWindowIndex = 0;
            OnPropertyChanged(nameof(SelectedToolWindowIndex));
        }

        private void ToolWindowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (SelectedToolWindowIndex >= viewManager.ToolWindows.Count)
                {
                    SelectedToolWindowIndex = viewManager.ToolWindows.Count - 1;
                    OnPropertyChanged(nameof(SelectedToolWindowIndex));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                SelectedToolWindowIndex = viewManager.ToolWindows.Count - 1;
                OnPropertyChanged(nameof(SelectedToolWindowIndex));
            }
        }

        public ViewInfo ActiveDocument => viewManager?.ActiveDocument;

        public int ActiveDocumentIndex
        {
            get => viewManager?.Views.IndexOf(viewManager.ActiveDocument) ?? -1;
            set
            {
                var viewInfo = viewManager.Views.ElementAtOrDefault(value);
                if (viewInfo != null)
                {
                    viewManager?.BringToFront(viewInfo);
                }
                else
                {
                    viewManager.ActiveDocument = null;
                    viewManager.OnPropertyChanged(nameof(ViewManager.ActiveDocument));
                }
            }
        }

        public ObservableCollection<ViewInfo> Views => viewManager.Views;

        public ObservableCollection<ViewInfo> ToolWindows => viewManager.ToolWindows;

        public int SelectedToolWindowIndex { get; set; }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    OnPropertyChanged(nameof(ActiveDocumentIndex));
                    OnPropertyChanged(nameof(ActiveDocument));
                    break;
            }
        }
    }
}
