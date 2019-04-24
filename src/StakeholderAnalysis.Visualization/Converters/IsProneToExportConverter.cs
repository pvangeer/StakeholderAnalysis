using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IsProneToExportConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) values[0] &&
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
