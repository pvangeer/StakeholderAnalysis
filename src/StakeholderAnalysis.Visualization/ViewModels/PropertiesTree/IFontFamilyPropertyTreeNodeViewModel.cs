using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    internal interface IFontFamilyPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        FontFamily SelectedValue { get; set; }
    }
}