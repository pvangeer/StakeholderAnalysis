using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class TwoAxisDiagramBrushXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "startcolor")]
        public string BrushStartColor { get; set; }

        [XmlAttribute(AttributeName = "endcolor")]
        public string BrushEndColor { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}