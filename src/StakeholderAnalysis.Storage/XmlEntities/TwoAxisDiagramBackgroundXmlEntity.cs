using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class TwoAxisDiagramBackgroundXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "textlefttop")]
        public string BackgroundTextLeftTop { get; set; }

        [XmlAttribute(AttributeName = "textleftbottom")]
        public string BackgroundTextLeftBottom { get; set; }

        [XmlAttribute(AttributeName = "textrighttop")]
        public string BackgroundTextRightTop { get; set; }

        [XmlAttribute(AttributeName = "textrightbottom")]
        public string BackgroundTextRightBottom { get; set; }

        [XmlAttribute(AttributeName = "textfontfamily")]
        public string BackgroundTextFontFamily { get; set; }

        [XmlAttribute(AttributeName = "textfontcolor")]
        public string BackgroundTextFontColor { get; set; }

        [XmlAttribute(AttributeName = "textfontbold")]
        public byte BackgroundTextFontBold { get; set; }

        [XmlAttribute(AttributeName = "textfontitalic")]
        public byte BackgroundTextFontItalic { get; set; }

        [XmlAttribute(AttributeName = "textfontsize")]
        public double BackgroundTextFontSize { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}