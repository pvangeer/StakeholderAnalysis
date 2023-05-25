﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace StakeholderAnalysis.Visualization.Converters.Diagrams.OnionDiagram
{
    public class PercentageToCanvasTopValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = (double)values[0];
            var asymmetry = (double)values[1];
            var orientationDegrees = (double)values[2];
            var orientation = Math.PI * (orientationDegrees - 90) / 180.0;
            var actualHeight = (double)values[3];

            if (Math.Abs(actualHeight) < 1e-8) return 0.0;

            var elementTopOnCanvas = (1 - percentage) * actualHeight / 2;
            return elementTopOnCanvas + Math.Sin(orientation) * asymmetry * elementTopOnCanvas;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}