using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;
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
                    OnPropertyChanged(nameof(NameViewModel));
                    break;
            }
        }

        public TwoAxisDiagramTextsViewModel TextsViewModel => ViewModelFactory.CreateTextsViewModel(ActiveTwoAxisDiagram);

        // TODO: Make IPropertyCollection...ViewModel and provide child viewmodels in a list. (change the content of the list based on selected diagram)
        public IStringPropertyTreeNodeViewModel NameViewModel => ActiveTwoAxisDiagram == null ? null : new StringPropertyTreeNodeViewModel(ActiveTwoAxisDiagram, nameof(ITwoAxisDiagram.Name), "Naam");

        private ITwoAxisDiagram ActiveTwoAxisDiagram => (viewManager?.ActiveDocument?.ViewModel as ITwoAxisDiagramViewModel)?.GetDiagram();

        public TwoAxisDiagramBushViewModel BackgroundBrushViewModel => ViewModelFactory.CreateTwoAxisDiagramBrushViewModel(ActiveTwoAxisDiagram);

    }
}
