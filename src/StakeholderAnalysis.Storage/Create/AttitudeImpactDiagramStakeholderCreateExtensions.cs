using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AttitudeImpactDiagramStakeholderCreateExtensions
    {
        internal static AttitudeImpactDiagramStakeholderXmlEntity Create(
            this PositionedStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.GetAsAttitudeImpactDiagramStakeholderXmlEntity(stakeholder);

            var entity = new AttitudeImpactDiagramStakeholderXmlEntity
            {
                StakeholderReferenceId = stakeholder.Stakeholder.Create(registry).Id,
                Attitude = 1.0 - stakeholder.Top,
                Impact = stakeholder.Left,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}