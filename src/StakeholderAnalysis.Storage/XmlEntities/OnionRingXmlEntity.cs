using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class OnionRingXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "percentage")]
        public double Percentage { get; set; }

        [XmlAttribute(AttributeName = "backgroundcolor")]
        public string BackgroundColor { get; set; }

        [XmlAttribute(AttributeName = "strokecolor")]
        public string StrokeColor { get; set; }

        [XmlAttribute(AttributeName = "strokethickness")]
        public double StrokeThickness { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}