using System;
using System.Collections.ObjectModel;
using System.IO;
using NUnit.Framework;
using StakeholderAnalysis.App;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Storage.Test
{
    [TestFixture]
    public class DatabaseReadWriteIntegrationTest
    {
        [Test]
        public void WriteAndReadProject()
        {
            var testProjectName = @"D:\Test\FirstProject.sqlite";

            if (File.Exists(testProjectName))
            {
                try
                {
                    File.Delete(testProjectName);
                }
                catch (Exception e)
                {
                    Assert.Fail("Unable to remove previous version of target file.");
                }
            }

            Analysis analysis = AnalysisGenerator.GetAnalysis();

            var storeAnalysis = new StorageSqLite();
            storeAnalysis.StageProject(analysis);
            storeAnalysis.SaveProjectAs(testProjectName);

            var retrievedProject = storeAnalysis.LoadProject(testProjectName);

            AssertEqualAnalysis(analysis, retrievedProject);
        }

        private void AssertEqualAnalysis(Analysis analysis, Analysis analysis2C)
        {
            AssertEqualStakeholders(analysis.Stakeholders, analysis2C.Stakeholders);
            AssertEqualForceFieldDiagrams(analysis.ForceFieldDiagrams, analysis2C.ForceFieldDiagrams);
            AssertEqualAttitudeImpactDiagrams(analysis.AttitudeImpactDiagrams, analysis2C.AttitudeImpactDiagrams);
        }

        private void AssertEqualForceFieldDiagrams(ObservableCollection<ForceFieldDiagram> forceFieldDiagrams, ObservableCollection<ForceFieldDiagram> twoCForceFieldDiagrams)
        {
            Assert.AreEqual(forceFieldDiagrams.Count, twoCForceFieldDiagrams.Count);
            for (int i = 0; i < forceFieldDiagrams.Count; i++)
            {
                var attitudeImpactDiagram = forceFieldDiagrams[i];
                var twoCAttitudeImpactDiagram = twoCForceFieldDiagrams[i];
                Assert.AreEqual(attitudeImpactDiagram.Name, twoCAttitudeImpactDiagram.Name);
                AssertAreEqualForceFieldDiagramStakeholders(attitudeImpactDiagram.Stakeholders, twoCAttitudeImpactDiagram.Stakeholders);
            }
        }

        private void AssertAreEqualForceFieldDiagramStakeholders(ObservableCollection<ForceFieldDiagramStakeholder> stakeholders, ObservableCollection<ForceFieldDiagramStakeholder> twoCStakeholders)
        {
            Assert.AreEqual(stakeholders.Count, twoCStakeholders.Count);
            for (int i = 0; i < stakeholders.Count; i++)
            {
                var stakeholder1 = stakeholders[i];
                var stakeholder2 = twoCStakeholders[i];
                Assert.AreEqual(stakeholder1.Influence, stakeholder2.Influence);
                Assert.AreEqual(stakeholder1.Interest, stakeholder2.Interest);
                AssertAreEqualStakeholders(stakeholder1.Stakeholder, stakeholder2.Stakeholder);
            }
        }

        private void AssertEqualAttitudeImpactDiagrams(ObservableCollection<AttitudeImpactDiagram> attitudeImpactDiagrams, ObservableCollection<AttitudeImpactDiagram> twoCAttitudeImpactDiagrams)
        {
            Assert.AreEqual(attitudeImpactDiagrams.Count, twoCAttitudeImpactDiagrams.Count);
            for (int i = 0; i < attitudeImpactDiagrams.Count; i++)
            {
                var attitudeImpactDiagram = attitudeImpactDiagrams[i];
                var twoCAttitudeImpactDiagram = twoCAttitudeImpactDiagrams[i];
                Assert.AreEqual(attitudeImpactDiagram.Name, twoCAttitudeImpactDiagram.Name);
                AssertAreEqualAttitudeImpactDiagramStakeholders(attitudeImpactDiagram.Stakeholders,twoCAttitudeImpactDiagram.Stakeholders);
            }
        }

        private void AssertAreEqualAttitudeImpactDiagramStakeholders(ObservableCollection<AttitudeImpactDiagramStakeholder> stakeholders, ObservableCollection<AttitudeImpactDiagramStakeholder> twoCStakeholders)
        {
            Assert.AreEqual(stakeholders.Count, twoCStakeholders.Count);
            for (int i = 0; i < stakeholders.Count; i++)
            {
                var stakeholder1 = stakeholders[i];
                var stakeholder2 = twoCStakeholders[i];
                Assert.AreEqual(stakeholder1.Attitude, stakeholder2.Attitude);
                Assert.AreEqual(stakeholder1.Impact, stakeholder2.Impact);
                AssertAreEqualStakeholders(stakeholder1.Stakeholder, stakeholder2.Stakeholder);
            }
        }

        private void AssertEqualStakeholders(ObservableCollection<Stakeholder> projectStakeholders, ObservableCollection<Stakeholder> project2CStakeholders)
        {
            Assert.AreEqual(projectStakeholders.Count, project2CStakeholders.Count);
            for (int i = 0; i < projectStakeholders.Count; i++)
            {
                var stakeholder1 = projectStakeholders[i];
                var stakeholder2 = project2CStakeholders[i];
                AssertAreEqualStakeholders(stakeholder1, stakeholder2);
            }
        }

        private static void AssertAreEqualStakeholders(Stakeholder stakeholder1, Stakeholder stakeholder2)
        {
            Assert.AreEqual(stakeholder1.Name, stakeholder2.Name);
            Assert.AreEqual(stakeholder1.Type, stakeholder2.Type);
        }
    }
}
