using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class DocumentViewInfoToPropertiesViewInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OnionDiagramViewModel onionDiagramViewModel) return onionDiagramViewModel.GetPropertiesViewModel();

            if (value is ITwoAxisDiagramViewModel twoAxisDiagramViewModel) return twoAxisDiagramViewModel.GetPropertiesViewModel();

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}