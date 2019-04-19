using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsSelectedViewTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StakeholderViewInfo) || !(parameter is StakeholderViewType)) return Visibility.Collapsed;

            var viewInfo = (StakeholderViewInfo) value;
            var validateAgainstType = (StakeholderViewType) parameter;
            return viewInfo.Type == validateAgainstType ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}