using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    // TODO: Combine all references into one class using one reference serializationclass
    [Serializable]
    public class OnionDiagramStakeholderReferenceXmlEntity : IXmlEntity
    {
        public OnionDiagramStakeholderReferenceXmlEntity()
        {
        }

        public OnionDiagramStakeholderReferenceXmlEntity(OnionDiagramStakeholderXmlEntity stakeholderXmlEntity)
        {
            Id = stakeholderXmlEntity.Id;
        }

        [XmlAttribute(AttributeName = "id")] public long Id { get; set; }
    }
}