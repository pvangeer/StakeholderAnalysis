using System.IO;
using System.Reflection;
using System.Windows.Media;
using NUnit.Framework;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.Migration;

namespace StakeholderAnalysis.Storage.Test
{
    [TestFixture]
    public class ReadBackwardCompatibilityTest
    {
        [Test]
        public void Read231Test()
        {
            var binFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Assert.IsNotNull(binFolder);
            var assemblyFolder = binFolder.Replace("bin\\x64\\", "")
                .Replace("Debug", "")
                .Replace("Release", "");
            
            var filename = Path.Combine(assemblyFolder, "Resources", "23.1-testproject.xml");

            Assert.IsTrue(XmlStorageMigrationService.NeedsMigration(filename));

            var oldFilName = filename;
            filename = Path.Combine(binFolder, Path.GetFileNameWithoutExtension(oldFilName) + "_testmigrated.xml");
            XmlStorageMigrationService.MigrateFile(oldFilName, filename);

            var storageXml = new StorageXml();
            var projectData = storageXml.LoadProject(filename);

            AssertCorrectProjectInformation(projectData);
        }

        private void AssertCorrectProjectInformation(ProjectData projectData)
        {
            Assert.AreEqual("Pieter van Geer (geer)", projectData.Author);
            Assert.AreEqual("2023-06-09 : 09:46:53", projectData.Created);

            var analysis = projectData.Analysis;
            Assert.IsNotNull(analysis);
            Assert.AreEqual(8, analysis.Stakeholders.Count);
            AssertEqualStakeholder(analysis.Stakeholders[0], "linksboven", "Eerste type");
            AssertEqualStakeholder(analysis.Stakeholders[1], "Linksonder", "Eerste type");
            AssertEqualStakeholder(analysis.Stakeholders[2], "Rechtsboven", "Eerste type");
            AssertEqualStakeholder(analysis.Stakeholders[3], "Rechtsonder", "Eerste type");
            AssertEqualStakeholder(analysis.Stakeholders[4], "Vaag contact", "Tweede type");
            AssertEqualStakeholder(analysis.Stakeholders[5], "Goed contact", "Tweede type");
            AssertEqualStakeholder(analysis.Stakeholders[6], "Leuk contact", "Tweede type");
            AssertEqualStakeholder(analysis.Stakeholders[7], "Minder leuk contact", "Tweede type");

            Assert.AreEqual(2, analysis.StakeholderTypes.Count);
            AsserEqualStakeholderType(analysis.StakeholderTypes[0], "Eerste type", StakeholderIconType.Wifi, ColorFromHex("#FFFFF8DC"));
            AsserEqualStakeholderType(analysis.StakeholderTypes[1], "Tweede type", StakeholderIconType.Mail, ColorFromHex("#FF00B389"));

            Assert.AreEqual(1, analysis.OnionDiagrams.Count);
            var onionDiagram = analysis.OnionDiagrams[0];
            Assert.AreEqual("Test ui-diagram", onionDiagram.Name);
            Assert.AreEqual(116.4, onionDiagram.Orientation);
            Assert.AreEqual(0.7, onionDiagram.Asymmetry);

            Assert.AreEqual(8, onionDiagram.Stakeholders.Count);
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[0], 0.14847620882296284, 0.15434985968194578, 0, "linksboven");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[1], 0.11, 0.81, 1, "Linksonder");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[2], 0.85, 0.14, 2, "Rechtsboven");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[3], 0.87, 0.82, 3, "Rechtsonder");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[4], 0.14, 0.39, 4, "Vaag contact");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[5], 0.64, 0.66, 5, "Goed contact");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[6], 0.57, 0.52, 6, "Leuk contact");
            AssertEqualPositionedStakeholder(onionDiagram.Stakeholders[7], 0.63, 0.43, 7, "Minder leuk contact");

            Assert.AreEqual(2, onionDiagram.ConnectionGroups.Count);
            AssertEqualConnectionGroup(onionDiagram.ConnectionGroups[0], "Test groep 1", ColorFromHex("#FF00B389"), true, 3.0,
                LineStyle.SmallDash);
            AssertEqualConnectionGroup(onionDiagram.ConnectionGroups[1], "Test groep 2", ColorFromHex("#FFFFD814"), true, 2.0,
                LineStyle.DashDotDot);

            Assert.AreEqual(7, onionDiagram.Connections.Count);
            AssertEqualConnection(onionDiagram.Connections[0], "Test groep 1", "Goed contact", "Leuk contact");
            AssertEqualConnection(onionDiagram.Connections[1], "Test groep 1", "Goed contact", "Minder leuk contact");
            AssertEqualConnection(onionDiagram.Connections[2], "Test groep 1", "Minder leuk contact", "Rechtsboven");
            AssertEqualConnection(onionDiagram.Connections[3], "Test groep 2", "Minder leuk contact", "linksboven");
            AssertEqualConnection(onionDiagram.Connections[4], "Test groep 2", "Vaag contact", "Linksonder");
            AssertEqualConnection(onionDiagram.Connections[5], "Test groep 2", "Linksonder", "Leuk contact");
            AssertEqualConnection(onionDiagram.Connections[6], "Test groep 2", "Rechtsonder", "Goed contact");

            Assert.AreEqual(3, onionDiagram.OnionRings.Count);
            AssertEqualOnionRings(onionDiagram.OnionRings[0], 0.44, ColorFromHex("#FFF2F2F2"), ColorFromHex("#FF080C80"), 2.0,
                LineStyle.Dash);
            AssertEqualOnionRings(onionDiagram.OnionRings[1], 0.75, ColorFromHex("#FF0D38E0"), ColorFromHex("#FF808080"), 0.0,
                LineStyle.Solid);
            AssertEqualOnionRings(onionDiagram.OnionRings[2], 1.0, ColorFromHex("#FF0EBBF0"), ColorFromHex("#FF808080"), 1.0,
                LineStyle.Solid);

            Assert.AreEqual(1, analysis.AttitudeImpactDiagrams.Count);
            var attitudeImpactDiagram = analysis.AttitudeImpactDiagrams[0];
            AssertEqualTwoAxisDiagramProperties(attitudeImpactDiagram, "Test houding-impact diagram",
                ColorFromHex("#FF0EBBF0"), ColorFromHex("#FF00CC96"),
                "Informeren 1", "Monitoren 1", "Betrekken 1", "Overtuigen 1", "Calibri", ColorFromHex("#FFE6E6E6"), false, false, 66,
                "Negatief 1", "Positief 1", "Lage impact 1", "Hoge impact 1", "Arial", ColorFromHex("#FFE6E6E6"), true, true, 22);
            Assert.AreEqual(8, attitudeImpactDiagram.Stakeholders.Count);
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[0], 0.5, 0.5, 0, "linksboven");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[1], 0.51, 1 - 0.48, 1, "Linksonder");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[2], 0.52, 1 - 0.45999999999999996, 2, "Rechtsboven");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[3], 0.53, 1 - 0.43999999999999995, 3, "Rechtsonder");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[4], 0.54, 1 - 0.41999999999999993, 4, "Vaag contact");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[5], 0.55, 1 - 0.39999999999999991, 5, "Goed contact");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[6], 0.56, 1 - 0.37999999999999989, 6, "Leuk contact");
            AssertEqualPositionedStakeholder(attitudeImpactDiagram.Stakeholders[7], 0.57000000000000006, 1 - 0.35999999999999988, 7,
                "Minder leuk contact");

            Assert.AreEqual(1, analysis.ForceFieldDiagrams.Count);
            var forceFieldDiagram = analysis.ForceFieldDiagrams[0];
            AssertEqualTwoAxisDiagramProperties(forceFieldDiagram, "Test krachtenvelddiagram",
                ColorFromHex("#FFE6E6E6"), ColorFromHex("#FF0EBBF0"),
                "Consulteren 1", "Monitoren 1", "Betrekken 1", "Informeren 1", "Arial", ColorFromHex("#FFE6E6E6"), false, false, 62,
                "Weinig invloed 1", "Veel invloed 1", "Klein belang 1", "Groot belang 1", "Arial", ColorFromHex("#FFE6E6E6"), true, true,
                22);
            Assert.AreEqual(8, forceFieldDiagram.Stakeholders.Count);
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[0], 0.5, 0.5, 0, "linksboven");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[1], 0.51, 1 - 0.48, 1, "Linksonder");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[2], 0.52, 1 - 0.45999999999999996, 2, "Rechtsboven");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[3], 0.53, 1 - 0.43999999999999995, 3, "Rechtsonder");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[4], 0.54, 1 - 0.41999999999999993, 4, "Vaag contact");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[5], 0.55, 1 - 0.39999999999999991, 5, "Goed contact");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[6], 0.56, 1 - 0.37999999999999989, 6, "Leuk contact");
            AssertEqualPositionedStakeholder(forceFieldDiagram.Stakeholders[7], 0.57000000000000006, 1 - 0.35999999999999988, 7,
                "Minder leuk contact");
        }

        private void AssertEqualTwoAxisDiagramProperties(TwoAxisDiagram diagram, string expectedName, Color expectedBrushStartColor,
            Color expectedBrushEndColor,
            string expectedBackgroundLeftTop, string expectedBackgroundLeftBottom, string expectedBackgroundRightTop,
            string expectedBackgroundRightBottom, string expectedBackgroundFontFamily, Color expectedBackgroundFontColor,
            bool expectedBackgroundFontBold, bool expectedBackgroundFontItalic, int expectedBackgroundFontSize,
            string expectedAxisYMin, string expectedAxisYMax, string expectedAxisXMin, string expectedAxisXMax,
            string expectedAxisFontFamily, Color expectedAxisFontColor, bool expectedAxisFontBold, bool expectedAxisFontItalic,
            int expectedAxisFontSize)
        {
            Assert.AreEqual(expectedName, diagram.Name);

            Assert.AreEqual(expectedBrushStartColor, diagram.BrushStartColor);
            Assert.AreEqual(expectedBrushEndColor, diagram.BrushEndColor);

            Assert.AreEqual(expectedBackgroundLeftBottom, diagram.BackgroundTextLeftBottom);
            Assert.AreEqual(expectedBackgroundLeftTop, diagram.BackgroundTextLeftTop);
            Assert.AreEqual(expectedBackgroundRightBottom, diagram.BackgroundTextRightBottom);
            Assert.AreEqual(expectedBackgroundRightTop, diagram.BackgroundTextRightTop);
            Assert.AreEqual(expectedBackgroundFontFamily, diagram.BackgroundFontFamily.Source);
            Assert.AreEqual(expectedBackgroundFontColor, diagram.BackgroundFontColor);
            Assert.AreEqual(expectedBackgroundFontBold, diagram.BackgroundFontBold);
            Assert.AreEqual(expectedBackgroundFontItalic, diagram.BackgroundFontItalic);
            Assert.AreEqual(expectedBackgroundFontSize, diagram.BackgroundFontSize, 1E-8);

            Assert.AreEqual(expectedAxisXMin, diagram.XAxisMinLabel);
            Assert.AreEqual(expectedAxisXMax, diagram.XAxisMaxLabel);
            Assert.AreEqual(expectedAxisYMin, diagram.YAxisMinLabel);
            Assert.AreEqual(expectedAxisYMax, diagram.YAxisMaxLabel);
            Assert.AreEqual(expectedAxisFontFamily, diagram.AxisFontFamily.Source);
            Assert.AreEqual(expectedAxisFontColor, diagram.AxisFontColor);
            Assert.AreEqual(expectedAxisFontBold, diagram.AxisFontBold);
            Assert.AreEqual(expectedAxisFontItalic, diagram.AxisFontItalic);
            Assert.AreEqual(expectedAxisFontSize, diagram.AxisFontSize, 1E-8);
        }

        private void AssertEqualOnionRings(OnionRing onionRing, double expectedPercentage, Color expectedBackgroundColor,
            Color expectedStrokeColor, double expectedStrokeThickness, LineStyle expectedLineStyle)
        {
            Assert.AreEqual(expectedPercentage, onionRing.Percentage, 1E-8);
            Assert.AreEqual(expectedBackgroundColor, onionRing.BackgroundColor);
            Assert.AreEqual(expectedStrokeColor, onionRing.StrokeColor);
            Assert.AreEqual(expectedStrokeThickness, onionRing.StrokeThickness, 1E-8);
            Assert.AreEqual(expectedLineStyle, onionRing.LineStyle);
        }

        private void AssertEqualConnection(StakeholderConnection connection, string expectedGroupName, string expectedFromName,
            string expectedToName)
        {
            Assert.AreEqual(expectedGroupName, connection.StakeholderConnectionGroup.Name);
            Assert.AreEqual(expectedFromName, connection.ConnectFrom.Stakeholder.Name);
            Assert.AreEqual(expectedToName, connection.ConnectTo.Stakeholder.Name);
        }

        private void AssertEqualConnectionGroup(StakeholderConnectionGroup connectionGroup, string expectedName, Color expectedStrokeColor,
            bool expectedVisible, double expectedStrokeThickness, LineStyle expectedLineStyle)
        {
            Assert.AreEqual(expectedName, connectionGroup.Name);
            Assert.AreEqual(expectedStrokeColor, connectionGroup.StrokeColor);
            Assert.AreEqual(expectedLineStyle, connectionGroup.LineStyle);
            Assert.AreEqual(expectedStrokeThickness, connectionGroup.StrokeThickness, 1E-8);
            Assert.AreEqual(expectedVisible, connectionGroup.Visible);
        }

        private static Color ColorFromHex(string hexString)
        {
            return (Color)ColorConverter.ConvertFromString(hexString);
        }

        private void AssertEqualPositionedStakeholder(PositionedStakeholder stakeholder, double expectedLeft, double expectedTop,
            int expectedRank, string expectedStakeholderName)
        {
            Assert.AreEqual(expectedLeft, stakeholder.Left, 1E-8);
            Assert.AreEqual(expectedTop, stakeholder.Top,1E-8);
            Assert.AreEqual(expectedRank, stakeholder.Rank);
            Assert.AreEqual(expectedStakeholderName, stakeholder.Stakeholder.Name);
        }

        private void AsserEqualStakeholderType(StakeholderType stakeholderType, string expectedName, StakeholderIconType expectedIcon,
            Color expectedColor)
        {
            Assert.AreEqual(expectedName, stakeholderType.Name);
            Assert.AreEqual(expectedIcon, stakeholderType.IconType);
            Assert.AreEqual(expectedColor, stakeholderType.Color);
        }

        private void AssertEqualStakeholder(Stakeholder stakeholder, string expectedName, string expectedTypeName)
        {
            Assert.AreEqual(expectedName, stakeholder.Name);
            Assert.AreEqual(expectedTypeName, stakeholder.Type.Name);
        }
    }
}