using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization
{
    public class LayoutItemContainerStyleSelector : StyleSelector
    {
        public Style AnchorableContainerStyle { get; set; }

        public Style DocumentContainerStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ViewInfo viewInfo && !viewInfo.IsDocumentView)
            {
                return AnchorableContainerStyle;
            }
            return DocumentContainerStyle;
        }
    }
}