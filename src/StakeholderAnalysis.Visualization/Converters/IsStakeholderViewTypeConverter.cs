﻿using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsStakeholderViewTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StakeholderViewType) || !(parameter is StakeholderViewType)) return false;
            return (StakeholderViewType) value == (StakeholderViewType) parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}