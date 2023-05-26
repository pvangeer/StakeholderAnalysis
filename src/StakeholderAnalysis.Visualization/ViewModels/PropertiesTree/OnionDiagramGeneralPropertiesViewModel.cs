using System.Collections.ObjectModel;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.Properties;
using StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class OnionDiagramGeneralPropertiesViewModel : PropertiesCollectionViewModelBase
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramGeneralPropertiesViewModel(ViewModelFactory viewModelFactory, OnionDiagram diagram) : base(viewModelFactory)
        {
            this.diagram = diagram;

            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<OnionDiagram>(diagram, nameof(OnionDiagram.Name), "Naam"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<OnionDiagram>(diagram, nameof(OnionDiagram.Asymmetry),
                    "Asymmetrie", 0, 1, 0.1, "0.#####"),
                new SliderPropertyValueTreeNodeViewModel<OnionDiagram>(diagram, nameof(OnionDiagram.Orientation), "Orientatie", 0, 360)
            };
        }

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override string DisplayName => "Algemeen";

        public override bool IsExpandable => true;

        public override CollectionType CollectionType { get; }

        public override bool IsViewModelFor(object o)
        {
            return o as OnionDiagram == diagram;
        }
    }
}