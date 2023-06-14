using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "stakeholdertypeid")]
        public long StakeholderTypeId { get; set; }

        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "telephone")]
        public string TelephoneNumber { get; set; }

        [XmlElement(ElementName = "notes")]
        public string Notes { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}