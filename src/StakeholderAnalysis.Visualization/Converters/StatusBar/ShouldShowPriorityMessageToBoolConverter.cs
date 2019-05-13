using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Visualization.Converters.StatusBar
{
    public class ShouldShowPriorityMessageToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is LogMessage message && message.HasPriority;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}