using System.Windows.Media;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class BackgroundColorPropertyTreeNodeViewModel : ColorPropertyTreeNodeViewModelBase<OnionRing>
    {
        public BackgroundColorPropertyTreeNodeViewModel(OnionRing ring) : base(ring, "Achtergrondkleur") { }

        public override Color ColorValue
        {
            get => Content.BackgroundColor;
            set
            {
                Content.BackgroundColor = value;
                Content.OnPropertyChanged(nameof(OnionRing.BackgroundColor));
            }
        }
    }
}