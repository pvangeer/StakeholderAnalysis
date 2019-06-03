using System;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AnalysisEntityReadExtensions
    {
        internal static Analysis Read(this AnalysisEntity entity, ReadConversionCollector collector)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }

            var stakeholderTypes = entity.StakeholderTypeEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var stakeholders = entity.StakeholderEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var onionDiagrams = entity.OnionDiagramEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var forceFieldDiagrams = entity.ForceFieldDiagramEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var attitudeImpactDiagrams = entity.AttitudeImpactDiagramEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));

            var analysis = new Analysis();
            foreach (var stakeholderType in stakeholderTypes)
            {
                analysis.StakeholderTypes.Add(stakeholderType);
            }

            foreach (var stakeholder in stakeholders)
            {
                analysis.Stakeholders.Add(stakeholder);
            }

            foreach (var onionDiagram in onionDiagrams)
            {
                analysis.OnionDiagrams.Add(onionDiagram);
            }

            foreach (var forceFieldDiagram in forceFieldDiagrams)
            {
                analysis.ForceFieldDiagrams.Add(forceFieldDiagram);
            }

            foreach (var attitudeImpactDiagram in attitudeImpactDiagrams)
            {
                analysis.AttitudeImpactDiagrams.Add(attitudeImpactDiagram);
            }

            return analysis;
        }
    }
}
