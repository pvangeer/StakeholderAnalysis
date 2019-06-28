using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization
{
    public class PropertyValueTreeNodeTemplateSelector : DataTemplateSelector
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

            if (item is IStringPropertyTreeNodeViewModelBase)
            {
                return StringTempalte;
            }

            if (item is IBooleanPropertyTreeNodeViewModel)
            {
                return CheckBoxTemplate;
            }

            // TODO: throws exception in case of unsupported implementation of ITreeNodeViewModel (only property descriptors are accepted).
            return base.SelectTemplate(item,container);
        }

        public DataTemplate StringTempalte { get; set; }

        public DataTemplate CheckBoxTemplate { get; set; }

        public DataTemplate ColorTemplate { get; set; }

        public DataTemplate DoubleUpDownTemplate { get; set; }
    }
}