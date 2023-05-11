using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderEntityReadExtensions
    {
        internal static Stakeholder Read(this StakeholderEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholder = new Stakeholder
            {
                Name = entity.Name,
                Type = entity.StakeholderTypeEntity.Read(collector)
            };

            collector.Collect(entity, stakeholder);

            return stakeholder;
        }
    }
}