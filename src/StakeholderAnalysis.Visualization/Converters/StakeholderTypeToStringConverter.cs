using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class StakeholderTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StakeholderType))
            {
                return null;
            }

            var stakeholderType = (StakeholderType) value;
            switch (stakeholderType)
            {
                case StakeholderType.Stakeholdergroep:
                    return "Stakeholdergroep";
                case StakeholderType.Ingenieursbureaus:
                    return "Ingenieursbureau";
                case StakeholderType.Kennisinstituut:
                    return "Kennisinstituut";
                case StakeholderType.Rijksoverheid:
                    return "Rijksoverheid";
                case StakeholderType.Waterkeringbeheerder:
                    return "Waterkeringbeheerder";
                case StakeholderType.Overig:
                    return "Overig";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
