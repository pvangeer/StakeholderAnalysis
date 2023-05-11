using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class AttitudeImpactDiagramStakeholderXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "attitude")]
        public double Attitude { get; set; }

        [XmlAttribute(AttributeName = "impact")]
        public double Impact { get; set; }

        [XmlAttribute(AttributeName = "rank")] public long Rank { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlElement(ElementName = "stakeholderreference")]
        public StakeholderReferenceXmlEntity StakeholderReferenceEntity { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}