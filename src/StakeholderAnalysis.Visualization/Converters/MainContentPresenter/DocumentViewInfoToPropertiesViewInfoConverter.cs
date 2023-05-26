using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Converters.MainContentPresenter
{
    public class DocumentViewInfoToPropertiesViewInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ProjectExplorerOnionDiagramViewModel projectExplorerOnionDiagramViewModel:
                    return projectExplorerOnionDiagramViewModel.GetPropertiesViewModel();
                case ProjectExplorerForceFieldDiagramViewModel projectExplorerForceFieldDiagramViewModel:
                    return projectExplorerForceFieldDiagramViewModel.GetPropertiesViewModel();
                case ProjectExplorerAttitudeImpactDiagramViewModel projectExplorerAttitudeImpactDiagramViewModel:
                    return projectExplorerAttitudeImpactDiagramViewModel.GetPropertiesViewModel();
                case OnionDiagramViewModel onionDiagramViewModel:
                    return onionDiagramViewModel.GetPropertiesViewModel();
                case ITwoAxisDiagramViewModel twoAxisDiagramViewModel:
                    return twoAxisDiagramViewModel.GetPropertiesViewModel();
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}