using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionDiagramStakeholderCreateExtensions
    {
        internal static OnionDiagramStakeholderXmlEntity Create(this OnionDiagramStakeholder stakeholder,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.Get(stakeholder);

            var entity = new OnionDiagramStakeholderXmlEntity
            {
                StakeholderId = stakeholder.Stakeholder.Create(registry).Id,
                Left = stakeholder.Left,
                Top = stakeholder.Top,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}