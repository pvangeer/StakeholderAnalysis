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
        private readonly XmlStorageMigrationService migrationService = new XmlStorageMigrationService();
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

        private void OpenNewProjectCore()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Stakeholder analyse bestand (*.xml)|*.xml",
                FileName = gui.ProjectFilePath
            };

            if ((bool)dialog.ShowDialog(Application.Current.MainWindow))
            {
                var fileName = dialog.FileName;
                var needsMigration = false;
                try
                {
                    needsMigration = migrationService.NeedsMigration(fileName);
                }
                catch (XmlStorageException e)
                {
                    log.Error("Bestand kon niet worden geopend.");
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

                worker.RunWorkerAsync(dialog.FileName);
            }
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
                FileName = fileName,
                OverwritePrompt = true,
                Filter = "Stakeholderanalyse bestand (*.xml)|*.xml"
            };

            if ((bool)dialog.ShowDialog(Application.Current.MainWindow))
            {
                migrationService.MigrateFile(fileName, dialog.FileName);
                newFileName = dialog.FileName;
                return true;
            }

            log.Info("Migratie gestopt door de gebruiker. Bestand wordt niet geopend.");
            newFileName = null;
            return false;
        }

        public void HandleUnsavedChanges(Action followingAction)
        {
            storageXml.StageAnalysis(gui.Analysis);
            storageXml.StageVersionInformation(gui.VersionInfo);
            if (storageXml.HasStagedProjectChanges())
            {
                if (gui.ShouldSaveOpenChanges != null && gui.ShouldSaveOpenChanges())
                    SaveProject(followingAction);
                else
                    followingAction();
            }
            else
            {
                followingAction();
            }
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