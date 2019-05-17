using System;
using System.Collections.ObjectModel;
using System.IO;
using NUnit.Framework;
using StakeholderAnalysis.App;
using StakeholderAnalysis.Data;

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
        }

        private void AssertEqualStakeholders(ObservableCollection<Stakeholder> projectStakeholders, ObservableCollection<Stakeholder> project2CStakeholders)
        {
            Assert.AreEqual(projectStakeholders.Count, project2CStakeholders.Count);
            for (int i = 0; i < projectStakeholders.Count; i++)
            {
                var stakeholder1 = projectStakeholders[i];
                var stakeholder2 = project2CStakeholders[i];
                Assert.AreEqual(stakeholder1.Name, stakeholder2.Name);
                Assert.AreEqual(stakeholder1.Type, stakeholder2.Type);
            }
        }
    }
}
