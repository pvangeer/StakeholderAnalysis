﻿using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Converters.MainContentPresenter
{
    public class IsProneToExportConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)values[0] &&
                   values[1] is ViewInfo viewInfo &&
                   values[2] != null &&
                   viewInfo.ViewModel == values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}