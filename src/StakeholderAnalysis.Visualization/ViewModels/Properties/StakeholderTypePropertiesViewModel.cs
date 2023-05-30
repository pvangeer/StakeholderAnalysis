using System.Collections.ObjectModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties
{
    public class StakeholderTypePropertiesViewModel : PropertiesCollectionViewModelBase
    {
        private readonly StakeholderType stakeholderType;

        public StakeholderTypePropertiesViewModel(ViewModelFactory factory, StakeholderType stakeholderType) : base(factory)
        {
            this.stakeholderType = stakeholderType;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<StakeholderType>(stakeholderType, nameof(StakeholderType.Name),
                    "Naam"),
                new ColorPropertyValueTreeNodeViewModel<StakeholderType>(stakeholderType, nameof(StakeholderType.Color),
                    "Kleur"),
                new StakeholderTypeIconPropertyValueTreeNodeViewModel(stakeholderType)
            };
        }

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        public override CollectionType CollectionType => CollectionType.PropertyValue;

        public override bool IsViewModelFor(object otherObject)
        {
            return otherObject as StakeholderType == stakeholderType;
        }
    }
}