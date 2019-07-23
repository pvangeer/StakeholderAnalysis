using System;
using System.Linq;
using System.Windows.Media;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AttitudeImpactDiagramEntityReadExtensions
    {
        internal static AttitudeImpactDiagram Read(this AttitudeImpactDiagramEntity entity, ReadConversionCollector collector)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }

            if (collector.Contains(entity))
            {
                return collector.Get(entity);
            }

            var stakeholders = entity.AttitudeImpactDiagramStakeholderEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));

            var attitudeImpactDiagram = new AttitudeImpactDiagram
            {
                Name = entity.Name,
                BrushStartColor = entity.BrushStartColor.ToColor(),
                BrushEndColor = entity.BrushEndColor.ToColor(),
                BackgroundTextLeftBottom = entity.BackgroundTextLeftBottom,
                BackgroundTextLeftTop = entity.BackgroundTextLeftTop,
                BackgroundTextRightTop = entity.BackgroundTextRightTop,
                BackgroundTextRightBottom = entity.BackgroundTextRightBottom,
                BackgroundFontBold = entity.BackgroundTextFontBold == 1,
                BackgroundFontColor = entity.BackgroundTextFontColor.ToColor(),
                BackgroundFontItalic = entity.BackgroundTextFontItalic == 1,
                BackgroundFontSize = entity.BackgroundTextFontSize,
                XAxisMinLabel = entity.XAxisMinLabel,
                XAxisMaxLabel = entity.XAxisMaxLabel,
                YAxisMaxLabel = entity.YAxisMaxLabel,
                YAxisMinLabel = entity.YAxisMinLabel,
                AxisFontBold = entity.AxisTextFontBold == 1,
                AxisFontColor = entity.AxisTextFontColor.ToColor(),
                AxisFontItalic = entity.AxisTextFontItalic == 1,
                AxisFontSize = entity.AxisTextFontSize
            };

            var converter = new FontFamilyConverter();
            if (converter.ConvertFromInvariantString(entity.BackgroundTextFontFamily) is FontFamily backgroundFontFamily)
            {
                // TODO: Log warning/error in case of null value. Could not interpret stored font family
                attitudeImpactDiagram.BackgroundFontFamily = backgroundFontFamily;
            }
            if (converter.ConvertFromInvariantString(entity.AxisTextFontFamily) is FontFamily axisTextFontFamily)
            {
                // TODO: Log warning/error in case of null value. Could not interpret stored font family
                attitudeImpactDiagram.AxisFontFamily = axisTextFontFamily;
            }

            foreach (var stakeholder in stakeholders)
            {
                attitudeImpactDiagram.Stakeholders.Add(stakeholder);
            }

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}
