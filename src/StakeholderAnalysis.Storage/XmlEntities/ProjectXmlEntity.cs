using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    [XmlRoot(ElementName = "project", IsNullable = false)]
    public class ProjectXmlEntity
    {
        public ProjectXmlEntity()
        {
            VersionInformation = new VersionXmlEntity();
        }

        [XmlElement(ElementName = "versioninformation")]
        public VersionXmlEntity VersionInformation { get; set; }

        [XmlElement(ElementName = "analysis")]
        public AnalysisXmlEntity Analysis { get; set; }
    }
}
