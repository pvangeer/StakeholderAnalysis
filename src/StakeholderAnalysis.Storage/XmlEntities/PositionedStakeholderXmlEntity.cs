using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class PositionedStakeholderXmlEntity : IPositionedStakeholderXmlEntity
    {
        [XmlAttribute(AttributeName = "top")]
        public double Top { get; set; }

        [XmlAttribute(AttributeName = "left")]
        public double Left { get; set; }

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