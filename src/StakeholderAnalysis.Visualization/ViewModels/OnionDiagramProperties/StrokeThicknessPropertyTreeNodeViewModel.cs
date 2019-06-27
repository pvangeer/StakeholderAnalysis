using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class StrokeThicknessPropertyTreeNodeViewModel : DoubleUpDownPropertyTreeNodeViewModelBase<OnionRing>
    {
        public StrokeThicknessPropertyTreeNodeViewModel(OnionRing ring) : base(ring, "Lijndikte", 0.0, 40.0, 0.5, "0.##") { }

        public override double DoubleValue
        {
            get => Content.StrokeThickness;
            set
            {
                Content.StrokeThickness = value;
                Content.OnPropertyChanged(nameof(OnionRing.StrokeThickness));
            }
        }
    }
}