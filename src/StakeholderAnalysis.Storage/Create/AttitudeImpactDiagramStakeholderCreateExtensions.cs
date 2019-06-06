using System;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AttitudeImpactDiagramStakeholderCreateExtensions
    {
        internal static AttitudeImpactDiagramStakeholderEntity Create(this AttitudeImpactDiagramStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(stakeholder))
            {
                return registry.Get(stakeholder);
            }

            var entity = new AttitudeImpactDiagramStakeholderEntity
            {
                StakeholderEntity = stakeholder.Stakeholder.Create(registry),
                Attitude = stakeholder.Attitude.ToNaNAsNull(),
                Impact = stakeholder.Impact.ToNaNAsNull(),
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}
