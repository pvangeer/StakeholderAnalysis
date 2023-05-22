using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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