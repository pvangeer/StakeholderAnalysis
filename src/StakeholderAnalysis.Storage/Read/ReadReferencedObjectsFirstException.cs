using System;

namespace StakeholderAnalysis.Storage.Read
{
    public class ReadReferencedObjectsFirstException : Exception
    {
        public ReadReferencedObjectsFirstException(string stakeholderTypeReferenceXmlEntityName)
        {
        }
    }
}