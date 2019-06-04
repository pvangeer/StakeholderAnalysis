using System;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        public ViewModelFactory ViewModelFactory { get; }

        private readonly StakeholderAnalysisGui gui;

        public MainWindowViewModel() : this(new StakeholderAnalysisGui()){ }

        public MainWindowViewModel(StakeholderAnalysisGui guiInput)
        {
            ViewModelFactory = new ViewModelFactory(guiInput, guiInput.ViewManager);
            gui = guiInput;
            RibbonViewModel = ViewModelFactory.CreateRibbonViewModel();
            RibbonViewModel.ToggleToolWindowCommand.Execute(typeof(ProjectExplorerViewModel));
            RibbonViewModel.ToggleToolWindowCommand.Execute(typeof(OnionDiagramPropertiesViewModel));
            MainContentPresenterViewModel = ViewModelFactory.CreateMainContentPresenterViewModel(gui);
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }

        public RibbonViewModel RibbonViewModel { get; }

        public StatusBarViewModel StatusBarViewModel => ViewModelFactory.CreateStatusBarViewModel();

        public void ForcedClosingMainWindow()
        {
            gui.GuiProjectServices.HandleUnsavedChanges(gui, () => {});
        }
    }
}