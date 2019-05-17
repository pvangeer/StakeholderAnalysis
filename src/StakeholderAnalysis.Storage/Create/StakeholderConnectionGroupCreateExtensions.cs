using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class StakeholderConnectionGroupCreateExtensions
    {
        internal static StakeholderConnectionGroupEntity Create(this StakeholderConnectionGroup group, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(group))
            {
                return registry.Get(group);
            }

            var entity = new StakeholderConnectionGroupEntity
            {
                Name = group.Name.DeepClone(),
                Color = group.Color.ToHexString(),
                Visible = group.Visible ? (byte)1 : (byte)0,
                StrokeThickness = group.StrokeThickness.ToNaNAsNull()
            };

            registry.Register(@group, entity);

            return entity;
        }
    }
}
