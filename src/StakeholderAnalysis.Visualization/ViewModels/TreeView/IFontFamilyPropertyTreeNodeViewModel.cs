using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    internal interface IFontFamilyPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        FontFamily SelectedValue { get; set; }
    }
}