using System;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AttitudeImpactDiagramCreateExtensions
    {
        internal static AttitudeImpactDiagramEntity Create(this AttitudeImpactDiagram diagram, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(diagram))
            {
                return registry.Get(diagram);
            }

            var entity = new AttitudeImpactDiagramEntity
            {
                Name = diagram.Name.DeepClone()
            };

            AddEntitiesForStakeholders(diagram, entity, registry);

            registry.Register(diagram,entity);

            return entity;
        }

        private static void AddEntitiesForStakeholders(AttitudeImpactDiagram diagram, AttitudeImpactDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Stakeholders.Count; index++)
            {
                var eventTreeEntity = diagram.Stakeholders[index].Create(registry);
                eventTreeEntity.Order = index;
                entity.AttitudeImpactDiagramStakeholderEntities.Add(eventTreeEntity);
            }
        }
    }
}
