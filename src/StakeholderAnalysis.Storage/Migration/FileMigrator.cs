using System.Xml;

namespace StakeholderAnalysis.Storage.Migration
{
    public abstract class FileMigrator
    {
        public abstract void Migrate(XmlDocument xmlDocument);
    }
}