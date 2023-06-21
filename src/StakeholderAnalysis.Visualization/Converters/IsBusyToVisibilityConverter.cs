using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsBusyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isBusy) || !(parameter is bool normalControls))
                return Visibility.Visible;

            return normalControls ? isBusy ? Visibility.Collapsed : Visibility.Visible :
                isBusy ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}