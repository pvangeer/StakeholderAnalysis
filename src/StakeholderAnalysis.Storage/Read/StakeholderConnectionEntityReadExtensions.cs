using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderConnectionEntityReadExtensions
    {
        internal static StakeholderConnection Read(this StakeholderConnectionEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var attitudeImpactDiagram = new StakeholderConnection(
                entity.StakeholderConnectionGroupEntity.Read(collector),
                entity.OnionDiagramStakeholderEntity1.Read(collector),
                entity.OnionDiagramStakeholderEntity.Read(collector));

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}