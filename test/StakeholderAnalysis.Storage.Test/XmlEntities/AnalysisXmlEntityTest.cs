using StakeholderAnalysis.Storage.XmlEntities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using NUnit.Framework;

namespace StakeholderAnalysis.Storage.Test.XmlEntities
{
    public class AnalysisXmlEntityTest
    {
        [Test]
        public void AnalysisSerializationTest()
        {
            var stakeholderType2XmlEntity = new StakeholderTypeXmlEntity
            {
                Id = 2,
                Color = Colors.BlanchedAlmond.ToHexString(),
                IconType = 14,
                Name = "test 2",
                Order = 2
            };
            var analysisXmlEntity = new AnalysisXmlEntity
            {
                StakeholderTypeXmlEntities = new Collection<StakeholderTypeXmlEntity>
                {
                    new StakeholderTypeXmlEntity
                    {
                        Id = 1,
                        Color = Colors.Aquamarine.ToHexString(),
                        IconType = 14,
                        Name = "test 1",
                        Order = 1
                    },
                    stakeholderType2XmlEntity
                },
                StakeholderXmlEntities = new Collection<StakeholderXmlEntity>
                {
                    new StakeholderXmlEntity{Name = "Stakeholder 1", Order = 1, Id = 1, StakeholderTypeId = stakeholderType2XmlEntity.Id}
                },
                AttitudeImpactDiagramXmlEntities = new Collection<AttitudeImpactDiagramXmlEntity>
                {
                    new AttitudeImpactDiagramXmlEntity
                    {
                        Id = 30,
                    },
                    new AttitudeImpactDiagramXmlEntity
                    {
                        Id = 31,
                    },
                    new AttitudeImpactDiagramXmlEntity
                    {
                        Id = 32,
                    }
                }
            };

            Stream stream = new MemoryStream();

            var serializer = new XmlSerializer(typeof(AnalysisXmlEntity));
            serializer.Serialize(stream, analysisXmlEntity);

            serializer.Serialize(Console.Out, analysisXmlEntity);

            Assert.AreNotEqual(0, stream.Length);
        }

        /*
        [Test]
        public void DeserializeAnalysisXmlEntity()
        {
            var xmlText =
                "<?xml version=\"1.0\" encoding=\"IBM437\"?>\r\n<analysis xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <id>2</id>\r\n  <attitudeimpactdiagrams>\r\n    <attitudeimpactdiagram>\r\n      <id>30</id>\r\n    </attitudeimpactdiagram>\r\n    <attitudeimpactdiagram>\r\n      <id>31</id>\r\n    </attitudeimpactdiagram>\r\n    <attitudeimpactdiagram>\r\n      <id>32</id>\r\n    </attitudeimpactdiagram>\r\n  </attitudeimpactdiagrams>\r\n</analysis>";
            var serializer = new XmlSerializer(typeof(AnalysisXmlEntity));

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(xmlText);
            writer.Flush();
            stream.Position = 0;

            var newAnalysis = (AnalysisXmlEntity)serializer.Deserialize(stream);
        }
    */
    }
}
