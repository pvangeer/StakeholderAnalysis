using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class PercentageToDimensionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = 0.0;
            var elementSize = 0.0;
            var actualSize = 0.0;
            if (values.Length == 2)
            {
                percentage = (double) values[0];
                elementSize = 0.0;
                actualSize = (double) values[1];
            }

            if (values.Length == 3)
            {
                percentage = (double) values[0];
                elementSize = (double) values[1];
                actualSize = (double) values[2];
            }

            return percentage * actualSize - elementSize / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}