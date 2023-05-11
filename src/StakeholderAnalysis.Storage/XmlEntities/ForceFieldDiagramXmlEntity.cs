using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class ForceFieldDiagramXmlEntity : IXmlEntity
    {
        public ForceFieldDiagramXmlEntity()
        {
            ForceFieldDiagramStakeholderXmlEntities = new Collection<ForceFieldDiagramStakeholderXmlEntity>();
        }

        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlElement(ElementName = "brush")] public TwoAxisDiagramBrushXmlEntity Brush { get; set; }

        [XmlElement(ElementName = "background")]
        public TwoAxisDiagramBackgroundXmlEntity Background { get; set; }

        [XmlElement(ElementName = "axis")] public TwoAxisDiagramAxisXmlEntity Axis { get; set; }

        [XmlArray(ElementName = "stakeholders")]
        [XmlArrayItem(ElementName = "stakeholder")]
        public Collection<ForceFieldDiagramStakeholderXmlEntity> ForceFieldDiagramStakeholderXmlEntities { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}