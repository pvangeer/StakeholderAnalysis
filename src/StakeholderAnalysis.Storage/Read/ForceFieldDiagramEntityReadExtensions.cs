using System;
using System.Linq;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class ForceFieldDiagramEntityReadExtensions
    {
        internal static ForceFieldDiagram Read(this ForceFieldDiagramXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholders = entity.ForceFieldDiagramStakeholderXmlEntities.OrderBy(e => e.Order)
                .Select(e => e.Read(collector));

            var forceFieldDiagram = new ForceFieldDiagram
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
                forceFieldDiagram.BackgroundFontFamily = backgroundFontFamily;
            if (converter.ConvertFromInvariantString(entity.Axis.AxisTextFontFamily) is FontFamily axisTextFontFamily)
                forceFieldDiagram.AxisFontFamily = axisTextFontFamily;

            foreach (var stakeholder in stakeholders) forceFieldDiagram.Stakeholders.Add(stakeholder);

            collector.Collect(entity, forceFieldDiagram);

            return forceFieldDiagram;
        }
    }
}