using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization
{
    public class StakeholderTypeToNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var type = values[0] as StakeholderType;
            return type == null ? values[0] : type.Name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}