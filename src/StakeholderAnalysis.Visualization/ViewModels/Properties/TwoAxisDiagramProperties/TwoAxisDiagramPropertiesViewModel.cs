using System.Collections.ObjectModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties
{
    public class TwoAxisDiagramPropertiesViewModel : PropertiesCollectionViewModelBase
    {
        private readonly ITwoAxisDiagram twoAxisDiagram;

        public TwoAxisDiagramPropertiesViewModel(ViewModelFactory factory, ViewManager viewManager, ITwoAxisDiagram twoAxisDiagram) :
            base(factory)
        {
            this.twoAxisDiagram = twoAxisDiagram;
            Items = GetItems();
            IsExpanded = true;
        }

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public override bool IsViewModelFor(object o)
        {
            return o as ITwoAxisDiagram == twoAxisDiagram;
        }

        private ObservableCollection<ITreeNodeViewModel> GetItems()
        {
            var backgroundItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextLeftBottom), "Linksonder"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextLeftTop), "Linksboven"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextRightBottom), "Rechtsonder"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundTextRightTop), "Rechtsboven"),
                new FontFamilyPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontFamily), "Lettertype"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontColor), "Tekstkleur"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontBold), "Vet"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontItalic), "Italic"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BackgroundFontSize), "Tekstgrootte", 1, 120, 1, "#"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BrushStartColor), "Startkleur"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.BrushEndColor), "Eindkleur")
            };
            var backgroundViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Achtergrond", backgroundItems,
                    CollectionType.PropertyValue);

            var axisLabelItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.XAxisMaxLabel), "Horizontaal +"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.XAxisMinLabel), "Horizontaal -"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.YAxisMaxLabel), "Vertikaal +"),
                new StringPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.YAxisMinLabel), "Vertikaal -"),
                new FontFamilyPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontFamily), "Lettertype"),
                new ColorPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontColor), "Tekstkleur"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontBold), "Vet"),
                new BooleanPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontItalic), "Italic"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<ITwoAxisDiagram>(twoAxisDiagram,
                    nameof(ITwoAxisDiagram.AxisFontSize), "Tekstgrootte", 1, 120, 1, "#")
            };
            var axisViewModel =
                ViewModelFactory.CreatePropertyCollectionViewModel("Aslabels", axisLabelItems,
                    CollectionType.PropertyValue);

            var generalItems = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<IStakeholderDiagram>(twoAxisDiagram, nameof(IStakeholderDiagram.Name),
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
    }
}