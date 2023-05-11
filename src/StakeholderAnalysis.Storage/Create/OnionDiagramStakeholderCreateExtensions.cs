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
                StakeholderReferenceEntity = stakeholder.Stakeholder.CreateReference(registry),
                Left = stakeholder.Left,
                Top = stakeholder.Top,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }

        internal static OnionDiagramStakeholderReferenceXmlEntity CreateReference(
            this OnionDiagramStakeholder stakeholder, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            var entity = new OnionDiagramStakeholderXmlEntity
            {
                StakeholderReferenceEntity = stakeholder.Stakeholder.CreateReference(registry),
                Left = stakeholder.Left,
                Top = stakeholder.Top,
                Rank = stakeholder.Rank
            };
            if (registry.Contains(stakeholder))
                entity = registry.Get(stakeholder);
            else
                registry.Register(stakeholder, entity);

            return new OnionDiagramStakeholderReferenceXmlEntity(entity);
        }
    }
}