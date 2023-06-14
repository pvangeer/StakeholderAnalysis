using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Storage;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;

        public MainWindowViewModel() : this(new StakeholderAnalysisGui())
        {
        }

        public MainWindowViewModel(StakeholderAnalysisGui guiInput) : base(new ViewModelFactory(guiInput))
        {
            gui = guiInput;
            gui.PropertyChanged += GuiPropertyChanged;

            RibbonViewModel = ViewModelFactory.CreateRibbonViewModel();
            MainContentPresenterViewModel = ViewModelFactory.CreateMainContentPresenterViewModel();
        }

        public string Version => $"{VersionInfo.Year}.{VersionInfo.MajorVersion}.{VersionInfo.MinorVersion}";

        public ICommand ExecuteHyperlinkCommand => new CanAlwaysExecuteActionCommand
        {
            ExecuteAction = OnExecuteHyperlink
        };

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }

        public RibbonViewModel RibbonViewModel { get; }

        public StatusBarViewModel StatusBarViewModel => ViewModelFactory.CreateStatusBarViewModel();

        public string WindowTitle =>
            gui?.ProjectFilePath == null || string.IsNullOrWhiteSpace(gui?.ProjectFilePath)
                ? "Stakeholderanalyse (*)"
                : $"Stakeholderanalyse ({Path.GetFileNameWithoutExtension(gui.ProjectFilePath)})";

        private void OnExecuteHyperlink(object obj)
        {
            Process.Start((string)obj);
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.ProjectFilePath):
                    OnPropertyChanged(nameof(WindowTitle));
                    break;
            }
        }

        public void ForcedClosingMainWindow()
        {
            gui.GuiProjectServices.HandleUnsavedChanges(() => { });
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}