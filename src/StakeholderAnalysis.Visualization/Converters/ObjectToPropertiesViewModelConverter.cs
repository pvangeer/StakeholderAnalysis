using System;
using System.Globalization;
using System.Windows.Data;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class ObjectToPropertiesViewModelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                throw new ArgumentOutOfRangeException();

            var viewModelFactory = values[1] as ViewModelFactory;
            var value = values[0];
            if (viewModelFactory == null)
                throw new ArgumentNullException();

            switch (value)
            {
                case OnionDiagram diagram:
                    return viewModelFactory.CreateOnionDiagramPropertiesViewModel(diagram);
                case ForceFieldDiagram diagram:
                    return viewModelFactory.CreateTwoAxisDiagramPropertiesViewModel(diagram);
                case AttitudeImpactDiagram diagram:
                    return viewModelFactory.CreateTwoAxisDiagramPropertiesViewModel(diagram);
                case StakeholderType type:
                    return viewModelFactory.CreateStakeholderTypePropertiesViewModel(type);
                default:
                    return value;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}