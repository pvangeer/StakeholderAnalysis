using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderTypeXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "icontype")]
        public byte IconType { get; set; }

        [XmlAttribute(AttributeName = "color")]
        public string Color { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}