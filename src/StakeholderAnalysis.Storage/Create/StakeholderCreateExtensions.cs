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

            var entity = new StakeholderEntity
            {
                Name = stakeholder.Name.DeepClone(),
                Type = Convert.ToByte(stakeholder.Type)
            };

            return entity;
        }
    }
}
