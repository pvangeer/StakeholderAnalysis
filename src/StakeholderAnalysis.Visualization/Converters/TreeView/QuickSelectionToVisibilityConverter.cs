using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.Converters.TreeView
{
    public class QuickSelectionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ConnectionGroupPropertiesViewModel) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v == Visibility.Visible;

            return false;
        }
    }
}