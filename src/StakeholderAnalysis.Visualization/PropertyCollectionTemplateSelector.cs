using System.Linq;
using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization
{
    public class PropertyCollectionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PropertyValueItemTemplate { get; set; }

        public DataTemplate PropertyCollectionItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IPropertyCollectionTreeNodeViewModel)
            {
                return PropertyCollectionItemTemplate;
            }

            return PropertyValueItemTemplate;
        }

    }
}