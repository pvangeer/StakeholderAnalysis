using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class TypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StakeholderType)) return value;

            switch ((StakeholderType) value)
            {
                case StakeholderType.Waterkeringbeheerder:
                    return new SolidColorBrush(Colors.AliceBlue);
                case StakeholderType.Kennisinstituut:
                    return new SolidColorBrush(Colors.DarkSeaGreen);
                case StakeholderType.Rijksoverheid:
                    return new SolidColorBrush(Colors.MistyRose);
                case StakeholderType.Stakeholdergroep:
                    return new SolidColorBrush(Colors.DarkGray);
                case StakeholderType.Ingenieursbureaus:
                    return new SolidColorBrush(Colors.CadetBlue);
                case StakeholderType.Overig:
                    return new SolidColorBrush(Colors.Chocolate);
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}