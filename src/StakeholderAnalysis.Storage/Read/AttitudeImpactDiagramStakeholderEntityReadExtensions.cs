using System;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AttitudeImpactDiagramStakeholderEntityReadExtensions
    {
        internal static AttitudeImpactDiagramStakeholder Read(this AttitudeImpactDiagramStakeholderEntity entity,
            ReadConversionCollector collector)
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

            var attitudeImpactDiagram = new AttitudeImpactDiagramStakeholder(entity.StakeholderEntity.Read(collector),
                entity.Attitude.ToNullAsNaN(), entity.Impact.ToNullAsNaN())
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}
