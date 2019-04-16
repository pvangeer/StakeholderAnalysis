using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.Converters
{
    public class SizeToHorizontalArrowPathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double) values[0];
            var height = (double) values[1];

            List<LineSegment> segments = new List<LineSegment>();
            double xMargin = 0;
            double yMargin = 0;
            var arrowHeadWidth = 10;
            var arrowHeadLength = 20;

            segments.Add(new LineSegment(new Point(xMargin,yMargin), true));
            segments.Add(new LineSegment(new Point(xMargin + arrowHeadWidth, arrowHeadLength + yMargin), true));
            segments.Add(new LineSegment(new Point(xMargin - arrowHeadWidth, arrowHeadLength + yMargin), false));
            segments.Add(new LineSegment(new Point(xMargin, yMargin), true));
            segments.Add(new LineSegment(new Point(xMargin, height - yMargin), false));
            segments.Add(new LineSegment(new Point(width - xMargin, height - yMargin), true));
            segments.Add(new LineSegment(new Point(width - xMargin - arrowHeadLength, height - yMargin + arrowHeadWidth), true));
            segments.Add(new LineSegment(new Point(width - xMargin - arrowHeadLength, height - yMargin - arrowHeadWidth), false));
            segments.Add(new LineSegment(new Point(width - xMargin, height - yMargin), true));
            PathFigure figure = new PathFigure(new Point(xMargin, height - yMargin), segments, false); //true if closed
            PathGeometry geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            return geometry;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
