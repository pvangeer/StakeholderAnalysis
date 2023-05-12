using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagramProperties
{
    public class TwoAxisDiagramPropertiesViewModel : ViewModelBase
    {
        private readonly ViewManager viewManager;

        private ITwoAxisDiagram activeTwoAxisDiagram;
        private ObservableCollection<ITreeNodeViewModel> axisLabelItems;
        private ObservableCollection<ITreeNodeViewModel> backgroundItems;
        private ObservableCollection<ITreeNodeViewModel> generalItems;

        public TwoAxisDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            ChangeActiveDiagram();
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
            TreeViewModel = ViewModelFactory.CreatePropertyCollectionViewModel(GetDisplayName(), GetItems(),
                CollectionType.PropertyItemsCollection);
        }

        public PropertyCollectionTreeNodeViewModel TreeViewModel { get; }

        private ObservableCollection<ITreeNodeViewModel> GetItems()
        {
            backgroundItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextLeftBottom), "Linksonder"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextLeftTop), "Linksboven"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextRightBottom), "Rechtsonder"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextRightTop), "Rechtsboven"),
                new FontFamilyPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontFamily), "Lettertype"),
                new ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontColor), "Tekstkleur"),
                new BooleanPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontBold), "Vet"),
                new BooleanPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontItalic), "Italic"),
                new DoubleUpDownPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontSize), "Tekstgrootte", 1, 120, 1, "#"),
                new ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BrushStartColor), "Startkleur"),
                new ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BrushEndColor), "Eindkleur")
            };
            var backgroundViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Achtergrond", backgroundItems,
                    CollectionType.PropertyValue);

            axisLabelItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.XAxisMaxLabel), "Horizontaal +"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.XAxisMinLabel), "Horizontaal -"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.YAxisMaxLabel), "Vertikaal +"),
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.YAxisMinLabel), "Vertikaal -"),
                new FontFamilyPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontFamily), "Lettertype"),
                new ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontColor), "Tekstkleur"),
                new BooleanPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontBold), "Vet"),
                new BooleanPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontItalic), "Italic"),
                new DoubleUpDownPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontSize), "Tekstgrootte", 1, 120, 1, "#")
            };
            var axisViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Aslabels", axisLabelItems,
                    CollectionType.PropertyValue);

            generalItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.Name),
                    "Naam")
            };
            var generalViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Algemeen", generalItems,
                    CollectionType.PropertyValue);

            return new ObservableCollection<ITreeNodeViewModel>
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

                    RefreshViewModelContent(backgroundItems, activeTwoAxisDiagram);
                    RefreshViewModelContent(axisLabelItems, activeTwoAxisDiagram);
                    RefreshViewModelContent(generalItems, activeTwoAxisDiagram);

                    break;
            }
        }

        private void RefreshViewModelContent(ObservableCollection<ITreeNodeViewModel> list, ITwoAxisDiagram newContent)
        {
            foreach (var textItem in list.OfType<StringPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<FontFamilyPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<ColorPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<BooleanPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<DoubleUpDownPropertyTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;
        }

        private void ChangeActiveDiagram()
        {
            if (activeTwoAxisDiagram != null) activeTwoAxisDiagram.PropertyChanged -= ActiveDiagramPropertyChanged;
            activeTwoAxisDiagram = (viewManager?.ActiveDocument?.ViewModel as ITwoAxisDiagramViewModel)?.GetDiagram();
            if (activeTwoAxisDiagram != null) activeTwoAxisDiagram.PropertyChanged += ActiveDiagramPropertyChanged;
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

        public string GetIconSourceString()
        {
            return activeTwoAxisDiagram == null
                ? ""
                : activeTwoAxisDiagram is ForceFieldDiagram
                    ? "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/forces.png"
                    : "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/involvement.png";
        }

        private string GetDisplayName()
        {
            return activeTwoAxisDiagram == null ? "" : activeTwoAxisDiagram.Name;
        }
    }
}