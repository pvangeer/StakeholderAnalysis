using System;
using System.Linq;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class TwoAxisDiagramEntityReadExtensions
    {
        internal static TwoAxisDiagram Read(this TwoAxisDiagramXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholders = entity.PositionedStakeholderXmlEntities.OrderBy(e => e.Order)
                .Select(e => e.Read(collector));

            var twoAxisDiagram = new TwoAxisDiagram
            {
                Name = entity.Name,
                BrushStartColor = entity.Brush.BrushStartColor.ToColor(),
                BrushEndColor = entity.Brush.BrushEndColor.ToColor(),
                BackgroundTextLeftBottom = entity.Background.BackgroundTextLeftBottom,
                BackgroundTextLeftTop = entity.Background.BackgroundTextLeftTop,
                BackgroundTextRightTop = entity.Background.BackgroundTextRightTop,
                BackgroundTextRightBottom = entity.Background.BackgroundTextRightBottom,
                BackgroundFontBold = entity.Background.BackgroundTextFontBold == 1,
                BackgroundFontColor = entity.Background.BackgroundTextFontColor.ToColor(),
                BackgroundFontItalic = entity.Background.BackgroundTextFontItalic == 1,
                BackgroundFontSize = entity.Background.BackgroundTextFontSize,
                XAxisMinLabel = entity.Axis.XAxisMinLabel,
                XAxisMaxLabel = entity.Axis.XAxisMaxLabel,
                YAxisMaxLabel = entity.Axis.YAxisMaxLabel,
                YAxisMinLabel = entity.Axis.YAxisMinLabel,
                AxisFontBold = entity.Axis.AxisTextFontBold == 1,
                AxisFontColor = entity.Axis.AxisTextFontColor.ToColor(),
                AxisFontItalic = entity.Axis.AxisTextFontItalic == 1,
                AxisFontSize = entity.Axis.AxisTextFontSize
            };

            var converter = new FontFamilyConverter();
            if (converter.ConvertFromInvariantString(entity.Background.BackgroundTextFontFamily) is FontFamily
                backgroundFontFamily)
                twoAxisDiagram.BackgroundFontFamily = backgroundFontFamily;
            if (converter.ConvertFromInvariantString(entity.Axis.AxisTextFontFamily) is FontFamily axisTextFontFamily)
                twoAxisDiagram.AxisFontFamily = axisTextFontFamily;

            foreach (var stakeholder in stakeholders) twoAxisDiagram.Stakeholders.Add(stakeholder);

            collector.Collect(entity, twoAxisDiagram);

            return twoAxisDiagram;
        }
    }
}