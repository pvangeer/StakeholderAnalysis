using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    public static class OnionRingEntityReadExtensions
    {
        internal static OnionRing Read(this OnionRingEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var attitudeImpactDiagram = new OnionRing
            {
                Percentage = entity.Percentage.ToNullAsNaN(),
                StrokeColor = entity.StrokeColor.ToColor(),
                StrokeThickness = entity.StrokeThickness.ToNullAsNaN(),
                BackgroundColor = entity.BackgroundColor.ToColor()
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}