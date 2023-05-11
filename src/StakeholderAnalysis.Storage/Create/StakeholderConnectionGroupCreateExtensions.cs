using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderConnectionGroupCreateExtensions
    {
        internal static StakeholderConnectionGroupXmlEntity Create(this StakeholderConnectionGroup group,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(group)) return registry.Get(group);

            var entity = new StakeholderConnectionGroupXmlEntity
            {
                Name = group.Name.DeepClone(),
                Color = group.StrokeColor.ToHexString(),
                Visible = group.Visible ? (byte)1 : (byte)0,
                StrokeThickness = group.StrokeThickness
            };

            registry.Register(group, entity);

            return entity;
        }

        internal static StakeholderConnectionGroupReferenceXmlEntity CreateReference(
            this StakeholderConnectionGroup group, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            var entity = new StakeholderConnectionGroupXmlEntity
            {
                Name = group.Name.DeepClone(),
                Color = group.StrokeColor.ToHexString(),
                Visible = group.Visible ? (byte)1 : (byte)0,
                StrokeThickness = group.StrokeThickness
            };
            if (registry.Contains(group))
                entity = registry.Get(group);
            else
                registry.Register(group, entity);

            return new StakeholderConnectionGroupReferenceXmlEntity(entity);
        }
    }
}