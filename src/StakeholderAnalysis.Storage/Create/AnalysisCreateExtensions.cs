using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AnalysisCreateExtensions
    {
        internal static AnalysisXmlEntity Create(this Analysis analysis, PersistenceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));

            var entity = new AnalysisXmlEntity();

            AddEntitiesForStakeholderTypes(analysis, entity, registry);
            AddEntitiesForStakeholders(analysis, entity, registry);
            AddEntitiesForOnionDiagrams(analysis, entity, registry);
            AddEntitiesForForceDiagrams(analysis, entity, registry);
            AddEntitiesForAttitudeImpactDiagrams(analysis, entity, registry);

            return entity;
        }

        private static void AddEntitiesForStakeholderTypes(Analysis analysis, AnalysisXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.StakeholderTypes.Count; index++)
            {
                var stakeholderTypeEntity = analysis.StakeholderTypes[index].Create(registry);
                stakeholderTypeEntity.Order = index;
                entity.StakeholderTypeXmlEntities.Add(stakeholderTypeEntity);
            }
        }

        private static void AddEntitiesForAttitudeImpactDiagrams(Analysis analysis, AnalysisXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.AttitudeImpactDiagrams.Count; index++)
            {
                var attitudeImpactDiagramEntity = analysis.AttitudeImpactDiagrams[index].Create(registry);
                attitudeImpactDiagramEntity.Order = index;
                entity.AttitudeImpactDiagramXmlEntities.Add(attitudeImpactDiagramEntity);
            }
        }

        private static void AddEntitiesForForceDiagrams(Analysis analysis, AnalysisXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.ForceFieldDiagrams.Count; index++)
            {
                var forceFieldDiagramEntity = analysis.ForceFieldDiagrams[index].Create(registry);
                forceFieldDiagramEntity.Order = index;
                entity.ForceFieldDiagramXmlEntities.Add(forceFieldDiagramEntity);
            }
        }

        private static void AddEntitiesForOnionDiagrams(Analysis analysis, AnalysisXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.OnionDiagrams.Count; index++)
            {
                var onionDiagramXmlEntity = analysis.OnionDiagrams[index].Create(registry);
                onionDiagramXmlEntity.Order = index;
                entity.OnionDiagramXmlEntities.Add(onionDiagramXmlEntity);
            }
        }

        private static void AddEntitiesForStakeholders(Analysis analysis, AnalysisXmlEntity entity,
            PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.Stakeholders.Count; index++)
            {
                var stakeholderEntity = analysis.Stakeholders[index].Create(registry);
                stakeholderEntity.Order = index;
                entity.StakeholderXmlEntities.Add(stakeholderEntity);
            }
        }
    }
}