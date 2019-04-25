using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels;
using StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ForceFieldDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView;

namespace StakeholderAnalysis.Visualization
{
    public class ViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnionDiagramViewTemplate { get; set; }

        public DataTemplate DefaultDataTemplate { get; set; }

        public DataTemplate ProjectExplorerDataTemplate { get; set; }

        public DataTemplate StakeholderTableViewTemplate {get;set;}

        public DataTemplate StakeholderForcesDiagramTemplate { get; set; }

        public DataTemplate AttitudeImpactDiagramTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is OnionDiagramViewModel)
            {
                return OnionDiagramViewTemplate;
            }
            if (item is ProjectExplorerViewModel)
            {
                return ProjectExplorerDataTemplate;
            }
            if (item is StakeholderTableViewModel)
            {
                return StakeholderTableViewTemplate;
            }
            if (item is ForceFieldDiagramViewModel)
            {
                return StakeholderForcesDiagramTemplate;
            }
            if (item is AttitudeImpactDiagramViewModel)
            {
                return AttitudeImpactDiagramTemplate;
            }

            return DefaultDataTemplate;
        }
    }
}
