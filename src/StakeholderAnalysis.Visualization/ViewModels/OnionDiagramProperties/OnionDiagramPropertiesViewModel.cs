using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class OnionDiagramPropertiesViewModel : NotifyPropertyChangedObservable
    {
        private readonly ViewManager viewManager;

        public OnionDiagramPropertiesViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
        }

        public string Name
        {
            get => SelectedOnionDiagram?.Name ?? "";
            set
            {
                if (SelectedOnionDiagram != null)
                {
                    SelectedOnionDiagram.Name = value;
                }
            }
        }

        public OnionDiagram SelectedOnionDiagram => (viewManager?.ActiveDocument?.ViewModel as OnionDiagramViewModel)?.GetDiagram();

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    OnPropertyChanged(nameof(Name));
                    break;
            }
        }
    }
}
