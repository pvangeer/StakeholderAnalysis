using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderConnectionGroupXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "color")]
        public string Color { get; set; }

        [XmlAttribute(AttributeName = "visible")]
        public byte Visible { get; set; }

        [XmlAttribute(AttributeName = "strokethickness")]
        public double StrokeThickness { get; set; }

        [XmlAttribute(AttributeName = "linestyle")]
        public byte LineStyle { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}