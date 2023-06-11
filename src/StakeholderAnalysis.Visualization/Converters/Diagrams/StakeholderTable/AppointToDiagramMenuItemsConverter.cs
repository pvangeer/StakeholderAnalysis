using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Fluent;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Visualization.Commands.Diagrams;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;
using MenuItem = System.Windows.Controls.MenuItem;

namespace StakeholderAnalysis.Visualization.Converters.Diagrams.StakeholderTable
{
    public class AppointToDiagramMenuItemsConverter : IValueConverter
    {
        private const string OnionDiagramIconSource =
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/onion.ico";

        private const string ForceFieldDiagramIconSource =
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/forces.ico";

        private const string AttitudeImpactDiagramIconSource =
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/involvement.ico";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItems = new List<MenuItem>();
            if (!(value is StakeholderTableViewModel tableViewModel))
                return menuItems;

            if (tableViewModel.OnionDiagrams.Any())
            {
                menuItems.Add(new GroupSeparatorMenuItem { Header = "UI-diagrammen" });
                menuItems.AddRange(
                    tableViewModel.OnionDiagrams.Select(d => CreateDiagramMenuItem(d, tableViewModel, OnionDiagramIconSource)));
            }

            if (tableViewModel.ForceFieldDiagrams.Any())
            {
                if (menuItems.Any())
                    menuItems.Add(new GroupSeparatorMenuItem { Header = "Krachtenveld diagrammen" });
                menuItems.AddRange(tableViewModel.ForceFieldDiagrams.Select(d =>
                    CreateDiagramMenuItem(d, tableViewModel, ForceFieldDiagramIconSource)));
            }

            if (tableViewModel.AttitudeImpactDiagrams.Any())
            {
                if (menuItems.Any())
                    menuItems.Add(new GroupSeparatorMenuItem { Header = "Houding - impact diagrammen" });
                menuItems.AddRange(tableViewModel.AttitudeImpactDiagrams.Select(d =>
                    CreateDiagramMenuItem(d, tableViewModel, AttitudeImpactDiagramIconSource)));
            }

            return menuItems;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private MenuItem CreateDiagramMenuItem(IStakeholderDiagram diagram, StakeholderTableViewModel tableViewModel, string iconSource)
        {
            return new MenuItem
            {
                Header = diagram.Name,
                Command = new AppointToDiagramCommand(tableViewModel, diagram),
                Icon = new Image
                {
                    Source = new BitmapImage(new Uri(iconSource, UriKind.RelativeOrAbsolute))
                }
            };
        }
    }
}