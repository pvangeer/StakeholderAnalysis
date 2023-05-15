using System;
using System.Xml;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Migration
{
    public class XmlStorageMigrationService
    {
        public bool NeedsMigration(string fileName)
        {
            IOUtils.ValidateFilePath(fileName);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                var versionInformation = GetVersionInformation(xmlDoc);
                if (versionInformation == null)
                    throw new XmlStorageException(
                        "Het gespecificeerde bestand heeft geen versieinformatie en kan niet worden gelezen.", null);

                return !HasCurrentVersion(versionInformation);
            }
            catch (Exception exception)
            {
                throw new XmlStorageException("Bestand kon niet worden gelezen", exception);
            }
        }

        private bool HasCurrentVersion(string versionNodeValue)
        {
            return versionNodeValue == VersionXmlEntity.CurrentVersion;
        }

        private string GetVersionInformation(XmlDocument xmlDoc)
        {
            return GetVersionNode(xmlDoc)?.InnerText;
        }

        private static XmlNode GetVersionNode(XmlDocument xmlDoc)
        {
            return xmlDoc.ChildNodes.Count == 2 &&
                   xmlDoc.ChildNodes[1].ChildNodes.Count == 2 &&
                   xmlDoc.ChildNodes[1].FirstChild.Name == "versioninformation" &&
                   xmlDoc.ChildNodes[1].FirstChild.FirstChild.Name == "version"
                ? xmlDoc.ChildNodes[1].FirstChild.FirstChild
                : null;
        }

        private static XmlNode GetLastChangedNode(XmlDocument xmlDoc)
        {
            return xmlDoc.ChildNodes.Count == 2 &&
                   xmlDoc.ChildNodes[1].ChildNodes.Count == 2 &&
                   xmlDoc.ChildNodes[1].FirstChild.Name == "versioninformation" &&
                   xmlDoc.ChildNodes[1].FirstChild.ChildNodes[1].Name == "lastchanged"
                ? xmlDoc.ChildNodes[1].FirstChild.ChildNodes[1]
                : null;
        }

        public void MigrateFile(string oldFileName, string newFileName)
        {
            // Same format to correct version number
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(oldFileName);

            var versionXmlEntity = new VersionXmlEntity();

            var versionNode = GetVersionNode(xmlDoc);
            if (versionNode != null) versionNode.InnerText = versionXmlEntity.Version;

            var createdNode = GetLastChangedNode(xmlDoc);
            if (createdNode != null) createdNode.InnerText = versionXmlEntity.LastChanged;

            xmlDoc.Save(newFileName);
        }
    }
}