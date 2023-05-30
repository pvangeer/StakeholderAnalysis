namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface IDoubleUpDownPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        double DoubleValue { get; set; }

        double MinValue { get; }

        double MaxValue { get; }

        double Increment { get; }

        string StringFormat { get; }
    }
}