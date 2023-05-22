using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
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
            RibbonViewModel = ViewModelFactory.CreateRibbonViewModel();
            RibbonViewModel.ToggleToolWindowCommand.Execute(typeof(OnionDiagramPropertiesViewModel));
            MainContentPresenterViewModel = ViewModelFactory.CreateMainContentPresenterViewModel();
            ProjectExplorerViewModel = ViewModelFactory.CreateProjectExplorerViewModel();
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }

        public ProjectExplorerViewModel ProjectExplorerViewModel { get; }

        public RibbonViewModel RibbonViewModel { get; }

        public StatusBarViewModel StatusBarViewModel => ViewModelFactory.CreateStatusBarViewModel();

        public void ForcedClosingMainWindow()
        {
            gui.GuiProjectServices.HandleUnsavedChanges(() => { });
        }
    }
}