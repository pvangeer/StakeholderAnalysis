using System;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AttitudeImpactDiagramStakeholderEntityReadExtensions
    {
        internal static PositionedStakeholder Read(this AttitudeImpactDiagramStakeholderXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholder = new PositionedStakeholder(
                collector.GetReferencedStakeholder(entity.StakeholderReferenceId),
                entity.Impact, 1.0 - entity.Attitude)
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, stakeholder);

            return stakeholder;
        }
    }
}