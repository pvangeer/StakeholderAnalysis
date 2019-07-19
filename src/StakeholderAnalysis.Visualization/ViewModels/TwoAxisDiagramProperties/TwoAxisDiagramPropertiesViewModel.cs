using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagramProperties
{
    public class TwoAxisDiagramPropertiesViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;
        private ObservableCollection<ITreeNodeViewModel> backgroundItems;
        private ObservableCollection<ITreeNodeViewModel> axisLabelItems;
        private ObservableCollection<ITreeNodeViewModel> generalItems;

        public TwoAxisDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
            TreeViewModel = ViewModelFactory.CreatePropertyCollectionViewModel(GetDisplayName(), GetItems(), CollectionType.PropertyItemsCollection);
        }

        private ObservableCollection<ITreeNodeViewModel> GetItems()
        {
            backgroundItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextLeftBottom), "Linksonder"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextLeftTop), "Linksboven"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextRightBottom), "Rechtsonder"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextRightTop), "Rechtsboven"),
                new ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.BrushStartColor), "Startkleur"),
                new ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.BrushEndColor), "Eindkleur"),
                // TODO: Add font characteristics
            };
            var backgroundViewModel = ViewModelFactory.CreatePropertyCollectionViewModel("Achtergrond", backgroundItems,CollectionType.PropertyValue);

            axisLabelItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.XAxisMaxLabel), "Horizontaal +"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.XAxisMinLabel), "Horizontaal -"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.YAxisMaxLabel), "Vertikaal +"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.YAxisMinLabel), "Vertikaal -"),
                // TODO: Add font characteristics
            };
            var axisViewModel = ViewModelFactory.CreatePropertyCollectionViewModel("Aslabels", axisLabelItems, CollectionType.PropertyValue);

            generalItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.Name), "Naam"),
            };
            var generalViewModel = ViewModelFactory.CreatePropertyCollectionViewModel("Algemeen", generalItems, CollectionType.PropertyValue);

            return new ObservableCollection<ITreeNodeViewModel>()
            {
                generalViewModel,
                axisViewModel,
                backgroundViewModel
            };
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    ChangeActiveDiagram();
                    TreeViewModel.DisplayName = GetDisplayName();
                    TreeViewModel.OnPropertyChanged(nameof(PropertyCollectionTreeNodeViewModel.DisplayName));
                    TreeViewModel.IconSourceString = GetIconSourceString();
                    TreeViewModel.OnPropertyChanged(nameof(PropertyCollectionTreeNodeViewModel.IconSourceString));
                    foreach (var textItem in backgroundItems.OfType<StringPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                    {
                        textItem.Content = activeTwoAxisDiagram;
                    }
                    foreach (var textItem in backgroundItems.OfType<ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                    {
                        textItem.Content = activeTwoAxisDiagram;
                    }
                    foreach (var textItem in axisLabelItems.OfType<StringPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                    {
                        textItem.Content = activeTwoAxisDiagram;
                    }
                    foreach (var textItem in generalItems.OfType<StringPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                    {
                        textItem.Content = activeTwoAxisDiagram;
                    }
                    break;
            }
        }

        private void ChangeActiveDiagram()
        {
            if (activeTwoAxisDiagram != null)
            {
                activeTwoAxisDiagram.PropertyChanged -= ActiveDiagramPropertyChanged;
            }
            activeTwoAxisDiagram = (viewManager?.ActiveDocument?.ViewModel as ITwoAxisDiagramViewModel)?.GetDiagram();
            if (activeTwoAxisDiagram != null)
            {
                activeTwoAxisDiagram.PropertyChanged += ActiveDiagramPropertyChanged;
            }
        }

        private void ActiveDiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ITwoAxisDiagram.Name):
                    TreeViewModel.DisplayName = GetDisplayName();
                    TreeViewModel.OnPropertyChanged(nameof(PropertyCollectionTreeNodeViewModel.DisplayName));
                    break;
            }
        }

        public PropertyCollectionTreeNodeViewModel TreeViewModel { get; }

        public string GetIconSourceString()
        {
            return activeTwoAxisDiagram == null
                ? ""
                :
                activeTwoAxisDiagram is ForceFieldDiagram
                    ?
                    "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/forces.png"
                    : "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/involvement.png";
        }

        private string GetDisplayName()
        {
            return activeTwoAxisDiagram == null ? "" : activeTwoAxisDiagram.Name;
        }

        private ITwoAxisDiagram activeTwoAxisDiagram;
    }
}
