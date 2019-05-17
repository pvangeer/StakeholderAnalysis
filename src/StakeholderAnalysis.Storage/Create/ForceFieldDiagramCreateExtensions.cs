using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class ForceFieldDiagramCreateExtensions
    {
        internal static ForceFieldDiagramEntity Create(this ForceFieldDiagram diagram, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(diagram))
            {
                return registry.Get(diagram);
            }

            var entity = new ForceFieldDiagramEntity()
            {
                Name = diagram.Name.DeepClone()
            };

            AddEntitiesForStakeholders(diagram, entity, registry);

            registry.Register(diagram, entity);

            return entity;
        }

        private static void AddEntitiesForStakeholders(ForceFieldDiagram diagram, ForceFieldDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Stakeholders.Count; index++)
            {
                var stakeholder = diagram.Stakeholders[index].Create(registry);
                stakeholder.Order = index;
                entity.ForceFieldDiagramStakeholderEntities.Add(stakeholder);
            }
        }
    }
}
