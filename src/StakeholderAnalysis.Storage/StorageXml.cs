using System;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.Create;
using StakeholderAnalysis.Storage.Read;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage
{
    public class StorageXml
    {
        private readonly int emptyAnalysisHash;
        private int lastOpenedOrSavedAnalysisHash;
        private AnalysisXmlEntity stagedAnalysisXmlEntity;
        private VersionInfo versionInfo;

        public StorageXml()
        {
            emptyAnalysisHash = FingerprintHelper.Get(AnalysisFactory.CreateStandardNewAnalysis().Create(new PersistenceRegistry()));
        }

        public bool HasStagedAnalysis => stagedAnalysisXmlEntity != null;

        public void StageVersionInformation(VersionInfo newVersionInfo)
        {
            versionInfo = newVersionInfo;
        }

        public void StageAnalysis(Analysis analysis)
        {
            if (analysis == null)
                throw new ArgumentNullException(nameof(analysis));

            stagedAnalysisXmlEntity = analysis.Create(new PersistenceRegistry());
        }

        public void UnStageAnalysis()
        {
            stagedAnalysisXmlEntity = null;
        }

        public void UnStageVersionInformation()
        {
            versionInfo = null;
        }

        public void SaveProjectAs(string databaseFilePath)
        {
            if (!HasStagedAnalysis)
                throw new InvalidOperationException("Call 'StageAnalysis(Analysis)' first before calling this method.");

            try
            {
                var writer = new BackedUpFileWriter(databaseFilePath);
                writer.Perform(() => SaveProjectInDatabase(databaseFilePath));
            }
            catch (IOException e)
            {
                throw new XmlStorageException(e.Message, e);
            }
            finally
            {
                UnStageAnalysis();
                UnStageVersionInformation();
            }
        }

        public ProjectData LoadProject(string filePath)
        {
            IOUtils.ValidateFilePath(filePath);

            try
            {
                ProjectXmlEntity projectXmlEntity;

                var serializer = new XmlSerializer(typeof(ProjectXmlEntity));

                using (Stream reader = new FileStream(filePath, FileMode.Open))
                {
                    try
                    {
                        projectXmlEntity = (ProjectXmlEntity)serializer.Deserialize(reader);
                    }
                    catch (InvalidOperationException exception)
                    {
                        throw CreateStorageReaderException(filePath, "Bestand kon niet worden gelezen", exception.InnerException);
                    }
                }

                lastOpenedOrSavedAnalysisHash = FingerprintHelper.Get(projectXmlEntity.Analysis);
                return new ProjectData
                {
                    Analysis = projectXmlEntity.Analysis.Read(new ReadConversionCollector()),
                    Created = projectXmlEntity.VersionInformation.Created,
                    Author = projectXmlEntity.VersionInformation.Creator
                };
            }
            catch (Exception exception)
            {
                throw CreateStorageReaderException(filePath, "Project kon niet worden ingeladen", exception);
            }
        }

        public bool HasStagedProjectChanges()
        {
            if (!HasStagedAnalysis)
                throw new InvalidOperationException("Call 'StageAnalysis(IProject)' first before calling this method.");

            var hash = FingerprintHelper.Get(stagedAnalysisXmlEntity);
            return hash != emptyAnalysisHash &&
                   lastOpenedOrSavedAnalysisHash != hash;
        }

        private void SaveProjectInDatabase(string filePath)
        {
            IOUtils.ValidateFilePath(filePath);

            try
            {
                var serializer = new XmlSerializer(typeof(ProjectXmlEntity));
                using (var writer = new StreamWriter(filePath))
                {
                    var projectXmlEntity = new ProjectXmlEntity { Analysis = stagedAnalysisXmlEntity };
                    projectXmlEntity.VersionInformation.Created = versionInfo != null
                        ? versionInfo.DateCreated
                        : projectXmlEntity.VersionInformation.LastChanged;
                    projectXmlEntity.VersionInformation.Creator = versionInfo != null
                        ? versionInfo.AuthorCreated
                        : projectXmlEntity.VersionInformation.LastAuthor;
                    serializer.Serialize(writer, projectXmlEntity);
                }

                lastOpenedOrSavedAnalysisHash = FingerprintHelper.Get(stagedAnalysisXmlEntity);
            }
            catch (DataException exception)
            {
                throw CreateStorageWriterException(filePath, "Er is een fout opgetreden bij het opslaan",
                    exception);
            }
            catch (SystemException exception)
            {
                if (exception is InvalidOperationException || exception is NotSupportedException)
                    throw CreateStorageWriterException(filePath, "Het was niet mogelijk een connectie te maken",
                        exception);

                throw;
            }
        }

        /// <summary>
        ///     Creates a configured instance of <see cref="XmlStorageException" /> when writing to the storage file failed.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file that was attempted to connect with.</param>
        /// <param name="errorMessage">The critical error message.</param>
        /// <param name="innerException">Exception that caused this exception to be thrown.</param>
        /// <returns>Returns a new <see cref="XmlStorageException" />.</returns>
        private static XmlStorageException CreateStorageWriterException(string databaseFilePath, string errorMessage,
            Exception innerException)
        {
            var message = string.Format("Het is niet gelukt om het bestand weg te schrijven op locatie \"{0}\": {1}",
                databaseFilePath, errorMessage);
            return new XmlStorageException(message, innerException);
        }

        /// <summary>
        ///     Creates a configured instance of <see cref="XmlStorageException" /> when reading the storage file failed.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file that was attempted to connect with.</param>
        /// <param name="errorMessage">The critical error message.</param>
        /// <param name="innerException">Exception that caused this exception to be thrown.</param>
        /// <returns>Returns a new <see cref="XmlStorageException" />.</returns>
        private static XmlStorageException CreateStorageReaderException(string databaseFilePath, string errorMessage,
            Exception innerException = null)
        {
            var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build(errorMessage);
            return new XmlStorageException(message, innerException);
        }
    }

    public class ProjectData
    {
        public Analysis Analysis { get; set; }

        public string Created { get; set; }

        public string Author { get; set; }
    }

    public class XmlStorageException : Exception
    {
        public XmlStorageException(string eMessage, Exception ioException) : base(eMessage, ioException)
        {
        }
    }
}