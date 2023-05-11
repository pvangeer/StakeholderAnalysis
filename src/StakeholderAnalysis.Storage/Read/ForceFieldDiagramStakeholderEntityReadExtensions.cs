using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class ForceFieldDiagramStakeholderEntityReadExtensions
    {
        internal static ForceFieldDiagramStakeholder Read(this ForceFieldDiagramStakeholderXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var forceFieldDiagramStakeholder = new ForceFieldDiagramStakeholder(
                collector.GetReferencedStakeholder(entity.StakeholderId),
                entity.Interest, entity.Influence)
            {
                Rank = (int)entity.Rank
            };

            collector.Collect(entity, forceFieldDiagramStakeholder);

            return forceFieldDiagramStakeholder;
        }
    }
}