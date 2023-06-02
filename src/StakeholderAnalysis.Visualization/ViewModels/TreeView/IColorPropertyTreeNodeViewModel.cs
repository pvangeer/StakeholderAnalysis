using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface IColorPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        Color ColorValue { get; set; }
    }
}