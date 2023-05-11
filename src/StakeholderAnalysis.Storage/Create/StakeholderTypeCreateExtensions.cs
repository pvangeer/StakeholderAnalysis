using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderTypeCreateExtensions
    {
        internal static StakeholderTypeXmlEntity Create(this StakeholderType stakeholderType,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholderType)) return registry.Get(stakeholderType);

            var entity = new StakeholderTypeXmlEntity
            {
                Name = stakeholderType.Name.DeepClone(),
                Color = stakeholderType.Color.ToHexString(),
                IconType = Convert.ToByte(stakeholderType.IconType)
            };

            registry.Register(stakeholderType, entity);

            return entity;
        }

        internal static StakeholderTypeReferenceXmlEntity CreateReference(this StakeholderType stakeholderType,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            var entity = new StakeholderTypeXmlEntity
            {
                Name = stakeholderType.Name.DeepClone(),
                Color = stakeholderType.Color.ToHexString(),
                IconType = Convert.ToByte(stakeholderType.IconType)
            };
            if (registry.Contains(stakeholderType))
                entity = registry.Get(stakeholderType);
            else
                registry.Register(stakeholderType, entity);

            return new StakeholderTypeReferenceXmlEntity(entity);
        }
    }
}