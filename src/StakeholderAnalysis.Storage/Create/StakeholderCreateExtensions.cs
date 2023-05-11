using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderCreateExtensions
    {
        internal static StakeholderXmlEntity Create(this Stakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.Get(stakeholder);

            var entity = new StakeholderXmlEntity
            {
                Name = stakeholder.Name.DeepClone(),
                StakeholderTypeReference = stakeholder.Type.CreateReference(registry)
            };

            registry.Register(stakeholder, entity);

            return entity;
        }

        internal static StakeholderReferenceXmlEntity CreateReference(this Stakeholder stakeholder,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            var entity = new StakeholderXmlEntity
            {
                Name = stakeholder.Name.DeepClone(),
                StakeholderTypeReference = stakeholder.Type.CreateReference(registry)
            };
            if (registry.Contains(stakeholder))
                entity = registry.Get(stakeholder);
            else
                registry.Register(stakeholder, entity);

            return new StakeholderReferenceXmlEntity(entity);
        }
    }
}