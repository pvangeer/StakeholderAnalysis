using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class AttitudeImpactDiagramStakeholderXmlEntity : IPositionedStakeholderXmlEntity
    {
        [XmlAttribute(AttributeName = "attitude")]
        public double Attitude { get; set; }

        [XmlAttribute(AttributeName = "impact")]
        public double Impact { get; set; }

        [XmlAttribute(AttributeName = "rank")]
        public long Rank { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "stakeholderid")]
        public long StakeholderReferenceId { get; set; }

        [XmlIgnore]
        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}