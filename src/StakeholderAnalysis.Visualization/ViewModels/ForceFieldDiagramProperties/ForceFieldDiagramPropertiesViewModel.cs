using System.ComponentModel;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ForceFieldDiagramProperties
{
    public class ForceFieldDiagramPropertiesViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;

        public ForceFieldDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    OnPropertyChanged(nameof(TextsViewModel));
                    break;
            }
        }

        private ForceFieldDiagram SelectedForceFieldDiagram => (viewManager?.ActiveDocument?.ViewModel as ForceFieldDiagramViewModel)?.GetDiagram();

        public TwoAxisDiagramTextsViewModel TextsViewModel => ViewModelFactory.CreateTextsViewModel(SelectedForceFieldDiagram);
    }
}
