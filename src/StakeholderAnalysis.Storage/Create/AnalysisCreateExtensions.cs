using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AnalysisCreateExtensions
    {
        internal static AnalysisEntity Create(this Analysis analysis, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            var entity = new AnalysisEntity();

            AddEntitiesForStakeholders(analysis, entity, registry);
            AddEntitiesForOnionDiagrams(analysis, entity, registry);
            AddEntitiesForForceDiagrams(analysis, entity, registry);
            AddEntitiesForAttitudeImpactDiagrams(analysis, entity, registry);

            return entity;
        }

        private static void AddEntitiesForAttitudeImpactDiagrams(Analysis analysis, AnalysisEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.AttitudeImpactDiagrams.Count; index++)
            {
                var attitudeImpactDiagramEntity = analysis.AttitudeImpactDiagrams[index].Create(registry);
                attitudeImpactDiagramEntity.Order = index;
                entity.AttitudeImpactDiagramEntities.Add(attitudeImpactDiagramEntity);
            }
        }

        private static void AddEntitiesForForceDiagrams(Analysis analysis, AnalysisEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.ForceFieldDiagrams.Count; index++)
            {
                var forceFieldDiagramEntity = analysis.ForceFieldDiagrams[index].Create(registry);
                forceFieldDiagramEntity.Order = index;
                entity.ForceFieldDiagramEntities.Add(forceFieldDiagramEntity);
            }
        }

        private static void AddEntitiesForOnionDiagrams(Analysis analysis, AnalysisEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.OnionDiagrams.Count; index++)
            {
                var onionDiagramEntity = analysis.OnionDiagrams[index].Create(registry);
                onionDiagramEntity.Order = index;
                entity.OnionDiagramEntities.Add(onionDiagramEntity);
            }
        }

        private static void AddEntitiesForStakeholders(Analysis analysis, AnalysisEntity entity, PersistenceRegistry registry)
        {
            for (var index = 0; index < analysis.Stakeholders.Count; index++)
            {
                var stakeholderEntity = analysis.Stakeholders[index].Create(registry);
                stakeholderEntity.Order = index;
                entity.StakeholderEntities.Add(stakeholderEntity);
            }
        }
    }
}
