using System;
using System.Linq;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    public static class OnionDiagramEntityReadExtensions
    {
        internal static OnionDiagram Read(this OnionDiagramXmlEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var rings = entity.OnionRingXmlEntities
                .OrderBy(e => e.Order)
                .Select(e => e.Read(collector))
                .ToList();
            var stakeholders = entity.PositionedStakeholderXmlEntities
                .OrderBy(e => e.Order)
                .Select(e => e.Read(collector))
                .ToList();
            var connectionGroups = entity.StakeholderConnectionGroupXmlEntities
                .OrderBy(e => e.Order)
                .Select(e => e.Read(collector))
                .ToList();
            var connections = entity.StakeholderConnectionXmlEntities
                .OrderBy(e => e.Order)
                .Select(e => e.Read(collector))
                .ToList();

            var onionDiagram = new OnionDiagram
            {
                Name = entity.Name,
                Asymmetry = entity.Asymmetry,
                Orientation = entity.Orientation
            };

            foreach (var stakeholder in stakeholders) onionDiagram.Stakeholders.Add(stakeholder);
            foreach (var ring in rings) onionDiagram.OnionRings.Add(ring);
            foreach (var connection in connections) onionDiagram.Connections.Add(connection);
            foreach (var connectionGroup in connectionGroups)
                onionDiagram.ConnectionGroups.Add(connectionGroup);

            collector.Collect(entity, onionDiagram);

            return onionDiagram;
        }
    }
}