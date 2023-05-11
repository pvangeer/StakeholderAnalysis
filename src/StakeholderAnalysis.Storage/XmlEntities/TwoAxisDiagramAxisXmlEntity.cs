using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class TwoAxisDiagramAxisXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "yaxisminlabel")]
        public string YAxisMinLabel { get; set; }

        [XmlAttribute(AttributeName = "yaxismaxlabel")]
        public string YAxisMaxLabel { get; set; }

        [XmlAttribute(AttributeName = "xaxisminlabel")]
        public string XAxisMinLabel { get; set; }

        [XmlAttribute(AttributeName = "xaxismaxlabel")]
        public string XAxisMaxLabel { get; set; }

        [XmlAttribute(AttributeName = "textfontfamily")]
        public string AxisTextFontFamily { get; set; }

        [XmlAttribute(AttributeName = "textfontcolor")]
        public string AxisTextFontColor { get; set; }

        [XmlAttribute(AttributeName = "textfontbold")]
        public byte AxisTextFontBold { get; set; }

        [XmlAttribute(AttributeName = "textfontitalic")]
        public byte AxisTextFontItalic { get; set; }

        [XmlAttribute(AttributeName = "textfontsize")]
        public double AxisTextFontSize { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}