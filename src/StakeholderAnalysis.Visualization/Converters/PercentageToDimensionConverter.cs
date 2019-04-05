using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class PercentageToDimensionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = (double) values[0];
            var actualSize = (double)values[1];
            return percentage * actualSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}