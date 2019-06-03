using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderCreateExtensions
    {
        internal static StakeholderEntity Create(this Stakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(stakeholder))
            {
                return registry.Get(stakeholder);
            }

            var entity = new StakeholderEntity
            {
                Name = stakeholder.Name.DeepClone(),
                StakeholderTypeEntity = stakeholder.Type.Create(registry)
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}
