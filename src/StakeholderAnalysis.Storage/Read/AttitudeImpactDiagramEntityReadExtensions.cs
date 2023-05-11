using System;
using System.Linq;
using System.Windows.Media;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AttitudeImpactDiagramEntityReadExtensions
    {
        internal static AttitudeImpactDiagram Read(this AttitudeImpactDiagramXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholders = entity.AttitudeImpactDiagramStakeholderXmlEntities.OrderBy(e => e.Order)
                .Select(e => e.Read(collector));

            var attitudeImpactDiagram = new AttitudeImpactDiagram
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
                // TODO: Log warning/error in case of null value. Could not interpret stored font family
                attitudeImpactDiagram.BackgroundFontFamily = backgroundFontFamily;
            if (converter.ConvertFromInvariantString(entity.Axis.AxisTextFontFamily) is FontFamily axisTextFontFamily)
                // TODO: Log warning/error in case of null value. Could not interpret stored font family
                attitudeImpactDiagram.AxisFontFamily = axisTextFontFamily;

            foreach (var stakeholder in stakeholders) attitudeImpactDiagram.Stakeholders.Add(stakeholder);

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}