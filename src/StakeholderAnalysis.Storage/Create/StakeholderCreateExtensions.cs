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
                StakeholderTypeId = stakeholder.Type.Create(registry).Id
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}