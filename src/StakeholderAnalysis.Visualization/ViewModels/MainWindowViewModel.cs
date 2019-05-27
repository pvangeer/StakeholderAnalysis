using System;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private readonly StakeholderAnalysisGui gui;

        public MainWindowViewModel() : this(new StakeholderAnalysisGui()){ }

        public MainWindowViewModel(StakeholderAnalysisGui guiInput)
        {
            gui = guiInput;
            RibbonViewModel = new RibbonViewModel(gui, () => OnInvalidateVisual?.Invoke(this, null));
            RibbonViewModel.ToggleToolWindowCommand.Execute(typeof(ProjectExplorerViewModel));
            RibbonViewModel.ToggleToolWindowCommand.Execute(typeof(OnionDiagramPropertiesViewModel));
            MainContentPresenterViewModel = new MainContentPresenterViewModel(gui);
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }

        public RibbonViewModel RibbonViewModel { get; }

        public StatusBarViewModel StatusBarViewModel => new StatusBarViewModel(gui);

        public event EventHandler OnInvalidateVisual;

        public void ForcedClosingMainWindow()
        {
            RibbonViewModel.GuiProjectSercices.HandleUnsavedChanges(gui, () => {});
        }
    }
}