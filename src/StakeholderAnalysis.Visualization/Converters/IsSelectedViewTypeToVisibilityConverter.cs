﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsSelectedViewTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) new IsSelectedViewTypeConverter().Convert(value, targetType, parameter, culture)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}