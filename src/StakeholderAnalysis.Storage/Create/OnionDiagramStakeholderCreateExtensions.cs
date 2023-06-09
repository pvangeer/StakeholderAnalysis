using System;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class OnionDiagramStakeholderCreateExtensions
    {
        internal static OnionDiagramStakeholderXmlEntity Create(this PositionedStakeholder stakeholder,
            PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            if (registry.Contains(stakeholder)) return registry.GetAsOnionDiagramStakeholderXmlEntity(stakeholder);

            var entity = new OnionDiagramStakeholderXmlEntity
            {
                StakeholderReferenceId = stakeholder.Stakeholder.Create(registry).Id,
                Left = stakeholder.Left,
                Top = stakeholder.Top,
                Rank = stakeholder.Rank
            };

            registry.Register(stakeholder, entity);

            return entity;
        }
    }
}