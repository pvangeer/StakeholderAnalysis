﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters.TreeView
{
    public class SourceStringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string s && !string.IsNullOrWhiteSpace(s) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}