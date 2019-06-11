using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization
{
    public class IsSelectedToStrokeThicknessConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return values;
            }

            var thickness = (double)values[0];
            var isSelected = (bool)values[1];
            return thickness + (isSelected ? thickness : 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}