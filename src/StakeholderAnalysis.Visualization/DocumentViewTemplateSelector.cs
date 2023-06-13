using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization
{
    public class DocumentViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }

        public DataTemplate OnionDiagramViewTemplate { get; set; }

        public DataTemplate StakeholderTableViewTemplate { get; set; }

        public DataTemplate TwoAxisDiagramTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case OnionDiagramViewModel _:
                    return OnionDiagramViewTemplate;
                case StakeholderTableViewModel _:
                    return StakeholderTableViewTemplate;
                case TwoAxisDiagramViewModel _:
                    return TwoAxisDiagramTemplate;
                default:
                    return DefaultDataTemplate;
            }
        }
    }
}