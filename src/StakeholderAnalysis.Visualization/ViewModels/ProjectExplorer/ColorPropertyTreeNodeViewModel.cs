using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ColorPropertyTreeNodeViewModel : ColorPropertyTreeNodeViewModelBase<StakeholderType>
    {
        public ColorPropertyTreeNodeViewModel(StakeholderType content) : base(content, "Kleur")
        {
        }

        public override Color ColorValue
        {
            get => Content.Color;
            set
            {
                Content.Color = value;
                Content.OnPropertyChanged(nameof(StakeholderType.Color));
            }
        }
    }
}
