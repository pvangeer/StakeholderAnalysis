using System;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class VersionXmlEntity
    {
        public VersionXmlEntity()
        {
            FileVersion = CurrentVersion;
            LastChanged = VersionInfo.CurrentDateTime;
            LastAuthor = VersionInfo.CurrentUser;
        }

        public static string CurrentVersion => $"{VersionInfo.Year}.{VersionInfo.MajorVersion}";

        [XmlElement(ElementName = "fileversion")] 
        public string FileVersion { get; set; }

        [XmlElement(ElementName = "creator")]
        public string Creator { get; set; }

        [XmlElement(ElementName = "created")]
        public string Created { get; set; }

        [XmlElement(ElementName = "lastauthor")]
        public string LastAuthor { get; set; }

        [XmlElement(ElementName = "lastchanged")]
        public string LastChanged { get; set; }
    }
}