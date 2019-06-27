namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface IDoubleUpDownPropertyTreeNodeViewModelBase : ITreeNodeViewModel
    {
        double DoubleValue { get; set; }

        double MinValue { get; }

        double MaxValue { get; }

        double Increment { get; }

        string StringFormat { get; }
    }
}