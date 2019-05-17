using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderConnectionCreateExtensions
    {
        internal static StakeholderConnectionEntity Create(this StakeholderConnection connection, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(connection))
            {
                return registry.Get(connection);
            }

            var entity = new StakeholderConnectionEntity
            {
                StakeholderConnectionGroupEntity = connection.StakeholderConnectionGroup.Create(registry),
                OnionDiagramStakeholderEntity1 = connection.ConnectFrom.Create(registry),
                OnionDiagramStakeholderEntity = connection.ConnectTo.Create(registry)
            };

            registry.Register(connection, entity);

            return entity;
        }
    }
}
