using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderEntityReadExtensions
    {
        internal static Stakeholder Read(this StakeholderXmlEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholderTypeReferenceXmlEntity = entity.StakeholderTypeId;
            var referencedStakeholderType = collector.GetReferencedStakeholderType(stakeholderTypeReferenceXmlEntity);

            var stakeholder = new Stakeholder
            {
                Name = entity.Name,
                Type = referencedStakeholderType,
                Email = entity.Email,
                TelephoneNumber = entity.TelephoneNumber,
                Notes = entity.Notes
            };

            collector.Collect(entity, stakeholder);

            return stakeholder;
        }
    }
}