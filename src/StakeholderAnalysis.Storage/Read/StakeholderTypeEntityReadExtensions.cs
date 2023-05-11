using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderTypeEntityReadExtensions
    {
        internal static StakeholderType Read(this StakeholderTypeEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholderType = new StakeholderType
            {
                Name = entity.Name,
                Color = entity.Color.ToColor(),
                IconType = (StakeholderIconType)entity.Icontype
            };

            collector.Collect(entity, stakeholderType);

            return stakeholderType;
        }
    }
}