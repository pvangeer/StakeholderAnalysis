using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.Commands.Diagrams;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.Converters.Diagrams.StakeholderTable
{
    public class AppointToDiagramMenuItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StakeholderTableViewModel tableViewModel))
                return new List<MenuItem>();

            return tableViewModel.AllDiagrams.Select(d => new MenuItem
            {
                Header = d.Name,
                Command = new AppointToDiagramCommand(tableViewModel, d),
                Icon = new Image
                {
                    Source = new BitmapImage(new Uri(DiagramToIconSourceString(d), UriKind.RelativeOrAbsolute))
                }
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string DiagramToIconSourceString(IStakeholderDiagram diagram)
        {
            switch (diagram)
            {
                case Data.Diagrams.OnionDiagrams.OnionDiagram onionDiagram:
                    return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/onion.ico";
                case AttitudeImpactDiagram attitudeImpactDiagram:
                    return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/forces.ico";
                case ForceFieldDiagram forceFieldDiagram:
                    return "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/involvement.ico";
                default:
                    return "";
            }
        }
    }
}