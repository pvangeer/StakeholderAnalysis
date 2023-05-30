using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface ILineStylePropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        LineStyle LineStyleValue { get; set; }
    }
}