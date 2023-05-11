using System;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AttitudeImpactDiagramStakeholderCreateExtensions
    {
        internal static AttitudeImpactDiagramStakeholderXmlEntity Create(
            this AttitudeImpactDiagramStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.Get(stakeholder);

            var entity = new AttitudeImpactDiagramStakeholderXmlEntity
            {
                StakeholderReferenceId = stakeholder.Stakeholder.Create(registry).Id,
                Attitude = stakeholder.Attitude,
                Impact = stakeholder.Impact,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}