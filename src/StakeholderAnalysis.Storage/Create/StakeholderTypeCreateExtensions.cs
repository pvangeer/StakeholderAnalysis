using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderTypeCreateExtensions
    {
        internal static StakeholderTypeEntity Create(this StakeholderType stakeholderType, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(stakeholderType))
            {
                return registry.Get(stakeholderType);
            }

            var entity = new StakeholderTypeEntity
            {
                Name = stakeholderType.Name.DeepClone(),
                Color = stakeholderType.Color.ToHexString(),
                Icontype = Convert.ToByte(stakeholderType.IconType)
            };

            registry.Register(stakeholderType, entity);

            return entity;
        }
    }
}
