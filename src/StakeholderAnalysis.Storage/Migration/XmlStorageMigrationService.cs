﻿using System;
using System.Collections.Generic;
using System.Xml;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Migration
{
    public static class XmlStorageMigrationService
    {
        public static void MigrateFile(string oldFileName, string newFileName)
        {
            // Same format to correct version number
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(oldFileName);

            var versionXmlEntity = new VersionXmlEntity();

            var versionNode = GetVersionNode(xmlDoc);
            var version = versionNode.InnerText;

            var migrators = new List<FileMigrator>();
            switch (version)
            {
                case "23.1":
                    migrators.Add(new Migrator231To232());
                    break;
            }

            foreach (var fileMigrator in migrators)
            {
                fileMigrator.Migrate(xmlDoc);
            }

            if (versionNode != null) versionNode.InnerText = versionXmlEntity.FileVersion;

            var createdNode = GetLastChangedNode(xmlDoc);
            if (createdNode != null) createdNode.InnerText = versionXmlEntity.LastChanged;

            xmlDoc.Save(newFileName);
        }

        public static bool NeedsMigration(string fileName)
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

        private static bool HasCurrentVersion(string versionNodeValue)
        {
            return versionNodeValue == VersionXmlEntity.CurrentVersion;
        }

        private static string GetVersionInformation(XmlDocument xmlDoc)
        {
            return GetVersionNode(xmlDoc)?.InnerText;
        }

        private static XmlNode GetVersionNode(XmlDocument xmlDoc)
        {
            return xmlDoc.ChildNodes.Count == 2 &&
                   xmlDoc.ChildNodes[1].ChildNodes.Count == 2 &&
                   xmlDoc.ChildNodes[1].FirstChild.Name == ProjectXmlEntity.VersionInformationElementName &&
                   xmlDoc.ChildNodes[1].FirstChild.FirstChild.Name == VersionXmlEntity.FileVersionElementName
                ? xmlDoc.ChildNodes[1].FirstChild.FirstChild
                : null;
        }

        private static XmlNode GetLastChangedNode(XmlDocument xmlDoc)
        {
            var projectNode = xmlDoc.SelectSingleNode("/project");
            if (projectNode == null)
                return null;

            var versionNode = projectNode.SelectSingleNode($"/{ProjectXmlEntity.VersionInformationElementName}");
            if (versionNode == null) 
                return null;

            return versionNode.SelectSingleNode($"/{VersionXmlEntity.LastChangedElementName}");
        }
    }
}