using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class ForceFieldDiagramStakeholderEntityReadExtensions
    {
        internal static ForceFieldDiagramStakeholder Read(this ForceFieldDiagramStakeholderEntity entity,
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

            var attitudeImpactDiagram = new ForceFieldDiagramStakeholder(entity.StakeholderEntity.Read(collector),
                entity.Interest.ToNullAsNaN(), entity.Influence.ToNullAsNaN())
            {
                Rank = (int) entity.Rank
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}
