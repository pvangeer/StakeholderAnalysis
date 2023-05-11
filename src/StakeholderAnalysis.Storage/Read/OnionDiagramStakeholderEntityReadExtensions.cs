using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class OnionDiagramStakeholderEntityReadExtensions
    {
        internal static OnionDiagramStakeholder Read(this OnionDiagramStakeholderEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var attitudeImpactDiagram = new OnionDiagramStakeholder(entity.StakeholderEntity.Read(collector),
                entity.Left.ToNullAsNaN(), entity.Top.ToNullAsNaN())
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}