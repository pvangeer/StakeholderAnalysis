using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using StakeholderAnalysis.Visualization.Converters;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization
{
    public class TestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as string;
            if (string.IsNullOrWhiteSpace(source)) return value;

            var iconUri = new Uri(source, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(iconUri);

            if (!(value is ContextMenuItemViewModel viewModel) || !(parameter is ContextMenuContentType type))
                return value;

            switch (type)
            {
                case ContextMenuContentType.Header:
                    return viewModel.Header;
                case ContextMenuContentType.IconReference:
                    return viewModel.IconReference;
                case ContextMenuContentType.IsEnabled:
                    return viewModel.IsEnabled;
                case ContextMenuContentType.Command:
                    return viewModel.Command;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}