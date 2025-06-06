using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Storage;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;
        private RelayCommand saveCanvasCommand;

        public RibbonViewModel(ViewModelFactory factory, StakeholderAnalysisGui guiInput) : base(factory)
        {
            gui = guiInput;
            if (gui != null)
            {
                gui.ShouldSaveOpenChanges = ShouldSaveOpenChanges;
                gui.ShouldMigrateProject = ShouldMigrateProject;
                gui.PropertyChanged += GuiPropertyChanged;
            }

            ViewManagerViewModel = ViewModelFactory.CreateViewManagerViewModel();
        }

        public ViewManagerViewModel ViewManagerViewModel { get; }

        public ICommand OpenCommand => CommandFactory.CreateOpenFileCommand();

        public ICommand SaveCommand => CommandFactory.CreateSaveFileCommand();

        public ICommand SaveAsCommand => CommandFactory.CreateSaveFileAsCommand();

        public ICommand NewCommand => CommandFactory.CreateNewProjectCommand();

        public ICommand CloseApplication => CommandFactory.CreateCloseApplicationCommand();

        public ICommand AddStakeholdersCommand => CommandFactory.CreateAddStakeholdersCommand();

        public string Version => $"{VersionInfo.Year}.{VersionInfo.MajorVersion}.{VersionInfo.MinorVersion}";

        public ICommand ExecuteHyperlinkCommand => new CanAlwaysExecuteActionCommand
        {
            ExecuteAction = OnExecuteHyperlink
        };

        public bool IsProjectExplorerVisible
        {
            get => gui.IsProjectExplorerVisible;
            set
            {
                gui.IsProjectExplorerVisible = value;
                gui.OnPropertyChanged();
            }
        }

        public bool IsPropertiesVisible
        {
            get => gui.IsPropertiesVisible;
            set
            {
                gui.IsPropertiesVisible = value;
                gui.OnPropertyChanged();
            }
        }

        public bool IsMagnifierActive
        {
            get => gui.IsMagnifierActive;
            set
            {
                gui.IsMagnifierActive = value;
                gui.OnPropertyChanged();
            }
        }

        public ICommand SaveImageCommand =>
            saveCanvasCommand ?? (saveCanvasCommand = CommandFactory.CreateSaveImageCommand());

        public ICommand EscapeCommand => CommandFactory.CreateEscapeCommand();

        private void OnExecuteHyperlink(object obj)
        {
            Process.Start((string)obj);
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
                case nameof(StakeholderAnalysisGui.Analysis):
                    OnPropertyChanged(nameof(AddStakeholdersCommand));
                    break;
                case nameof(StakeholderAnalysisGui.IsProjectExplorerVisible):
                    OnPropertyChanged(nameof(IsProjectExplorerVisible));
                    break;
                case nameof(StakeholderAnalysisGui.IsPropertiesVisible):
                    OnPropertyChanged(nameof(IsPropertiesVisible));
                    break;
            }
        }

        private ShouldProceedState ShouldSaveOpenChanges()
        {
            var messageBoxText = "U heeft aanpassingen aan uw project nog niet opgeslagen. Wilt u dat alsnog doen?";
            var caption = "Aanpassingen opslaan";
            var messageBoxResult =
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            return messageBoxResult == MessageBoxResult.Yes ? ShouldProceedState.Yes :
                messageBoxResult == MessageBoxResult.No ? ShouldProceedState.No :
                ShouldProceedState.Cancel;
        }

        private bool ShouldMigrateProject()
        {
            var messageBoxText =
                "U wilt een verouderd bestand openen. Wilt u dit bestand migreren naar het nieuwe format om het te kunnen openen?";
            var caption = "Bestand migreren naar nieuwste versie";
            var messageBoxResult =
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            return messageBoxResult == MessageBoxResult.Yes;
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }

        public void OpenFile(string file)
        {
            if (!string.IsNullOrWhiteSpace(file))
                gui?.GuiProjectServices.OpenProject(file);
        }
    }
}