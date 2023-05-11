using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderTypeReferenceXmlEntity : IXmlEntity
    {
        public StakeholderTypeReferenceXmlEntity()
        {
        }

        public StakeholderTypeReferenceXmlEntity(StakeholderTypeXmlEntity stakeholderTypeXmlEntity)
        {
            Id = stakeholderTypeXmlEntity.Id;
        }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}