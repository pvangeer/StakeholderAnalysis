using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderConnectionCreateExtensions
    {
        internal static StakeholderConnectionXmlEntity Create(this StakeholderConnection connection,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(connection)) return registry.Get(connection);

            var entity = new StakeholderConnectionXmlEntity
            {
                StakeholderConnectionGroupReferenceXmlEntity =
                    connection.StakeholderConnectionGroup.CreateReference(registry),
                StakeholderFrom = connection.ConnectFrom.CreateReference(registry),
                StakeholderTo = connection.ConnectTo.CreateReference(registry)
            };

            registry.Register(connection, entity);

            return entity;
        }
    }
}