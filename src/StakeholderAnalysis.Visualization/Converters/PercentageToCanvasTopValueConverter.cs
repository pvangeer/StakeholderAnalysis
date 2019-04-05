using System;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class PercentageToCanvasTopValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double percentage = (double)values[0];
            double asymmetry = (double)values[1];
            double actualHeight = (double)values[2];
            if (Math.Abs(actualHeight) < 1e-8)
            {
                return 0.0;
            }

            var elementTopOnCanvas = (1 - percentage) * actualHeight / 2;
            return elementTopOnCanvas + asymmetry * elementTopOnCanvas;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}