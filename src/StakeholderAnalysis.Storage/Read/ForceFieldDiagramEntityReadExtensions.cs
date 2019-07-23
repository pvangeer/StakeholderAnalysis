using System;
using System.Linq;
using System.Windows.Media;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class ForceFieldDiagramEntityReadExtensions
    {
        internal static ForceFieldDiagram Read(this ForceFieldDiagramEntity entity, ReadConversionCollector collector)
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

            var stakeholders = entity.ForceFieldDiagramStakeholderEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));

            var forceFieldDiagram = new ForceFieldDiagram
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
                forceFieldDiagram.BackgroundFontFamily = backgroundFontFamily;
            }
            if (converter.ConvertFromInvariantString(entity.AxisTextFontFamily) is FontFamily axisTextFontFamily)
            {
                // TODO: Log warning/error in case of null value. Could not interpret stored font family
                forceFieldDiagram.AxisFontFamily = axisTextFontFamily;
            }

            foreach (var stakeholder in stakeholders)
            {
                forceFieldDiagram.Stakeholders.Add(stakeholder);
            }

            collector.Collect(entity, forceFieldDiagram);

            return forceFieldDiagram;
        }
    }
}
