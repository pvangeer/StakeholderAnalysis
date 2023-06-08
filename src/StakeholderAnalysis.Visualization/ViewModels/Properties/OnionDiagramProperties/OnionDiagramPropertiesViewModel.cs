using System.Collections.ObjectModel;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class OnionDiagramPropertiesViewModel : PropertiesCollectionViewModelBase
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramPropertiesViewModel(ViewModelFactory factory, OnionDiagram diagram) : base(factory)
        {
            this.diagram = diagram;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                ViewModelFactory.CreateOnionDiagramGeneralPropertiesViewModel(diagram),
                ViewModelFactory.CreateOnionRingsPropertiesViewModel(diagram),
                ViewModelFactory.CreateConnectionGroupsPropertiesViewModel(diagram)
            };
        }

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;

        public override bool IsViewModelFor(object otherObject)
        {
            return otherObject as OnionDiagram == diagram;
        }
    }
}