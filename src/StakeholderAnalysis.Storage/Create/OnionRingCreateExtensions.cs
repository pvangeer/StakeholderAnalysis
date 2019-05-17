using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionRingCreateExtensions
    {
        internal static OnionRingEntity Create(this OnionRing ring, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (registry.Contains(ring))
            {
                return registry.Get(ring);
            }

            var entity = new OnionRingEntity
            {
                Percentage = ring.Percentage.ToNaNAsNull(),
                BackgroundColor = ring.BackgroundColor.ToHexString(),
                StrokeColor = ring.StrokeColor.ToHexString(),
                StrokeThickness = ring.StrokeThickness.ToNaNAsNull()
            };

            registry.Register(ring, entity);

            return entity;
        }
    }
}
