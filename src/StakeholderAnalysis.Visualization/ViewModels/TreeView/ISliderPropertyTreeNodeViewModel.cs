namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface ISliderPropertyTreeNodeViewModel
    {
        double Value { get; set; }

        double MinValue { get; }

        double MaxValue { get; }
    }
}