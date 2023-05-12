using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.Converters.StatusBar
{
    public class MessageListToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is MessageListViewModel viewModel)) return value;

            return $"{viewModel.MessageList.Count} Berichten";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}