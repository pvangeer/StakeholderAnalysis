using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class ForceFieldDiagramStakeholderXmlEntity : IPositionedStakeholderXmlEntity
    {
        [XmlAttribute(AttributeName = "interest")]
        public double Interest { get; set; }

        [XmlAttribute(AttributeName = "influence")]
        public double Influence { get; set; }

        [XmlAttribute(AttributeName = "rank")]
        public long Rank { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "stakeholderid")]
        public long StakeholderReferenceId { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}