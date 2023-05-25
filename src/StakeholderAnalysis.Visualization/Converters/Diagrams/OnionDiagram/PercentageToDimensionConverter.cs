using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters.Diagrams.OnionDiagram
{
    public class PercentageToDimensionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = 0.0;
            var elementSize = 0.0;
            var actualSize = 0.0;
            var pixelOffset = 0.0;
            if (values.Length == 2)
            {
                percentage = GetValueAsDouble(values, 0);
                elementSize = 0.0;
                actualSize = GetValueAsDouble(values, 1);
            }

            if (values.Length == 3)
            {
                percentage = GetValueAsDouble(values, 0);
                elementSize = GetValueAsDouble(values, 1);
                actualSize = GetValueAsDouble(values, 2);
            }

            if (values.Length == 4)
            {
                percentage = GetValueAsDouble(values, 0);
                elementSize = GetValueAsDouble(values, 1);
                actualSize = GetValueAsDouble(values, 2);
                pixelOffset = GetValueAsDouble(values, 3);
            }

            return percentage * actualSize - elementSize / 2 + pixelOffset;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static double GetValueAsDouble(object[] values, int i)
        {
            return values[i] is double valueAsDouble ? valueAsDouble : 0.0;
        }
    }
}