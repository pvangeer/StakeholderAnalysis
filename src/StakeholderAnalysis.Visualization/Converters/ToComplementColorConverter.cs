using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class ToComplementColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                var color = brush.Color;
                var newColor = Color.FromArgb(color.A, (byte) (255 - color.R), (byte) (255 - color.G), (byte) (254 - color.B));
                return new SolidColorBrush(newColor);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                var color = brush.Color;
                var newColor = Color.FromArgb(color.A, (byte)(255 - color.R), (byte)(255 - color.G), (byte)(254 - color.B));
                return new SolidColorBrush(newColor);
            }

            return value;
        }
    }
}
