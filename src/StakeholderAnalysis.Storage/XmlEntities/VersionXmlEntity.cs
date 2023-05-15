using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class VersionXmlEntity
    {
        public static string CurrentVersion => "23.1.1";

        public VersionXmlEntity()
        {
            Version = CurrentVersion;
            LastChanged = DateTime.Now.ToString("yyyy-MM-DD : hh:mm:ss");
        }

        [XmlElement(ElementName = "version")] 
        public string Version { get; set; }

        [XmlElement(ElementName = "lastchanged")]
        public string LastChanged { get; set; }

        // TODO: Add author
    }
}
