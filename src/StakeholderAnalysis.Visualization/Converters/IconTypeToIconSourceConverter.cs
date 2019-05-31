using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class IconTypeToIconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StakeholderIconType iconType)
            {
                switch (iconType)
                {
                    case StakeholderIconType.Waterkeringbeheerder:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Waterschap.png";
                    case StakeholderIconType.Kennisinstituut:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Knowledge.png";
                    case StakeholderIconType.Rijksoverheid:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Pak.png";
                    case StakeholderIconType.Stakeholdergroep:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Group.png";
                    case StakeholderIconType.Ingenieursbureaus:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/euro.png";
                    case StakeholderIconType.Overig:
                        return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/other.png";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
