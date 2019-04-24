using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization
{
    public class ViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnionDiagramViewTemplate { get; set; }

        public DataTemplate DefaultnDataTemplate { get; set; }

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
            if (item is StakeholderForcesDiagramViewModel)
            {
                return StakeholderForcesDiagramTemplate;
            }
            if (item is AttitudeImpactDiagramViewModel)
            {
                return AttitudeImpactDiagramTemplate;
            }

            return DefaultnDataTemplate;
        }
    }
}
