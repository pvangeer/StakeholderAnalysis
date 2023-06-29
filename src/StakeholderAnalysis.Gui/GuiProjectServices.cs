using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Storage;
using StakeholderAnalysis.Storage.Migration;

namespace StakeholderAnalysis.Gui
{
    public class GuiProjectServices
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly StakeholderAnalysisLog log = new StakeholderAnalysisLog(typeof(GuiProjectServices));
        private readonly StorageXml storageXml;

        public GuiProjectServices(StakeholderAnalysisGui gui)
        {
            this.gui = gui;
            storageXml = new StorageXml();
        }

        public void NewProject()
        {
            HandleUnsavedChanges(CreateNewProject);
        }

        private void CreateNewProject()
        {
            storageXml.UnStageAnalysis();
            storageXml.UnStageVersionInformation();

            foreach (var viewInfo in gui.ViewManager.Views.ToArray()) gui.ViewManager.CloseView(viewInfo);

            gui.ProjectFilePath = "";
            gui.VersionInfo = null;
            gui.Analysis = AnalysisFactory.CreateStandardNewAnalysis();

            gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.Analysis));
            gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.ProjectFilePath));
        }

        public void OpenProject()
        {
            storageXml.UnStageAnalysis();
            storageXml.UnStageVersionInformation();

            HandleUnsavedChanges(OpenNewProjectCore);
        }

        public void OpenProject(string fileName)
        {
            storageXml.UnStageAnalysis();
            storageXml.UnStageVersionInformation();

            HandleUnsavedChanges(() => OpenProjectCore(fileName));
        }

        private void OpenNewProjectCore()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Stakeholder analyse bestand (*.xml)|*.xml",
                FileName = gui.ProjectFilePath
            };

            if ((bool)dialog.ShowDialog(Application.Current.MainWindow))
                OpenProjectCore(dialog.FileName);
        }

        private void OpenProjectCore(string fileName)
        {
            var needsMigration = false;
            try
            {
                needsMigration = XmlStorageMigrationService.NeedsMigration(fileName);
            }
            catch (XmlStorageException e)
            {
                var message = e.Message;
                if (e.InnerException != null)
                    message = $"Er is een fout opgetreden bij het lezen van dit bestand: {e.InnerException}";
                log.Error(message);
                return;
            }

            if (needsMigration && gui.ShouldMigrateProject != null &&
                gui.ShouldMigrateProject())
            {
                if (!MigrateProject(fileName, out var newFileName))
                    return;

                fileName = newFileName;
            }

            foreach (var viewInfo in gui.ViewManager.Views.ToArray()) gui.ViewManager.CloseView(viewInfo);

            ChangeState(StorageState.Busy);

            var worker = new BackgroundWorker();
            worker.DoWork += OpenProjectAsync;
            worker.RunWorkerCompleted += (o, e) => BackgroundWorkerAsyncFinished(o, e,
                () =>
                {
                    gui.ProjectFilePath = fileName;
                    log.Info($"Klaar met openen van project uit bestand '{gui.ProjectFilePath}'.");
                });
            worker.WorkerSupportsCancellation = false;

            worker.RunWorkerAsync(fileName);
        }

        public void SaveProject()
        {
            storageXml.UnStageAnalysis();
            storageXml.UnStageVersionInformation();
            SaveProject(null);
        }

        public void SaveProjectAs()
        {
            storageXml.UnStageAnalysis();
            storageXml.UnStageVersionInformation();
            SaveProjectAs(null);
        }

        private void SaveProject(Action followingAction)
        {
            if (string.IsNullOrWhiteSpace(gui.ProjectFilePath))
            {
                SaveProjectAs(followingAction);
                return;
            }

            StageProjectAndStore(followingAction);
        }

        private void SaveProjectAs(Action followingAction)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                FileName = gui.ProjectFilePath,
                OverwritePrompt = true,
                Filter = "Stakeholder analyse bestand (*.xml)|*.xml"
            };

            if ((bool)dialog.ShowDialog(Application.Current.MainWindow))
            {
                gui.ProjectFilePath = dialog.FileName;
                gui.OnPropertyChanged(nameof(gui.ProjectFilePath));
                StageProjectAndStore(followingAction);
            }
        }

        private void StageProjectAndStore(Action followingAction = null)
        {
            ChangeState(StorageState.Busy);
            var worker = new BackgroundWorker();
            worker.DoWork += StageAndStoreProjectAsync;
            worker.RunWorkerCompleted += (o, e) => BackgroundWorkerAsyncFinished(o, e,
                () =>
                {
                    log.Info($"Project is opgeslagen in bestand '{gui.ProjectFilePath}'.");
                    followingAction?.Invoke();
                });
            worker.WorkerSupportsCancellation = false;

            worker.RunWorkerAsync();
        }

        private void StageAndStoreProjectAsync(object sender, DoWorkEventArgs e)
        {
            try
            {
                StageAndStoreProjectCore();
            }
            catch (Exception exception)
            {
                e.Result = exception;
            }
        }

        private void BackgroundWorkerAsyncFinished(object sender, RunWorkerCompletedEventArgs e,
            Action workFinishedAction)
        {
            if (e.Result is Exception exception)
                log.Error(exception.Message);
            else
                workFinishedAction();

            ChangeState(StorageState.Idle);
        }

        private void OpenProjectAsync(object sender, DoWorkEventArgs e)
        {
            var fileName = e.Argument as string;

            try
            {
                var readProjectData = storageXml.LoadProject(fileName);
                gui.Analysis = readProjectData.Analysis;
                gui.VersionInfo = new VersionInfo
                {
                    AuthorCreated = readProjectData.Author,
                    DateCreated = readProjectData.Created
                };
                gui.ProjectFilePath = fileName;

                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.Analysis));
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.ProjectFilePath));
            }
            catch (Exception exception)
            {
                e.Result = exception;
            }
        }

        private bool MigrateProject(string fileName, out string newFileName)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                FileName = fileName.Replace(".xml",
                    $"-migrated-{VersionInfo.Year}.{VersionInfo.MajorVersion}.{VersionInfo.MinorVersion}.xml"),
                OverwritePrompt = true,
                Filter = "Stakeholderanalyse bestand (*.xml)|*.xml"
            };
            newFileName = null;

            if ((bool)dialog.ShowDialog(Application.Current.MainWindow))
            {
                try
                {
                    XmlStorageMigrationService.MigrateFile(fileName, dialog.FileName);
                }
                catch (XmlMigrationException e)
                {
                    log.Error(e.Message);
                }
                catch (Exception e)
                {
                    log.Error($"Er is een fout opgetreden bij het migreren van dit bestand: {e.Message}");
                    return false;
                }

                newFileName = dialog.FileName;
                log.Info($"Migratie van bestand '{fileName}' is voltooid. Het resultaat is opgeslagen in het bestand '{newFileName}'");
                return true;
            }

            log.Info("Migratie gestopt door de gebruiker. Bestand wordt niet geopend.");
            return false;
        }

        public bool HandleUnsavedChanges(Action followingAction)
        {
            storageXml.StageAnalysis(gui.Analysis);
            storageXml.StageVersionInformation(gui.VersionInfo);
            if (storageXml.HasStagedProjectChanges())
            {
                if (gui.ShouldSaveOpenChanges != null)
                    switch (gui.ShouldSaveOpenChanges())
                    {
                        case ShouldProceedState.Yes:
                            SaveProject(followingAction);
                            break;
                        case ShouldProceedState.No:
                            followingAction();
                            break;
                        case ShouldProceedState.Cancel:
                            return false;
                    }
                else
                    followingAction();
            }
            else
            {
                followingAction();
            }

            return true;
        }

        private void StageAndStoreProjectCore()
        {
            if (!storageXml.HasStagedAnalysis) storageXml.StageAnalysis(gui.Analysis);
            storageXml.StageVersionInformation(gui.VersionInfo);

            storageXml.SaveProjectAs(gui.ProjectFilePath);
        }

        private void ChangeState(StorageState state)
        {
            gui.BusyIndicator = state;
            gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.BusyIndicator));
        }
    }
}