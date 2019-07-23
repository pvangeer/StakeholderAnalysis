using System;
using System.Windows.Media;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class ForceFieldDiagramCreateExtensions
    {
        internal static ForceFieldDiagramEntity Create(this ForceFieldDiagram diagram, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(diagram))
            {
                return registry.Get(diagram);
            }

            var converter = new FontFamilyConverter();
            var entity = new ForceFieldDiagramEntity
            {
                Name = diagram.Name.DeepClone(),
                BrushStartColor = diagram.BrushStartColor.ToHexString(),
                BrushEndColor = diagram.BrushEndColor.ToHexString(),
                BackgroundTextLeftBottom = diagram.BackgroundTextLeftBottom,
                BackgroundTextLeftTop = diagram.BackgroundTextLeftTop,
                BackgroundTextRightTop = diagram.BackgroundTextRightTop,
                BackgroundTextRightBottom = diagram.BackgroundTextRightBottom,
                BackgroundTextFontFamily = converter.ConvertToInvariantString(diagram.BackgroundFontFamily),
                BackgroundTextFontBold = diagram.BackgroundFontBold ? (byte)1 : (byte)0,
                BackgroundTextFontColor = diagram.BackgroundFontColor.ToHexString(),
                BackgroundTextFontItalic = diagram.BackgroundFontItalic ? (byte)1 : (byte)0,
                BackgroundTextFontSize = diagram.BackgroundFontSize,
                XAxisMinLabel = diagram.XAxisMinLabel,
                XAxisMaxLabel = diagram.XAxisMaxLabel,
                YAxisMaxLabel = diagram.YAxisMaxLabel,
                YAxisMinLabel = diagram.YAxisMinLabel,
                AxisTextFontFamily = converter.ConvertToInvariantString(diagram.AxisFontFamily),
                AxisTextFontBold = diagram.AxisFontBold ? (byte)1 : (byte)0,
                AxisTextFontColor = diagram.AxisFontColor.ToHexString(),
                AxisTextFontItalic = diagram.AxisFontItalic ? (byte)1 : (byte)0,
                AxisTextFontSize = diagram.AxisFontSize
            };

            AddEntitiesForStakeholders(diagram, entity, registry);

            registry.Register(diagram, entity);

            return entity;
        }

        private static void AddEntitiesForStakeholders(ForceFieldDiagram diagram, ForceFieldDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Stakeholders.Count; index++)
            {
                var stakeholder = diagram.Stakeholders[index].Create(registry);
                stakeholder.Order = index;
                entity.ForceFieldDiagramStakeholderEntities.Add(stakeholder);
            }
        }
    }
}
