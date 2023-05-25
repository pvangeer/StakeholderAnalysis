using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters.Diagrams.OnionDiagram
{
    public class PercentageToCanvasLeftValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = (double)values[0];
            var asymmetry = (double)values[1];
            var orientationDegrees = (double)values[2];
            var orientation = Math.PI * (orientationDegrees - 90) / 180.0;
            var actualWidth = (double)values[3];

            if (Math.Abs(actualWidth) < 1e-8) return 0.0;

            var elementLeftOnCanvas = (1 - percentage) * actualWidth / 2.0;
            return elementLeftOnCanvas + Math.Cos(orientation) * asymmetry * elementLeftOnCanvas;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}