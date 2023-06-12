using System.Globalization;
using System.Xml;

namespace StakeholderAnalysis.Storage.Migration
{
    public class Migrator231To232 : FileMigrator
    {
        public override string BaseVersion => "23.1";

        public override string TargetVersion => "23.2";

        public override void Migrate(XmlDocument xmlDocument)
        {
            MigrateOnionDiagrams(xmlDocument);
            MigrateForceFieldDiagrams(xmlDocument);
            MigrateAttitudeImpactDiagrams(xmlDocument);
        }

        private void MigrateOnionDiagrams(XmlDocument xmlDocument)
        {
            var onionDiagrams = xmlDocument.SelectSingleNode("/project/analysis/oniondiagrams");
            var oniondiagramNodes = onionDiagrams.SelectNodes("oniondiagram");
            if (oniondiagramNodes != null)
                foreach (XmlElement oldDiagram in oniondiagramNodes)
                    MigrateDiagramStakeholders(xmlDocument, oldDiagram, "onionstakeholder", "", "", false);
        }

        private static void MigrateForceFieldDiagrams(XmlDocument xmlDocument)
        {
            var forceFieldDiagramsNode = xmlDocument.SelectSingleNode("/project/analysis/forcefielddiagrams");
            var forceFieldDiagramNodes = forceFieldDiagramsNode.SelectNodes("forcefielddiagram");
            if (forceFieldDiagramNodes != null)
                foreach (XmlElement oldDiagram in forceFieldDiagramNodes)
                {
                    var newDiagramNode = RenameElementNode(xmlDocument, oldDiagram, "twoaxisdiagram", forceFieldDiagramsNode);

                    MigrateDiagramStakeholders(xmlDocument, newDiagramNode, "stakeholder", "interest", "influence");
                }
        }

        private void MigrateAttitudeImpactDiagrams(XmlDocument xmlDocument)
        {
            var attitudeImpactDiagramsNode = xmlDocument.SelectSingleNode("/project/analysis/attitudeimpactdiagrams");
            var attitudeImpactDiagramsElementList = attitudeImpactDiagramsNode.SelectNodes("attitudeimpactdiagram");
            if (attitudeImpactDiagramsElementList != null)
                foreach (XmlElement oldDiagram in attitudeImpactDiagramsElementList)
                {
                    var newDiagramNode = RenameElementNode(xmlDocument, oldDiagram, "twoaxisdiagram", attitudeImpactDiagramsNode);

                    MigrateDiagramStakeholders(xmlDocument, newDiagramNode, "attitudeimpactstakeholder", "impact", "attitude");
                }
        }

        private static XmlElement RenameElementNode(XmlDocument xmlDocument, XmlElement oldNode, string newName, XmlNode parentNode)
        {
            var newElementNode = xmlDocument.CreateElement(newName);

            CopyAttributes(xmlDocument, oldNode, newElementNode);

            if (!string.IsNullOrEmpty(oldNode.InnerText))
                newElementNode.InnerText = oldNode.InnerText;

            if (!string.IsNullOrEmpty(oldNode.InnerXml))
                newElementNode.InnerXml = oldNode.InnerXml;

            parentNode.InsertBefore(newElementNode, oldNode);
            parentNode.RemoveChild(oldNode);
            return newElementNode;
        }

        private static void CopyAttributes(XmlDocument xmlDocument, XmlElement oldNode, XmlElement newNode)
        {
            foreach (XmlAttribute attribute in oldNode.Attributes)
            {
                var newAttribute = xmlDocument.CreateAttribute(attribute.Name);
                newAttribute.InnerText = attribute.InnerText;
                newNode.Attributes.Append(newAttribute);
            }
        }

        private static void MigrateDiagramStakeholders(XmlDocument xmlDocument, XmlElement diagramNode,
            string oldStakeholderElementName, string oldLeftAttributeName, string oldTopAttributeName, bool invertTopValue = true)
        {
            var parentNode = diagramNode.SelectSingleNode("stakeholders");
            var stakeholderNodes = parentNode.SelectNodes($"{oldStakeholderElementName}");
            if (stakeholderNodes != null)
                foreach (XmlNode stakeholderNode in stakeholderNodes)
                {
                    var newStakeholder = xmlDocument.CreateElement("positionedstakeholder");
                    foreach (XmlAttribute stakeholderAttribute in stakeholderNode.Attributes)
                    {
                        var attributeName = stakeholderAttribute.Name;
                        if (!string.IsNullOrWhiteSpace(oldLeftAttributeName))
                            attributeName = attributeName.Replace(oldLeftAttributeName, "left");
                        if (!string.IsNullOrWhiteSpace(oldTopAttributeName))
                            attributeName = attributeName.Replace(oldTopAttributeName, "top");

                        var newAttribute = xmlDocument.CreateAttribute(attributeName);
                        var valueText = stakeholderAttribute.InnerText;
                        if (invertTopValue & (attributeName == "top"))
                        {
                            var oldValue = double.Parse(valueText, CultureInfo.InvariantCulture);
                            valueText = (1.0 - oldValue).ToString(CultureInfo.InvariantCulture);
                        }

                        newAttribute.InnerText = valueText;
                        newStakeholder.Attributes.Append(newAttribute);
                    }

                    newStakeholder.InnerText = stakeholderNode.InnerText;

                    parentNode.InsertBefore(newStakeholder, stakeholderNode);
                    parentNode.RemoveChild(stakeholderNode);
                }
        }
    }
}