using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class ForceFieldDiagramStakeholderCreateExtensions
    {
        internal static ForceFieldDiagramStakeholderXmlEntity Create(this PositionedStakeholder stakeholder,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.Get(stakeholder);

            var entity = new ForceFieldDiagramStakeholderXmlEntity
            {
                StakeholderId = stakeholder.Stakeholder.Create(registry).Id,
                Influence = 1.0 - stakeholder.Top,
                Interest = stakeholder.Left,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}