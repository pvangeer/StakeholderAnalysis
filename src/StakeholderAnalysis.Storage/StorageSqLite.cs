﻿using System;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.Create;
using StakeholderAnalysis.Storage.DbContext;
using StakeholderAnalysis.Storage.Read;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage
{
    /// <summary>
    ///     This class interacts with an SQLite database file using the Entity Framework.
    /// </summary>
    public class StorageSqLite
    {
        private readonly byte[] emptyProjectHash;
        private StagedProject stagedProject;

        public StorageSqLite()
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

        public void UnstageProject()
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
                UnstageProject();
            }
        }

        public Analysis LoadProject(string databaseFilePath)
        {
            var connectionString = GetConnectionToExistingFile(databaseFilePath);
            try
            {
                Analysis project;
                using (var dbContext = new Entities(connectionString))
                {
                    ValidateDatabaseVersion(dbContext, databaseFilePath);

                    dbContext.LoadTablesIntoContext();

                    AnalysisEntity projectEntity;
                    try
                    {
                        projectEntity = dbContext.AnalysisEntities.Local.Single();
                    }
                    catch (InvalidOperationException exception)
                    {
                        throw CreateStorageReaderException(databaseFilePath, "Geen geldige database", exception);
                    }

                    project = projectEntity.Read(new ReadConversionCollector());
                }

                return project;
            }
            catch (DataException exception)
            {
                throw CreateStorageReaderException(databaseFilePath, "Geen geldige database", exception);
            }
            catch (SystemException exception)
            {
                throw CreateStorageReaderException(databaseFilePath, "Geen geldige database", exception);
            }
        }

        public bool HasStagedProjectChanges(string filePath)
        {
            if (!HasStagedProject)
                throw new InvalidOperationException("Call 'StageProject(IProject)' first before calling this method.");

            var hash = FingerprintHelper.Get(stagedProject.Entity);
            if (FingerprintHelper.AreEqual(hash, emptyProjectHash)) return false;

            if (string.IsNullOrWhiteSpace(filePath)) return true;

            var connectionString = GetConnectionToExistingFile(filePath);
            try
            {
                byte[] originalHash;
                using (var dbContext = new Entities(connectionString))
                {
                    originalHash = dbContext.VersionEntities.Select(v => v.FingerPrint).First();
                }

                return !FingerprintHelper.AreEqual(originalHash, hash);
            }
            catch (Exception e)
            {
                if (e.InnerException is QuotaExceededException)
                    throw new StorageException(
                        "Opgeslagen project bevat teveel objecten om een vingerafdruk van te maken", e);
                throw new StorageException(e.Message, e);
            }
        }

        private void SaveProjectInDatabase(string databaseFilePath)
        {
            var connectionString = GetConnectionToNewFile(databaseFilePath);
            using (var dbContext = new Entities(connectionString))
            {
                try
                {
                    dbContext.VersionEntities.Add(new VersionEntity
                    {
                        Version = StakeholderAnalysisVersionHelper.GetCurrentDatabaseVersion(),
                        TimeStamp = DateTime.Now,
                        FingerPrint = FingerprintHelper.Get(stagedProject.Entity)
                    });
                    /*dbContext.AnalysisEntities.Add(stagedProject.Entity);
                    dbContext.SaveChanges();*/
                }
                catch (DataException exception)
                {
                    throw CreateStorageWriterException(databaseFilePath, "Er is een fout opgetreden bij het opslaan",
                        exception);
                }
                catch (SystemException exception)
                {
                    if (exception is InvalidOperationException || exception is NotSupportedException)
                        throw CreateStorageWriterException(databaseFilePath,
                            "Het was niet mogelijk een connectie te maken", exception);
                    throw;
                }
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
        ///     Attempts to set the connection to an existing storage file <paramref name="databaseFilePath" />.
        /// </summary>
        /// <param name="databaseFilePath">Path to database file.</param>
        /// <exception cref="ArgumentException"><paramref name="databaseFilePath" /> is invalid.</exception>
        /// <exception cref="StorageException">
        ///     Thrown when:
        ///     <list type="bullet">
        ///         <item><paramref name="databaseFilePath" /> does not exist</item>
        ///         <item>the database has an invalid schema.</item>
        ///     </list>
        /// </exception>
        private static string GetConnectionToExistingFile(string databaseFilePath)
        {
            IOUtils.ValidateFilePath(databaseFilePath);
            return GetConnectionToFile(databaseFilePath);
        }

        /// <summary>
        ///     Sets the connection to a newly created (empty) Ringtoets database file.
        /// </summary>
        /// <param name="databaseFilePath">Path to database file.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown when:
        ///     <list type="bullet">
        ///         <item><paramref name="databaseFilePath" /> is invalid</item>
        ///         <item><paramref name="databaseFilePath" /> points to an existing file</item>
        ///     </list>
        /// </exception>
        /// <exception cref="StorageException">
        ///     Thrown when:
        ///     <list type="bullet">
        ///         <item>executing <c>DatabaseStructure</c> script failed</item>
        ///     </list>
        /// </exception>
        private static string GetConnectionToNewFile(string databaseFilePath)
        {
            IOUtils.ValidateFilePath(databaseFilePath);
            StorageSqliteCreator.CreateDatabaseStructure(databaseFilePath);
            return GetConnectionToFile(databaseFilePath);
        }

        /// <summary>
        ///     Establishes a connection to an existing <paramref name="databaseFilePath" />.
        /// </summary>
        /// <param name="databaseFilePath">The path of the database file to connect to.</param>
        /// <exception cref="CouldNotConnectException">No file exists at <paramref name="databaseFilePath" />.</exception>
        private static string GetConnectionToFile(string databaseFilePath)
        {
            if (!File.Exists(databaseFilePath))
            {
                var message = new FileReaderErrorMessageBuilder(databaseFilePath).Build("Bestand bestaat niet");
                throw new Exception(message);
            }

            return GetConnectionToStorage(databaseFilePath);
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

    public class StorageException : Exception
    {
        public StorageException(string eMessage, Exception ioException) : base(eMessage, ioException)
        {
        }
    }
}