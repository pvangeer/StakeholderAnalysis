using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsSelectedStakeholderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[0] is StakeholderViewModel selectedStakeholderViewModel &&
                   selectedStakeholderViewModel.IsViewModelFor(values[1] as StakeholderViewModel);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}