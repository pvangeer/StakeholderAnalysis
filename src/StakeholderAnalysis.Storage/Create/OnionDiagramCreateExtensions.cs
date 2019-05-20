using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionDiagramCreateExtensions
    {
        internal static OnionDiagramEntity Create(this OnionDiagram diagram, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(diagram))
            {
                return registry.Get(diagram);
            }

            var entity = new OnionDiagramEntity
            {
                Name = diagram.Name.DeepClone(),
                Asymmetry = diagram.Asymmetry.ToNaNAsNull()
            };

            AddEntitiesForRings(diagram, entity, registry);
            AddEntitiesForStakeholders(diagram, entity, registry);
            AddEntitiesForStakeholderConnections(diagram, entity, registry);
            AddEntitiesForStakeholderConnectionGroups(diagram, entity, registry);

            registry.Register(diagram, entity);

            return entity;
        }

        private static void AddEntitiesForStakeholders(OnionDiagram diagram, OnionDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Stakeholders.Count; index++)
            {
                var stakeholder = diagram.Stakeholders[index].Create(registry);
                stakeholder.Order = index;
                entity.OnionDiagramStakeholderEntities.Add(stakeholder);
            }
        }

        private static void AddEntitiesForRings(OnionDiagram diagram, OnionDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.OnionRings.Count; index++)
            {
                var stakeholder = diagram.OnionRings[index].Create(registry);
                stakeholder.Order = index;
                entity.OnionRingEntities.Add(stakeholder);
            }
        }

        private static void AddEntitiesForStakeholderConnections(OnionDiagram diagram, OnionDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Connections.Count; index++)
            {
                var stakeholder = diagram.Connections[index].Create(registry);
                stakeholder.Order = index;
                entity.StakeholderConnectionEntities.Add(stakeholder);
            }
        }

        private static void AddEntitiesForStakeholderConnectionGroups(OnionDiagram diagram, OnionDiagramEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.ConnectionGroups.Count; index++)
            {
                var stakeholder = diagram.ConnectionGroups[index].Create(registry);
                stakeholder.Order = index;
                entity.StakeholderConnectionGroupEntities.Add(stakeholder);
            }
        }
    }
}
