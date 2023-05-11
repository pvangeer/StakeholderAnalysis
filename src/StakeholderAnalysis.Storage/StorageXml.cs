using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.Create;
using StakeholderAnalysis.Storage.DbContext;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage
{
    /// <summary>
    ///     This class interacts with an SQLite database file using the Entity Framework.
    /// </summary>
    public class StorageXml
    {
        private readonly byte[] emptyProjectHash;
        private StagedProject stagedProject;

        public StorageXml()
        {
            var emptyProject = new Analysis();
            var emptyStagedProject = new StagedProject(emptyProject, emptyProject.Create(new PersistenceRegistry()));
            emptyProjectHash = FingerprintHelper.Get(emptyStagedProject.Entity);
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
                throw new StorageException(e.Message, e);
            }
            finally
            {
                UnStageProject();
            }
        }

        public Analysis LoadProject(string databaseFilePath)
        {
            IOUtils.ValidateFilePath(databaseFilePath);

            /*var serializer = XmlSerializer(typeof(AnalysisEntity));
            
            try
            {
                Analysis project;
                using (var dbContext = new Entities(connectionString))
                {
                    ValidateDatabaseVersion(dbContext, filePath);

                    dbContext.LoadTablesIntoContext();

                    AnalysisEntity projectEntity;
                    try
                    {
                        projectEntity = dbContext.AnalysisEntities.Local.Single();
                    }
                    catch (InvalidOperationException exception)
                    {
                        throw CreateStorageReaderException(filePath, "Geen geldige database", exception);
                    }

                    project = projectEntity.Read(new ReadConversionCollector());
                }

                return project;
            }
            catch (DataException exception)
            {
                throw CreateStorageReaderException(filePath, "Geen geldige database", exception);
            }
            catch (SystemException exception)
            {
                throw CreateStorageReaderException(filePath, "Geen geldige database", exception);
            }*/
            return new Analysis();
        }

        private object XmlSerializer(Type type)
        {
            throw new NotImplementedException();
        }

        public bool HasStagedProjectChanges(string filePath)
        {
            if (!HasStagedProject)
                throw new InvalidOperationException("Call 'StageProject(IProject)' first before calling this method.");

            var hash = FingerprintHelper.Get(stagedProject.Entity);
            if (FingerprintHelper.AreEqual(hash, emptyProjectHash)) return false;

            if (string.IsNullOrWhiteSpace(filePath)) return true;

            IOUtils.ValidateFilePath(filePath);
            return false;
            // TODO: Read file and look at differences in fingerprint.
            /*try
            {
                byte[] originalHash;
                using (var dbContext = new Entities(connectionString))
                    originalHash = dbContext.VersionEntities.Select(v => v.FingerPrint).First();

                return !FingerprintHelper.AreEqual(originalHash, hash);
            }
            catch (Exception e)
            {
                if (e.InnerException is QuotaExceededException)
                {
                    throw new StorageException("Opgeslagen project bevat teveel objecten om een vingerafdruk van te maken", e);
                }
                throw new StorageException(e.Message, e);
            }*/
        }

        private void SaveProjectInDatabase(string filePath)
        {
            IOUtils.ValidateFilePath(filePath);

            try
            {
                var serializer = new XmlSerializer(typeof(AnalysisXmlEntity));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, stagedProject.Entity);
                }
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

        private static void ValidateDatabaseVersion(Entities stakeholderAnalysisEntities, string databaseFilePath)
        {
            try
            {
                var databaseVersion = stakeholderAnalysisEntities.VersionEntities.Select(v => v.Version).Single();
                if (!StakeholderAnalysisVersionHelper.IsValidVersion(databaseVersion))
                {
                    var m = string.Format("Database versie ('{0}') is ongeldig",
                        databaseVersion);
                    var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build(m);
                    throw new FormatException(message);
                }

                if (StakeholderAnalysisVersionHelper.IsNewerThanCurrent(databaseVersion))
                {
                    var m = string.Format("Database versie ('{0}') is nieuwer dan de huidige versie ('{1}')",
                        databaseVersion, StakeholderAnalysisVersionHelper.GetCurrentDatabaseVersion());
                    var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build(m);
                    throw new FormatException(message);
                }
            }
            catch (InvalidOperationException e)
            {
                var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build(
                    "Er mag maximaal 1 rij aanwezig zijn in de VersionEntity tabel van het opslagformaat.");
                throw new FormatException(message);
            }
        }

        /// <summary>
        ///     Sets the connection to the Ringtoets database.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file, which is used for creating exceptions.</param>
        /// <exception cref="StorageValidationException">Thrown when the database does not contain the table <c>version</c>.</exception>
        private static string GetConnectionToStorage(string databaseFilePath)
        {
            var connectionString =
                SqLiteEntityConnectionStringBuilder.BuildSqLiteEntityConnectionString(databaseFilePath);

            using (var dbContext = new Entities(connectionString))
            {
                try
                {
                    dbContext.Database.Initialize(true);
                    dbContext.LoadVersionTableIntoContext();
                }
                catch (Exception exception)
                {
                    var message =
                        new FileReaderErrorMessageBuilder(databaseFilePath).Build("Geen geldig opslagbestand");
                    throw new Exception(message, exception);
                }
            }

            return connectionString;
        }

        /// <summary>
        ///     Creates a configured instance of <see cref="StorageException" /> when writing to the storage file failed.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file that was attempted to connect with.</param>
        /// <param name="errorMessage">The critical error message.</param>
        /// <param name="innerException">Exception that caused this exception to be thrown.</param>
        /// <returns>Returns a new <see cref="StorageException" />.</returns>
        private static StorageException CreateStorageWriterException(string databaseFilePath, string errorMessage,
            Exception innerException)
        {
            var message = string.Format("Het is niet gelukt om het bestand weg te schrijven op locatie \"{0}\": {1}",
                databaseFilePath, errorMessage);
            return new StorageException(message, innerException);
        }

        /// <summary>
        ///     Creates a configured instance of <see cref="StorageException" /> when reading the storage file failed.
        /// </summary>
        /// <param name="databaseFilePath">The path of the file that was attempted to connect with.</param>
        /// <param name="errorMessage">The critical error message.</param>
        /// <param name="innerException">Exception that caused this exception to be thrown.</param>
        /// <returns>Returns a new <see cref="StorageException" />.</returns>
        private static StorageException CreateStorageReaderException(string databaseFilePath, string errorMessage,
            Exception innerException = null)
        {
            var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build(errorMessage);
            return new StorageException(message, innerException);
        }

        private class StagedProject
        {
            public StagedProject(Analysis projectModel, AnalysisXmlEntity projectEntity)
            {
                Model = projectModel;
                Entity = projectEntity;
            }

            public Analysis Model { get; }
            public AnalysisXmlEntity Entity { get; }
        }
    }

    public class XmlStorageException : Exception
    {
        public XmlStorageException(string eMessage, Exception ioException) : base(eMessage, ioException)
        {
        }
    }
}