using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface IColorPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        Color ColorValue { get; set; }
    }
}