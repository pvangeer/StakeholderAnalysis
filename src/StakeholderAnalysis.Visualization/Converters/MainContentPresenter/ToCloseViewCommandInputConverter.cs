using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Converters.MainContentPresenter
{
    public class ToCloseViewCommandInputConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) return null;

            return new CloseViewWithMiddleMouseCommandParameters
            {
                ClickedViewInfo = values[0] as ViewInfo,
                ViewManager = values[1] as ViewManagerViewModel
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}