using System.ComponentModel;
using System.IO;
using StakeholderAnalysis.Gui;
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

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }

        public RibbonViewModel RibbonViewModel { get; }

        public StatusBarViewModel StatusBarViewModel => ViewModelFactory.CreateStatusBarViewModel();

        public string WindowTitle =>
            gui?.ProjectFilePath == null || string.IsNullOrWhiteSpace(gui?.ProjectFilePath)
                ? "Stakeholderanalyse (*)"
                : $"Stakeholderanalyse ({Path.GetFileNameWithoutExtension(gui.ProjectFilePath)})";

        public bool IsBusy => gui.BusyIndicator == StorageState.Busy;

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.ProjectFilePath):
                    OnPropertyChanged(nameof(WindowTitle));
                    break;
                case nameof(StakeholderAnalysisGui.BusyIndicator):
                    OnPropertyChanged(nameof(IsBusy));
                    break;
            }
        }

        public bool ForcedClosingMainWindow()
        {
            return gui.GuiProjectServices.HandleUnsavedChanges(() => { });
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}