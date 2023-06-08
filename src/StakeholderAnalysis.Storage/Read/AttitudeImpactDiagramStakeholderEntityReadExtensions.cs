using System;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AttitudeImpactDiagramStakeholderEntityReadExtensions
    {
        internal static AttitudeImpactDiagramStakeholder Read(this AttitudeImpactDiagramStakeholderXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var attitudeImpactDiagram = new AttitudeImpactDiagramStakeholder(
                collector.GetReferencedStakeholder(entity.StakeholderReferenceId),
                1.0 - entity.Attitude, entity.Impact)
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}