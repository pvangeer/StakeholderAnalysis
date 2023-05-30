using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.Properties;
using StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties;

namespace StakeholderAnalysis.Visualization
{
    public class PropertiesViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultEmptyDataTemplate { get; set; }

        public DataTemplate HasPropertiesDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case OnionDiagramPropertiesViewModel _:
                case TwoAxisDiagramPropertiesViewModel _:
                case StakeholderTypePropertiesViewModel _:
                    return HasPropertiesDataTemplate;
                default:
                    return DefaultEmptyDataTemplate;
            }
        }
    }
}