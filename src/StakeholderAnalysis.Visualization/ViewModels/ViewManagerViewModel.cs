using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewManagerViewModel : ViewModelBase
    {
        private readonly SelectionManager selectionManager;
        private readonly ViewManager viewManager;

        public ViewManagerViewModel(ViewModelFactory factory, ViewManager viewManager, SelectionManager selectionManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.selectionManager = selectionManager;
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
                    if (viewInfo.ViewModel is ISelectable iSelectable)
                        selectionManager.Select(iSelectable);
                }
                else
                {
                    viewManager.ActiveDocument = null;
                    viewManager.OnPropertyChanged(nameof(ViewManager.ActiveDocument));
                }
            }
        }

        public ObservableCollection<ViewInfo> Views => viewManager.Views;

        public ViewManager GetViewManager()
        {
            return viewManager;
        }

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

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}