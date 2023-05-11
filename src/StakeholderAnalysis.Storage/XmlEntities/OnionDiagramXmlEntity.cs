using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class OnionDiagramXmlEntity : IXmlEntity
    {
        public OnionDiagramXmlEntity()
        {
            OnionDiagramStakeholderXmlEntities = new Collection<OnionDiagramStakeholderXmlEntity>();
            OnionRingXmlEntities = new Collection<OnionRingXmlEntity>();
            StakeholderConnectionXmlEntities = new Collection<StakeholderConnectionXmlEntity>();
            StakeholderConnectionGroupXmlEntities = new Collection<StakeholderConnectionGroupXmlEntity>();
        }

        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "asymmetry")]
        public double Asymmetry { get; set; }

        [XmlArray(ElementName = "stakeholders")]
        [XmlArrayItem(ElementName = "onionstakeholder")]
        public Collection<OnionDiagramStakeholderXmlEntity> OnionDiagramStakeholderXmlEntities { get; set; }

        [XmlArray(ElementName = "onionrings")]
        [XmlArrayItem(ElementName = "onionring")]
        public Collection<OnionRingXmlEntity> OnionRingXmlEntities { get; set; }

        [XmlArray(ElementName = "connections")]
        [XmlArrayItem(ElementName = "connection")]
        public Collection<StakeholderConnectionXmlEntity> StakeholderConnectionXmlEntities { get; set; }

        [XmlArray(ElementName = "connectiongroups")]
        [XmlArrayItem(ElementName = "connectiongroup")]
        public Collection<StakeholderConnectionGroupXmlEntity> StakeholderConnectionGroupXmlEntities { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}