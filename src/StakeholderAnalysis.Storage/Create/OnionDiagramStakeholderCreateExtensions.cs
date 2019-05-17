using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionDiagramStakeholderCreateExtensions
    {
        internal static OnionDiagramStakeholderEntity Create(this OnionDiagramStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(stakeholder))
            {
                return registry.Get(stakeholder);
            }

            var entity = new OnionDiagramStakeholderEntity
            {
                StakeholderEntity = stakeholder.Stakeholder.Create(registry),
                Left = stakeholder.Left.ToNaNAsNull(),
                Top = stakeholder.Top.ToNaNAsNull()
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}
