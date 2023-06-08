using System;
using System.Linq;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;

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
            switch (diagram)
            {
                case OnionDiagram onionDiagram:
                    AddStakeholderToDiagram(onionDiagram, stakeholder);
                    break;
                case ForceFieldDiagram forceFieldDiagram:
                    AddStakeholderToDiagram(forceFieldDiagram, stakeholder);
                    break;
                case AttitudeImpactDiagram attitudeImpactDiagram:
                    AddStakeholderToDiagram(attitudeImpactDiagram, stakeholder);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #region AddStakeholdersToDiagrams
        // TODO: Introduce interface to avoid multiple implementations of the same thing.

        private static void AddStakeholderToDiagram(OnionDiagram diagram, Stakeholder stakeholder)
        {
                if (diagram.Stakeholders.All(s => s.Stakeholder != stakeholder))
                {
                    var onionDiagramStakeholder = new PositionedStakeholder(stakeholder, 0.5, 0.5) { Rank = diagram.Stakeholders.Count };
                    FindPositionForNewOnionDiagramStakeholder(diagram, onionDiagramStakeholder);
                    
                    diagram.Stakeholders.Add(onionDiagramStakeholder);
                }
        }

        private static void FindPositionForNewOnionDiagramStakeholder(OnionDiagram diagram, PositionedStakeholder stakeholder, int count = 1)
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

        private static void AddStakeholderToDiagram(ForceFieldDiagram diagram, Stakeholder stakeholder)
        {
            if (diagram.Stakeholders.All(s => s.Stakeholder != stakeholder))
            {
                var diagramStakeholder = new PositionedStakeholder(stakeholder, 0.5, 0.5) { Rank = diagram.Stakeholders.Count };
                FindPositionForNewStakeholder(diagram, diagramStakeholder);
                diagram.Stakeholders.Add(diagramStakeholder);
            }
        }

        private static void FindPositionForNewStakeholder(ForceFieldDiagram diagram, PositionedStakeholder stakeholder, int count = 1)
        {
            if (count > 200)
            {
                return;
            }

            count += 1;

            if (diagram.Stakeholders.Any(s =>
                    Math.Abs(s.Top - stakeholder.Top) < 1E-3 & Math.Abs(s.Left - stakeholder.Left) < 1E-3))
            {
                var newTop = stakeholder.Top + 0.02;
                stakeholder.Top = newTop > 1.0 ? newTop - 1.0 : newTop;
                
                var newLeft = stakeholder.Left + 0.01;
                stakeholder.Left = newLeft > 1.0 ? newLeft - 1.0 : newLeft;

                FindPositionForNewStakeholder(diagram, stakeholder, count);
            }
        }

        private static void AddStakeholderToDiagram(AttitudeImpactDiagram diagram, Stakeholder stakeholder)
        {
            if (diagram.Stakeholders.All(s => s.Stakeholder != stakeholder))
            {
                var attitudeImpactDiagramStakeholder = new PositionedStakeholder(stakeholder, 0.5, 0.5) { Rank = diagram.Stakeholders.Count };
                FindPositionForNewStakeholder(diagram, attitudeImpactDiagramStakeholder);
                diagram.Stakeholders.Add(attitudeImpactDiagramStakeholder);
            }
        }

        private static void FindPositionForNewStakeholder(AttitudeImpactDiagram diagram, PositionedStakeholder stakeholder, int count = 1)
        {
            if (count > 200)
            {
                return;
            }

            count += 1;

            if (diagram.Stakeholders.Any(s =>
                    Math.Abs(s.Top - stakeholder.Top) < 1E-3 & Math.Abs(s.Left - stakeholder.Left) < 1E-3))
            {
                var newTop = stakeholder.Top - 0.02;
                stakeholder.Top = newTop < 0.0 ? newTop + 1.0 : newTop;
                var newLeft = stakeholder.Left + 0.01;
                stakeholder.Left = newLeft > 1.0 ? newLeft - 1.0 : newLeft;
                FindPositionForNewStakeholder(diagram, stakeholder, count);
            }
        }

        #endregion
    }
}