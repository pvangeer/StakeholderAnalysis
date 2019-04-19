using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsSelectedViewTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StakeholderViewInfo) || !(parameter is StakeholderViewType)) return false;

            var viewInfo = (StakeholderViewInfo)value;
            var validateAgainstType = (StakeholderViewType)parameter;
            return viewInfo.Type == validateAgainstType;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
