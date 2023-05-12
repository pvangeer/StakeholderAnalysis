﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class ValueToSameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}