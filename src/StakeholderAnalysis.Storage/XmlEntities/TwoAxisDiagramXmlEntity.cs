using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class TwoAxisDiagramXmlEntity : IXmlEntity
    {
        public TwoAxisDiagramXmlEntity()
        {
            PositionedStakeholderXmlEntities = new Collection<PositionedStakeholderXmlEntity>();
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlElement(ElementName = "brush")]
        public TwoAxisDiagramBrushXmlEntity Brush { get; set; }

        [XmlElement(ElementName = "background")]
        public TwoAxisDiagramBackgroundXmlEntity Background { get; set; }

        [XmlElement(ElementName = "axis")]
        public TwoAxisDiagramAxisXmlEntity Axis { get; set; }

        [XmlArray(ElementName = "stakeholders")]
        [XmlArrayItem(ElementName = "positionedstakeholder")]
        public Collection<PositionedStakeholderXmlEntity> PositionedStakeholderXmlEntities { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}