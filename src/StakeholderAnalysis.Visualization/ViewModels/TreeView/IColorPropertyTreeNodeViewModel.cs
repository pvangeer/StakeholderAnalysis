using System.Windows.Media;
using Fluent.Converters;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface IColorPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        Color ColorValue { get; set; }
    }
}