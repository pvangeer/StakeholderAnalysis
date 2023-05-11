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
        private readonly byte[] emptyProjectHash;
        private byte[] lastOpenedOrSavedProjectHash;
        private StagedProject stagedProject;

        public StorageXml()
        {
            var emptyProject = new Analysis();
            var emptyStagedProject = new StagedProject(emptyProject, emptyProject.Create(new PersistenceRegistry()));
            emptyProjectHash = FingerprintHelper.Get(emptyStagedProject.XmlEntity);
        }

        public bool HasStagedProject => stagedProject != null;

        public void StageProject(Analysis project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            stagedProject = new StagedProject(project, project.Create(new PersistenceRegistry()));
        }

        public void UnStageProject()
        {
            stagedProject = null;
        }

        public void SaveProjectAs(string databaseFilePath)
        {
            if (!HasStagedProject)
                throw new InvalidOperationException("Call 'StageProject(IProject)' first before calling this method.");

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
                UnStageProject();
            }
        }

        public Analysis LoadProject(string databaseFilePath)
        {
            IOUtils.ValidateFilePath(databaseFilePath);

            try
            {
                AnalysisXmlEntity analysisXmlEntity;

                var serializer = new XmlSerializer(typeof(AnalysisXmlEntity));

                using (Stream reader = new FileStream(databaseFilePath, FileMode.Open))
                {
                    try
                    {
                        analysisXmlEntity = (AnalysisXmlEntity)serializer.Deserialize(reader);
                    }
                    catch (Exception exception)
                    {
                        throw CreateStorageReaderException(databaseFilePath, "Bestand kon niet worden gelezen",
                            exception);
                    }
                }

                lastOpenedOrSavedProjectHash = FingerprintHelper.Get(analysisXmlEntity);
                return analysisXmlEntity.Read(new ReadConversionCollector());
            }
            catch (Exception exception)
            {
                throw CreateStorageReaderException(databaseFilePath, "Project kon niet worden ingeladen", exception);
            }
        }

        public bool HasStagedProjectChanges(string filePath)
        {
            if (!HasStagedProject)
                throw new InvalidOperationException("Call 'StageProject(IProject)' first before calling this method.");

            var hash = FingerprintHelper.Get(stagedProject.XmlEntity);
            if (FingerprintHelper.AreEqual(hash, emptyProjectHash)) return false;

            if (string.IsNullOrWhiteSpace(filePath)) return true;

            IOUtils.ValidateFilePath(filePath);

            return !FingerprintHelper.AreEqual(lastOpenedOrSavedProjectHash, hash);
        }

        private void SaveProjectInDatabase(string filePath)
        {
            IOUtils.ValidateFilePath(filePath);

            try
            {
                var serializer = new XmlSerializer(typeof(AnalysisXmlEntity));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, stagedProject.XmlEntity);
                }

                lastOpenedOrSavedProjectHash = FingerprintHelper.Get(stagedProject.XmlEntity);
            }
            // TODO: Change catch to catch proper exceptions
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
        ///     Creates a configured instance of <see cref="StorageException" /> when writing to the storage file failed.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file that was attempted to connect with.</param>
        /// <param name="errorMessage">The critical error message.</param>
        /// <param name="innerException">Exception that caused this exception to be thrown.</param>
        /// <returns>Returns a new <see cref="StorageException" />.</returns>
        private static XmlStorageException CreateStorageWriterException(string databaseFilePath, string errorMessage,
            Exception innerException)
        {
            var message = string.Format("Het is niet gelukt om het bestand weg te schrijven op locatie \"{0}\": {1}",
                databaseFilePath, errorMessage);
            return new XmlStorageException(message, innerException);
        }

        /// <summary>
        ///     Creates a configured instance of <see cref="StorageException" /> when reading the storage file failed.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file that was attempted to connect with.</param>
        /// <param name="errorMessage">The critical error message.</param>
        /// <param name="innerException">Exception that caused this exception to be thrown.</param>
        /// <returns>Returns a new <see cref="StorageException" />.</returns>
        private static XmlStorageException CreateStorageReaderException(string databaseFilePath, string errorMessage,
            Exception innerException = null)
        {
            var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build(errorMessage);
            return new XmlStorageException(message, innerException);
        }

        private class StagedProject
        {
            public StagedProject(Analysis projectModel, AnalysisXmlEntity projectXmlEntity)
            {
                Model = projectModel;
                XmlEntity = projectXmlEntity;
            }

            public Analysis Model { get; }
            public AnalysisXmlEntity XmlEntity { get; }
        }
    }

    public class XmlStorageException : Exception
    {
        public XmlStorageException(string eMessage, Exception ioException) : base(eMessage, ioException)
        {
        }
    }
}