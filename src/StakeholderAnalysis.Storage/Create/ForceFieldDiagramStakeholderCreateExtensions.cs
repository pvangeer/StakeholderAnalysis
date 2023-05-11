using System;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class ForceFieldDiagramStakeholderCreateExtensions
    {
        internal static ForceFieldDiagramStakeholderXmlEntity Create(this ForceFieldDiagramStakeholder stakeholder,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.Get(stakeholder);

            var entity = new ForceFieldDiagramStakeholderXmlEntity
            {
                StakeholderReferenceEntity = stakeholder.Stakeholder.CreateReference(registry),
                Influence = stakeholder.Influence,
                Interest = stakeholder.Interest,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}