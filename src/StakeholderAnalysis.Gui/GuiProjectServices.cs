﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Storage;

namespace StakeholderAnalysis.Gui
{
    public class GuiProjectServices
    {
        private readonly StakeholderAnalysisLog log = new StakeholderAnalysisLog(typeof(GuiProjectServices));
        private readonly StakeholderAnalysisGui gui;
        private readonly StorageSqLite storageSqLite;
        
        public GuiProjectServices(StakeholderAnalysisGui gui)
        {
            this.gui = gui;
            storageSqLite = new StorageSqLite();
        }

        public void NewProject()
        {
            HandleUnsavedChanges(gui, CreateNewProject);
        }

        private void CreateNewProject()
        {
            storageSqLite.UnstageProject();
            foreach (var viewInfo in gui.ViewManager.Views.ToArray())
            {
                gui.ViewManager.CloseView(viewInfo);
            }

            gui.ProjectFilePath = "";
            
            gui.Analysis = new Analysis();

            gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.Analysis));
            gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.ProjectFilePath));
        }

        public void OpenProject()
        {
            storageSqLite.UnstageProject();

            HandleUnsavedChanges(gui, OpenNewProjectCore);
        }

        private void OpenNewProjectCore()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "StoryTree bestand (*.sqlite)|*.sqlite",
                FileName = gui.ProjectFilePath,
            };

            if ((bool) dialog.ShowDialog(Application.Current.MainWindow))
            {
                foreach (var viewInfo in gui.ViewManager.Views.ToArray())
                {
                    gui.ViewManager.CloseView(viewInfo);
                }

                ChangeState(StorageState.Busy);

                var worker = new BackgroundWorker();
                worker.DoWork += OpenProjectAsync;
                worker.RunWorkerCompleted += (o, e) => BackgroundWorkerAsyncFinished(o, e,
                    () =>
                    {
                        gui.ProjectFilePath = dialog.FileName;
                        log.Info($"Klaar met openen van project uit bestand '{gui.ProjectFilePath}'.");
                    });
                worker.WorkerSupportsCancellation = false;

                worker.RunWorkerAsync(dialog.FileName);
            }
        }

        public void SaveProject()
        {
            storageSqLite.UnstageProject();
            SaveProject(null);
        }

        public void SaveProjectAs()
        {
            storageSqLite.UnstageProject();
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
                Filter = "StoryTree bestand (*.sqlite)|*.sqlite"
            };

            if ((bool)dialog.ShowDialog(Application.Current.MainWindow))
            {
                gui.ProjectFilePath = dialog.FileName;
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

        private void BackgroundWorkerAsyncFinished(object sender, RunWorkerCompletedEventArgs e, Action workFinishedAction)
        {
            if (e.Result is Exception exception)
            {
                log.Error(exception.Message);
            }
            else
            {
                workFinishedAction();
            }

            ChangeState(StorageState.Idle);
        }

        private void OpenProjectAsync(object sender, DoWorkEventArgs e)
        {
            var fileName = e.Argument as string;
            try
            {
                gui.Analysis = storageSqLite.LoadProject(fileName);
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.Analysis));
            }
            catch (Exception exception)
            {
                e.Result = exception;
            }
        }

        public void HandleUnsavedChanges(StakeholderAnalysisGui gui, Action followingAction)
        {
            storageSqLite.StageProject(gui.Analysis);
            if (storageSqLite.HasStagedProjectChanges(gui.ProjectFilePath))
            {
                if (gui.ShouldSaveOpenChanges != null && gui.ShouldSaveOpenChanges())
                {
                    SaveProject(followingAction);
                }
                else
                {
                    followingAction();
                }
            }
            else
            {
                followingAction();
            }
        }

        private void StageAndStoreProjectCore()
        {
            if (!storageSqLite.HasStagedProject)
            {
                storageSqLite.StageProject(gui.Analysis);
            }

            storageSqLite.SaveProjectAs(gui.ProjectFilePath);
        }

        private void ChangeState(StorageState state)
        {
            gui.BusyIndicator = state;
            gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.BusyIndicator));
        }
    }
}
