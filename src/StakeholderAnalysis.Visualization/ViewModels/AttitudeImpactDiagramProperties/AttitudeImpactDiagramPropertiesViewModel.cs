using System.ComponentModel;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramProperties
{
    public class AttitudeImpactDiagramPropertiesViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;

        public AttitudeImpactDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
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

        private AttitudeImpactDiagram SelectedAttitudeImpactDiagram => (viewManager?.ActiveDocument?.ViewModel as AttitudeImpactDiagramViewModel)?.GetDiagram();

        public TwoAxisDiagramTextsViewModel TextsViewModel => ViewModelFactory.CreateTextsViewModel(SelectedAttitudeImpactDiagram);
    }
}
