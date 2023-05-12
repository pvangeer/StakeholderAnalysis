using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsActiveToolWindowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as IEnumerable<ViewInfo>)?.Any(vi => vi.ViewModel.GetType() == parameter as Type) ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}