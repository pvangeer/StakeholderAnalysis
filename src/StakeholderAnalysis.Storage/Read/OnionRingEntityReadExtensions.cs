using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    public static class OnionRingEntityReadExtensions
    {
        internal static OnionRing Read(this OnionRingXmlEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var attitudeImpactDiagram = new OnionRing
            {
                Percentage = entity.Percentage,
                StrokeColor = entity.StrokeColor.ToColor(),
                StrokeThickness = entity.StrokeThickness,
                BackgroundColor = entity.BackgroundColor.ToColor()
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}