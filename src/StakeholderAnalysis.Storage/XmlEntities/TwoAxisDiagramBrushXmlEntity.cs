using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class TwoAxisDiagramBrushXmlEntity
    {
        [XmlAttribute(AttributeName = "startcolor")]
        public string BrushStartColor { get; set; }

        [XmlAttribute(AttributeName = "endcolor")]
        public string BrushEndColor { get; set; }
    }
}