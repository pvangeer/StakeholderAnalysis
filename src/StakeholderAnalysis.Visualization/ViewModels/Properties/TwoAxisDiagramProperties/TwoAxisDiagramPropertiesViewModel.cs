using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties
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
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextLeftBottom), "Linksonder"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextLeftTop), "Linksboven"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextRightBottom), "Rechtsonder"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextRightTop), "Rechtsboven"),
                new FontFamilyPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontFamily), "Lettertype"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontColor), "Tekstkleur"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontBold), "Vet"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontItalic), "Italic"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontSize), "Tekstgrootte", 1, 120, 1, "#"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BrushStartColor), "Startkleur"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.BrushEndColor), "Eindkleur")
            };
            var backgroundViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Achtergrond", backgroundItems,
                    CollectionType.PropertyValue);

            axisLabelItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.XAxisMaxLabel), "Horizontaal +"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.XAxisMinLabel), "Horizontaal -"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.YAxisMaxLabel), "Vertikaal +"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.YAxisMinLabel), "Vertikaal -"),
                new FontFamilyPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontFamily), "Lettertype"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontColor), "Tekstkleur"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontBold), "Vet"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontItalic), "Italic"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontSize), "Tekstgrootte", 1, 120, 1, "#")
            };
            var axisViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Aslabels", axisLabelItems,
                    CollectionType.PropertyValue);

            generalItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(activeTwoAxisDiagram, nameof(ITwoAxisDiagram.Name),
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
            foreach (var textItem in list.OfType<StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<FontFamilyPropertyValueTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;

            foreach (var textItem in list.OfType<DoubleUpDownPropertyValueTreeNodeViewModel<ITwoAxisDiagram>>())
                textItem.Content = newContent;
        }

        private void ChangeActiveDiagram()
        {
            if (activeTwoAxisDiagram != null)
                activeTwoAxisDiagram.PropertyChanged -= ActiveDiagramPropertyChanged;

            activeTwoAxisDiagram = (viewManager?.ActiveDocument?.ViewModel as ITwoAxisDiagramViewModel)?.GetDiagram();

            if (activeTwoAxisDiagram != null)
                activeTwoAxisDiagram.PropertyChanged += ActiveDiagramPropertyChanged;
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