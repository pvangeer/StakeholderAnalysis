using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    // TODO: Use ITreeNodeViewModel instead
    public interface IExpandableContentViewModel : IExpandable
    {
        string DisplayName { get; }
    }
}
