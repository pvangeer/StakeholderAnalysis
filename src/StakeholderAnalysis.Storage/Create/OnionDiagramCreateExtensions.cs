using System;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionDiagramCreateExtensions
    {
        internal static OnionDiagramXmlEntity Create(this OnionDiagram diagram, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(diagram)) return registry.Get(diagram);

            var entity = new OnionDiagramXmlEntity
            {
                Name = diagram.Name.DeepClone(),
                Asymmetry = diagram.Asymmetry,
                Orientation = diagram.Orientation
            };

            AddEntitiesForRings(diagram, entity, registry);
            AddEntitiesForStakeholders(diagram, entity, registry);
            AddEntitiesForStakeholderConnections(diagram, entity, registry);
            AddEntitiesForStakeholderConnectionGroups(diagram, entity, registry);

            registry.Register(diagram, entity);

            return entity;
        }

        private static void AddEntitiesForStakeholders(OnionDiagram diagram, OnionDiagramXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Stakeholders.Count; index++)
            {
                var stakeholder = OnionDiagramStakeholderCreateExtensions.Create(diagram.Stakeholders[index], registry);
                stakeholder.Order = index;
                entity.OnionDiagramStakeholderXmlEntities.Add(stakeholder);
            }
        }

        private static void AddEntitiesForRings(OnionDiagram diagram, OnionDiagramXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.OnionRings.Count; index++)
            {
                var onionRingXmlEntity = diagram.OnionRings[index].Create(registry);
                onionRingXmlEntity.Order = index;
                entity.OnionRingXmlEntities.Add(onionRingXmlEntity);
            }
        }

        private static void AddEntitiesForStakeholderConnections(OnionDiagram diagram, OnionDiagramXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.Connections.Count; index++)
            {
                var connectionXmlEntity = diagram.Connections[index].Create(registry);
                connectionXmlEntity.Order = index;
                entity.StakeholderConnectionXmlEntities.Add(connectionXmlEntity);
            }
        }

        private static void AddEntitiesForStakeholderConnectionGroups(OnionDiagram diagram,
            OnionDiagramXmlEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < diagram.ConnectionGroups.Count; index++)
            {
                var connectionGroupXmlEntity = diagram.ConnectionGroups[index].Create(registry);
                connectionGroupXmlEntity.Order = index;
                entity.StakeholderConnectionGroupXmlEntities.Add(connectionGroupXmlEntity);
            }
        }
    }
}