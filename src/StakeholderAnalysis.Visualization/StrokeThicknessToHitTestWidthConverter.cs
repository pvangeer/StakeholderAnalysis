using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization
{
    public class StrokeThicknessToHitTestWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width && 
                parameter is string hitTestMargin &&
                double.TryParse(hitTestMargin, NumberStyles.Any, CultureInfo.InvariantCulture, out var hitTestMarginValue))
            {
                return width + hitTestMarginValue;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}