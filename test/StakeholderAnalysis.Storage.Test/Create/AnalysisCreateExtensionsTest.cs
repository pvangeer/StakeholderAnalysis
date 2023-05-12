using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using NUnit.Framework;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.Create;

namespace StakeholderAnalysis.Storage.Test.Create
{
    [TestFixture]
    public class AnalysisCreateExtensionsTest
    {
        [Test]
        public void CreateAnalysisXmlEntityWithStakeholderTypes()
        {
            var color1 = Colors.Aqua;
            var color2 = Colors.Bisque;
            var color3 = Colors.DarkMagenta;
            var icon1 = StakeholderIconType.Coffee;
            var icon2 = StakeholderIconType.Angel;
            var icon3 = StakeholderIconType.GroupTable;
            var name1 = "Test 1";
            var name2 = "Test 2";
            var name3 = "Test 3";
            var analysis = new Analysis
            {
                StakeholderTypes =
                {
                    new StakeholderType { Color = color1, IconType = icon1, Name = name1 },
                    new StakeholderType { Color = color2, IconType = icon2, Name = name2 },
                    new StakeholderType { Color = color3, IconType = icon3, Name = name3 }
                }
            };

            var registry = new PersistenceRegistry();
            var xmlEntity = analysis.Create(registry);

            Assert.IsNotNull(xmlEntity);
            Assert.AreEqual(analysis.StakeholderTypes.Count, xmlEntity.StakeholderTypeXmlEntities.Count);
            for (var index = 0; index < xmlEntity.StakeholderTypeXmlEntities.Count; index++)
            {
                var stakeholderTypeXmlEntity = xmlEntity.StakeholderTypeXmlEntities[index];
                var stakeholderType = analysis.StakeholderTypes[index];

                Assert.AreEqual(stakeholderType.Color.ToHexString(), stakeholderTypeXmlEntity.Color);
                Assert.AreEqual(Convert.ToByte(stakeholderType.IconType), stakeholderTypeXmlEntity.IconType);
                Assert.AreEqual(stakeholderType.Name, stakeholderTypeXmlEntity.Name);

                Assert.IsTrue(registry.Contains(stakeholderType));
                Assert.AreEqual(stakeholderTypeXmlEntity, registry.Get(stakeholderType));
            }
        }

        [Test]
        public void CreateAnalysisXmlEntityWithStakeholders()
        {
            var stakeholderType = new StakeholderType
            {
                Color = Colors.Aqua,
                IconType = StakeholderIconType.Coffee,
                Name = "Test 1"
            };
            var analysis = new Analysis
            {
                StakeholderTypes =
                {
                    stakeholderType
                },
                Stakeholders =
                {
                    new Stakeholder("Stakeholder 1", stakeholderType),
                    new Stakeholder("Stakeholder 2", stakeholderType),
                    new Stakeholder("Stakeholder 3", stakeholderType)
                }
            };

            var registry = new PersistenceRegistry();
            var xmlEntity = analysis.Create(registry);

            Assert.IsNotNull(xmlEntity);
            var firstStakeholderType = xmlEntity.StakeholderTypeXmlEntities.FirstOrDefault();
            Assert.IsNotNull(firstStakeholderType);

            Assert.AreEqual(analysis.Stakeholders.Count, xmlEntity.StakeholderXmlEntities.Count);
            for (var index = 0; index < xmlEntity.StakeholderXmlEntities.Count; index++)
            {
                var stakeholderXmlEntity = xmlEntity.StakeholderXmlEntities[index];
                var stakeholder = analysis.Stakeholders[index];

                Assert.AreEqual(index, stakeholderXmlEntity.Order);
                Assert.AreEqual(stakeholder.Name, stakeholderXmlEntity.Name);
                Assert.AreEqual(firstStakeholderType.Id, stakeholderXmlEntity.StakeholderTypeId);

                Assert.IsTrue(registry.Contains(stakeholder));
                Assert.AreEqual(stakeholderXmlEntity, registry.Get(stakeholder));
            }
        }

        [Test]
        public void CreateAnalysisXmlEntityWithForceFieldDiagramNoStakeholders()
        {
            var testText = "Test1";
            var testColor = Colors.DarkSalmon;
            var analysis = new Analysis
            {
                AttitudeImpactDiagrams =
                {
                    new AttitudeImpactDiagram("Diagram 1"),
                    new AttitudeImpactDiagram("Diagram 2")
                    {
                        BrushStartColor = testColor,
                        BrushEndColor = testColor,
                        BackgroundTextLeftTop = testText,
                        BackgroundTextLeftBottom = testText,
                        BackgroundTextRightTop = testText,
                        BackgroundTextRightBottom = testText,
                        BackgroundFontColor = testColor,
                        BackgroundFontSize = 6,
                        BackgroundFontBold = false,
                        BackgroundFontItalic = false,
                        YAxisMaxLabel = testText,
                        YAxisMinLabel = testText,
                        XAxisMaxLabel = testText,
                        XAxisMinLabel = testText,
                        AxisFontColor = testColor,
                        AxisFontSize = 4,
                        AxisFontBold = true,
                        AxisFontItalic = true
                    }
                }
            };

            var registry = new PersistenceRegistry();
            var xmlEntity = analysis.Create(registry);

            Assert.IsNotNull(xmlEntity);
            Assert.AreEqual(analysis.AttitudeImpactDiagrams.Count, xmlEntity.AttitudeImpactDiagramXmlEntities.Count);
            for (var index = 0; index < xmlEntity.AttitudeImpactDiagramXmlEntities.Count; index++)
            {
                var diagramXmlEntity = xmlEntity.AttitudeImpactDiagramXmlEntities[index];
                var diagram = analysis.AttitudeImpactDiagrams[index];

                Assert.AreEqual(index, diagramXmlEntity.Order);
                Assert.AreEqual(diagram.Name, diagramXmlEntity.Name);

                Assert.AreEqual(diagram.BrushStartColor.ToHexString(), diagramXmlEntity.Brush.BrushStartColor);
                Assert.AreEqual(diagram.BrushEndColor.ToHexString(), diagramXmlEntity.Brush.BrushEndColor);
                Assert.AreEqual(diagram.BackgroundTextLeftTop, diagramXmlEntity.Background.BackgroundTextLeftTop);
                Assert.AreEqual(diagram.BackgroundTextLeftBottom, diagramXmlEntity.Background.BackgroundTextLeftBottom);
                Assert.AreEqual(diagram.BackgroundTextRightTop, diagramXmlEntity.Background.BackgroundTextRightTop);
                Assert.AreEqual(diagram.BackgroundTextRightBottom,
                    diagramXmlEntity.Background.BackgroundTextRightBottom);
                Assert.AreEqual(diagram.BackgroundFontColor.ToHexString(),
                    diagramXmlEntity.Background.BackgroundTextFontColor);
                Assert.AreEqual(diagram.BackgroundFontSize, diagramXmlEntity.Background.BackgroundTextFontSize);
                Assert.AreEqual(diagram.BackgroundFontBold ? (byte)1 : (byte)0,
                    diagramXmlEntity.Background.BackgroundTextFontBold);
                Assert.AreEqual(diagram.BackgroundFontItalic ? (byte)1 : (byte)0,
                    diagramXmlEntity.Background.BackgroundTextFontItalic);
                Assert.AreEqual(diagram.YAxisMaxLabel, diagramXmlEntity.Axis.YAxisMaxLabel);
                Assert.AreEqual(diagram.YAxisMinLabel, diagramXmlEntity.Axis.YAxisMinLabel);
                Assert.AreEqual(diagram.XAxisMaxLabel, diagramXmlEntity.Axis.XAxisMaxLabel);
                Assert.AreEqual(diagram.XAxisMinLabel, diagramXmlEntity.Axis.XAxisMinLabel);
                Assert.AreEqual(diagram.AxisFontColor.ToHexString(), diagramXmlEntity.Axis.AxisTextFontColor);
                Assert.AreEqual(diagram.AxisFontSize, diagramXmlEntity.Axis.AxisTextFontSize);
                Assert.AreEqual(diagram.AxisFontBold ? (byte)1 : (byte)0, diagramXmlEntity.Axis.AxisTextFontBold);
                Assert.AreEqual(diagram.AxisFontItalic ? (byte)1 : (byte)0, diagramXmlEntity.Axis.AxisTextFontItalic);
                // TODO: Test Font family converter.

                Assert.IsTrue(registry.Contains(diagram));
                Assert.AreEqual(diagramXmlEntity, registry.Get(diagram));
            }
        }

        [Test]
        public void CreateAnalysisXmlEntityWithAttitudeImpactDiagramWithStakeholders()
        {
            var stakeholderType = new StakeholderType
            {
                Color = Colors.Aqua,
                IconType = StakeholderIconType.Coffee,
                Name = "Test 1"
            };

            var stakeholder1 = new Stakeholder("Stakeholder 1", stakeholderType);
            var stakeholder2 = new Stakeholder("Stakeholder 2", stakeholderType);
            var stakeholder3 = new Stakeholder("Stakeholder 3", stakeholderType);

            var analysis = new Analysis
            {
                StakeholderTypes =
                {
                    stakeholderType
                },
                Stakeholders =
                {
                    stakeholder1,
                    stakeholder2,
                    stakeholder3
                },
                AttitudeImpactDiagrams =
                {
                    new AttitudeImpactDiagram("Diagram 1")
                    {
                        Stakeholders =
                        {
                            new AttitudeImpactDiagramStakeholder(stakeholder1, 0.15, 0.5),
                            new AttitudeImpactDiagramStakeholder(stakeholder3, 0.25, 0.6)
                        }
                    }
                }
            };

            var registry = new PersistenceRegistry();
            var xmlEntity = analysis.Create(registry);

            Assert.IsNotNull(xmlEntity);
            var firstDiagram = analysis.AttitudeImpactDiagrams.FirstOrDefault();
            Assert.IsNotNull(firstDiagram);
            var firstXmlEntity = xmlEntity.AttitudeImpactDiagramXmlEntities.FirstOrDefault();
            Assert.IsNotNull(firstXmlEntity);

            Assert.AreEqual(firstDiagram.Stakeholders.Count,
                firstXmlEntity.AttitudeImpactDiagramStakeholderXmlEntities.Count);
            for (var index = 0; index < firstXmlEntity.AttitudeImpactDiagramStakeholderXmlEntities.Count; index++)
            {
                var xmlDiagramStakeholder = firstXmlEntity.AttitudeImpactDiagramStakeholderXmlEntities[index];
                var diagramStakeholder = firstDiagram.Stakeholders[index];

                Assert.AreEqual(diagramStakeholder.Attitude, xmlDiagramStakeholder.Attitude);
                Assert.AreEqual(diagramStakeholder.Impact, xmlDiagramStakeholder.Impact);
                Assert.AreEqual(diagramStakeholder.Rank, xmlDiagramStakeholder.Rank);
                Assert.AreEqual(index, xmlDiagramStakeholder.Order);

                Assert.IsTrue(registry.Contains(diagramStakeholder.Stakeholder));
                var xmlStakeholder = registry.Get(diagramStakeholder.Stakeholder);
                Assert.AreEqual(xmlStakeholder.Id, xmlDiagramStakeholder.StakeholderReferenceId);
            }
        }

        [Test]
        public void CreateAnalysisXmlEntityWithForceFieldDiagramWithStakeholders()
        {
            var stakeholderType = new StakeholderType
            {
                Color = Colors.Aqua,
                IconType = StakeholderIconType.Coffee,
                Name = "Test 1"
            };

            var stakeholder1 = new Stakeholder("Stakeholder 1", stakeholderType);
            var stakeholder2 = new Stakeholder("Stakeholder 2", stakeholderType);
            var stakeholder3 = new Stakeholder("Stakeholder 3", stakeholderType);

            var analysis = new Analysis
            {
                StakeholderTypes =
                {
                    stakeholderType
                },
                Stakeholders =
                {
                    stakeholder1,
                    stakeholder2,
                    stakeholder3
                },
                ForceFieldDiagrams =
                {
                    new ForceFieldDiagram("Diagram 1")
                    {
                        Stakeholders =
                        {
                            new ForceFieldDiagramStakeholder(stakeholder1, 0.15, 0.5),
                            new ForceFieldDiagramStakeholder(stakeholder3, 0.25, 0.6)
                        }
                    }
                }
            };

            var registry = new PersistenceRegistry();
            var xmlEntity = analysis.Create(registry);

            Assert.IsNotNull(xmlEntity);
            var firstDiagram = analysis.ForceFieldDiagrams.FirstOrDefault();
            Assert.IsNotNull(firstDiagram);
            var firstXmlEntity = xmlEntity.ForceFieldDiagramXmlEntities.FirstOrDefault();
            Assert.IsNotNull(firstXmlEntity);

            Assert.AreEqual(firstDiagram.Stakeholders.Count,
                firstXmlEntity.ForceFieldDiagramStakeholderXmlEntities.Count);
            for (var index = 0; index < firstXmlEntity.ForceFieldDiagramStakeholderXmlEntities.Count; index++)
            {
                var xmlDiagramStakeholder = firstXmlEntity.ForceFieldDiagramStakeholderXmlEntities[index];
                var diagramStakeholder = firstDiagram.Stakeholders[index];

                Assert.AreEqual(diagramStakeholder.Interest, xmlDiagramStakeholder.Interest);
                Assert.AreEqual(diagramStakeholder.Influence, xmlDiagramStakeholder.Influence);
                Assert.AreEqual(diagramStakeholder.Rank, xmlDiagramStakeholder.Rank);
                Assert.AreEqual(index, xmlDiagramStakeholder.Order);

                Assert.IsTrue(registry.Contains(diagramStakeholder.Stakeholder));
                var xmlStakeholder = registry.Get(diagramStakeholder.Stakeholder);
                Assert.AreEqual(xmlStakeholder.Id, xmlDiagramStakeholder.StakeholderId);
            }
        }

        [Test]
        public void CreateAnalysisXmlEntityWithFilledOnionDiagram()
        {
            var stakeholderType = new StakeholderType
            {
                Color = Colors.Aqua,
                IconType = StakeholderIconType.Coffee,
                Name = "Test 1"
            };
            var connectionGroup = new StakeholderConnectionGroup("Test", Colors.Cornsilk, 2.3, false);

            var stakeholder1 = new Stakeholder("Stakeholder 1", stakeholderType);
            var stakeholder2 = new Stakeholder("Stakeholder 2", stakeholderType);
            var stakeholder3 = new Stakeholder("Stakeholder 3", stakeholderType);
            var diagramStakeholder1 = new OnionDiagramStakeholder(stakeholder1, 0.2, 0.4);
            var diagramStakeholder2 = new OnionDiagramStakeholder(stakeholder2, 0.7, 0.2);
            var diagramStakeholder3 = new OnionDiagramStakeholder(stakeholder3, 0.3, 0.6);
            var onionDiagram = new OnionDiagram("Beoordelen", new ObservableCollection<OnionRing>
            {
                new OnionRing() { BackgroundColor = Colors.LightBlue },
                new OnionRing(0.65) { BackgroundColor = Colors.CornflowerBlue },
                new OnionRing(0.3) { BackgroundColor = Colors.DarkSlateBlue }
            })
            {
                Asymmetry = 0.7,
                Stakeholders =
                {
                    diagramStakeholder1,
                    diagramStakeholder2,
                    diagramStakeholder3
                },
                ConnectionGroups = { connectionGroup },
                Connections = { new StakeholderConnection(connectionGroup, diagramStakeholder1, diagramStakeholder3) }
            };

            var analysis = new Analysis
            {
                StakeholderTypes =
                {
                    stakeholderType
                },
                Stakeholders =
                {
                    stakeholder1,
                    stakeholder2,
                    stakeholder3
                },
                OnionDiagrams = { onionDiagram }
            };

            var registry = new PersistenceRegistry();
            var xmlEntity = analysis.Create(registry);

            Assert.IsNotNull(xmlEntity);
            var firstDiagram = analysis.OnionDiagrams.FirstOrDefault();
            Assert.IsNotNull(firstDiagram);
            var firstXmlEntity = xmlEntity.OnionDiagramXmlEntities.FirstOrDefault();
            Assert.IsNotNull(firstXmlEntity);

            Assert.AreEqual(firstDiagram.Stakeholders.Count, firstXmlEntity.OnionDiagramStakeholderXmlEntities.Count);
            for (var index = 0; index < firstXmlEntity.OnionDiagramStakeholderXmlEntities.Count; index++)
            {
                var xmlDiagramStakeholder = firstXmlEntity.OnionDiagramStakeholderXmlEntities[index];
                var diagramStakeholder = firstDiagram.Stakeholders[index];

                Assert.AreEqual(diagramStakeholder.Left, xmlDiagramStakeholder.Left);
                Assert.AreEqual(diagramStakeholder.Top, xmlDiagramStakeholder.Top);
                Assert.AreEqual(diagramStakeholder.Rank, xmlDiagramStakeholder.Rank);
                Assert.AreEqual(index, xmlDiagramStakeholder.Order);

                Assert.IsTrue(registry.Contains(diagramStakeholder.Stakeholder));
                var xmlStakeholder = registry.Get(diagramStakeholder.Stakeholder);
                Assert.AreEqual(xmlStakeholder.Id, xmlDiagramStakeholder.StakeholderId);
            }

            Assert.AreEqual(firstDiagram.OnionRings.Count, firstXmlEntity.OnionRingXmlEntities.Count);
            for (var index = 0; index < firstXmlEntity.OnionRingXmlEntities.Count; index++)
            {
                var onionRingXmlEntity = firstXmlEntity.OnionRingXmlEntities[index];
                var diagramOnionRing = firstDiagram.OnionRings[index];

                Assert.AreEqual(diagramOnionRing.BackgroundColor.ToHexString(), onionRingXmlEntity.BackgroundColor);
                Assert.AreEqual(diagramOnionRing.Percentage, onionRingXmlEntity.Percentage);
                Assert.AreEqual(diagramOnionRing.StrokeColor.ToHexString(), onionRingXmlEntity.StrokeColor);
                Assert.AreEqual(diagramOnionRing.StrokeThickness, onionRingXmlEntity.StrokeThickness);
                Assert.AreEqual(index, onionRingXmlEntity.Order);

                Assert.IsTrue(registry.Contains(diagramOnionRing));
            }

            Assert.AreEqual(firstDiagram.ConnectionGroups.Count,
                firstXmlEntity.StakeholderConnectionGroupXmlEntities.Count);
            for (var index = 0; index < firstXmlEntity.StakeholderConnectionGroupXmlEntities.Count; index++)
            {
                var connectionGroupXmlEntity = firstXmlEntity.StakeholderConnectionGroupXmlEntities[index];
                var diagramConnectionGroup = firstDiagram.ConnectionGroups[index];

                Assert.AreEqual(diagramConnectionGroup.StrokeColor.ToHexString(), connectionGroupXmlEntity.Color);
                Assert.AreEqual(diagramConnectionGroup.Name, connectionGroupXmlEntity.Name);
                Assert.AreEqual(diagramConnectionGroup.StrokeThickness, connectionGroupXmlEntity.StrokeThickness);
                Assert.AreEqual(diagramConnectionGroup.Visible ? (byte)1 : (byte)0, connectionGroupXmlEntity.Visible);
                Assert.AreEqual(index, connectionGroupXmlEntity.Order);

                Assert.IsTrue(registry.Contains(diagramConnectionGroup));
            }

            Assert.AreEqual(firstDiagram.Connections.Count, firstXmlEntity.StakeholderConnectionXmlEntities.Count);
            for (var index = 0; index < firstXmlEntity.StakeholderConnectionXmlEntities.Count; index++)
            {
                var connectionXmlEntity = firstXmlEntity.StakeholderConnectionXmlEntities[index];
                var diagramConnection = firstDiagram.Connections[index];

                Assert.AreEqual(index, connectionXmlEntity.Order);

                Assert.IsTrue(registry.Contains(diagramConnection.StakeholderConnectionGroup));
                Assert.AreEqual(registry.Get(diagramConnection.StakeholderConnectionGroup).Id,
                    connectionXmlEntity.StakeholderConnectionGroupId);

                Assert.IsTrue(registry.Contains(diagramConnection.ConnectFrom));
                Assert.AreEqual(registry.Get(diagramConnection.ConnectFrom).Id, connectionXmlEntity.StakeholderFromId);

                Assert.IsTrue(registry.Contains(diagramConnection.ConnectTo));
                Assert.AreEqual(registry.Get(diagramConnection.ConnectTo).Id, connectionXmlEntity.StakeholderToId);

                Assert.IsTrue(registry.Contains(diagramConnection));
            }
        }
    }
}