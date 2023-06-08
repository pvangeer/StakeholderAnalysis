using System;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderConnectionEntityReadExtensions
    {
        internal static StakeholderConnection Read(this StakeholderConnectionXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);


            var attitudeImpactDiagram = new StakeholderConnection(
                collector.GetReferencedStakeholderConnectionGroup(entity.StakeholderConnectionGroupId),
                collector.GetReferencedOnionDiagramStakeholder(entity.StakeholderFromId),
                collector.GetReferencedOnionDiagramStakeholder(entity.StakeholderToId));

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}