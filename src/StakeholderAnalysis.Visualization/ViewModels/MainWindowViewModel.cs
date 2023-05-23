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
    }
}