using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class ForceFieldDiagramStakeholderEntityReadExtensions
    {
        internal static PositionedStakeholder Read(this ForceFieldDiagramStakeholderXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var forceFieldDiagramStakeholder = new PositionedStakeholder(
                collector.GetReferencedStakeholder(entity.StakeholderId),
                entity.Interest, 1.0 - entity.Influence)
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, forceFieldDiagramStakeholder);

            return forceFieldDiagramStakeholder;
        }
    }
}