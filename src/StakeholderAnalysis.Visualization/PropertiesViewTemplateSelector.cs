using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties;

namespace StakeholderAnalysis.Visualization
{
    public class PropertiesViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }

        public DataTemplate OnionDiagramPropertiesTemplate { get; set; }

        public DataTemplate TwoAxisDiagramPropertiesTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is OnionDiagramPropertiesViewModel) return OnionDiagramPropertiesTemplate;
            if (item is TwoAxisDiagramPropertiesViewModel) return TwoAxisDiagramPropertiesTemplate;

            return DefaultDataTemplate;
        }
    }
}