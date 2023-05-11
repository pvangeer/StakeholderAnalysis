using System;
using System.Linq;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    public static class OnionDiagramEntityReadExtensions
    {
        internal static OnionDiagram Read(this OnionDiagramEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholders = entity.OnionDiagramStakeholderEntities.OrderBy(e => e.Order)
                .Select(e => e.Read(collector));
            var rings = entity.OnionRingEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var connections = entity.StakeholderConnectionEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var connectionGroups = entity.StakeholderConnectionGroupEntities.OrderBy(e => e.Order)
                .Select(e => e.Read(collector));

            var attitudeImpactDiagram = new OnionDiagram
            {
                Name = entity.Name,
                Asymmetry = entity.Asymmetry.ToNullAsNaN()
            };

            foreach (var stakeholder in stakeholders) attitudeImpactDiagram.Stakeholders.Add(stakeholder);
            foreach (var ring in rings) attitudeImpactDiagram.OnionRings.Add(ring);
            foreach (var connection in connections) attitudeImpactDiagram.Connections.Add(connection);
            foreach (var connectionGroup in connectionGroups)
                attitudeImpactDiagram.ConnectionGroups.Add(connectionGroup);

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}