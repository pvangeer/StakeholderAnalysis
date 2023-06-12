using System.Xml;

namespace StakeholderAnalysis.Storage.Migration
{
    public abstract class FileMigrator
    {
        public abstract string BaseVersion { get; }

        public abstract string TargetVersion { get; }

        public abstract void Migrate(XmlDocument xmlDocument);
    }
}