using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class StakeholderTypeIconPropertyValueTreeNodeViewModel : PropertyValueTreeNodeViewModelBase,
        IStakeholderTypeIconPropertyTreeNodeViewModel
    {
        private readonly StakeholderType content;

        public StakeholderTypeIconPropertyValueTreeNodeViewModel(StakeholderType content) : base("Beeldmerk")
        {
            this.content = content;
        }

        public StakeholderIconType IconTypeValue
        {
            get => content.IconType;
            set
            {
                content.IconType = value;
                content.OnPropertyChanged(nameof(StakeholderType.IconType));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, content);
        }
    }
}