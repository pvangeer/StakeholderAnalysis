using System;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AnalysisEntityReadExtensions
    {
        internal static Analysis Read(this AnalysisXmlEntity entity, ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            var stakeholderTypes =
                entity.StakeholderTypeXmlEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var stakeholders = entity.StakeholderXmlEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var onionDiagrams = entity.OnionDiagramXmlEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var forceFieldDiagrams =
                entity.ForceFieldDiagramXmlEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));
            var attitudeImpactDiagrams = entity.AttitudeImpactDiagramXmlEntities.OrderBy(e => e.Order)
                .Select(e => e.Read(collector));

            var analysis = AnalysisFactory.CreateEmptyAnalysis();

            foreach (var stakeholderType in stakeholderTypes) analysis.StakeholderTypes.Add(stakeholderType);

            foreach (var stakeholder in stakeholders) analysis.Stakeholders.Add(stakeholder);

            foreach (var onionDiagram in onionDiagrams) analysis.OnionDiagrams.Add(onionDiagram);

            foreach (var forceFieldDiagram in forceFieldDiagrams) analysis.ForceFieldDiagrams.Add(forceFieldDiagram);

            foreach (var attitudeImpactDiagram in attitudeImpactDiagrams)
                analysis.AttitudeImpactDiagrams.Add(attitudeImpactDiagram);

            return analysis;
        }
    }
}