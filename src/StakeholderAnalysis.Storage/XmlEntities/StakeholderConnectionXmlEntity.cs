﻿using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class StakeholderConnectionXmlEntity : IXmlEntity
    {
        [XmlAttribute(AttributeName = "order")]
        public long Order { get; set; }

        [XmlAttribute(AttributeName = "connectiongroupid")]
        public long StakeholderConnectionGroupId { get; set; }

        [XmlAttribute(AttributeName = "positionedstakeholderfromid")]
        public long StakeholderFromId { get; set; }

        [XmlAttribute(AttributeName = "positionedstakeholdertoid")]
        public long StakeholderToId { get; set; }

        [XmlIgnore]
        [XmlAttribute(AttributeName = "id")]
        public long Id { get; set; }
    }
}