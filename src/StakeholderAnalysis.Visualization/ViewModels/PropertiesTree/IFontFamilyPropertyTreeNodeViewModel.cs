using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    interface IFontFamilyPropertyTreeNodeViewModel : ITreeNodeViewModel
    {
        FontFamily SelectedValue { get; set; }
    }
}
