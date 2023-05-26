using System.ComponentModel;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class SelectionManagerViewModel : ViewModelBase
    {
        private readonly SelectionManager selectionManager;

        public SelectionManagerViewModel(ViewModelFactory viewModelFactory, SelectionManager guiSelectionManager) : base(viewModelFactory)
        {
            selectionManager = guiSelectionManager;
            selectionManager.PropertyChanged += SelectionManagerOnPropertyChanged;
        }

        public object Selection => selectionManager.Selection;

        private void SelectionManagerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectionManager.Selection):
                    OnPropertyChanged(nameof(Selection));
                    break;
            }
        }
    }
}