using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization
{
    public class PropertyTreeNodeValueTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IDoubleUpDownPropertyTreeNodeViewModelBase)
            {
                return DoubleUpDownTemplate;
            }

            if (item is IColorPropertyTreeNodeViewModel)
            {
                return ColorTemplate;
            }

            // TODO: throws exception in case of unsupported implementation of ITreeNodeViewModel (only property descriptors are accepted).
            return base.SelectTemplate(item,container);
        }

        public DataTemplate ColorTemplate { get; set; }

        public DataTemplate DoubleUpDownTemplate { get; set; }
    }
}