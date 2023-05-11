using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderConnectionGroupReferenceXmlEntity : IXmlEntity
    {
        public StakeholderConnectionGroupReferenceXmlEntity()
        {
        }

        public StakeholderConnectionGroupReferenceXmlEntity(
            StakeholderConnectionGroupXmlEntity connectionGroupXmlEntity)
        {
            Id = connectionGroupXmlEntity.Id;
        }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}