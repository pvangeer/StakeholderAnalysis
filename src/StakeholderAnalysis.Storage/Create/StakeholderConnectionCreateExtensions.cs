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
                StakeholderConnectionGroupId =
                    connection.StakeholderConnectionGroup.Create(registry).Id,
                StakeholderFromId = connection.ConnectFrom.Create(registry).Id,
                StakeholderToId = connection.ConnectTo.Create(registry).Id
            };

            registry.Register(connection, entity);

            return entity;
        }
    }
}