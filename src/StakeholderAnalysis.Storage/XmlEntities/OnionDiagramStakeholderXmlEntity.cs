using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class OnionDiagramStakeholderXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "rank")]
        public long Rank { get; set; }

        [XmlAttribute(AttributeName = "left")]
        public double Left { get; set; }

        [XmlAttribute(AttributeName = "top")]
        public double Top { get; set; }

        [XmlAttribute(AttributeName = "stakeholderid")]
        public long StakeholderId { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}