using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionRingCreateExtensions
    {
        internal static OnionRingXmlEntity Create(this OnionRing ring, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(ring)) return registry.Get(ring);

            var entity = new OnionRingXmlEntity
            {
                Percentage = ring.Percentage,
                BackgroundColor = ring.BackgroundColor.ToHexString(),
                StrokeColor = ring.StrokeColor.ToHexString(),
                StrokeThickness = ring.StrokeThickness,
                LineStyle = Convert.ToByte(ring.LineStyle)
            };

            registry.Register(ring, entity);

            return entity;
        }
    }
}