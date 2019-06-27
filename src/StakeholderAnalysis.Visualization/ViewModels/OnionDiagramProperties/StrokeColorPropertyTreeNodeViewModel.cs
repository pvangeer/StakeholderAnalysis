using System.Windows.Media;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class StrokeColorPropertyTreeNodeViewModel : ColorPropertyTreeNodeViewModelBase<OnionRing>
    {
        public StrokeColorPropertyTreeNodeViewModel(OnionRing ring) : base(ring, "Lijnkleur") { }

        public override Color ColorValue
        {
            get => Content.StrokeColor;
            set
            {
                Content.StrokeColor = value;
                Content.OnPropertyChanged(nameof(OnionRing.StrokeColor));
            }
        }
    }
}