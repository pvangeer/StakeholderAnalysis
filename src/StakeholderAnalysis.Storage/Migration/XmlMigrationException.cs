using System;

namespace StakeholderAnalysis.Storage.Migration
{
    public class XmlMigrationException : Exception
    {
        public XmlMigrationException(string message) : base(message)
        {
        }

        public XmlMigrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}