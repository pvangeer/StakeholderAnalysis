using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization
{
    public class IsSelectedStakeholderToOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool) value ? 10.0 : 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}