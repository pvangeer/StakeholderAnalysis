namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface ISliderPropertyTreeNodeViewModel
    {
        double Value { get; set; }

        double MinValue { get; }

        double MaxValue { get; }

    }
}