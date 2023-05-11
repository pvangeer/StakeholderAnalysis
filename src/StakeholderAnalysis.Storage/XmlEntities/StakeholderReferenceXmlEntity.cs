using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderReferenceXmlEntity : IXmlEntity
    {
        public StakeholderReferenceXmlEntity()
        {
        }

        public StakeholderReferenceXmlEntity(StakeholderXmlEntity stakeholderXmlEntity)
        {
            Id = stakeholderXmlEntity.Id;
        }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}