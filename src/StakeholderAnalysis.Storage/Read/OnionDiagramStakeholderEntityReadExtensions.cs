using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class OnionDiagramStakeholderEntityReadExtensions
    {
        internal static OnionDiagramStakeholder Read(this OnionDiagramStakeholderXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var attitudeImpactDiagram = new OnionDiagramStakeholder(
                collector.GetReferencedStakeholder(entity.StakeholderId),
                entity.Left, entity.Top)
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}