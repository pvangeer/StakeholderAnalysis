using System.ComponentModel;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class OnionDiagramPropertiesViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;

        public OnionDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
        }

        public string Name
        {
            get => SelectedOnionDiagram?.Name ?? "";
            set
            {
                if (SelectedOnionDiagram != null) SelectedOnionDiagram.Name = value;
            }
        }

        public OnionRingsPropertiesViewModel OnionRingsViewModel =>
            ViewModelFactory.CreateOnionRingsPropertiesViewModel();

        private OnionDiagram SelectedOnionDiagram =>
            (viewManager?.ActiveDocument?.ViewModel as OnionDiagramViewModel)?.GetDiagram();

        public ConnectionGroupsPropertiesViewModel ConnectionGroupsViewModel =>
            ViewModelFactory.CreateConnectionGroupsPropertiesViewModel();

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    OnPropertyChanged(Name);
                    break;
            }
        }
    }
}