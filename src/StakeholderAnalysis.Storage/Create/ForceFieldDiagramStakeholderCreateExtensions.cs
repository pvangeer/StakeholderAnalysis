using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class ForceFieldDiagramStakeholderCreateExtensions
    {
        internal static ForceFieldDiagramStakeholderEntity Create(this ForceFieldDiagramStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(stakeholder))
            {
                return registry.Get(stakeholder);
            }

            var entity = new ForceFieldDiagramStakeholderEntity
            {
                StakeholderEntity = stakeholder.Stakeholder.Create(registry),
                Influence = stakeholder.Influence.ToNaNAsNull(),
                Interest = stakeholder.Interest.ToNaNAsNull()
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}
