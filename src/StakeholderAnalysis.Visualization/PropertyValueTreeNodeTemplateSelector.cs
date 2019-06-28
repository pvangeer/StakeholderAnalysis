using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization
{
    public class PropertyValueTreeNodeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IDoubleUpDownPropertyTreeNodeViewModel)
            {
                return DoubleUpDownTemplate;
            }

            if (item is IColorPropertyTreeNodeViewModel)
            {
                return ColorTemplate;
            }

            if (item is IStringPropertyTreeNodeViewModel)
            {
                return StringTempalte;
            }

            if (item is IBooleanPropertyTreeNodeViewModel)
            {
                return CheckBoxTemplate;
            }

            if (item is IStakeholderTypeIconPropertyTreeNodeViewModel)
            {
                return StakeholderTypeIconDataTemplate;
            }

            return base.SelectTemplate(item,container);
        }

        public DataTemplate StakeholderTypeIconDataTemplate { get; set; }

        public DataTemplate StringTempalte { get; set; }

        public DataTemplate CheckBoxTemplate { get; set; }

        public DataTemplate ColorTemplate { get; set; }

        public DataTemplate DoubleUpDownTemplate { get; set; }
    }
}