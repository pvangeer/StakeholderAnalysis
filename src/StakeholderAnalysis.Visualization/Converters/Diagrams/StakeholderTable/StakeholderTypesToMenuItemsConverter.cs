using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using StakeholderAnalysis.Visualization.Commands.Diagrams;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.Converters.Diagrams.StakeholderTable
{
    public class StakeholderTypesToMenuItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tableViewModel = value as StakeholderTableViewModel;

            if (tableViewModel == null)
                return new List<MenuItem>();

            var iconTypeConverter = new IconTypeToIconSourceConverter();

            return tableViewModel.StakeholderTypes.Select(st =>
            {
                var iconReference = (string)iconTypeConverter.Convert(st.IconType, typeof(string), null, CultureInfo.CurrentUICulture);
                return new MenuItem
                {
                    Header = st.Name,
                    Command = new ChangeStakeholderTypeCommand(tableViewModel),
                    CommandParameter = st,
                    Icon = new Image
                    {
                        Source = new BitmapImage(new Uri(
                            iconReference, UriKind.RelativeOrAbsolute))
                    }
                };
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}