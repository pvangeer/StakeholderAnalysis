using System.Linq;

namespace StakeholderAnalysis.Data
{
    public static class AnalysisServices
    {
        public static void RemoveStakeholderFromAnalysis(Analysis analysis, Stakeholder stakeholder)
        {
            if (analysis.Stakeholders.Contains(stakeholder))
            {
                analysis.Stakeholders.Remove(stakeholder);
                foreach (var onionDiagram in analysis.OnionDiagrams)
                foreach (var onionDiagramStakeholder in onionDiagram.Stakeholders
                             .Where(s => s.Stakeholder == stakeholder)
                             .ToArray())
                {
                    onionDiagram.Stakeholders.Remove(onionDiagramStakeholder);
                    foreach (var stakeholderConnection in onionDiagram.Connections.Where(c =>
                                     c.ConnectFrom == onionDiagramStakeholder || c.ConnectTo == onionDiagramStakeholder)
                                 .ToArray())
                        onionDiagram.Connections.Remove(stakeholderConnection);
                }

                foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
                foreach (var forceFieldDiagramStakeholder in forceFieldDiagram.Stakeholders
                             .Where(s => s.Stakeholder == stakeholder).ToArray())
                    forceFieldDiagram.Stakeholders.Remove(forceFieldDiagramStakeholder);

                foreach (var attitudeImpactDiagram in analysis.AttitudeImpactDiagrams)
                foreach (var attitudeImpactDiagramStakeholder in attitudeImpactDiagram.Stakeholders
                             .Where(s => s.Stakeholder == stakeholder).ToArray())
                    attitudeImpactDiagram.Stakeholders.Remove(attitudeImpactDiagramStakeholder);
            }
        }
    }
}