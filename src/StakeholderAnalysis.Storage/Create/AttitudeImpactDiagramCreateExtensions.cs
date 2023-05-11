using System;
using System.Windows.Media;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AttitudeImpactDiagramCreateExtensions
    {
        internal static AttitudeImpactDiagramXmlEntity Create(this AttitudeImpactDiagram diagram,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(diagram)) return registry.Get(diagram);

            var converter = new FontFamilyConverter();
            var entity = new AttitudeImpactDiagramXmlEntity
            {
                Name = diagram.Name.DeepClone(),
                Brush = new TwoAxisDiagramBrushXmlEntity
                {
                    BrushStartColor = diagram.BrushStartColor.ToHexString(),
                    BrushEndColor = diagram.BrushEndColor.ToHexString()
                },
                Background = new TwoAxisDiagramBackgroundXmlEntity
                {
                    BackgroundTextLeftBottom = diagram.BackgroundTextLeftBottom,
                    BackgroundTextLeftTop = diagram.BackgroundTextLeftTop,
                    BackgroundTextRightTop = diagram.BackgroundTextRightTop,
                    BackgroundTextRightBottom = diagram.BackgroundTextRightBottom,
                    BackgroundTextFontFamily = converter.ConvertToInvariantString(diagram.BackgroundFontFamily),
                    BackgroundTextFontBold = diagram.BackgroundFontBold ? (byte)1 : (byte)0,
                    BackgroundTextFontColor = diagram.BackgroundFontColor.ToHexString(),
                    BackgroundTextFontItalic = diagram.BackgroundFontItalic ? (byte)1 : (byte)0,
                    BackgroundTextFontSize = diagram.BackgroundFontSize
                },
                Axis = new TwoAxisDiagramAxisXmlEntity
                {
                    XAxisMinLabel = diagram.XAxisMinLabel,
                    XAxisMaxLabel = diagram.XAxisMaxLabel,
                    YAxisMaxLabel = diagram.YAxisMaxLabel,
                    YAxisMinLabel = diagram.YAxisMinLabel,
                    AxisTextFontFamily = converter.ConvertToInvariantString(diagram.AxisFontFamily),
                    AxisTextFontBold = diagram.AxisFontBold ? (byte)1 : (byte)0,
                    AxisTextFontColor = diagram.AxisFontColor.ToHexString(),
                    AxisTextFontItalic = diagram.AxisFontItalic ? (byte)1 : (byte)0,
                    AxisTextFontSize = diagram.AxisFontSize
                }
            };

            AddEntitiesForStakeholders(diagram, entity, registry);

            registry.Register(diagram, entity);

            return entity;
        }

        private static void AddEntitiesForStakeholders(AttitudeImpactDiagram diagram,
            AttitudeImpactDiagramXmlEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Stakeholders.Count; index++)
            {
                var eventTreeEntity = diagram.Stakeholders[index].Create(registry);
                eventTreeEntity.Order = index;
                entity.AttitudeImpactDiagramStakeholderXmlEntities.Add(eventTreeEntity);
            }
        }
    }
}