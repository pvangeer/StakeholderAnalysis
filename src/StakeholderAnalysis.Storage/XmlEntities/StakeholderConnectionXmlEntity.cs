using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderConnectionXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlElement(ElementName = "connectiongroupreference")]
        public StakeholderConnectionGroupReferenceXmlEntity StakeholderConnectionGroupReferenceXmlEntity { get; set; }

        [XmlElement(ElementName = "stakeholderfromreference")]
        public OnionDiagramStakeholderReferenceXmlEntity StakeholderFrom { get; set; }

        [XmlElement(ElementName = "stakeholdertoreference")]
        public OnionDiagramStakeholderReferenceXmlEntity StakeholderTo { get; set; }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}