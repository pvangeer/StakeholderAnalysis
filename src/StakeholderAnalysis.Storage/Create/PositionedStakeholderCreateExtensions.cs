using System;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class PositionedStakeholderCreateExtensions
    {
        internal static PositionedStakeholderXmlEntity Create(
            this PositionedStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.Get(stakeholder);

            var entity = new PositionedStakeholderXmlEntity
            {
                StakeholderReferenceId = stakeholder.Stakeholder.Create(registry).Id,
                Top = stakeholder.Top,
                Left = stakeholder.Left,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}