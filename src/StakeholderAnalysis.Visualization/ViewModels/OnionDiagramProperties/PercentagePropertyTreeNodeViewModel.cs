using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class PercentagePropertyTreeNodeViewModel : DoubleUpDownPropertyTreeNodeViewModelBase<OnionRing>
    {
        public PercentagePropertyTreeNodeViewModel(OnionRing ring) : base(ring,"Grootte",0.0,1.0,0.01,"0.###") { }

        public override double DoubleValue
        {
            get => Content.Percentage;
            set
            {
                Content.Percentage = value;
                Content.OnPropertyChanged(nameof(OnionRing.Percentage));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return o as OnionRing == Content;
        }
    }
}