using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface ILineStylePropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        LineStyle LineStyleValue { get; set; }
    }
}