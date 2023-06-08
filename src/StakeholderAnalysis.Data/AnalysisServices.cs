using System;
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

        public static void AddStakeholderToDiagram(IStakeholderDiagram diagram, Stakeholder stakeholder)
        {
                if (diagram.Stakeholders.All(s => s.Stakeholder != stakeholder))
                {
                    var onionDiagramStakeholder = new PositionedStakeholder(stakeholder, 0.5, 0.5) { Rank = diagram.Stakeholders.Count };
                    FindPositionForNewOnionDiagramStakeholder(diagram, onionDiagramStakeholder);
                    
                    diagram.Stakeholders.Add(onionDiagramStakeholder);
                }
        }

        private static void FindPositionForNewOnionDiagramStakeholder(IStakeholderDiagram diagram, PositionedStakeholder stakeholder, int count = 1)
        {
            if (count > 200)
            {
                return;
            }

            count += 1;

            if (diagram.Stakeholders.Any(s =>
                    Math.Abs(s.Left - stakeholder.Left) < 1E-3 & Math.Abs(s.Top - stakeholder.Top) < 1E-3))
            {
                var newLeft = stakeholder.Left + 0.01;
                stakeholder.Left = newLeft > 1.0 ? newLeft - 1.0 : newLeft;

                var newTop = stakeholder.Top + 0.02;
                stakeholder.Top = newTop > 1.0 ? newTop - 1.0 : newTop;

                FindPositionForNewOnionDiagramStakeholder(diagram, stakeholder, count);
            }
        }
    }
}