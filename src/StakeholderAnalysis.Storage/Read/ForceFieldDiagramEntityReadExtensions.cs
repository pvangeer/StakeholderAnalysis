using System;
using System.Linq;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class ForceFieldDiagramEntityReadExtensions
    {
        internal static ForceFieldDiagram Read(this ForceFieldDiagramEntity entity, ReadConversionCollector collector)
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

            var stakeholders = entity.ForceFieldDiagramStakeholderEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));

            var forceFieldDiagram = new ForceFieldDiagram
            {
                Name = entity.Name
            };

            foreach (var stakeholder in stakeholders)
            {
                forceFieldDiagram.Stakeholders.Add(stakeholder);
            }

            collector.Collect(entity, forceFieldDiagram);

            return forceFieldDiagram;
        }
    }
}
