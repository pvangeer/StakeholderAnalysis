using System.ComponentModel;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagramProperties
{
    public class TwoAxisDiagramPropertiesViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;

        public TwoAxisDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
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

        public TwoAxisDiagramTextsViewModel TextsViewModel =>
            ViewModelFactory.CreateTextsViewModel((viewManager?.ActiveDocument?.ViewModel as ITwoAxisDiagramViewModel)
                ?.GetDiagram());
    }
}
