using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class PercentageToCanvasLeftValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = (double)values[0];
            var width = (double)values[1];
            
            return (1 - percentage) * width / 2.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
